using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IQueueRepository : IRepository<Queues>
    {
        Queues GetById(int id);
        Queues GetByIdWithWorkOrders(int id);
        SaveResult<Queues> UpdateQueue(Queues entity);
        SaveResult<Queues> CreateQueue(Queues entity);
        SaveResult<Queues> DeleteQueue(Queues entity);
        IQueryable<Queues> GetAllWithIncludeTranslations();
        IQueryable<Queues> GetAllById(IList<int> ids);
        IQueryable<Queues> GetByIds(IList<int> ids);
        IQueryable<Queues> GetAllByFiltersWithPermisions(PermisionsFilterQueryDto filterQuery);
    }
}