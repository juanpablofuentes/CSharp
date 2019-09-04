using System;
using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class ManipulatorPostconditionExecution : IManipulatorPostconditionExecution
    {
        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            if (postconditionExecutionValues.Postcondition.PeopleManipulatorId == int.MinValue)
            {
                postconditionExecutionValues.WorkOrder.PeopleManipulatorId = null;
            }
            else if (postconditionExecutionValues.Postcondition.PeopleManipulatorId == (int.MinValue + 1))
            {
                postconditionExecutionValues.WorkOrder.PeopleManipulatorId = postconditionExecutionValues.WorkOrder.PeopleResponsibleId;
            }
            return postconditionExecutionValues.Result;
        }
    }
}
