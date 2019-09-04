using Group.Salto.Common;
using Group.Salto.DataAccess.Context;
using Group.Salto.Entities;
using Group.Salto.SOM.Web.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.SOM.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Group.Salto.SOM.Web.Extensions
{
    public static class SOMIdentityExtensions
    {

        public static void AddSOMIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SOMContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(AppsettingsKeys.ConnectionString)));

            // first step is configure the options

            //MANU comment:
            //here add the identity to pipeline and return the builder in order to 
            //respect the FluentAPI pattern

            services.AddIdentityCore<Users>()
                   .AddRoles<Roles>()
                   .AddEntityFrameworkStores<SOMContext>()
                   .AddSignInManager()
                   .AddDefaultTokenProviders();
            services.AddScoped<IUserClaimsPrincipalFactory<Users>, SOMUsersClaimsPrincipalFactory>();

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
                //options.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
            });

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;

            }).AddIdentityCookies(options =>
            {                
                options.ApplicationCookie.Configure(opt =>
                {
                    opt.LoginPath = "/Account/Login";
                    opt.ExpireTimeSpan = TimeSpan.FromHours(configuration.GetSection(AppsettingsKeys.CookiesConfiguration)
                                                                            .Get<CookiesConfiguration>().ExpirationHours);                    
                });
            });
        }
    }
}
