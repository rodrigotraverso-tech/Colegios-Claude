using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class TiposEvaluacion
{
    public Guid Id { get; set; }

    public Guid ColegioId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal? Porcentaje { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual Colegio Colegio { get; set; } = null!;
}
