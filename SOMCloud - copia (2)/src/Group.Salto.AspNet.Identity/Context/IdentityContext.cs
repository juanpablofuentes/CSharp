using DataAccess.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.AspNet.Identity.Context
{
    public class IdentityContext<TEntity> : IdentityDbContext<TEntity> where TEntity : IdentityUser
    { 
        public IdentityContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
