using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aeropuerto
{
    public class Contexto : DbContext
    {


        public Contexto()
        {
        }

        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public virtual DbSet<Avion> Aviones { get; set; }
        public virtual DbSet<Piloto> Pilotos { get; set; }
        public virtual DbSet<Vuelo> Vuelos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=aeropuerto;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avion>(entity =>
            {


                entity.Property(e => e.Modelo)
                    .HasColumnName("Modelo")
                    .HasMaxLength(150)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<Piloto>(entity =>
            {
                entity.Property(e => e.Categoria)
                    .HasColumnName("Categoria")
                    .HasMaxLength(50)
                    .IsUnicode(true);
            });
            modelBuilder.Entity<Vuelo>().HasOne(e => e.Piloto)
            .WithMany(b => b.Vuelos)
            .HasForeignKey(p => p.IdPiloto)
            .HasConstraintName("ForeignKey_Vuelo_Piloto"); ;
        }
    }

}