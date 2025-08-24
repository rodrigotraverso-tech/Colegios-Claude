using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class ProfesorColegio
{
    public Guid Id { get; set; }

    public Guid ProfesorId { get; set; }

    public Guid ColegioId { get; set; }

    public bool? Activo { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public virtual Colegio Colegio { get; set; } = null!;

    public virtual Profesore Profesor { get; set; } = null!;
}
