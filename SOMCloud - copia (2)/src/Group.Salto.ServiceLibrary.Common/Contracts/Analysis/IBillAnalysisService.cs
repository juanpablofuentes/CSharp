using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Analysis
{
    public interface IBillAnalysisService
    {
        ResultDto<bool> AddAllBillsToAnalize(WorkOrders currentWorkOrder);
    }
}
