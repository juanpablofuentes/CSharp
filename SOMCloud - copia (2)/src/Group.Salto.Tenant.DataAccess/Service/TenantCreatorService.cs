using Group.Salto.Common.Entities.Settings;
using Group.Salto.DataAccess.Tenant.UoW;
using Group.Salto.Entities.Tenant.ResultEntities;
using Group.Salto.Infrastructure.Common.Service;
using System;

namespace Group.Salto.DataAccess.Tenant.Service
{
    public class TenantCreatorService : ITenantCreatorService
    {
        private readonly TenantConfiguration _configuration;

        public TenantCreatorService(TenantConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(TenantConfiguration));
        }

        public DatabaseCreation Create(string name)
        {
            bool isCreated;
            
            var dbName = GetDatabaseName(name);
            var connectionString = GetConnectionString(dbName, _configuration);
            
            using (var uow = new UnitOfWorkTenant(connectionString))
            {
                try
                {
                    isCreated = uow.EnsureCreatedDB();
                }
                catch (Exception)
                {
                    isCreated = false;
                }
            }
            return isCreated ? new DatabaseCreation()
            {
                DatabaseName = dbName,
                ConnectionString = connectionString
            } : null;
        }

        private string GetConnectionString(string name, TenantConfiguration configuration)
        {
            var connectionString = string.Format(configuration.ConnectionStringFormat,
                                                    configuration.Hosting,
                                                    configuration.Port,
                                                    name,
                                                    configuration.User,
                                                    configuration.Password,
                                                    configuration.Timeout);
            return connectionString;
        }

        public string GetDatabaseName(string name)
        {
            return $"{_configuration.TenantPrefix}{name}";
        }
    }

}
