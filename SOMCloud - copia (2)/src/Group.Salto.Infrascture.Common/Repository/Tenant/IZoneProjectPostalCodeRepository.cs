using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IZoneProjectPostalCodeRepository
    {
        IQueryable<ZoneProjectPostalCode> GetPostalcodesByProjectId(int id);
        ZoneProjectPostalCode DeletePostalCodeOnContext(ZoneProjectPostalCode entity);
        ZoneProjectPostalCode CheckPostalCodeExists(string PostalCode);
    }
}