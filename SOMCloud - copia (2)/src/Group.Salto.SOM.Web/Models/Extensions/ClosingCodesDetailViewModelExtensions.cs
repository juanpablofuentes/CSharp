using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.ClosingCode;
using Group.Salto.SOM.Web.Models.ClosingCode;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ClosingCodesDetailViewModelExtensions
    {
        public static ClosingCodeDetailViewModel ToViewModel(this ClosingCodeDto source)
        {
            ClosingCodeDetailViewModel result = null;
            if (source != null)
            {
                result = new ClosingCodeDetailViewModel()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Childs = source.Childs.ToViewModel()
                };
            }

            return result;
        }

        public static IList<ClosingCodeDetailViewModel> ToViewModel(this IList<ClosingCodeDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }


        public static ClosingCodeDto ToDto(this ClosingCodeDetailViewModel source)
        {
            ClosingCodeDto result = null;
            if (source != null)
            {
                result = new ClosingCodeDto()
                {
                    Name = source.Name,
                    Id = source.IdClonedItem == 0 ? 0 : source.Id,
                    Description = source.Description,
                    Childs = source.Childs.ToDto()
                };
            }

            return result;
        }

        public static IList<ClosingCodeDto> ToDto(this IList<ClosingCodeDetailViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}