using Group.Salto.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace Group.Salto.DataAccess.Context
{
    public class DesignTimeSOMDbContextFactory : IDesignTimeDbContextFactory<SOMContext>
    {
        public SOMContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<SOMContext>();
            var connectionString = configuration.GetConnectionString(AppsettingsKeys.ConnectionString);
            builder.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(SOMContext).GetTypeInfo().Assembly.GetName().Name));
            return new SOMContext(builder.Options);
        }
    }
}
