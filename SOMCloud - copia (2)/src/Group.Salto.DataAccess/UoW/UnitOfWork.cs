using DataAccess.Common;
using DataAccess.Common.Context;
using Group.Salto.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Group.Salto.DataAccess.UoW
{
    public class UnitOfWork: UnitOfWorkBase, IUnitOfWork
    {
        public UnitOfWork(string connectionString, string ownerId = null) : base(ownerId)
        {
            var builder = new DbContextOptionsBuilder<SOMContext>();
            if (builder == null)
            {
                throw new ArgumentNullException($"Unit Of Work {nameof(DbContextOptionsBuilder)} is null");
            }
            builder.UseSqlServer(connectionString);
            if (Context == null)
            {
                Context = new SOMContext(builder.Options);
                ((IOwnerIdentifier)Context).OwnerId = this.OwnerId;
            }
        }

    }
}