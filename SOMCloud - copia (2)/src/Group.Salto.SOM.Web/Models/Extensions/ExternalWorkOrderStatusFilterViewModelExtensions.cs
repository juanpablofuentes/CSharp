using Group.Salto.Common.Helpers;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.ExternalWorkOrderStatus;
using Group.Salto.SOM.Web.Models.ExternalWorkOrderStatus;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExternalWorkOrderStatusFilterViewModelExtensions
    {
        public static ExternalWorkOrderStatusFilterDto ToDto(this ExternalWorkOrderStatusFilterViewModel source)
        {
            ExternalWorkOrderStatusFilterDto result = null;
            if (source != null)
            {
                result = new ExternalWorkOrderStatusFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    LanguageId = source.LanguageId,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result ?? new ExternalWorkOrderStatusFilterDto();
        }

        public static ExternalWorkOrderStatusListViewModel ToViewModel(this ExternalWorkOrderStatusListDto source)
        {
            ExternalWorkOrderStatusListViewModel result = null;
            if (source != null)
            {
                result = new ExternalWorkOrderStatusListViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                };
            }
            return result;
        }

        public static IList<ExternalWorkOrderStatusListViewModel> ToViewModel(this IList<ExternalWorkOrderStatusListDto> source)
        {
            return source?.MapList(wc => wc.ToViewModel());
        }
    }
}