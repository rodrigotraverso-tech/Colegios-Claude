using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SistemaColegios.Data;
using SistemaColegios.Models.Entities;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SistemaColegios.Services.Interfaces;

/// <summary>
/// Interfaz para el servicio de autenticación
/// </summary>
public interface IAuthService
{
    Task<bool> LoginAsync(string username, string password, HttpContext httpContext);
    Task LogoutAsync(HttpContext httpContext);
    Task<bool> ValidarUsuarioAsync(string username, string password);
    Task<Usuario?> ObtenerUsuarioAsync(string username);
    string HashPassword(string password, string salt);
    string GenerarSalt();
    Task<bool> CambiarPasswordAsync(Guid usuarioId, string passwordActual, string passwordNuevo);
}