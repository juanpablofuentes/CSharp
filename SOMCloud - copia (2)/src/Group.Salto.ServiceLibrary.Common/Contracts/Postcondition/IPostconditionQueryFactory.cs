namespace Group.Salto.ServiceLibrary.Common.Contracts.Postcondition
{
    public interface IPostconditionQueryFactory
    {
        IPostconditionResult GetQuery(string postconditionType);
    }
}