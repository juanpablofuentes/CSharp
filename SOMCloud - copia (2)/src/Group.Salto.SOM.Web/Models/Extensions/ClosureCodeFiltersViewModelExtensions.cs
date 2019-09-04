using Group.Salto.ServiceLibrary.Common.Dtos.ClosureCode;
using Group.Salto.SOM.Web.Models.ClosureCode;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ClosureCodeFiltersViewModelExtensions
    {
        public static ClosureCodeFilterDto ToDto(this ClosureCodeFilterViewModel source)
        {
            ClosureCodeFilterDto result = null;
            if (source != null)
            {
                result = new ClosureCodeFilterDto()
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