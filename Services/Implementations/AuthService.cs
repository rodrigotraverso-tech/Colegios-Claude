using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using SistemaColegios.Data;
using SistemaColegios.Models.Entities;
using SistemaColegios.Services.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SistemaColegios.Services.Implementations;

/// <summary>
/// Implementación del servicio de autenticación
/// </summary>
public class AuthService : IAuthService
{
    private readonly SistemaColegiosDbContext _context;
    private readonly ILogger<AuthService> _logger;

    public AuthService(SistemaColegiosDbContext context, ILogger<AuthService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> LoginAsync(string username, string password, HttpContext httpContext)
    {
        try
        {
            var usuario = await ObtenerUsuarioAsync(username);
            if (usuario == null)
            {
                _logger.LogWarning("Intento de login con usuario inexistente: {Username}", username);
                return false;
            }

            // Verificar si la cuenta está bloqueada
            if (usuario.BloqueadoHasta.HasValue && usuario.BloqueadoHasta.Value > DateTime.UtcNow)
            {
                _logger.LogWarning("Intento de login con cuenta bloqueada: {Username}", username);
                return false;
            }

            // Verificar si el usuario está activo
            if (!usuario.Activo.HasValue || !usuario.Activo.Value)
            {
                _logger.LogWarning("Intento de login con usuario inactivo: {Username}", username);
                return false;
            }

            // Validar password
            if (!await ValidarUsuarioAsync(username, password))
            {
                await IncrementarIntentosFallidosAsync(usuario);
                return false;
            }

            // Resetear intentos fallidos
            await ResetearIntentosFallidosAsync(usuario);

            // Crear claims para la cookie de autenticación
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("UserId", usuario.Id.ToString()),
                new Claim("Username", usuario.Username)
            };

            // TODO: Agregar claims de roles específicos por colegio
            // var roles = await _context.UsuarioRoles
            //     .Where(ur => ur.UsuarioId == usuario.Id && ur.Activo)
            //     .Include(ur => ur.Rol)
            //     .ToListAsync();
            // 
            // foreach (var rol in roles)
            // {
            //     claims.Add(new Claim(ClaimTypes.Role, rol.Rol.Codigo));
            //     if (rol.ColegioId.HasValue)
            //     {
            //         claims.Add(new Claim("ColegioId", rol.ColegioId.Value.ToString()));
            //     }
            // }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Recordar login
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Actualizar último acceso
            usuario.UltimoAcceso = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Usuario autenticado exitosamente: {Username}", username);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error durante la autenticación del usuario: {Username}", username);
            return false;
        }
    }

    public async Task LogoutAsync(HttpContext httpContext)
    {
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        _logger.LogInformation("Usuario cerró sesión");
    }

    public async Task<bool> ValidarUsuarioAsync(string username, string password)
    {
        var usuario = await ObtenerUsuarioAsync(username);
        if (usuario == null) return false;

        var salt = usuario.Salt ?? "";
        var hashedPassword = HashPassword(password, salt);

        return hashedPassword == usuario.PasswordHash;
    }

    public async Task<Usuario?> ObtenerUsuarioAsync(string username)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username || u.Email == username);
    }

    public string HashPassword(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var saltedPassword = password + salt;
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
        return Convert.ToBase64String(hashBytes);
    }

    public string GenerarSalt()
    {
        var saltBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }

    public async Task<bool> CambiarPasswordAsync(Guid usuarioId, string passwordActual, string passwordNuevo)
    {
        try
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) return false;

            // Verificar password actual
            var salt = usuario.Salt ?? "";
            var hashedPasswordActual = HashPassword(passwordActual, salt);
            if (hashedPasswordActual != usuario.PasswordHash)
                return false;

            // Cambiar password
            var nuevoSalt = GenerarSalt();
            var nuevoHashedPassword = HashPassword(passwordNuevo, nuevoSalt);

            usuario.PasswordHash = nuevoHashedPassword;
            usuario.Salt = nuevoSalt;
            usuario.RequiereCambioPassword = false;
            usuario.FechaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Password cambiado exitosamente para usuario: {UserId}", usuarioId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cambiar password para usuario: {UserId}", usuarioId);
            return false;
        }
    }

    private async Task IncrementarIntentosFallidosAsync(Usuario usuario)
    {
        usuario.IntentosFallidos++;

        // Bloquear cuenta después de 3 intentos fallidos
        if (usuario.IntentosFallidos >= 3)
        {
            usuario.BloqueadoHasta = DateTime.UtcNow.AddMinutes(30);
            _logger.LogWarning("Cuenta bloqueada por intentos fallidos: {Username}", usuario.Username);
        }

        await _context.SaveChangesAsync();
    }

    private async Task ResetearIntentosFallidosAsync(Usuario usuario)
    {
        if (usuario.IntentosFallidos > 0 || usuario.BloqueadoHasta.HasValue)
        {
            usuario.IntentosFallidos = 0;
            usuario.BloqueadoHasta = null;
            await _context.SaveChangesAsync();
        }
    }
}