using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ZoneProjectPostalCodeRepository : BaseRepository<ZoneProjectPostalCode>, IZoneProjectPostalCodeRepository
    {
        public ZoneProjectPostalCodeRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }
 
        public IQueryable<ZoneProjectPostalCode> GetPostalcodesByProjectId(int id)
        {
            return Filter(x => x.ZoneProjectId == id);   
        }

        public ZoneProjectPostalCode DeletePostalCodeOnContext(ZoneProjectPostalCode entity)
        {
            Delete(entity);
            return entity;
        }

        public ZoneProjectPostalCode CheckPostalCodeExists(string PostalCode)
        {
            return Filter(x => x.PostalCode == PostalCode).FirstOrDefault();
        }
    }
}