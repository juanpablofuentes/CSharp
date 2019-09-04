using Group.Salto.ServiceLibrary.Common.Dtos.SalesRate;
using Group.Salto.SOM.Web.Models.SalesRate;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SalesRateFilterViewModelExtensions
    {
        public static SalesRateFilterDto ToDto(this SalesRateFilterViewModel source)
        {
            SalesRateFilterDto result = null;
            if (source != null)
            {
                result = new SalesRateFilterDto()
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