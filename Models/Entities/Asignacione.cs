using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Asignacione
{
    public Guid Id { get; set; }

    public Guid ProfesorId { get; set; }

    public Guid GrupoId { get; set; }

    public Guid MateriaId { get; set; }

    public Guid ColegioId { get; set; }

    public Guid AnoAcademicoId { get; set; }

    public bool? Activo { get; set; }

    public virtual AnosAcademico AnoAcademico { get; set; } = null!;

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual Grupo Grupo { get; set; } = null!;

    public virtual Materia Materia { get; set; } = null!;

    public virtual Profesore Profesor { get; set; } = null!;
}
