using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Serilog;
using SistemaColegios.Data;
using SistemaColegios.Services.Interfaces;
using SistemaColegios.Services.Implementations;
using AutoMapper;
using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

// ===============================================
// CONFIGURACIÓN DE SERILOG
// ===============================================
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/sistema-colegios-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Usar Serilog como logger
builder.Host.UseSerilog();

// ===============================================
// SERVICIOS DEL CONTENEDOR
// ===============================================

// Servicios básicos de Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// ===============================================
// BASE DE DATOS - ENTITY FRAMEWORK + POSTGRESQL
// ===============================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<SistemaColegiosDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
    options.EnableDetailedErrors(builder.Environment.IsDevelopment());
});

// ===============================================
// MUDBLAZOR UI COMPONENTS
// ===============================================
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomLeft;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = MudBlazor.Variant.Filled;
});

// ===============================================
// AUTENTICACIÓN CON COOKIES
// ===============================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "SistemaColegios.Auth";
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
        options.AccessDeniedPath = "/auth/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8); // 8 horas de sesión
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// ===============================================
// AUTOMAPPER - MAPEO DE OBJETOS
// ===============================================
builder.Services.AddAutoMapper(config =>
{
    // TODO: Configurar mapeos cuando creemos los DTOs
    // config.CreateMap<Estudiante, EstudianteDto>();
    // config.CreateMap<Usuario, UsuarioDto>();
}, typeof(Program));

// ===============================================
// FLUENTVALIDATION - VALIDACIONES
// ===============================================
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// ===============================================
// SERVICIOS DE APLICACIÓN
// ===============================================

// Servicios de autenticación
builder.Services.AddScoped<IAuthService, AuthService>();

// Servicios del dominio (se agregarán según vayamos implementando)
// builder.Services.AddScoped<IEstudianteService, EstudianteService>();
// builder.Services.AddScoped<ICalificacionService, CalificacionService>();
// builder.Services.AddScoped<IFinancieroService, FinancieroService>();

// ===============================================
// CACHE EN MEMORIA
// ===============================================
builder.Services.AddMemoryCache();

// ===============================================
// SERVICIOS PARA MANEJO DE SESSIONES Y BROWSER STORAGE
// ===============================================
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<ProtectedLocalStorage>();

// ===============================================
// BUILD DE LA APLICACIÓN
// ===============================================
var app = builder.Build();

// ===============================================
// PIPELINE DE MIDDLEWARES
// ===============================================

// Manejo de errores
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// Middlewares básicos
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Logging de requests
app.UseSerilogRequestLogging();

// Blazor
app.MapBlazorHub();
app.MapRazorPages();
app.MapFallbackToPage("/_Host");

// ===============================================
// INICIALIZACIÓN DE BASE DE DATOS
// ===============================================
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<SistemaColegiosDbContext>();

        // Verificar conexión a la base de datos
        if (await context.Database.CanConnectAsync())
        {
            Log.Information("✅ Conexión a base de datos PostgreSQL exitosa");

            // Aplicar migraciones pendientes si las hay
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                Log.Information("🔄 Aplicando {Count} migraciones pendientes", pendingMigrations.Count());
                await context.Database.MigrateAsync();
            }
        }
        else
        {
            Log.Error("No se puede conectar a la base de datos");
        }
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Error durante la inicialización de base de datos");
    }
}

Log.Information("SistemaColegios iniciado en {Environment}", app.Environment.EnvironmentName);

app.Run();