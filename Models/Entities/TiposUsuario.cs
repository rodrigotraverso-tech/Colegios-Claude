using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class TiposUsuario
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }
}
