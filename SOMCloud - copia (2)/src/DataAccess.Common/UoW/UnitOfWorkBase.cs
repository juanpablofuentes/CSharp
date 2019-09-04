using DataAccess.Common.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Common
{
    public class UnitOfWorkBase : IDisposable, IOwnerIdentifier
    {
        public string OwnerId { get; set; }

        public UnitOfWorkBase()
        {

        }

        public UnitOfWorkBase(string ownerId = null)
        {
            OwnerId = ownerId;
        }

        private bool disposed = false;

        public DbContext Context { get; set; }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context?.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
