using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jardin
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public Contexto()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=jardin;Trusted_Connection=True;");
            }
        }

        public DbSet<Planta> Plantas { get; set; }
        public DbSet<Jardinero> Jardineros { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planta>(entity =>
            {
                 entity.Property(e => e.Tipo)
                            .HasColumnName("Tipo")
                            .HasMaxLength(50)
                            .IsUnicode(false);
            });
        }
    }
}
