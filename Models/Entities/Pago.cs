using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Pago
{
    public Guid Id { get; set; }

    public Guid FacturaId { get; set; }

    public DateOnly FechaPago { get; set; }

    public decimal ValorPago { get; set; }

    public string? MetodoPago { get; set; }

    public string? Referencia { get; set; }

    public string? Observaciones { get; set; }

    public Guid RegistradoPor { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Factura Factura { get; set; } = null!;

    public virtual Usuario RegistradoPorNavigation { get; set; } = null!;
}
