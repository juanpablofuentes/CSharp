using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using Group.Salto.SOM.Web.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class HiredServicesViewModelExtensions
    {
        public static HiredServicesViewModel ToViewModel(this HiredServicesDto source)
        {
            HiredServicesViewModel result = null;
            if (source != null)
            {
                result = new HiredServicesViewModel()
                {
                    HiredServicesId = source.Id,
                    HiredServicesName = source.Name,
                    HiredServicesObservations = source.Observations
                };
            }
            return result;
        }

        public static HiredServicesDto ToDto(this HiredServicesViewModel source)
        {
            HiredServicesDto result = null;
            if (source != null)
            {
                result = new HiredServicesDto()
                {
                    Name = source.HiredServicesName,
                    Observations = source.HiredServicesObservations
                };
            }
            return result;
        }

        public static IList<HiredServicesDto> ToListDto(this IList<HiredServicesViewModel> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}