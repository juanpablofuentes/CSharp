using Group.Salto.ServiceLibrary.Common.Dtos.Query;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Query
{
    public interface IQueryFactory
    {
        IQueryResult GetQuery(QueryTypeEnum queryType);
    }
}