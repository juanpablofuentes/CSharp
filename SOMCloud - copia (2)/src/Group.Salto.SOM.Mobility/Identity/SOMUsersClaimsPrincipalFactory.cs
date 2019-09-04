using Group.Salto.Common.Constants;
using Group.Salto.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Mobility.Identity
{
    public class SOMUsersClaimsPrincipalFactory : UserClaimsPrincipalFactory<Users, Roles>
    {
        private IHttpContextAccessor context;
        public SOMUsersClaimsPrincipalFactory(IHttpContextAccessor context,
            UserManager<Users> userManager,
            RoleManager<Roles> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
            this.context = context;
        }

        public override Task<ClaimsPrincipal> CreateAsync(Users user)
        {
            return base.CreateAsync(user);
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Users user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            if (identity.FindFirst(AppIdentityClaims.TenantId) == null)
            {
                Users usr = await base.UserManager.Users
                                    .Include(u => u.Customer)
                                    .SingleAsync(u => u.UserName == user.UserName);

                identity.AddClaim(new Claim(AppIdentityClaims.TenantId, usr.Customer.Id.ToString()));
                identity.AddClaim(new Claim(AppIdentityClaims.NumberEntriesPerPage, usr.NumberEntriesPerPage.ToString()));
            }
            if (identity.FindFirst(AppIdentityClaims.Mail) == null && !string.IsNullOrEmpty(user.Email))
                identity.AddClaim(new Claim(AppIdentityClaims.Mail, user.Email));
      
            return identity;
        }
    }

}
