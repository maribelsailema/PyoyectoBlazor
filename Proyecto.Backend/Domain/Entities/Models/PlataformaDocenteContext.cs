using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Backend.Domain.Entities.Models;

public partial class PlataformaDocenteContext : DbContext
{
    public PlataformaDocenteContext() { }

    public PlataformaDocenteContext(DbContextOptions<PlataformaDocenteContext> options)
        : base(options) { }

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
            entity.HasKey(e => e.IdCap);

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
            entity.HasKey(e => e.IdCarrera);

            entity.HasIndex(e => e.Nombre).IsUnique();
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Carreras)
                .HasForeignKey(d => d.IdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carreras_Facultad");
        });

        modelBuilder.Entity<EvaluacionesDocente>(entity =>
        {
            entity.HasKey(e => e.IdEval);

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
            entity.HasKey(e => e.IdFacultad);

            entity.HasIndex(e => e.Nombre).IsUnique();
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Investigacione>(entity =>
        {
            entity.HasKey(e => e.IdInv);

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
                .HasConstraintName("FK_Inv_Carrera");
        });

        modelBuilder.Entity<Obra>(entity =>
        {
            entity.HasKey(e => e.IdObra);

            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Titulo).HasMaxLength(255);
            entity.Property(e => e.TipoObra).HasMaxLength(50);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Pais).HasMaxLength(100);
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.Editorial).HasMaxLength(255);
            entity.Property(e => e.ISBN).HasMaxLength(50);
            entity.Property(e => e.DOI).HasMaxLength(100);
            entity.Property(e => e.Enlace).HasMaxLength(255);
            entity.Property(e => e.Autores).HasMaxLength(255);
            entity.Property(e => e.Resumen).HasColumnType("nvarchar(max)");
            entity.Property(e => e.PDF).HasColumnName("PDF");

            entity.HasOne(d => d.CedulaNavigation).WithMany(p => p.Obras)
                .HasForeignKey(d => d.Cedula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Obra_Usuario");
        });

        modelBuilder.Entity<RequisitosAscenso>(entity =>
        {
            entity.HasKey(e => e.Id);

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
            entity.HasKey(e => e.IdRol);

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
            entity.HasKey(e => e.Ced);

            entity.HasIndex(e => e.Usuari).IsUnique();

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