using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

/// <summary>
/// Almacena todas las calificaciones con flexibilidad para diferentes sistemas de evaluación.
/// </summary>
public partial class Calificacione
{
    public Guid Id { get; set; }

    public Guid EstudianteId { get; set; }

    public Guid AsignacionId { get; set; }

    public Guid PeriodoAcademicoId { get; set; }

    public Guid TipoEvaluacionId { get; set; }

    public decimal Calificacion { get; set; }

    public string? Observaciones { get; set; }

    public DateOnly? FechaCalificacion { get; set; }

    public Guid ProfesorId { get; set; }

    public Guid ColegioId { get; set; }

    public virtual Asignacione Asignacion { get; set; } = null!;

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;

    public virtual PeriodosAcademico PeriodoAcademico { get; set; } = null!;

    public virtual Profesore Profesor { get; set; } = null!;

    public virtual TiposEvaluacion TipoEvaluacion { get; set; } = null!;
}
