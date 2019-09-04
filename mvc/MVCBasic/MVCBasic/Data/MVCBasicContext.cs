using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVCBasic.Models
{
    public class MVCBasicContext : DbContext
    {
        public MVCBasicContext (DbContextOptions<MVCBasicContext> options)
            : base(options)
        {
        }

        public DbSet<MVCBasic.Models.Cientifico> Cientifico { get; set; }
    }
}
