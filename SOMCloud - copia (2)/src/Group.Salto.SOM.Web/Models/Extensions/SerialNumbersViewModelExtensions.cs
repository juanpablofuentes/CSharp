using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using Group.Salto.SOM.Web.Models.Items;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SerialNumbersViewModelExtensions
    {
        public static IList<SerialNumbersViewModel> ToListViewModel(this IList<ItemsSerialNumberDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static SerialNumbersViewModel ToViewModel(this ItemsSerialNumberDto source) 
        {
            SerialNumbersViewModel result = null;
            if (source != null)
            {
                result = new SerialNumbersViewModel()
                {
                    SerialNumberId = source.Id,
                    SerialNumberSerialNumber = source.SerialNumber,
                    SerialNumberObservations = source.Observations,
                    SerialNumberStatusId = source.SerialNumberStatusId ?? 0
                };
            }
            return result;       
        }

        public static IList<ItemsSerialNumberDto> ToListDto(this IList<SerialNumbersViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }

        public static ItemsSerialNumberDto ToDto(this SerialNumbersViewModel source) 
        {
            ItemsSerialNumberDto result = null;
            if (source != null)
            {
                result = new ItemsSerialNumberDto()
                {
                    Id = source.SerialNumberId,
                    SerialNumber = source.SerialNumberSerialNumber,
                    Observations = source.SerialNumberObservations,
                    SerialNumberStatusId = source.SerialNumberStatusId
                };
            }
            return result;       
        }
    }
}