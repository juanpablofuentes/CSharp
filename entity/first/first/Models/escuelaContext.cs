using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace first.Models
{
    public partial class escuelaContext : DbContext
    {
        public escuelaContext()
        {
        }

        public escuelaContext(DbContextOptions<escuelaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Profesor> Profesor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=escuela;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.Idalumno);

                entity.ToTable("alumno");

                entity.Property(e => e.Idalumno).HasColumnName("idalumno");

                entity.Property(e => e.Idcurso).HasColumnName("idcurso");

                entity.Property(e => e.Mail)
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Curso)
                    .WithMany(p => p.Alumno)
                    .HasForeignKey(d => d.Idcurso)
                    .HasConstraintName("FK_alumno_curso");
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.Idcurso);

                entity.ToTable("curso");

                entity.Property(e => e.Idcurso).HasColumnName("idcurso");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.HasKey(e => e.IdProfesor);

                entity.ToTable("profesor");

                entity.Property(e => e.IdProfesor).HasColumnName("idProfesor");

                entity.Property(e => e.Idcurso).HasColumnName("idcurso");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Curso)
                    .WithMany(p => p.Profesor)
                    .HasForeignKey(d => d.Idcurso)
                    .HasConstraintName("FK_profesor_curso");
            });
        }
    }
}
