using Group.Salto.Common.Constants;
using Group.Salto.ServiceLibrary.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        readonly Claim _claim;

        public CustomAuthorizationFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string claimUserConfigurationId = string.Empty;
            if (context.HttpContext.User.Identity is ClaimsIdentity claimsIdentity)
            {
                claimUserConfigurationId = claimsIdentity.FindFirst(AppIdentityClaims.UserId).Value;
            }

            bool canDoAction = RolesActionGroupsActionsTenantHelper.CanDoAction(claimUserConfigurationId, _claim.Type, _claim.Value);
            if (!canDoAction)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}