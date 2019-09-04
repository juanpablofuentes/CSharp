using System;
using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class AssignmentDatePostconditionExecution : IAssignmentDatePostconditionExecution
    {
        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            postconditionExecutionValues.WorkOrder.AssignmentTime = DateTime.UtcNow.AddMinutes(postconditionExecutionValues.Postcondition.EnterValue ?? 0);
            return postconditionExecutionValues.Result;
        }
    }
}
