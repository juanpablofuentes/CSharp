using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IModelsRepository
    {
        IQueryable<Models> FilterById(IEnumerable<int> ids);
        Models DeleteOnContextModels(Models entity);
        Models GetById(int value);
        IQueryable<Models> GetAllByFilters(FilterQueryDto filterQuery);
        IQueryable<Models> FilterByBrand(string text, int?[] selected);
    }
}