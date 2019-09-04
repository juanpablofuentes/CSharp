using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Postcondition
{
    public interface IPostconditionFactory
    {
        IPostconditionExecution GetPostconditionExecution(PostconditionActionTypeEnum postconditionType);
    }
}
