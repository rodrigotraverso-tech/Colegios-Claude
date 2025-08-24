using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class FacturaDetalle
{
    public Guid Id { get; set; }

    public Guid FacturaId { get; set; }

    public Guid ConceptoFacturacionId { get; set; }

    public Guid? EstudianteId { get; set; }

    public int? Cantidad { get; set; }

    public decimal ValorUnitario { get; set; }

    public decimal? Descuento { get; set; }

    public decimal ValorTotal { get; set; }

    public virtual ConceptosFacturacion ConceptoFacturacion { get; set; } = null!;

    public virtual Estudiante? Estudiante { get; set; }

    public virtual Factura Factura { get; set; } = null!;
}
