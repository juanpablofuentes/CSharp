using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class ParentWoExternalStatusPostconditionExecution : IParentWoExternalStatusPostconditionExecution
    {
        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            if (postconditionExecutionValues.WorkOrder.WorkOrdersFather != null && postconditionExecutionValues.Postcondition.ExternalWorOrderStatusId.HasValue)
            {
                postconditionExecutionValues.WorkOrder.WorkOrdersFather.ExternalWorOrderStatusId = postconditionExecutionValues.Postcondition.ExternalWorOrderStatusId.Value;
            }

            return postconditionExecutionValues.Result;
        }
    }
}
