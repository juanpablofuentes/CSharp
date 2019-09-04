using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCost;
using Group.Salto.SOM.Web.Models.People;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PeopleCostDetailViewModelExtensions
    {
        public static PeopleCostEditViewModel ToPeopleCostEditViewModel(this PeopleCostDetailDto source)
        {
            PeopleCostEditViewModel result = null;
            if (source != null)
            {
                result = new PeopleCostEditViewModel()
                {
                    CostId = source.Id,
                    PeopleId = source.PeopleId,
                    HourCost = (source.HourCost)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    StartDate = source.StartDate,
                    EndDate = source.EndDate
                };
            }
            return result;
        }

        public static IList<PeopleCostEditViewModel> ToPeopleCostEditViewModel(this IList<PeopleCostDetailDto> source)
        {
            return source?.MapList(pc => pc.ToPeopleCostEditViewModel());
        }

        public static PeopleCostDetailDto ToPeopleCostDto(this PeopleCostEditViewModel source)
        {
            PeopleCostDetailDto result = null;
            if (source != null)
            {
                result = new PeopleCostDetailDto()
                {
                    Id = source.CostId,
                    PeopleId = source.PeopleId,
                    HourCost = source.HourCost.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    StartDate =  source.StartDate,
                    EndDate = source.EndDate
                };
            }
            return result;
        }

        public static IList<PeopleCostDetailDto> ToPeopleCostDto(this IList<PeopleCostEditViewModel> source)
        {
            return source?.MapList(pc => pc.ToPeopleCostDto());
        }
    }
}