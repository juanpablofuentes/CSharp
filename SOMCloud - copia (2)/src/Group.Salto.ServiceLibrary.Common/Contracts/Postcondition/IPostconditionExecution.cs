using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Postconditions;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Postcondition
{
    public interface IPostconditionExecution
    {
        ResultDto<bool> PerformPostcondition(PostconditionExecutionValues postconditionExecutionValues);
    }
}
