using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaColegios.Models.Entities;

/// <summary>
/// Entidad temporal para Usuario - será reemplazada por scaffold de BD
/// </summary>
[Table("usuarios")]
public class Usuario
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public string? Salt { get; set; }

    public bool Activo { get; set; } = true;

    public bool RequiereCambioPassword { get; set; } = false;

    public DateTime? UltimoAcceso { get; set; }

    public int IntentosFallidos { get; set; } = 0;

    public DateTime? BloqueadoHasta { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
}

// ===============================================
// ENTIDADES STUB TEMPORALES
// ===============================================
// Estas son entidades mínimas para que compile el DbContext
// Serán reemplazadas por las entidades generadas desde la BD

public class Colegio { public Guid Id { get; set; } }
public class TipoDocumento { public int Id { get; set; } }
public class TipoUsuario { public int Id { get; set; } }
public class Rol { public Guid Id { get; set; } }
public class UsuarioRol { public Guid Id { get; set; } }
public class AnoAcademico { public Guid Id { get; set; } }
public class NivelEducativo { public Guid Id { get; set; } }
public class Grado { public Guid Id { get; set; } }
public class Grupo { public Guid Id { get; set; } }
public class Materia { public Guid Id { get; set; } }
public class Pensum { public Guid Id { get; set; } }
public class Persona { public Guid Id { get; set; } }
public class Profesor { public Guid Id { get; set; } }
public class ProfesorColegio { public Guid Id { get; set; } }
public class Estudiante { public Guid Id { get; set; } }
public class Matricula { public Guid Id { get; set; } }
public class Acudiente { public Guid Id { get; set; } }
public class EstudianteAcudiente { public Guid Id { get; set; } }
public class PeriodoAcademico { public Guid Id { get; set; } }
public class Asignacion { public Guid Id { get; set; } }
public class TipoEvaluacion { public Guid Id { get; set; } }
public class Calificacion { public Guid Id { get; set; } }
public class Asistencia { public Guid Id { get; set; } }
public class ConceptoFacturacion { public Guid Id { get; set; } }
public class Factura { public Guid Id { get; set; } }
public class FacturaDetalle { public Guid Id { get; set; } }
public class Pago { public Guid Id { get; set; } }
public class TipoNotificacion { public int Id { get; set; } }
public class Notificacion { public Guid Id { get; set; } }
public class NotificacionDestinatario { public Guid Id { get; set; } }
public class Mensaje { public Guid Id { get; set; } }
public class LogAuditoria { public Guid Id { get; set; } }