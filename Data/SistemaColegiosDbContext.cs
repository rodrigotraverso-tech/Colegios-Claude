using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaColegios.Models.Entities;

namespace SistemaColegios.Data;

public partial class SistemaColegiosDbContext : DbContext
{
    public SistemaColegiosDbContext()
    {
    }

    public SistemaColegiosDbContext(DbContextOptions<SistemaColegiosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acudiente> Acudientes { get; set; }

    public virtual DbSet<AnosAcademico> AnosAcademicos { get; set; }

    public virtual DbSet<Asignacione> Asignaciones { get; set; }

    public virtual DbSet<Asistencium> Asistencia { get; set; }

    public virtual DbSet<Calificacione> Calificaciones { get; set; }

    public virtual DbSet<Colegio> Colegios { get; set; }

    public virtual DbSet<ConceptosFacturacion> ConceptosFacturacions { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<EstudianteAcudiente> EstudianteAcudientes { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }

    public virtual DbSet<Grado> Grados { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<LogsAuditorium> LogsAuditoria { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<Mensaje> Mensajes { get; set; }

    public virtual DbSet<NivelesEducativo> NivelesEducativos { get; set; }

    public virtual DbSet<NotificacionDestinatario> NotificacionDestinatarios { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pensum> Pensums { get; set; }

    public virtual DbSet<PeriodosAcademico> PeriodosAcademicos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<ProfesorColegio> ProfesorColegios { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TiposDocumento> TiposDocumentos { get; set; }

    public virtual DbSet<TiposEvaluacion> TiposEvaluacions { get; set; }

    public virtual DbSet<TiposNotificacion> TiposNotificacions { get; set; }

    public virtual DbSet<TiposUsuario> TiposUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRole> UsuarioRoles { get; set; }

    public virtual DbSet<VistaRolesColegio> VistaRolesColegios { get; set; }

    public virtual DbSet<VistaRolesGlobale> VistaRolesGlobales { get; set; }

    public virtual DbSet<VistaUsuariosRole> VistaUsuariosRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=colegios-claude;Username=postgres;Password=postgres;Port=5432");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("pgcrypto")
            .HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Acudiente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("acudientes_pkey");

            entity.ToTable("acudientes");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Empresa)
                .HasMaxLength(200)
                .HasColumnName("empresa");
            entity.Property(e => e.PersonaId).HasColumnName("persona_id");
            entity.Property(e => e.Profesion)
                .HasMaxLength(100)
                .HasColumnName("profesion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Persona).WithMany(p => p.Acudientes)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("acudientes_persona_id_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Acudientes)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("acudientes_usuario_id_fkey");
        });

        modelBuilder.Entity<AnosAcademico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("anos_academicos_pkey");

            entity.ToTable("anos_academicos");

            entity.HasIndex(e => new { e.ColegioId, e.Codigo }, "anos_academicos_colegio_id_codigo_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasColumnName("codigo");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.Configuracion)
                .HasColumnType("jsonb")
                .HasColumnName("configuracion");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Colegio).WithMany(p => p.AnosAcademicos)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("anos_academicos_colegio_id_fkey");
        });

        modelBuilder.Entity<Asignacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("asignaciones_pkey");

            entity.ToTable("asignaciones");

            entity.HasIndex(e => new { e.GrupoId, e.MateriaId, e.AnoAcademicoId }, "asignaciones_grupo_id_materia_id_ano_academico_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.AnoAcademicoId).HasColumnName("ano_academico_id");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.GrupoId).HasColumnName("grupo_id");
            entity.Property(e => e.MateriaId).HasColumnName("materia_id");
            entity.Property(e => e.ProfesorId).HasColumnName("profesor_id");

            entity.HasOne(d => d.AnoAcademico).WithMany(p => p.Asignaciones)
                .HasForeignKey(d => d.AnoAcademicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignaciones_ano_academico_id_fkey");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Asignaciones)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignaciones_colegio_id_fkey");

            entity.HasOne(d => d.Grupo).WithMany(p => p.Asignaciones)
                .HasForeignKey(d => d.GrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignaciones_grupo_id_fkey");

            entity.HasOne(d => d.Materia).WithMany(p => p.Asignaciones)
                .HasForeignKey(d => d.MateriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignaciones_materia_id_fkey");

            entity.HasOne(d => d.Profesor).WithMany(p => p.Asignaciones)
                .HasForeignKey(d => d.ProfesorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asignaciones_profesor_id_fkey");
        });

        modelBuilder.Entity<Asistencium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("asistencia_pkey");

            entity.ToTable("asistencia");

            entity.HasIndex(e => new { e.EstudianteId, e.GrupoId, e.Fecha }, "asistencia_estudiante_id_grupo_id_fecha_key").IsUnique();

            entity.HasIndex(e => new { e.EstudianteId, e.Fecha }, "idx_asistencia_estudiante_fecha");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.GrupoId).HasColumnName("grupo_id");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asistencia_colegio_id_fkey");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asistencia_estudiante_id_fkey");

            entity.HasOne(d => d.Grupo).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.GrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asistencia_grupo_id_fkey");

            entity.HasOne(d => d.RegistradoPorNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.RegistradoPor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("asistencia_registrado_por_fkey");
        });

        modelBuilder.Entity<Calificacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("calificaciones_pkey");

            entity.ToTable("calificaciones", tb => tb.HasComment("Almacena todas las calificaciones con flexibilidad para diferentes sistemas de evaluación."));

            entity.HasIndex(e => new { e.EstudianteId, e.PeriodoAcademicoId }, "idx_calificaciones_estudiante_periodo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.AsignacionId).HasColumnName("asignacion_id");
            entity.Property(e => e.Calificacion)
                .HasPrecision(4, 2)
                .HasColumnName("calificacion");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.FechaCalificacion)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("fecha_calificacion");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.PeriodoAcademicoId).HasColumnName("periodo_academico_id");
            entity.Property(e => e.ProfesorId).HasColumnName("profesor_id");
            entity.Property(e => e.TipoEvaluacionId).HasColumnName("tipo_evaluacion_id");

            entity.HasOne(d => d.Asignacion).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.AsignacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calificaciones_asignacion_id_fkey");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calificaciones_colegio_id_fkey");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calificaciones_estudiante_id_fkey");

            entity.HasOne(d => d.PeriodoAcademico).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.PeriodoAcademicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calificaciones_periodo_academico_id_fkey");

            entity.HasOne(d => d.Profesor).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.ProfesorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calificaciones_profesor_id_fkey");

            entity.HasOne(d => d.TipoEvaluacion).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.TipoEvaluacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calificaciones_tipo_evaluacion_id_fkey");
        });

        modelBuilder.Entity<Colegio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("colegios_pkey");

            entity.ToTable("colegios", tb => tb.HasComment("Tabla principal para manejo multi-tenant. Cada colegio es un tenant independiente."));

            entity.HasIndex(e => e.Codigo, "colegios_codigo_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasColumnName("codigo");
            entity.Property(e => e.ColoresTema)
                .HasColumnType("jsonb")
                .HasColumnName("colores_tema");
            entity.Property(e => e.Configuracion)
                .HasColumnType("jsonb")
                .HasColumnName("configuracion");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(500)
                .HasColumnName("logo_url");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<ConceptosFacturacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("conceptos_facturacion_pkey");

            entity.ToTable("conceptos_facturacion");

            entity.HasIndex(e => new { e.ColegioId, e.Codigo }, "conceptos_facturacion_colegio_id_codigo_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.EsObligatorio)
                .HasDefaultValue(true)
                .HasColumnName("es_obligatorio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.Periodicidad)
                .HasMaxLength(20)
                .HasColumnName("periodicidad");
            entity.Property(e => e.ValorBase)
                .HasPrecision(12, 2)
                .HasColumnName("valor_base");

            entity.HasOne(d => d.Colegio).WithMany(p => p.ConceptosFacturacions)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("conceptos_facturacion_colegio_id_fkey");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("estudiantes_pkey");

            entity.ToTable("estudiantes");

            entity.HasIndex(e => e.CodigoEstudiante, "estudiantes_codigo_estudiante_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.CodigoEstudiante)
                .HasMaxLength(20)
                .HasColumnName("codigo_estudiante");
            entity.Property(e => e.PersonaId).HasColumnName("persona_id");

            entity.HasOne(d => d.Persona).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudiantes_persona_id_fkey");
        });

        modelBuilder.Entity<EstudianteAcudiente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("estudiante_acudientes_pkey");

            entity.ToTable("estudiante_acudientes");

            entity.HasIndex(e => new { e.EstudianteId, e.AcudienteId }, "estudiante_acudientes_estudiante_id_acudiente_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.AcudienteId).HasColumnName("acudiente_id");
            entity.Property(e => e.AutorizadoRecoger)
                .HasDefaultValue(false)
                .HasColumnName("autorizado_recoger");
            entity.Property(e => e.EsContactoEmergencia)
                .HasDefaultValue(false)
                .HasColumnName("es_contacto_emergencia");
            entity.Property(e => e.EsResponsableFinanciero)
                .HasDefaultValue(false)
                .HasColumnName("es_responsable_financiero");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.Parentesco)
                .HasMaxLength(50)
                .HasColumnName("parentesco");

            entity.HasOne(d => d.Acudiente).WithMany(p => p.EstudianteAcudientes)
                .HasForeignKey(d => d.AcudienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudiante_acudientes_acudiente_id_fkey");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.EstudianteAcudientes)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudiante_acudientes_estudiante_id_fkey");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("facturas_pkey");

            entity.ToTable("facturas", tb => tb.HasComment("Facturación que puede consolidar múltiples estudiantes de una familia en diferentes colegios."));

            entity.HasIndex(e => new { e.ColegioId, e.NumeroFactura }, "facturas_colegio_id_numero_factura_key").IsUnique();

            entity.HasIndex(e => new { e.AcudienteId, e.Estado }, "idx_facturas_acudiente_estado");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.AcudienteId).HasColumnName("acudiente_id");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.Descuentos)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("descuentos");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDIENTE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaFactura).HasColumnName("fecha_factura");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(50)
                .HasColumnName("numero_factura");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasPrecision(12, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.Acudiente).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.AcudienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("facturas_acudiente_id_fkey");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("facturas_colegio_id_fkey");
        });

        modelBuilder.Entity<FacturaDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("factura_detalles_pkey");

            entity.ToTable("factura_detalles");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasDefaultValue(1)
                .HasColumnName("cantidad");
            entity.Property(e => e.ConceptoFacturacionId).HasColumnName("concepto_facturacion_id");
            entity.Property(e => e.Descuento)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("descuento");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.FacturaId).HasColumnName("factura_id");
            entity.Property(e => e.ValorTotal)
                .HasPrecision(12, 2)
                .HasColumnName("valor_total");
            entity.Property(e => e.ValorUnitario)
                .HasPrecision(12, 2)
                .HasColumnName("valor_unitario");

            entity.HasOne(d => d.ConceptoFacturacion).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.ConceptoFacturacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("factura_detalles_concepto_facturacion_id_fkey");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("factura_detalles_estudiante_id_fkey");

            entity.HasOne(d => d.Factura).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("factura_detalles_factura_id_fkey");
        });

        modelBuilder.Entity<Grado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("grados_pkey");

            entity.ToTable("grados");

            entity.HasIndex(e => new { e.ColegioId, e.Codigo }, "grados_colegio_id_codigo_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasColumnName("codigo");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.NivelEducativoId).HasColumnName("nivel_educativo_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Orden).HasColumnName("orden");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Grados)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grados_colegio_id_fkey");

            entity.HasOne(d => d.NivelEducativo).WithMany(p => p.Grados)
                .HasForeignKey(d => d.NivelEducativoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grados_nivel_educativo_id_fkey");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("grupos_pkey");

            entity.ToTable("grupos");

            entity.HasIndex(e => new { e.ColegioId, e.AnoAcademicoId, e.Codigo }, "grupos_colegio_id_ano_academico_id_codigo_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.AnoAcademicoId).HasColumnName("ano_academico_id");
            entity.Property(e => e.CapacidadMaxima)
                .HasDefaultValue(30)
                .HasColumnName("capacidad_maxima");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasColumnName("codigo");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.DirectorGrupoId).HasColumnName("director_grupo_id");
            entity.Property(e => e.GradoId).HasColumnName("grado_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.AnoAcademico).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.AnoAcademicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grupos_ano_academico_id_fkey");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grupos_colegio_id_fkey");

            entity.HasOne(d => d.Grado).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.GradoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("grupos_grado_id_fkey");
        });

        modelBuilder.Entity<LogsAuditorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("logs_auditoria_pkey");

            entity.ToTable("logs_auditoria");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Accion)
                .HasMaxLength(20)
                .HasColumnName("accion");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.DatosAnteriores)
                .HasColumnType("jsonb")
                .HasColumnName("datos_anteriores");
            entity.Property(e => e.DatosNuevos)
                .HasColumnType("jsonb")
                .HasColumnName("datos_nuevos");
            entity.Property(e => e.FechaAccion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_accion");
            entity.Property(e => e.IpAddress).HasColumnName("ip_address");
            entity.Property(e => e.RegistroId).HasColumnName("registro_id");
            entity.Property(e => e.Tabla)
                .HasMaxLength(100)
                .HasColumnName("tabla");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Colegio).WithMany(p => p.LogsAuditoria)
                .HasForeignKey(d => d.ColegioId)
                .HasConstraintName("logs_auditoria_colegio_id_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.LogsAuditoria)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("logs_auditoria_usuario_id_fkey");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("materias_pkey");

            entity.ToTable("materias");

            entity.HasIndex(e => new { e.ColegioId, e.Codigo }, "materias_colegio_id_codigo_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .HasColumnName("area");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Materia)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("materias_colegio_id_fkey");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("matriculas_pkey");

            entity.ToTable("matriculas", tb => tb.HasComment("Relación estudiante-colegio-año académico. Permite historial completo de matrículas."));

            entity.HasIndex(e => new { e.EstudianteId, e.ColegioId }, "idx_matriculas_estudiante_colegio");

            entity.HasIndex(e => new { e.EstudianteId, e.ColegioId, e.AnoAcademicoId }, "matriculas_estudiante_id_colegio_id_ano_academico_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.AnoAcademicoId).HasColumnName("ano_academico_id");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ACTIVA'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.FechaMatricula)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("fecha_matricula");
            entity.Property(e => e.GrupoId).HasColumnName("grupo_id");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");

            entity.HasOne(d => d.AnoAcademico).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.AnoAcademicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("matriculas_ano_academico_id_fkey");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("matriculas_colegio_id_fkey");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("matriculas_estudiante_id_fkey");

            entity.HasOne(d => d.Grupo).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.GrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("matriculas_grupo_id_fkey");
        });

        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mensajes_pkey");

            entity.ToTable("mensajes");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Asunto)
                .HasMaxLength(200)
                .HasColumnName("asunto");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_envio");
            entity.Property(e => e.FechaLectura)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_lectura");
            entity.Property(e => e.Leido)
                .HasDefaultValue(false)
                .HasColumnName("leido");
            entity.Property(e => e.Mensaje1).HasColumnName("mensaje");
            entity.Property(e => e.UsuarioEmisorId).HasColumnName("usuario_emisor_id");
            entity.Property(e => e.UsuarioReceptorId).HasColumnName("usuario_receptor_id");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mensajes_colegio_id_fkey");

            entity.HasOne(d => d.UsuarioEmisor).WithMany(p => p.MensajeUsuarioEmisors)
                .HasForeignKey(d => d.UsuarioEmisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mensajes_usuario_emisor_id_fkey");

            entity.HasOne(d => d.UsuarioReceptor).WithMany(p => p.MensajeUsuarioReceptors)
                .HasForeignKey(d => d.UsuarioReceptorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mensajes_usuario_receptor_id_fkey");
        });

        modelBuilder.Entity<NivelesEducativo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("niveles_educativos_pkey");

            entity.ToTable("niveles_educativos");

            entity.HasIndex(e => new { e.ColegioId, e.Codigo }, "niveles_educativos_colegio_id_codigo_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasColumnName("codigo");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Orden).HasColumnName("orden");

            entity.HasOne(d => d.Colegio).WithMany(p => p.NivelesEducativos)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("niveles_educativos_colegio_id_fkey");
        });

        modelBuilder.Entity<NotificacionDestinatario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notificacion_destinatarios_pkey");

            entity.ToTable("notificacion_destinatarios");

            entity.HasIndex(e => new { e.NotificacionId, e.UsuarioId }, "notificacion_destinatarios_notificacion_id_usuario_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.FechaLectura)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_lectura");
            entity.Property(e => e.Leida)
                .HasDefaultValue(false)
                .HasColumnName("leida");
            entity.Property(e => e.NotificacionId).HasColumnName("notificacion_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Notificacion).WithMany(p => p.NotificacionDestinatarios)
                .HasForeignKey(d => d.NotificacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notificacion_destinatarios_notificacion_id_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.NotificacionDestinatarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notificacion_destinatarios_usuario_id_fkey");
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notificaciones_pkey");

            entity.ToTable("notificaciones");

            entity.HasIndex(e => new { e.ColegioId, e.FechaCreacion }, "idx_notificaciones_colegio_fecha");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaProgramada)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_programada");
            entity.Property(e => e.Mensaje).HasColumnName("mensaje");
            entity.Property(e => e.TipoNotificacionId).HasColumnName("tipo_notificacion_id");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .HasColumnName("titulo");
            entity.Property(e => e.UsuarioEmisorId).HasColumnName("usuario_emisor_id");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notificaciones_colegio_id_fkey");

            entity.HasOne(d => d.TipoNotificacion).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.TipoNotificacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notificaciones_tipo_notificacion_id_fkey");

            entity.HasOne(d => d.UsuarioEmisor).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.UsuarioEmisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notificaciones_usuario_emisor_id_fkey");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pagos_pkey");

            entity.ToTable("pagos");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.FacturaId).HasColumnName("factura_id");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.Referencia)
                .HasMaxLength(100)
                .HasColumnName("referencia");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");
            entity.Property(e => e.ValorPago)
                .HasPrecision(12, 2)
                .HasColumnName("valor_pago");

            entity.HasOne(d => d.Factura).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pagos_factura_id_fkey");

            entity.HasOne(d => d.RegistradoPorNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.RegistradoPor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pagos_registrado_por_fkey");
        });

        modelBuilder.Entity<Pensum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pensum_pkey");

            entity.ToTable("pensum");

            entity.HasIndex(e => new { e.GradoId, e.MateriaId, e.AnoAcademicoId }, "pensum_grado_id_materia_id_ano_academico_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.AnoAcademicoId).HasColumnName("ano_academico_id");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.EsObligatoria)
                .HasDefaultValue(true)
                .HasColumnName("es_obligatoria");
            entity.Property(e => e.GradoId).HasColumnName("grado_id");
            entity.Property(e => e.IntensidadHoraria)
                .HasDefaultValue(1)
                .HasColumnName("intensidad_horaria");
            entity.Property(e => e.MateriaId).HasColumnName("materia_id");

            entity.HasOne(d => d.AnoAcademico).WithMany(p => p.Pensums)
                .HasForeignKey(d => d.AnoAcademicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pensum_ano_academico_id_fkey");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Pensums)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pensum_colegio_id_fkey");

            entity.HasOne(d => d.Grado).WithMany(p => p.Pensums)
                .HasForeignKey(d => d.GradoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pensum_grado_id_fkey");

            entity.HasOne(d => d.Materia).WithMany(p => p.Pensums)
                .HasForeignKey(d => d.MateriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pensum_materia_id_fkey");
        });

        modelBuilder.Entity<PeriodosAcademico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("periodos_academicos_pkey");

            entity.ToTable("periodos_academicos");

            entity.HasIndex(e => new { e.AnoAcademicoId, e.Numero }, "periodos_academicos_ano_academico_id_numero_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.AnoAcademicoId).HasColumnName("ano_academico_id");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Numero).HasColumnName("numero");

            entity.HasOne(d => d.AnoAcademico).WithMany(p => p.PeriodosAcademicos)
                .HasForeignKey(d => d.AnoAcademicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("periodos_academicos_ano_academico_id_fkey");

            entity.HasOne(d => d.Colegio).WithMany(p => p.PeriodosAcademicos)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("periodos_academicos_colegio_id_fkey");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("personas_pkey");

            entity.ToTable("personas", tb => tb.HasComment("Tabla base para todas las personas del sistema. Incluye nombres completos (primer y segundo nombre/apellido) y datos básicos."));

            entity.HasIndex(e => new { e.TipoDocumentoId, e.NumeroDocumento }, "idx_personas_documento");

            entity.HasIndex(e => new { e.TipoDocumentoId, e.NumeroDocumento }, "personas_tipo_documento_id_numero_documento_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .HasColumnName("apellidos");
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .HasColumnName("celular");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.FotoUrl)
                .HasMaxLength(500)
                .HasColumnName("foto_url");
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .HasColumnName("genero");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .HasColumnName("nombres");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(20)
                .HasColumnName("numero_documento");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(100)
                .HasComment("Segundo apellido de la persona (opcional)")
                .HasColumnName("segundo_apellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(100)
                .HasComment("Segundo nombre de la persona (opcional)")
                .HasColumnName("segundo_nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoDocumentoId).HasColumnName("tipo_documento_id");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.Personas)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personas_tipo_documento_id_fkey");
        });

        modelBuilder.Entity<ProfesorColegio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("profesor_colegios_pkey");

            entity.ToTable("profesor_colegios");

            entity.HasIndex(e => new { e.ProfesorId, e.ColegioId }, "profesor_colegios_profesor_id_colegio_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.ProfesorId).HasColumnName("profesor_id");

            entity.HasOne(d => d.Colegio).WithMany(p => p.ProfesorColegios)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profesor_colegios_colegio_id_fkey");

            entity.HasOne(d => d.Profesor).WithMany(p => p.ProfesorColegios)
                .HasForeignKey(d => d.ProfesorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profesor_colegios_profesor_id_fkey");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("profesores_pkey");

            entity.ToTable("profesores");

            entity.HasIndex(e => e.CodigoEmpleado, "profesores_codigo_empleado_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.CodigoEmpleado)
                .HasMaxLength(20)
                .HasColumnName("codigo_empleado");
            entity.Property(e => e.Especialidades).HasColumnName("especialidades");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.PersonaId).HasColumnName("persona_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Persona).WithMany(p => p.Profesores)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profesores_persona_id_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Profesores)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("profesores_usuario_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles", tb => tb.HasComment("Roles del sistema. Puede ser global (colegio_id IS NULL) para ADMIN_GLOBAL y SUPER_ADMIN, o específico por colegio."));

            entity.HasIndex(e => new { e.ColegioId, e.Codigo }, "idx_roles_colegio").HasFilter("(colegio_id IS NOT NULL)");

            entity.HasIndex(e => e.Codigo, "idx_roles_globales").HasFilter("(colegio_id IS NULL)");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.ColegioId)
                .HasComment("ID del colegio. NULL para roles globales (ADMIN_GLOBAL, SUPER_ADMIN).")
                .HasColumnName("colegio_id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Permisos)
                .HasColumnType("jsonb")
                .HasColumnName("permisos");

            entity.HasOne(d => d.Colegio).WithMany(p => p.Roles)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("roles_colegio_id_fkey");
        });

        modelBuilder.Entity<TiposDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipos_documento_pkey");

            entity.ToTable("tipos_documento");

            entity.HasIndex(e => e.Codigo, "tipos_documento_codigo_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TiposEvaluacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipos_evaluacion_pkey");

            entity.ToTable("tipos_evaluacion");

            entity.HasIndex(e => new { e.ColegioId, e.Codigo }, "tipos_evaluacion_colegio_id_codigo_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Porcentaje)
                .HasPrecision(5, 2)
                .HasColumnName("porcentaje");

            entity.HasOne(d => d.Colegio).WithMany(p => p.TiposEvaluacions)
                .HasForeignKey(d => d.ColegioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tipos_evaluacion_colegio_id_fkey");
        });

        modelBuilder.Entity<TiposNotificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipos_notificacion_pkey");

            entity.ToTable("tipos_notificacion");

            entity.HasIndex(e => e.Codigo, "tipos_notificacion_codigo_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TiposUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipos_usuario_pkey");

            entity.ToTable("tipos_usuario");

            entity.HasIndex(e => e.Codigo, "tipos_usuario_codigo_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarios_pkey");

            entity.ToTable("usuarios", tb => tb.HasComment("Gestión unificada de usuarios con SSO. Un usuario puede tener acceso a múltiples colegios."));

            entity.HasIndex(e => e.Email, "idx_usuarios_email");

            entity.HasIndex(e => e.Username, "idx_usuarios_username");

            entity.HasIndex(e => e.Email, "usuarios_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "usuarios_username_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.BloqueadoHasta)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("bloqueado_hasta");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IntentosFallidos)
                .HasDefaultValue(0)
                .HasColumnName("intentos_fallidos");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.RequiereCambioPassword)
                .HasDefaultValue(false)
                .HasColumnName("requiere_cambio_password");
            entity.Property(e => e.Salt)
                .HasMaxLength(100)
                .HasColumnName("salt");
            entity.Property(e => e.UltimoAcceso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ultimo_acceso");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UsuarioRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuario_roles_pkey");

            entity.ToTable("usuario_roles", tb => tb.HasComment("Asignación de roles a usuarios. Para roles globales, colegio_id es NULL. La consistencia se valida mediante triggers."));

            entity.HasIndex(e => new { e.UsuarioId, e.ColegioId }, "idx_usuario_roles_colegio").HasFilter("(colegio_id IS NOT NULL)");

            entity.HasIndex(e => new { e.UsuarioId, e.RolId }, "idx_usuario_roles_globales").HasFilter("(colegio_id IS NULL)");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ColegioId)
                .HasComment("ID del colegio. NULL para asignaciones de roles globales.")
                .HasColumnName("colegio_id");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Colegio).WithMany(p => p.UsuarioRoles)
                .HasForeignKey(d => d.ColegioId)
                .HasConstraintName("usuario_roles_colegio_id_fkey");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsuarioRoles)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("usuario_roles_rol_id_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioRoles)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_roles_usuario_id_fkey");
        });

        modelBuilder.Entity<VistaRolesColegio>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vista_roles_colegio");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Ambito).HasColumnName("ambito");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.ColegioNombre)
                .HasMaxLength(200)
                .HasColumnName("colegio_nombre");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Permisos)
                .HasColumnType("jsonb")
                .HasColumnName("permisos");
        });

        modelBuilder.Entity<VistaRolesGlobale>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vista_roles_globales");

            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Ambito).HasColumnName("ambito");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Permisos)
                .HasColumnType("jsonb")
                .HasColumnName("permisos");
        });

        modelBuilder.Entity<VistaUsuariosRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vista_usuarios_roles");

            entity.Property(e => e.ColegioId).HasColumnName("colegio_id");
            entity.Property(e => e.ColegioNombre)
                .HasMaxLength(200)
                .HasColumnName("colegio_nombre");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FechaAsignacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.RolActivo).HasColumnName("rol_activo");
            entity.Property(e => e.RolCodigo)
                .HasMaxLength(20)
                .HasColumnName("rol_codigo");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.RolNombre)
                .HasMaxLength(100)
                .HasColumnName("rol_nombre");
            entity.Property(e => e.TipoRol).HasColumnName("tipo_rol");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.UsuarioRolId).HasColumnName("usuario_rol_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
