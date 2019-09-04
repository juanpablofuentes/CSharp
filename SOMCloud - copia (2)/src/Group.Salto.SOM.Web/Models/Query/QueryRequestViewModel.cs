using Group.Salto.ServiceLibrary.Common.Dtos.Query;

namespace Group.Salto.SOM.Web.Models.Query
{
    public class QueryRequestViewModel
    {
        public QueryTypeEnum QueryType { get; set; }
        public QueryTypeParametersViewModel QueryTypeParameters { get; set; }
    }
}