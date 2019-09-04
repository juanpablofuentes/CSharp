using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IToolsRepository
    {
        IQueryable<Tools> GetAll();
        Tools GetById(int id);
        SaveResult<Tools> CreateTools(Tools tools);
        SaveResult<Tools> UpdateTools(Tools tools);
        SaveResult<Tools> DeleteTools(Tools entity);
    }
}
