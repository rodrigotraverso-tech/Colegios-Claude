using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

/// <summary>
/// Roles del sistema. Puede ser global (colegio_id IS NULL) para ADMIN_GLOBAL y SUPER_ADMIN, o específico por colegio.
/// </summary>
public partial class Role
{
    public Guid Id { get; set; }

    /// <summary>
    /// ID del colegio. NULL para roles globales (ADMIN_GLOBAL, SUPER_ADMIN).
    /// </summary>
    public Guid? ColegioId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Permisos { get; set; }

    public bool? Activo { get; set; }

    public virtual Colegio? Colegio { get; set; }

    public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; } = new List<UsuarioRole>();
}
