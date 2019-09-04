using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using Group.Salto.SOM.Web.Models.Items;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ItemsFilterViewModelExtensions
    {
        public static ItemsFilterDto ToDto(this ItemsFilterViewModel source)
        {
            ItemsFilterDto result = null;
            if (source != null)
            {
                result = new ItemsFilterDto()
                {
                    Name = source.Name,
                    Blocked = source.Blocked,
                    ERPReference = source.ERPReference,
                    InternalReference = source.InternalReference,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}