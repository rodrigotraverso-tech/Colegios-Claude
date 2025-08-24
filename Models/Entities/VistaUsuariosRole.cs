using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class VistaUsuariosRole
{
    public Guid? UsuarioId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public Guid? UsuarioRolId { get; set; }

    public Guid? RolId { get; set; }

    public string? RolCodigo { get; set; }

    public string? RolNombre { get; set; }

    public Guid? ColegioId { get; set; }

    public string? ColegioNombre { get; set; }

    public string? TipoRol { get; set; }

    public bool? RolActivo { get; set; }

    public DateTime? FechaAsignacion { get; set; }
}
