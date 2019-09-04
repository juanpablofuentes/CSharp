using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.SOM.Web.Models.Query;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class QueryCascadeViewModelExtensions
    {
        public static QueryCascadeDto ToDto(this QueryCascadeViewModel source)
        {
            QueryCascadeDto result = null;
            if (source != null)
            {
                result = new QueryCascadeDto()
                {
                    Text = source.Text,
                    Selected = source.Selected ?? new int?[] { }
                };
            }
            return result;
        }  
    }
}