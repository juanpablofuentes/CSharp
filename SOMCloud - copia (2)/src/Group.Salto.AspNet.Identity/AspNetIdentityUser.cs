using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Group.Salto.AspNet.Identity
{

    public interface IBaseIdentityUser<T> where T : IdentityUser
    {
        Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<T> manager, T user);
    }

    public class AspNetIdentityUser<T> : IdentityUser, IBaseIdentityUser<T> where T : IdentityUser
    {

        public virtual async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<T> manager, T user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
