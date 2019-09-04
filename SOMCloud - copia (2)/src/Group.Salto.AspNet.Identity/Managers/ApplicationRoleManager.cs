using Group.Salto.AspNet.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.AspNet.Identity.Managers
{
    public class ApplicationRoleManager<TEntity> : RoleManager<IdentityRole> where TEntity : IdentityUser
    {
        public ApplicationRoleManager(RoleStore<IdentityRole, string> store) : base(store) { }

        public static ApplicationRoleManager<TEntity> Create(IdentityFactoryOptions<ApplicationRoleManager<TEntity>> options, IOwinContext context)
        {
            var rolManager = new ApplicationRoleManager<TEntity>(new RoleStore<IdentityRole>(context.Get<IdentityContext<TEntity>>()));
            return rolManager;
        }

    }
}
