using Group.Salto.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace Group.Salto.DataAccess.Tenant.Context
{
    public class DesignTimeSOMTenantDbContextFactory : IDesignTimeDbContextFactory<SOMTenantContext>
    {
        public SOMTenantContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<SOMTenantContext>();
            var connectionString = configuration.GetConnectionString(AppsettingsKeys.ConnectionStringTenant);
            builder.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(SOMTenantContext).GetTypeInfo().Assembly.GetName().Name));
            return new SOMTenantContext(builder.Options);
        }
    }
}
