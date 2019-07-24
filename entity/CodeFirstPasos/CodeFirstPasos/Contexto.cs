using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFirstPasos
{
    class Contexto : DbContext
    {
        public Contexto()
        {
           // this.Database.Migrate();
        }
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=hormiguero3;Trusted_Connection=True;");
            }
        }
        public DbSet<Hormiga> Hormigas { get; set; }

    }
}
