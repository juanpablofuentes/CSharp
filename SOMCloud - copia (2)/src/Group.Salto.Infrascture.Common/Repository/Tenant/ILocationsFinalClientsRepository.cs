using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public interface ILocationsFinalClientsRepository
    {
        LocationsFinalClients DeleteOnContext(LocationsFinalClients entity);
    }
}