using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.SOM.Web.Models.Query;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class QueryTypeParametersViewModelExtensions
    {
        public static QueryTypeParametersDto ToDto(this QueryTypeParametersViewModel source)
        {
            QueryTypeParametersDto result = null;
            if (source != null)
            {
                result = new QueryTypeParametersDto()
                {
                    Value = source.Value,
                    Text = source.Text,
                };
            }
            return result;
        }
    }
}