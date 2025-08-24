using System;
using System.Collections.Generic;

namespace SistemaColegios.Models.Entities;

public partial class Acudiente
{
    public Guid Id { get; set; }

    public Guid PersonaId { get; set; }

    public Guid? UsuarioId { get; set; }

    public string? Profesion { get; set; }

    public string? Empresa { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<EstudianteAcudiente> EstudianteAcudientes { get; set; } = new List<EstudianteAcudiente>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Persona Persona { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
