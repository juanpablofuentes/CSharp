using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Multitenant;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.DataAccess.Tenant.UoW;
using Group.Salto.DataAccess.UoW;
using Group.Salto.Ioc;
using Group.Salto.Ioc.Extensions;
using Group.Salto.ServiceLibrary.Common.Contracts;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Implementations.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ContainerIoC
    {
        public static AutofacServiceProvider Create(IOCConfiguration iocConfiguration)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new ResolverIoC(iocConfiguration.Configuration));
            iocConfiguration.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            iocConfiguration.Services.AddSingleton<ICache, MemoryCache>();
            iocConfiguration.Services.AddTransient<ITenantIdentificationStrategy, UserStrategy>();
            iocConfiguration.Services.AddTransient<INotificationFactory, NotificationFactory>();

            //Unit of Work resolution
            iocConfiguration.Services.AddScoped(typeof(ITenantUnitOfWork), (provider) =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                ITenantService tenantService = provider.GetService<ITenantService>();
                string connectionString = default(string);
                string ownerId = null;
                if (context.HttpContext?.User != null && ((ClaimsIdentity)context.HttpContext.User?.Identity).FindFirst(AppIdentityClaims.TenantId) != null)
                {
                    connectionString = tenantService.GetTenant(Guid.Parse(((ClaimsIdentity)context.HttpContext.User?.Identity).FindFirst(AppIdentityClaims.TenantId).Value)).Data.ConnectionString;
                    if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("Connectionstring is empty");
                    ownerId = ((ClaimsIdentity)context.HttpContext.User?.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                }
                UnitOfWorkTenant uoW = new UnitOfWorkTenant(connectionString, ownerId);
                uoW.OwnerId = ownerId;
                return uoW;
            });
            iocConfiguration.Services.AddScoped(typeof(IUnitOfWork), (provider) =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                var config = provider.GetService<IConfiguration>();
                string connectionString = config.GetConnectionString(AppsettingsKeys.ConnectionString);
                string ownerId = null;
                if (context.HttpContext?.User != null && context.HttpContext.User.Identity.IsAuthenticated)
                {
                    ownerId = ((ClaimsIdentity)context.HttpContext.User?.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                }
                UnitOfWork uoW = new UnitOfWork(connectionString, ownerId);
                return uoW;
            });

            containerBuilder.Populate(iocConfiguration.Services);
            var container = containerBuilder.Build();
            container.SetInstancesHelper();
            var strategy = container.Resolve<ITenantIdentificationStrategy>();
            var mtc = new MultitenantContainer(strategy, container);

            iocConfiguration.ApplicationContainer = mtc;
            return new AutofacServiceProvider(mtc);
        }

        public static AutofacServiceProvider CreateMobility(IOCMobilityConfiguration iocConfiguration)
        {

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new ResolverIoC(iocConfiguration.Configuration));
            iocConfiguration.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            iocConfiguration.Services.AddSingleton<ICache, MemoryCache>();
            iocConfiguration.Services.AddTransient<ITenantIdentificationStrategy, UserStrategy>();
            iocConfiguration.Services.AddTransient<INotificationFactory, NotificationFactory>();
            //Unit of Work resolution
            iocConfiguration.Services.AddScoped(typeof(ITenantUnitOfWork), (provider) =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                ITenantService tenantService = provider.GetService<ITenantService>();
                string connectionString = default(string);
                string ownerId = null;
                if (context.HttpContext?.User != null && ((ClaimsIdentity)context.HttpContext.User?.Identity).FindFirst(AppIdentityClaims.TenantId) != null)
                {
                    connectionString = tenantService.GetTenant(Guid.Parse(((ClaimsIdentity)context.HttpContext.User?.Identity).FindFirst(AppIdentityClaims.TenantId).Value)).Data.ConnectionString;
                    if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("Connectionstring is empty");
                    ownerId = ((ClaimsIdentity)context.HttpContext.User?.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                }
                UnitOfWorkTenant uoW = new UnitOfWorkTenant(connectionString, ownerId);
                uoW.OwnerId = ownerId;
                return uoW;
            });
            iocConfiguration.Services.AddScoped(typeof(IUnitOfWork), (provider) =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                var config = provider.GetService<IConfiguration>();
                string connectionString = config.GetConnectionString(AppsettingsKeys.ConnectionString);
                string ownerId = null;
                if (context.HttpContext?.User != null && context.HttpContext.User.Identity.IsAuthenticated)
                {
                    ownerId = ((ClaimsIdentity)context.HttpContext.User?.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
                }
                UnitOfWork uoW = new UnitOfWork(connectionString, ownerId);
                return uoW;
            });

            containerBuilder.Populate(iocConfiguration.Services);

            var container = containerBuilder.Build();
            container.SetInstancesHelper();
            var strategy = container.Resolve<ITenantIdentificationStrategy>();
            var mtc = new MultitenantContainer(strategy, container);

            iocConfiguration.ApplicationContainer = mtc;

            return new AutofacServiceProvider(mtc);
        }
    }
}