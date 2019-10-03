using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWeb2.Models
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

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

    }
}
