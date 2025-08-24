using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Grupo
{
    public Guid Id { get; set; }

    public Guid GradoId { get; set; }

    public Guid AnoAcademicoId { get; set; }

    public Guid ColegioId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int? CapacidadMaxima { get; set; }

    public Guid? DirectorGrupoId { get; set; }

    public bool? Activo { get; set; }

    public virtual AnosAcademico AnoAcademico { get; set; } = null!;

    public virtual ICollection<Asignacione> Asignaciones { get; set; } = new List<Asignacione>();

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual Grado Grado { get; set; } = null!;

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
