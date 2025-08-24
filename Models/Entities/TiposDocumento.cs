using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class TiposDocumento
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
