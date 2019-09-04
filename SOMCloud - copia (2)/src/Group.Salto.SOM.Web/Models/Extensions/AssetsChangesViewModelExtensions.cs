using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.AssetsAudit;
using Group.Salto.SOM.Web.Models.AssetsAudit;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class AssetsChangesViewModelExtensions
    {
        public static AssetsChangesViewModel ToViewModel(this AssetsChangesDto source)
        {
            AssetsChangesViewModel result = null;
            if (source != null)
            {
                result = new AssetsChangesViewModel
                {
                    Property = source.Property,
                    OldValue = source.OldValue,
                    NewValue = source.NewValue
                };
            }
            return result;
        }

        public static IList<AssetsChangesViewModel> ToListViewModel(this IList<AssetsChangesDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}