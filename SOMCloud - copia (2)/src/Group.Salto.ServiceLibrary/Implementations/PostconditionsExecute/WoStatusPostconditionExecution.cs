using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class WoStatusPostconditionExecution : IWoStatusPostconditionExecution
    {
        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            postconditionExecutionValues.WorkOrder.WorkOrderStatusId = postconditionExecutionValues.Postcondition.WorkOrderStatusId ?? int.MinValue;
            return postconditionExecutionValues.Result;
        }
    }
}
