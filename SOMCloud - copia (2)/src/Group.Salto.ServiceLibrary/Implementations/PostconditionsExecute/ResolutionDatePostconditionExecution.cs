using System;
using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class ResolutionDatePostconditionExecution : IResolutionDatePostconditionExecution
    {
        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            if (postconditionExecutionValues.Postcondition.EnterValue == null)
            {
                postconditionExecutionValues.WorkOrder.ResolutionDateSla = null;
            }
            else
            {
                postconditionExecutionValues.WorkOrder.ResolutionDateSla = DateTime.UtcNow.AddMinutes(postconditionExecutionValues.Postcondition.EnterValue ?? 0);
            }
            return postconditionExecutionValues.Result;
        }
    }
}
