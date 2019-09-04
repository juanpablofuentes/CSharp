using System;
using Group.Salto.ServiceLibrary.Common.Contracts.PostconditionExecute;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionsExecute
{
    public class ObservationsPostconditionExecution : IObservationsPostconditionExecution
    {
        public ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues)
        {
            postconditionExecutionValues.WorkOrder.Observations += $"{Environment.NewLine}{postconditionExecutionValues.Postcondition.StringValue}";
            return postconditionExecutionValues.Result;
        }
    }
}
