using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

/// <summary>
/// Tabla base para todas las personas del sistema. Incluye nombres completos (primer y segundo nombre/apellido) y datos básicos.
/// </summary>
public partial class Persona
{
    public Guid Id { get; set; }

    public int TipoDocumentoId { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public char? Genero { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Celular { get; set; }

    public string? Email { get; set; }

    public string? FotoUrl { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    /// <summary>
    /// Segundo nombre de la persona (opcional)
    /// </summary>
    public string? SegundoNombre { get; set; }

    /// <summary>
    /// Segundo apellido de la persona (opcional)
    /// </summary>
    public string? SegundoApellido { get; set; }

    public virtual ICollection<Acudiente> Acudientes { get; set; } = new List<Acudiente>();

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

    public virtual ICollection<Profesore> Profesores { get; set; } = new List<Profesore>();

    public virtual TiposDocumento TipoDocumento { get; set; } = null!;
}
