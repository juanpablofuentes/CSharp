using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Company;
using Group.Salto.SOM.Web.Models.Company;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CompanyViewModelExtensions
    {
        public static CompanyViewModel ToViewModel(this CompanyDto source)
        {
            CompanyViewModel result = null;
            if (source != null)
            {
                result = new CompanyViewModel();
                source.ToViewModel(result);
            }
            return result;
        }

        public static IList<CompanyViewModel> ToViewModel(this IList<CompanyDto> source)
        {
            return source?.MapList(c => c.ToViewModel());
        }

        public static void ToViewModel(this CompanyDto source, CompanyViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.CostKm = ((decimal?)source.CostKm)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName);
                target.Name = source.Name;
                target.HasPeopleAssigned = source.HasPeopleAssigned;
            }
        }

        public static void ToDto(this CompanyViewModel source, CompanyDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.CostKm = (double?)source.CostKm?.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName);
                target.Name = source.Name;
                target.HasPeopleAssigned = source.HasPeopleAssigned;
            }
        }
    }
}
