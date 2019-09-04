using Group.Salto.Common.Constants;
using Group.Salto.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Identity
{
    public class SOMUsersClaimsPrincipalFactory : UserClaimsPrincipalFactory<Users, Roles>
    {
        private IHttpContextAccessor context;
        public SOMUsersClaimsPrincipalFactory(IHttpContextAccessor context,
            UserManager<Users> userManager,
            RoleManager<Roles> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
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
                identity.AddClaim(new Claim(AppIdentityClaims.UserId, usr.UserConfigurationId.Value.ToString()));
                identity.AddClaim(new Claim(AppIdentityClaims.TimeZoneId, "Romance Standard Time")); //TODO: cambiar cuando se guarde en el usuario

                //We use context items in order to share the tenantid with the controller 
                //context.HttpContext.Items[AppIdentityClaims.TenantId] = usr.Customer.Id.ToString();
            }

            if (identity.FindFirst(AppIdentityClaims.Mail) == null && !string.IsNullOrEmpty(user.Email))
            {
                identity.AddClaim(new Claim(AppIdentityClaims.Mail, user.Email));
            }
            return identity;
        }
    }
}