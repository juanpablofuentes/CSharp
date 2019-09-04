using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ISomFileRepository
    {
        SaveResult<SomFiles> DeleteSomFile(SomFiles entity);
    }
}