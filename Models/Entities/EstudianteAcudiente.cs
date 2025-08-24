using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class EstudianteAcudiente
{
    public Guid Id { get; set; }

    public Guid EstudianteId { get; set; }

    public Guid AcudienteId { get; set; }

    public string Parentesco { get; set; } = null!;

    public bool? EsResponsableFinanciero { get; set; }

    public bool? EsContactoEmergencia { get; set; }

    public bool? AutorizadoRecoger { get; set; }

    public bool? Activo { get; set; }

    public virtual Acudiente Acudiente { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
