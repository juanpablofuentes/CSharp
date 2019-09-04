using Group.Salto.DataAccess.Context;
using Group.Salto.Entities;
using Group.Salto.SOM.Mobility.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Group.Salto.Common.Constants.ApiSettings;
using Group.Salto.Common.Entities.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Group.Salto.SOM.Mobility.Extensions
{
    public static class SOMIdentityExtensions
    {
        private static OAuthConfiguration _configurationApi;
        public static void AddSOMIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SOMContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ApiSettingsKeys.ConnectionString)));

            _configurationApi = configuration.GetSection(ApiSettingsKeys.OAuthConfiguration)
                                 .Get<OAuthConfiguration>() ??
                             throw new ArgumentNullException(nameof(OAuthConfiguration));
            // first step is configure the options
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
            });


            services.AddIdentityCore<Users>()
                .AddRoles<Roles>()
                .AddEntityFrameworkStores<SOMContext>()
                 .AddSignInManager()
                .AddDefaultTokenProviders();
 

            services.AddScoped<IUserClaimsPrincipalFactory<Users>, SOMUsersClaimsPrincipalFactory>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;
                        cfg.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = _configurationApi.Issuer,
                            ValidAudience = _configurationApi.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationApi.PrivateKey)),
                            ClockSkew = TimeSpan.Zero // remove delay of token when expire
                        };
                    }).AddIdentityCookies(o => { }); ;

        }
    }
}
