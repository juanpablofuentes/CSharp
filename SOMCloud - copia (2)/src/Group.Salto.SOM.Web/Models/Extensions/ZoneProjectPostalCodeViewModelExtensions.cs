using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProjectPostalCode;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.ZoneProjectPostalCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ZoneProjectPostalCodeViewModelExtensions
    {
        public static ResultViewModel<ZoneProjectPostalCodeViewModel> ToViewModel(this ResultDto<ZoneProjectPostalCodeDto> source)
        {
            var response = source != null ? new ResultViewModel<ZoneProjectPostalCodeViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ZoneProjectPostalCodeViewModel ToViewModel(this ZoneProjectPostalCodeDto source)
        {
            ZoneProjectPostalCodeViewModel result = null;
            if (source != null)
            {
                result = new ZoneProjectPostalCodeViewModel()
                {
                    PostalCodeId = source.PostalCodeId,
                    PostalCode = source.PostalCode,
                    State = source.State,
                    ZoneProjectId = source.ZoneProjectId,
                    ZoneProject = source.ZoneProject.ToViewModel()
                };
            }
            return result;
        }

        public static IList<ZoneProjectPostalCodeViewModel> ToViewModel(this IList<ZoneProjectPostalCodeDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static ZoneProjectPostalCodeDto ToDto(this ZoneProjectPostalCodeViewModel source)
        {
            ZoneProjectPostalCodeDto result = null;
            if (source != null)
            {
                result = new ZoneProjectPostalCodeDto()
                {
                    PostalCodeId = source.PostalCodeId,
                    ZoneProjectId = source.ZoneProjectId,
                    PostalCode = source.PostalCode,
                    State = source.State
                    
                };
            }
            return result;
        }

        public static IList<ZoneProjectPostalCodeDto> ToListDto(this IList<ZoneProjectPostalCodeViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}
