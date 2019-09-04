using Group.Salto.ServiceLibrary.Common.Dtos.SubContract;
using Group.Salto.SOM.Web.Models.SubContract;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SubContractsFiltersViewModelExtensions
    {
        public static SubContractFilterDto ToDto(this SubContractFilterViewModel source)
        {
            SubContractFilterDto result = null;
            if (source != null)
            {
                result = new SubContractFilterDto()
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