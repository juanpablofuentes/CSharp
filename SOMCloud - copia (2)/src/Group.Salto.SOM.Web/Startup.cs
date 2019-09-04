using Autofac.Multitenant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Group.Salto.Common;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Filters;
using Microsoft.AspNetCore.Http.Features;

namespace Group.Salto.SOM.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // public IServiceProvider ApplicationContainer { get; private set; }
        // This is what the middleware will use to create your request lifetime scope.
        public static MultitenantContainer ApplicationContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var configuration = new IOCConfiguration()
            {
                Services = services,
                Configuration = Configuration,
            };

            // Add SOM identity configuration
            services.AddSOMIdentity(configuration.Configuration);

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);          

            var provider = ContainerIoC.Create(configuration);
            ApplicationContainer = configuration.ApplicationContainer;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLogConfiguration(Configuration.GetSection(AppsettingsKeys.Log));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
               // routes.Routes.Add(new MultitenancyRouting(routes.DefaultHandler));
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");

                //routes.MapRoute(
                //    name: "tenantRouted",
                //    template: "{tenantId?}/{controller=account}/{action=login}/{id?}"
                //    );
                //routes.MapRoute(
                //    name: "api",
                //    template: "api/{controller}/{id?}");

            });
            app.UseCookiePolicy();
        }

    }
}