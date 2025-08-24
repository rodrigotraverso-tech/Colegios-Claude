using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class PeriodosAcademico
{
    public Guid Id { get; set; }

    public Guid AnoAcademicoId { get; set; }

    public Guid ColegioId { get; set; }

    public int Numero { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public bool? Activo { get; set; }

    public virtual AnosAcademico AnoAcademico { get; set; } = null!;

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual Colegio Colegio { get; set; } = null!;
}
