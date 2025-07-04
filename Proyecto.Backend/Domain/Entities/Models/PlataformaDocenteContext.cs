using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Backend.Domain.Entities.Models;

public partial class PlataformaDocenteContext : DbContext
{
    public PlataformaDocenteContext()
    {
    }

    public PlataformaDocenteContext(DbContextOptions<PlataformaDocenteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Capacitacione> Capacitaciones { get; set; }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<EvaluacionesDocente> EvaluacionesDocentes { get; set; }

    public virtual DbSet<Facultade> Facultades { get; set; }

    public virtual DbSet<Investigacione> Investigaciones { get; set; }

    public virtual DbSet<Obra> Obras { get; set; }

    public virtual DbSet<RequisitosAscenso> RequisitosAscensos { get; set; }

    public virtual DbSet<RolesDocente> RolesDocentes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Postulacion> Postulaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Capacitacione>(entity =>
        {
            entity.HasKey(e => e.IdCap).HasName("PK__Capacita__0FA7805A886188BE");

            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NombreCurso).HasMaxLength(100);
            entity.Property(e => e.Pdf).HasColumnName("PDF");

            entity.HasOne(d => d.CedulaNavigation).WithMany(p => p.Capacitaciones)
                .HasForeignKey(d => d.Cedula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cap_Usuario");
        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.IdCarrera).HasName("PK__Carreras__884A8F1F7ED44CD4");

            entity.HasIndex(e => e.Nombre, "UQ__Carreras__75E3EFCF7AFD1A87").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Carreras)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Carreras__IdFacu__5165187F");
        });

        modelBuilder.Entity<EvaluacionesDocente>(entity =>
        {
            entity.HasKey(e => e.IdEval).HasName("PK__Evaluaci__0E05EE774C941474");

            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Periodo).HasMaxLength(20);
            entity.Property(e => e.PuntajeFinal).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.CedulaNavigation).WithMany(p => p.EvaluacionesDocentes)
                .HasForeignKey(d => d.Cedula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Eval_Usuario");
        });

        modelBuilder.Entity<Facultade>(entity =>
        {
            entity.HasKey(e => e.IdFacultad).HasName("PK__Facultad__443D4D5E2E176AE5");

            entity.HasIndex(e => e.Nombre, "UQ__Facultad__75E3EFCF5C0C7E6C").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Investigacione>(entity =>
        {
            entity.HasKey(e => e.IdInv).HasName("PK__Investig__0C1AC169E2EC0101");

            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NombreProyecto).HasMaxLength(100);
            entity.Property(e => e.Pdf).HasColumnName("PDF");

            entity.HasOne(d => d.CedulaNavigation).WithMany(p => p.Investigaciones)
                .HasForeignKey(d => d.Cedula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inv_Usuario");

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.Investigaciones)
                .HasForeignKey(d => d.IdCarrera)
                .HasConstraintName("FK__Investiga__IdCar__52593CB8");
        });

        modelBuilder.Entity<Obra>(entity =>
        {
            entity.HasKey(e => e.IdObra).HasName("PK__Obras__8034B032485C9638");

            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Documento).HasColumnName("PDF");
            entity.Property(e => e.TipoObra).HasMaxLength(50);

            entity.HasOne(d => d.CedulaNavigation).WithMany(p => p.Obras)
                .HasForeignKey(d => d.Cedula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Obra_Usuario");
        });

        modelBuilder.Entity<RequisitosAscenso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requisit__3214EC072A51AC7D");

            entity.ToTable("RequisitosAscenso");

            entity.Property(e => e.DesdeRol)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.HaciaRol)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PuntajeEvaluacionMinimo).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<RolesDocente>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__RolesDoc__2A49584C23304EE2");

            entity.ToTable("RolesDocente");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FechaAsignacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rol)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.CedulaNavigation).WithMany(p => p.RolesDocentes)
                .HasForeignKey(d => d.Cedula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Ced).HasName("PK__Usuarios__C1FE05F2A6CF09A3");

            entity.HasIndex(e => e.Usuari, "UQ__Usuarios__B6E95C87E8D98C9C").IsUnique();

            entity.Property(e => e.Ced)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Ape1).HasMaxLength(50);
            entity.Property(e => e.Ape2).HasMaxLength(50);
            entity.Property(e => e.Nom1).HasMaxLength(50);
            entity.Property(e => e.Nom2).HasMaxLength(50);
            entity.Property(e => e.Pass)
                .HasMaxLength(255)
                .HasColumnName("pass");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Usuari).HasMaxLength(100);
        });

        modelBuilder.Entity<Postulacion>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.RolActual)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.RolSolicitado)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.FechaSolicitud)
                .HasColumnType("datetime");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);

        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
