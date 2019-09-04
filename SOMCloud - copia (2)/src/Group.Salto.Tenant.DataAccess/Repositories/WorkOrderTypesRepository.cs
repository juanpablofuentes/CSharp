using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class WorkOrderTypesRepository : BaseRepository<WorkOrderTypes>, IWorkOrderTypesRepository
    {
        public WorkOrderTypesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public WorkOrderTypes GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public WorkOrderTypes GetByIdIncludingFather(int id)
        {
            return Filter(x => x.Id == id)
                .Include(wot => wot.WorkOrderTypesFather)
                .FirstOrDefault();
        }

        public IQueryable<WorkOrderTypes> GetAllByWorkOrderTypesFatherId(int id)
        {
            return Filter(x => x.WorkOrderTypesFatherId == id && x.CollectionsTypesWorkOrdersId != null && !x.IsDeleted);
        }

        public List<WorkOrderTypes> GetAllByWorkOrderTypesByFathersIds(List<int?> IdsToMatch)
        {
            return Filter(y => IdsToMatch.Contains(y.WorkOrderTypesFatherId)).ToList();
        }

        public List<WorkOrderTypes> GetByCollectionsTypesWorkOrdersId(int id)
        {
            return Filter(x => x.CollectionsTypesWorkOrdersId == id && !x.IsDeleted)
                  .OrderBy(x => x.Name)
                  .ToList();
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .Where(x => !x.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<WorkOrderTypes> GetByIds(IList<int> ids)
        {
            return Filter(x => !x.IsDeleted && ids.Contains(x.Id));
        }

        public WorkOrderTypes DeleteOnContext(WorkOrderTypes entity)
        {
            Delete(entity);
            return entity;
        }
    }
}