using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;
using System;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class ActuationEndDatePostconditionExecution : IActuationEndDatePostconditionExecution
    {
        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            postconditionExecutionValues.WorkOrder.ActuationEndDate = DateTime.UtcNow.AddMinutes(postconditionExecutionValues.Postcondition.EnterValue ?? 0);
            return postconditionExecutionValues.Result;
        }
    }
}
