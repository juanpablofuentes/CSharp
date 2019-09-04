using Group.Salto.ServiceLibrary.Common.Dtos.Identity;
using Group.Salto.SOM.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class LoginViewModelExtensions
    {
        public static IdentityLoginDto ToIdentityLoginDto(this LoginViewModel source)
        {
            IdentityLoginDto result = new IdentityLoginDto();

            result.Email = source.Email;
            result.Password = source.Password;
            result.RememberMe = source.RememberMe;
            result.UserName = source.UserName;
            result.ReturnUrl = source.ReturnUrl;
            return result;
        }

        public static LoginViewModel ToLoginViewModel(this IdentityLoginDto source)
        {
            LoginViewModel result = new LoginViewModel();

            result.Email = source.Email;
            result.Password = source.Password;
            result.RememberMe = source.RememberMe;
            result.UserName = source.UserName;
            result.ReturnUrl = source.ReturnUrl;
            return result;
        }
    }
}