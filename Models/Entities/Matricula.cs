using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

/// <summary>
/// Relación estudiante-colegio-año académico. Permite historial completo de matrículas.
/// </summary>
public partial class Matricula
{
    public Guid Id { get; set; }

    public Guid EstudianteId { get; set; }

    public Guid ColegioId { get; set; }

    public Guid AnoAcademicoId { get; set; }

    public Guid GrupoId { get; set; }

    public DateOnly? FechaMatricula { get; set; }

    public string? Estado { get; set; }

    public string? Observaciones { get; set; }

    public virtual AnosAcademico AnoAcademico { get; set; } = null!;

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;

    public virtual Grupo Grupo { get; set; } = null!;
}
