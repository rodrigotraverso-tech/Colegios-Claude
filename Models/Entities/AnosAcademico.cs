using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class AnosAcademico
{
    public Guid Id { get; set; }

    public Guid ColegioId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public bool? Activo { get; set; }

    public string? Configuracion { get; set; }

    public virtual ICollection<Asignacione> Asignaciones { get; set; } = new List<Asignacione>();

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();

    public virtual ICollection<Pensum> Pensums { get; set; } = new List<Pensum>();

    public virtual ICollection<PeriodosAcademico> PeriodosAcademicos { get; set; } = new List<PeriodosAcademico>();
}
