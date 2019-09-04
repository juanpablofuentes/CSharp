using Group.Salto.ServiceLibrary.Common.Dtos.Contracts;
using Group.Salto.SOM.Web.Models.Contracts;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ContractsFilterViewModelExtensions
    {
        public static ContractsFilterDto ToDto(this ContractsFilterViewModel source)
        {
            ContractsFilterDto result = null;
            if (source != null)
            {
                result = new ContractsFilterDto()
                {
                    Object = source.Object,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}