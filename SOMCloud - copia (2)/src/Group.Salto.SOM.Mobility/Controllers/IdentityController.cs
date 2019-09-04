using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Group.Salto.Common;
using Group.Salto.Common.Constants;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Auth;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Extension;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Group.Salto.SOM.Mobility.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService, 
                                  IConfiguration configuration) : base(configuration)
        {
            _identityService = identityService ?? throw new ArgumentNullException($"{nameof(identityService)} is null");
        }

        private const string TokenType = "Bearer";

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = new ResultDto<UserAuthDto>();

            if (ModelState.IsValid)
            {
                var identityResult = await _identityService.SignInToken(login.ToIdentityLoginDto());
                if (identityResult.Data.Result == Salto.Common.Enums.IdentityResultEnum.Success)
                {
                    string token =  await GenerateJwtToken(login.Email, identityResult.Data.Data);

                    UserAuthDto tokenData = new UserAuthDto
                    {
                        TokenType = TokenType,
                        AccessToken = token,
                        LangCode = identityResult.Data.LanguageCode,
                        UserName = login.Email
                    };
                    result.Data = tokenData;
                    return Ok(result);
                }

                return OnLoginFailed(result);
            }

            return OnLoginInvalid(result);
        }

        private IActionResult OnLoginInvalid(ResultDto<UserAuthDto> login)
        {
            login.Errors.AddError(new ErrorDto()
            {
                ErrorMessageKey = "Invalid Data",
                ErrorType = ErrorType.ValidationError
            });
            return Ok(login);
        }

        private IActionResult OnLoginFailed(ResultDto<UserAuthDto> login)
        {
            login.Errors.AddError(new ErrorDto()
            {
                ErrorMessageKey = "Login failed",
                ErrorType = ErrorType.CannotAccessDatabase
            });
            return Ok(login);
        }
        private async Task<string> GenerateJwtToken(string email, UserApiDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserConfigurationId.ToString())
            };
            claims.Add(new Claim(AppIdentityClaims.TenantId, user.TennantId.ToString()));
            claims.Add(new Claim(AppIdentityClaims.NumberEntriesPerPage, user.NumberEntriesPerPage.ToString()));
            claims.Add(new Claim(AppIdentityClaims.Mail, email));
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.PrivateKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration.ExpireTimeMinutes));

            var token = new JwtSecurityToken(
                _configuration.Issuer,
                _configuration.Audience,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}