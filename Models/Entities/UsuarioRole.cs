using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

/// <summary>
/// Asignación de roles a usuarios. Para roles globales, colegio_id es NULL. La consistencia se valida mediante triggers.
/// </summary>
public partial class UsuarioRole
{
    public Guid Id { get; set; }

    public Guid UsuarioId { get; set; }

    public Guid RolId { get; set; }

    /// <summary>
    /// ID del colegio. NULL para asignaciones de roles globales.
    /// </summary>
    public Guid? ColegioId { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public virtual Colegio? Colegio { get; set; }

    public virtual Role Rol { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
