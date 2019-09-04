using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.ServiceGauges;
using Group.Salto.SOM.Web.Models.ServiceGauges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ServiceGaugesEconomicIExtensions
    {
        public static ServiceGaugesEconomicDto ToDto(this ServiceGaugesEconomicViewModel source)
        {
            ServiceGaugesEconomicDto result = null;
            if (source != null)
            {
                result = new ServiceGaugesEconomicDto()
                {
                    Data = source.Data,
                    Name = source.Name,
                };
            }
            return result;
        }
        public static IList<ServiceGaugesEconomicDto> ToListDto(this IList<ServiceGaugesEconomicViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }
        public static ServiceGaugesEconomicViewModel ToViewModel(this ServiceGaugesEconomicDto source)
        {
            ServiceGaugesEconomicViewModel result = null;
            if (source != null)
            {
                result = new ServiceGaugesEconomicViewModel()
                {
                    Data = source.Data,
                    Name = source.Name,
                };
            }
            return result;
        }
        public static IList<ServiceGaugesEconomicViewModel> ToListViewModel(this IList<ServiceGaugesEconomicDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}
