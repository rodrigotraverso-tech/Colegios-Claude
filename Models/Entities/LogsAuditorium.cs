using System;
using System.Collections.Generic;
using System.Net;

namespace SistemaColegios.Models.Entities;

public partial class LogsAuditorium
{
    public Guid Id { get; set; }

    public Guid? ColegioId { get; set; }

    public Guid? UsuarioId { get; set; }

    public string Tabla { get; set; } = null!;

    public Guid? RegistroId { get; set; }

    public string Accion { get; set; } = null!;

    public string? DatosAnteriores { get; set; }

    public string? DatosNuevos { get; set; }

    public IPAddress? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime? FechaAccion { get; set; }

    public virtual Colegio? Colegio { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
