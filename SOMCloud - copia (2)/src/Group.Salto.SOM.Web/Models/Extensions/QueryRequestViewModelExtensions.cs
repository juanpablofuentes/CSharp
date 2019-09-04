using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.SOM.Web.Models.Query;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class QueryRequestViewModelExtensions
    {
        public static QueryRequestDto ToDto(this QueryRequestViewModel source)
        {
            QueryRequestDto result = null;
            if (source != null)
            {
                result = new QueryRequestDto()
                {
                    QueryType = source.QueryType,
                    QueryTypeParameters = source.QueryTypeParameters.ToDto()
                };
            }
            return result;
        }        
    }
}