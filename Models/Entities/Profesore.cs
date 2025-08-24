using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Profesore
{
    public Guid Id { get; set; }

    public Guid PersonaId { get; set; }

    public Guid? UsuarioId { get; set; }

    public string? CodigoEmpleado { get; set; }

    public List<string>? Especialidades { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Asignacione> Asignaciones { get; set; } = new List<Asignacione>();

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual Persona Persona { get; set; } = null!;

    public virtual ICollection<ProfesorColegio> ProfesorColegios { get; set; } = new List<ProfesorColegio>();

    public virtual Usuario? Usuario { get; set; }
}
