using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using Group.Salto.SOM.Web.Models.Items;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ItemViewModelExtensions
    {
        public static ItemViewModel ToViewModel(this ItemsListDto source)
        {
            ItemViewModel result = null;
            if (source != null)
            {
                result = new ItemViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ERPReference = source.ERPReference,
                    InternalReference = source.InternalReference,
                    IsBlocked = source.IsBlocked,
                    ItemsTypeId = source.ItemsTypeId,
                    ItemsTypeName = source.ItemsTypeName,
                    SyncErp = source.SyncErp
                };
            }
            return result;
        }

        public static IList<ItemViewModel> ToViewModel(this IList<ItemsListDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}