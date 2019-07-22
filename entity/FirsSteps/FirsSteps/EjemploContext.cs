using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FirsSteps
{
    class EjemploContext : DbContext
    {
        public virtual DbSet<Alumno> Alumno { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder
optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=TestEntity;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.Property(e => e.idAlumno).HasColumnName("idAlumno");
                entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            });
        }
    }
}