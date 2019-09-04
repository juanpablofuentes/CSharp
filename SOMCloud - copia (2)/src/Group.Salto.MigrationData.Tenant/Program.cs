using Dapper;
using Group.Salto.DataAccess.Tenant.Context;
using Group.Salto.Entities.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Group.Salto.MigrationData.Tenant
{
    class Program
    {
        private static IConfigurationRoot Configuration;
        private static SOMTenantContext ContextTenant;
        private static SqlConnection SqlConnectionOldDatabase;

        static void Main(string[] args)
        {
            ConfigureSettings();
            CreateSqlConnection();
            CreateEFContextTenant();

            Type[] asmTypes = GetTypeEntitiesFromAssembly();
            var valuesList = ReadQueriesFromCsv();

            foreach (KeyValuePair<string, string> val in valuesList)
            {
                var type = asmTypes.First(t => t.Name.Equals(val.Key, StringComparison.InvariantCultureIgnoreCase));
                if (type != null)
                {
                    Migrate(type, val);
                }
                else Console.WriteLine($"The table {val.Key} doesn't exist in the entities");
            }
        }

        private static void ConfigureSettings()
        {
            Configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
        }

        private static void CreateEFContextTenant()
        {
            var connString = Configuration.GetConnectionString("DatabaseToTenant");
            var optionsBuilder = new DbContextOptionsBuilder<SOMTenantContext>()
                .UseSqlServer(connString)
                .Options;

            ContextTenant = new SOMTenantContext(optionsBuilder);
        }

        private static void CreateSqlConnection()
        {
            var connString = Configuration.GetConnectionString("DatabaseFrom");
            SqlConnectionOldDatabase = new SqlConnection(connString);
        }

        private static Type[] GetTypeEntitiesFromAssembly()
        {
            Assembly asm = Assembly.Load("Group.Salto.Entities.Tenant");
            Type[] asmTypes = asm.GetTypes();
            return asmTypes;
        }

        private static Dictionary<string, string> ReadQueriesFromCsv()
        {
            var reader = new StreamReader(Configuration.GetSection("CsvPath").Value);
            var valuesList = new Dictionary<string, string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                if (values.Any() && !string.IsNullOrEmpty(values[1]))
                {
                    valuesList.Add(values[0], values[1]);
                }
            }
            return valuesList;
        }

        private static void Migrate(Type type, KeyValuePair<string, string> value)
        {
            var dataList = GetDataFromOldDatabase(type, value.Value);
            if (dataList.Any())
            {
                if (value.Key.Equals("People"))
                {
                      
                }
                using (var transaction = ContextTenant.Database.BeginTransaction())
                {
                    ContextTenant.Database.ExecuteSqlCommand((string)$"SET IDENTITY_INSERT {value.Key} ON");

                    ContextTenant.AddRange(dataList);
                    ContextTenant.SaveChanges();

                    ContextTenant.Database.ExecuteSqlCommand((string)$"SET IDENTITY_INSERT {value.Key} OFF");
                    transaction.Commit();
                }
            }
            else Console.WriteLine($"No results data from table {value.Key}");
        }

        private static IEnumerable<object> GetDataFromOldDatabase(Type type, string sql)
        {
            var results = SqlConnectionOldDatabase.Query(type, sql);
            return results;
        }
    }
}
