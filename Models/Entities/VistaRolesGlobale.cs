using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class VistaRolesGlobale
{
    public Guid? Id { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Permisos { get; set; }

    public bool? Activo { get; set; }

    public string? Ambito { get; set; }
}
