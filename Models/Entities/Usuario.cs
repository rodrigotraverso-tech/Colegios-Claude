using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

/// <summary>
/// Gestión unificada de usuarios con SSO. Un usuario puede tener acceso a múltiples colegios.
/// </summary>
public partial class Usuario
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public bool? Activo { get; set; }

    public bool? RequiereCambioPassword { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    public int? IntentosFallidos { get; set; }

    public DateTime? BloqueadoHasta { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<Acudiente> Acudientes { get; set; } = new List<Acudiente>();

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual ICollection<LogsAuditorium> LogsAuditoria { get; set; } = new List<LogsAuditorium>();

    public virtual ICollection<Mensaje> MensajeUsuarioEmisors { get; set; } = new List<Mensaje>();

    public virtual ICollection<Mensaje> MensajeUsuarioReceptors { get; set; } = new List<Mensaje>();

    public virtual ICollection<NotificacionDestinatario> NotificacionDestinatarios { get; set; } = new List<NotificacionDestinatario>();

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Profesore> Profesores { get; set; } = new List<Profesore>();

    public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; } = new List<UsuarioRole>();
}
