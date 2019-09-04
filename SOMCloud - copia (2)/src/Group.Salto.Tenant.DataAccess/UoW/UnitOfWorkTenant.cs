using DataAccess.Common;
using DataAccess.Common.Context;
using DataAccess.Common.UoW;
using Group.Salto.DataAccess.Tenant.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Group.Salto.DataAccess.Tenant.UoW
{
    public class UnitOfWorkTenant : UnitOfWorkBase, ITenantUnitOfWork, ICreationDB
    {

        private DbContext _explicitContext;

        public UnitOfWorkTenant(string connectionString, string ownerId = null) : base(ownerId)
        {
            Context = CreateContext(connectionString);
        }

        public bool EnsureCreatedDB()
        {
            return Context.Database.EnsureCreated();
        }

        public void CreateInstance(string connectionString)
        {
            _explicitContext = Context;
            Context = CreateContext(connectionString);
        }

        public void DestroyInstance()
        {
            Context?.Dispose();
            Context = _explicitContext;
        }

        private DbContext CreateContext(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<SOMTenantContext>();
            if (builder == null)
            {
                throw new ArgumentNullException($"Unit Of Work {nameof(DbContextOptionsBuilder)} is null");
            }

            if (!string.IsNullOrEmpty(connectionString))
            {
                builder.UseSqlServer(connectionString);
                DbContext res = default(DbContext);
                res = new SOMTenantContext(builder.Options);
                ((IOwnerIdentifier)res).OwnerId = this.OwnerId;
                return res;
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _explicitContext?.Dispose();
        }
    }
}