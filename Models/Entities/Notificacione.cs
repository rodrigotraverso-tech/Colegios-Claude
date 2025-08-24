using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Notificacione
{
    public Guid Id { get; set; }

    public Guid ColegioId { get; set; }

    public int TipoNotificacionId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    public Guid UsuarioEmisorId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaProgramada { get; set; }

    public bool? Activo { get; set; }

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual ICollection<NotificacionDestinatario> NotificacionDestinatarios { get; set; } = new List<NotificacionDestinatario>();

    public virtual TiposNotificacion TipoNotificacion { get; set; } = null!;

    public virtual Usuario UsuarioEmisor { get; set; } = null!;
}
