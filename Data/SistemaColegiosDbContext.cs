using Microsoft.EntityFrameworkCore;
using SistemaColegios.Models.Entities;
using static MudBlazor.Icons;
using System.Text.RegularExpressions;

namespace SistemaColegios.Data;

/// <summary>
/// Contexto principal de la base de datos del Sistema de Colegios
/// </summary>
public class SistemaColegiosDbContext : DbContext
{
    public SistemaColegiosDbContext(DbContextOptions<SistemaColegiosDbContext> options) : base(options)
    {
    }

    // ===============================================
    // DBSETS - TABLAS PRINCIPALES
    // ===============================================

    // Configuración Global
    public DbSet<Colegio> Colegios { get; set; }
    public DbSet<TipoDocumento> TiposDocumento { get; set; }
    public DbSet<TipoUsuario> TiposUsuario { get; set; }

    // Usuarios y Roles
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<UsuarioRol> UsuarioRoles { get; set; }

    // Estructura Académica
    public DbSet<AnoAcademico> AnosAcademicos { get; set; }
    public DbSet<NivelEducativo> NivelesEducativos { get; set; }
    public DbSet<Grado> Grados { get; set; }
    public DbSet<Grupo> Grupos { get; set; }
    public DbSet<Materia> Materias { get; set; }
    public DbSet<Pensum> Pensum { get; set; }

    // Personas
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<ProfesorColegio> ProfesorColegios { get; set; }
    public DbSet<Estudiante> Estudiantes { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Acudiente> Acudientes { get; set; }
    public DbSet<EstudianteAcudiente> EstudianteAcudientes { get; set; }

    // Académico
    public DbSet<PeriodoAcademico> PeriodosAcademicos { get; set; }
    public DbSet<Asignacion> Asignaciones { get; set; }
    public DbSet<TipoEvaluacion> TiposEvaluacion { get; set; }
    public DbSet<Calificacion> Calificaciones { get; set; }
    public DbSet<Asistencia> Asistencia { get; set; }

    // Financiero
    public DbSet<ConceptoFacturacion> ConceptosFacturacion { get; set; }
    public DbSet<Factura> Facturas { get; set; }
    public DbSet<FacturaDetalle> FacturaDetalles { get; set; }
    public DbSet<Pago> Pagos { get; set; }

    // Comunicaciones
    public DbSet<TipoNotificacion> TiposNotificacion { get; set; }
    public DbSet<Notificacion> Notificaciones { get; set; }
    public DbSet<NotificacionDestinatario> NotificacionDestinatarios { get; set; }
    public DbSet<Mensaje> Mensajes { get; set; }

    // Auditoría
    public DbSet<LogAuditoria> LogsAuditoria { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ===============================================
        // CONFIGURACIONES GLOBALES
        // ===============================================

        // Usar snake_case para nombres de tablas y columnas (PostgreSQL convention)
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName()?.ToSnakeCase());

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName()?.ToSnakeCase());
            }
        }

        // ===============================================
        // CONFIGURACIONES ESPECÍFICAS
        // ===============================================

        // TODO: Aplicar configuraciones específicas cuando tengamos las entidades
        // modelBuilder.ApplyConfiguration(new ColegioConfiguration());
        // modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        // etc...

        // Configuración temporal para evitar errores de compilación
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired();
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Fallback connection string para desarrollo
            optionsBuilder.UseNpgsql("Host=localhost;Database=sistema_colegios_dev;Username=postgres;Password=123456");
        }
    }
}

/// <summary>
/// Extensión para convertir strings a snake_case
/// </summary>
public static class StringExtensions
{
    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        return string.Concat(
            input.Select((x, i) => i > 0 && char.IsUpper(x)
                ? "_" + x.ToString().ToLower()
                : x.ToString().ToLower())
        );
    }
}