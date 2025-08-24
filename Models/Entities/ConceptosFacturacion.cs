using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class ConceptosFacturacion
{
    public Guid Id { get; set; }

    public Guid ColegioId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal? ValorBase { get; set; }

    public bool? EsObligatorio { get; set; }

    public string? Periodicidad { get; set; }

    public bool? Activo { get; set; }

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();
}
