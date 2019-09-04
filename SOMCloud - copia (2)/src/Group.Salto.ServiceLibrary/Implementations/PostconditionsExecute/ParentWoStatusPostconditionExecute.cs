using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class ParentWoStatusPostconditionExecute : IParentWoStatusPostconditionExecute
    {
        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            if (postconditionExecutionValues.WorkOrder.WorkOrdersFather != null && postconditionExecutionValues.Postcondition.WorkOrderStatusId.HasValue)
            {
                postconditionExecutionValues.WorkOrder.WorkOrdersFather.WorkOrderStatusId = postconditionExecutionValues.Postcondition.WorkOrderStatusId.Value;
            }

            return postconditionExecutionValues.Result;
        }
    }
}
