using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class WorkOrderAnalysisRepository : BaseRepository<WorkOrderAnalysis>, IWorkOrderAnalysisRepository
    {
        public WorkOrderAnalysisRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public void DeleteWorkOrderAnalysisWithoutSaving(WorkOrderAnalysis entity)
        {
            Delete(entity);
        }

        public WorkOrderAnalysis GetWOAnalisys(int Id)
        {
            return Filter(x => x.WorkOrderCode == Id).FirstOrDefault();
        }
    }
}
