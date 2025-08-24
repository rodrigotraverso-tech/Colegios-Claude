using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Materia
{
    public Guid Id { get; set; }

    public Guid ColegioId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Area { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Asignacione> Asignaciones { get; set; } = new List<Asignacione>();

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual ICollection<Pensum> Pensums { get; set; } = new List<Pensum>();
}
