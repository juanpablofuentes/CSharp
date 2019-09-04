using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.RepetitionParameter;
using Group.Salto.SOM.Web.Models.RepetitionParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class RepetitionParameterViewModelExtension
    {
        public static RepetitionParameterViewModel ToViewModel(this RepetitionParameterDto source)
        {
            RepetitionParameterViewModel result = null;
            if (source != null)
            {
                result = new RepetitionParameterViewModel()
                {
                    Id = source.Id,
                    Days = source.Days,
                };
            }
            return result;
        }

        public static IList<RepetitionParameterViewModel> ToViewModel(this IList<RepetitionParameterDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
        
        public static RepetitionParameterViewModel ToViewModelText(this RepetitionParameterTextDto source)
        {
            RepetitionParameterViewModel result = null;
            if (source != null)
            {
                result = new RepetitionParameterViewModel()
                {
                    Id = source.Id,
                    Days = source.Days,
                    CalculationType = source.CalculationType,
                    DamagedEquipment = source.DamagedEquipment,
                    DaysType = source.DaysType
                };
            }
            return result;
        }

        public static IList<RepetitionParameterViewModel> ToViewModelText(this IList<RepetitionParameterTextDto> source)
        {
            return source?.MapList(x => x.ToViewModelText());
        }
    }
}