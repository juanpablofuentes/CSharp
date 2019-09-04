using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Zones;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ZoneViewModelExtensions
    {
        public static ResultViewModel<ZoneViewModel> ToViewModel(this ResultDto<ZonesDto> source)
        {
            var response = source != null ? new ResultViewModel<ZoneViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ZoneViewModel ToViewModel(this ZonesDto source)
        {
            ZoneViewModel result = null;
            if (source != null)
            {
                result = new ZoneViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    ZonesProjects = source.ZoneProject?.ToViewModel()
                };
            }
            return result;
        }

        public static IList<ZoneViewModel> ToViewModel(this IList<ZonesDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static ZonesDto ToDto(this ZoneViewModel source)
        {
            ZonesDto result = null;
            if (source != null)
            {
                result = new ZonesDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    ZoneProject = source.ZonesProjects.ToListDto()
                };
            }
            return result;
        }
    }
}