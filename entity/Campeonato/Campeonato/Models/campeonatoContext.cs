using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Campeonato.Models
{
    public partial class campeonatoContext : DbContext
    {
        public campeonatoContext()
        {
        }

        public campeonatoContext(DbContextOptions<campeonatoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Jugador> Jugador { get; set; }
        public virtual DbSet<Partido> Partido { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=campeonato;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Jugador>(entity =>
            {
                entity.ToTable("jugador");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Alias)
                    .HasColumnName("alias")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Puntos).HasColumnName("puntos");
            });

            modelBuilder.Entity<Partido>(entity =>
            {
                entity.ToTable("partido");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idjugador).HasColumnName("idjugador");

                entity.HasOne(d => d.IdjugadorNavigation)
                    .WithMany(p => p.Partido)
                    .HasForeignKey(d => d.Idjugador)
                    .HasConstraintName("FK_partido_jugador");
            });
        }
    }
}
