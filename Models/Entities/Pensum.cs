using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Pensum
{
    public Guid Id { get; set; }

    public Guid GradoId { get; set; }

    public Guid MateriaId { get; set; }

    public Guid ColegioId { get; set; }

    public Guid AnoAcademicoId { get; set; }

    public int? IntensidadHoraria { get; set; }

    public bool? EsObligatoria { get; set; }

    public bool? Activo { get; set; }

    public virtual AnosAcademico AnoAcademico { get; set; } = null!;

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual Grado Grado { get; set; } = null!;

    public virtual Materia Materia { get; set; } = null!;
}
