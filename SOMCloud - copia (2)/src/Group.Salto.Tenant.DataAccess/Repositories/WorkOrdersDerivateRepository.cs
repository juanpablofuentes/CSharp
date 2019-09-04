using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class WorkOrdersDerivateRepository : BaseRepository<WorkOrdersDeritative>, IWorkOrdersDerivateRepository
    {
        public WorkOrdersDerivateRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public WorkOrdersDeritative GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public WorkOrdersDeritative GetEditById(int id)
        {
            return Filter(x => x.Id == id)
                .Include(x => x.Project)
                .Include(x => x.PeopleResponsible)
                .Include(x => x.FinalClient)
                .Include(x => x.Location)
                .Include(x => x.Asset)
                .Include(x => x.SiteUser)
                .FirstOrDefault();
        }

        public IList<WorkOrdersDeritative> GetByTaskId(int id)
        {
            return Filter(x => x.TaskId == id)
                .Include(x => x.Project)
                .Include(x => x.Queue)
                .Include(x => x.PeopleResponsible)
                .Include(x => x.FinalClient)
                .Include(x => x.Location)
                .Include(x => x.Asset)
                .Include(x => x.SiteUser)
                .Include(x => x.WorkOrderStatus)
                .Include(x => x.ExternalWorOrderStatus)
                .Include(x => x.WorkOrderType)
                .Include(x => x.WorkOrderCategory)
                .ToList();
        }

        public SaveResult<WorkOrdersDeritative> CreateWorkOrderDerivated(WorkOrdersDeritative entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<WorkOrdersDeritative> UpdateWorkOrderDerivated(WorkOrdersDeritative entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }
    }
}