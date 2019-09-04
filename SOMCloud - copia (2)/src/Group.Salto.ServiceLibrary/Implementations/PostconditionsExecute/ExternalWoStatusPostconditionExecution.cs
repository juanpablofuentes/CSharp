using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class ExternalWoStatusPostconditionExecution : IExternalWoStatusPostconditionExecution
    {
        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            postconditionExecutionValues.WorkOrder.ExternalWorOrderStatusId = postconditionExecutionValues.Postcondition.ExternalWorOrderStatusId ?? int.MinValue;
            return postconditionExecutionValues.Result;
        }
    }
}
