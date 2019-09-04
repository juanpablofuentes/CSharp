namespace Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions
{
    public interface IQueryFactory
    {
        ILiteralResult GetQuery(string literalType);
    }
}