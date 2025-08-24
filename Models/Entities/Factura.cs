using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

/// <summary>
/// Facturación que puede consolidar múltiples estudiantes de una familia en diferentes colegios.
/// </summary>
public partial class Factura
{
    public Guid Id { get; set; }

    public string NumeroFactura { get; set; } = null!;

    public Guid AcudienteId { get; set; }

    public Guid ColegioId { get; set; }

    public DateOnly FechaFactura { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    public decimal Subtotal { get; set; }

    public decimal? Descuentos { get; set; }

    public decimal Total { get; set; }

    public string? Estado { get; set; }

    public string? Observaciones { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Acudiente Acudiente { get; set; } = null!;

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
