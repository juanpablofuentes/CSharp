using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Group.Salto.AspNet.Identity
{
    public static class PrincipalExtension
    {
        public static bool IsInRole(this IPrincipal source, string rol)
        {
            var claims = source.Identity as ClaimsIdentity;
            var rolesClaims = claims.Claims.Where(it => it.Type == ClaimTypes.Role).ToList();
            return rolesClaims.Any(it => it.Value == rol);
        }

        public static bool IsInRoles(this IPrincipal source, IEnumerable<string> roles)
        {
            var claims = source.Identity as ClaimsIdentity;
            var rolesClaims = claims.Claims.Where(it => it.Type == ClaimTypes.Role).ToList();
            return rolesClaims.Any(it => roles.Any(rol => rol.Trim() == it.Value));
        }
    }
}
