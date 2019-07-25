using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia
{
    class Contexto:DbContext
    {
        public Contexto()
        {
        }

        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public virtual DbSet<Estudiante> Estudiantes { get; set; }
        public virtual DbSet<Curso> Cursos { get; set; }
        public virtual DbSet<Matricula> Matriculas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=academia;Trusted_Connection=True;");
            }
        }
    }
}
