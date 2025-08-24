using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Grado
{
    public Guid Id { get; set; }

    public Guid NivelEducativoId { get; set; }

    public Guid ColegioId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int Orden { get; set; }

    public bool? Activo { get; set; }

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    public virtual NivelesEducativo NivelEducativo { get; set; } = null!;

    public virtual ICollection<Pensum> Pensums { get; set; } = new List<Pensum>();
}
