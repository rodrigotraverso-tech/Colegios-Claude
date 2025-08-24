using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Asistencium
{
    public Guid Id { get; set; }

    public Guid EstudianteId { get; set; }

    public Guid GrupoId { get; set; }

    public DateOnly Fecha { get; set; }

    public string Estado { get; set; } = null!;

    public string? Observaciones { get; set; }

    public Guid RegistradoPor { get; set; }

    public Guid ColegioId { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;

    public virtual Grupo Grupo { get; set; } = null!;

    public virtual Usuario RegistradoPorNavigation { get; set; } = null!;
}
