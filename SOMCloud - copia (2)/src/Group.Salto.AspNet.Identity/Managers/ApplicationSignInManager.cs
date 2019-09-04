using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Group.Salto.AspNet.Identity.Managers
{
    public class ApplicationSignInManager<TEntity> : SignInManager<TEntity, string> where TEntity : IdentityUser
    {
        public ApplicationSignInManager(ApplicationUserManager<TEntity> userManager, AuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(TEntity user) => ((IBaseIdentityUser<TEntity>)user).GenerateUserIdentityAsync((UserManager<TEntity>)UserManager, user);

        public static ApplicationSignInManager<TEntity> Create(IdentityFactoryOptions<ApplicationSignInManager<TEntity>> options, IOwinContext context)
        {
            return new ApplicationSignInManager<TEntity>(context.GetUserManager<ApplicationUserManager<TEntity>>(), context.Authentication);
        }
    }
}
