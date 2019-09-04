using Group.Salto.AspNet.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.AspNet.Identity.Managers
{
    public class ApplicationUserManager<TEntity> : UserManager<TEntity> where TEntity : IdentityUser
    {
        public ApplicationUserManager(IUserStore<TEntity> store)
            : base(store) { }

        public static ApplicationUserManager<TEntity> Create(IdentityFactoryOptions<ApplicationUserManager<TEntity>> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager<TEntity>(new UserStore<TEntity>(context.Get<IdentityContext<TEntity>>()));
            // Configure validation logic for usernames
            manager.UserValidator = new CustomUserValidator<TEntity>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<TEntity>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<TEntity>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<TEntity>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
