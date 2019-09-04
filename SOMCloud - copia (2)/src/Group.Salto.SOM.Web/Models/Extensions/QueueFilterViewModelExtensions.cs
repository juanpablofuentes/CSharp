using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Queue;
using Group.Salto.SOM.Web.Models.Queue;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class QueueFilterViewModelExtensions
    {
        public static QueueFilterDto ToDto(this QueueFilterViewModel source)
        {
            QueueFilterDto result = null;
            if (source != null)
            {
                result = new QueueFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    LanguageId = source.LanguageId,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result ?? new QueueFilterDto();
        }

        public static QueueListViewModel ToViewModel(this QueueListDto source)
        {
            QueueListViewModel result = null;
            if (source != null)
            {
                result = new QueueListViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                };
            }
            return result;
        }

        public static IList<QueueListViewModel> ToViewModel(this IList<QueueListDto> source)
        {
            return source?.MapList(wc => wc.ToViewModel());
        }
    }
}