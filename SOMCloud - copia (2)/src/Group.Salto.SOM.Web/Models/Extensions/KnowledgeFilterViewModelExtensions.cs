using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Knowledge;
using Group.Salto.SOM.Web.Models.Knowledge;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class KnowledgeFilterViewModelExtensions
    {
        public static KnowledgeFilterDto ToDto(this KnowledgesFilterViewModel source)
        {
            KnowledgeFilterDto result = null;
            if (source != null)
            {
                result = new KnowledgeFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}
