using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

/// <summary>
/// Tabla principal para manejo multi-tenant. Cada colegio es un tenant independiente.
/// </summary>
public partial class Colegio
{
    public Guid Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? LogoUrl { get; set; }

    public string? ColoresTema { get; set; }

    public string? Configuracion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<AnosAcademico> AnosAcademicos { get; set; } = new List<AnosAcademico>();

    public virtual ICollection<Asignacione> Asignaciones { get; set; } = new List<Asignacione>();

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual ICollection<ConceptosFacturacion> ConceptosFacturacions { get; set; } = new List<ConceptosFacturacion>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<Grado> Grados { get; set; } = new List<Grado>();

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    public virtual ICollection<LogsAuditorium> LogsAuditoria { get; set; } = new List<LogsAuditorium>();

    public virtual ICollection<Materia> Materia { get; set; } = new List<Materia>();

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();

    public virtual ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();

    public virtual ICollection<NivelesEducativo> NivelesEducativos { get; set; } = new List<NivelesEducativo>();

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<Pensum> Pensums { get; set; } = new List<Pensum>();

    public virtual ICollection<PeriodosAcademico> PeriodosAcademicos { get; set; } = new List<PeriodosAcademico>();

    public virtual ICollection<ProfesorColegio> ProfesorColegios { get; set; } = new List<ProfesorColegio>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<TiposEvaluacion> TiposEvaluacions { get; set; } = new List<TiposEvaluacion>();

    public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; } = new List<UsuarioRole>();
}
