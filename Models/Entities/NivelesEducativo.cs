using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class NivelesEducativo
{
    public Guid Id { get; set; }

    public Guid ColegioId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int Orden { get; set; }

    public bool? Activo { get; set; }

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual ICollection<Grado> Grados { get; set; } = new List<Grado>();
}
