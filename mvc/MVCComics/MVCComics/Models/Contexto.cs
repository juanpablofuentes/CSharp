using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCComics
{
   public class Contexto:DbContext
    {
        public Contexto()
        {
          
        }

        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
          
        }

        public virtual DbSet<Comic> Comic { get; set; }
        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<ComicAutor> ComicAutor { get; set; }

    }
}
