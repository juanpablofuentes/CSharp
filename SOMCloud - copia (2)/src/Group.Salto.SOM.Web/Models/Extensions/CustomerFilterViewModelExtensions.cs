using Group.Salto.ServiceLibrary.Common.Dtos.Customer;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CustomerFilterViewModelExtensions
    {
        public static CustomerFilterDto ToDto(this CustomerFilterViewModel source)
        {
            CustomerFilterDto result = null;
            if (source != null)
            {
                result = new CustomerFilterDto()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result ?? new CustomerFilterDto();
        }
    }
}
