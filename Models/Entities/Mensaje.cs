using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Mensaje
{
    public Guid Id { get; set; }

    public Guid ColegioId { get; set; }

    public Guid UsuarioEmisorId { get; set; }

    public Guid UsuarioReceptorId { get; set; }

    public string? Asunto { get; set; }

    public string Mensaje1 { get; set; } = null!;

    public bool? Leido { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public DateTime? FechaLectura { get; set; }

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual Usuario UsuarioEmisor { get; set; } = null!;

    public virtual Usuario UsuarioReceptor { get; set; } = null!;
}
