using System;
using System.Collections.Generic;
using System.Text;
using Group.Salto.Entities.Tenant;
using DataAccess.Common;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class LocationsFinalClientsRepository : BaseRepository<LocationsFinalClients>, ILocationsFinalClientsRepository
    {
        public LocationsFinalClientsRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public LocationsFinalClients DeleteOnContext(LocationsFinalClients entity)
        {
            Delete(entity);
            return entity;
        }
    }
}