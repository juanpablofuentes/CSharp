using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWorkOrderAnalysisRepository : IRepository<WorkOrderAnalysis>
    {
        void DeleteWorkOrderAnalysisWithoutSaving(WorkOrderAnalysis entity);
        WorkOrderAnalysis GetWOAnalisys(int Id);
    }
}
