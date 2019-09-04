using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ServiceRepository : BaseRepository<Services>, IServiceRepository
    {
        public ServiceRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public Services GetServiceByIdIncludeExtraFields(int id)
        {
            return Filter(s => s.Id == id)
                .Include(s => s.ExtraFieldsValues)
                .ThenInclude(e => e.ExtraField).FirstOrDefault();
        }

        public IQueryable<Services> GetServiceWOForms(int Id)
        {
            return Filter(s => s.WorkOrderId == Id)
                .Include(s => s.WorkOrder)
                    .ThenInclude(s => s.WorkOrderStatus)
                .Include(s => s.WorkOrder)
                    .ThenInclude(s => s.WorkOrderAnalysis)
                .Include(s => s.WorkOrder)
                    .ThenInclude(s => s.Bill)
                .Include(s => s.PredefinedService)
                .Include(s => s.PeopleResponsible)
                .Include(s => s.ExtraFieldsValues)
                    .ThenInclude(ef => ef.MaterialForm)
                .Include(s => s.ExtraFieldsValues)
                    .ThenInclude(ef => ef.ExtraField);
        }

        public IQueryable<Services> GetGeneratedServiceWOForms(int Id)
        {
            return Filter(s => s.Id == Id)
                .Include(s => s.PredefinedService)
                .Include(s => s.PeopleResponsible)
                .Include(s => s.ExtraFieldsValues)
                    .ThenInclude(e => e.ExtraField);
        }
    }
}
