using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ISubFamiliesRepository
    {
        IQueryable<SubFamilies> GetAllByFilters(FilterQueryDto filter);
        SubFamilies GetById(int id);
        SubFamilies DeleteOnContextSubFamilie(SubFamilies entity);
        IQueryable<SubFamilies> FilterByClientSite(string filter, int?[] parents);
    }
}