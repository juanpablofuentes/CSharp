using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Analysis
{
    public interface IWoAnalysisService
    {
        ResultDto<bool> UpdateWoAnalysis(WorkOrders currentWorkOrder);
        ResultDto<bool> AddWoAnalysis(WorkOrders currentWorkOrder);
    }
}
