using Group.Salto.ServiceLibrary.Common.Dtos.Identity;
using Group.Salto.SOM.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ResetPasswordViewModelExtensions
    {
        public static IdentityResetPasswordDto ToIdentityLoginDto(this ResetPasswordViewModel source)
        {
            IdentityResetPasswordDto result = null;
            if (source != null)
            {
                result = new IdentityResetPasswordDto()
                {
                    UserId = source.UserId,
                    Token = source.Token,
                    Password = source.Password
                };                
            }               
            return result;
        }
    }
}