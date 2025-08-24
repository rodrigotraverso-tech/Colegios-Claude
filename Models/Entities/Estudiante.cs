using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Estudiante
{
    public Guid Id { get; set; }

    public Guid PersonaId { get; set; }

    public string CodigoEstudiante { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual ICollection<EstudianteAcudiente> EstudianteAcudientes { get; set; } = new List<EstudianteAcudiente>();

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();

    public virtual Persona Persona { get; set; } = null!;
}
