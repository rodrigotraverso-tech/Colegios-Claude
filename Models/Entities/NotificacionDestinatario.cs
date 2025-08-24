using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class NotificacionDestinatario
{
    public Guid Id { get; set; }

    public Guid NotificacionId { get; set; }

    public Guid UsuarioId { get; set; }

    public bool? Leida { get; set; }

    public DateTime? FechaLectura { get; set; }

    public virtual Notificacione Notificacion { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
