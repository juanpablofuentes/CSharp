using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IItemsRepository : IRepository<Items>
    {
        IQueryable<Items> GetAll();   
        SaveResult<Items> CreateItem(Items Item);
        SaveResult<Items> UpdateItem(Items Item);
        Items GetById(int Id);
        Items GetByIdCanDelete(int Id);
        Items GetByIdIncludeReferencesToDelete(int Id);
        SaveResult<Items> DeleteItem(Items Item);
        Items GetByErpReference(string materialFormReference);
        IQueryable<Items> GetAllByFilters(FilterQueryDto filterQuery);
    }
}