using Group.Salto.DataAccess.Context;
using Group.Salto.DataAccess.Tenant.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

namespace Group.Salto.MigrationData
{
    public class ConfigurationDatabases
    {
        public IConfigurationRoot ConfigureSettings()
        {
            Console.WriteLine("ConfigureSettings");
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
            return configuration;
        }

        public SOMContext CreateEFContextInfrastructure(IConfigurationRoot configuration)
        {
            Console.WriteLine("CreateEFContextInfrastructure");
            string connString = configuration.GetConnectionString("DatabaseToInfrastructure");
            var optionsBuilder = new DbContextOptionsBuilder<SOMContext>()
                .UseSqlServer(connString)
                .Options;

            return new SOMContext(optionsBuilder);
        }

        public SOMTenantContext CreateEFContextTenant(IConfigurationRoot configuration)
        {
            Console.WriteLine("CreateEFContextTenant");
            string connString = configuration.GetConnectionString("DatabaseToTenant");
            var optionsBuilder = new DbContextOptionsBuilder<SOMTenantContext>()
                .UseSqlServer(connString)
                .Options;

            return new SOMTenantContext(optionsBuilder);
        }

        public SqlConnection CreateOldSqlConnection(IConfigurationRoot configuration)
        {
            Console.WriteLine("CreateOldSqlConnection");
            string connString = configuration.GetConnectionString("DatabaseFrom");
            return new SqlConnection(connString);
        }

        public SqlConnection CreateTenantSqlConnection(IConfigurationRoot configuration)
        {
            Console.WriteLine("CreateTenantSqlConnection");
            string connString = configuration.GetConnectionString("DatabaseToTenant");
            return new SqlConnection(connString);
        }
    }
}