using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.PredefinedServices;
using Group.Salto.SOM.Web.Models.PredefinedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PredefinedServicesEditViewModelExtensions
    {
        public static PredefinedServicesEditViewModel ToPredefinedServicesEditViewModel(this PredefinedServicesDto source)
        {
            PredefinedServicesEditViewModel result = null;
            if (source != null)
            {
                result = new PredefinedServicesEditViewModel()
                {
                    PredefinedServicesId = source.Id,
                    PredefinedServicesName = source.Name,
                    PredefinedServicesLinkClosingCode = source.LinkClosingCode,
                    PredefinedServicesBillable = source.Billable,
                    PredefinedServicesMustValidate = source.MustValidate,
                    CollectionExtraFieldId = source.ExtraFieldCollectionId.HasValue ? source.ExtraFieldCollectionId.Value : 0,
                    CollectionExtraFieldName = source.ExtraFieldCollectionName ?? string.Empty,
                    PredefinedServicesPermissionsString = source.PermissionsString ?? string.Empty,
                    PredefinedServicesPermissionsIds = PermissionsIdsToString(source.PermissionsIds)
                };
                if (result.PredefinedServicesPermissionsString.Length > 40)
                {
                    result.PredefinedServicesPermissionsString = result.PredefinedServicesPermissionsString.Substring(0, 40);
                    result.PredefinedServicesPermissionsString += "...";
                }
            }
            return result;
        }

        private static string PermissionsIdsToString(List<int> ids)
        {
            StringBuilder result = new StringBuilder();
            foreach(int id in ids)
            {
                result.Append(id);
                result.Append(",");
            }
            if (result.Length > 1)
            { 
                result.Remove(result.Length - 1, 1);
            }
            return result.ToString();
        }

        public static IList<PredefinedServicesEditViewModel> ToPredefinedServicesEditViewModel(this IList<PredefinedServicesDto> source)
        {
            return source?.MapList(pk => pk.ToPredefinedServicesEditViewModel());
        }

        public static PredefinedServicesDto ToPredefinedServicesDto(this PredefinedServicesEditViewModel source)
        {
            PredefinedServicesDto result = null;
            if (source != null)
            {
                result = new PredefinedServicesDto()
                {
                    Id = source.PredefinedServicesId,
                    Name = source.PredefinedServicesName,
                    LinkClosingCode = source.PredefinedServicesLinkClosingCode,
                    Billable = source.PredefinedServicesBillable,
                    MustValidate = source.PredefinedServicesMustValidate,
                    ExtraFieldCollectionId = source.CollectionExtraFieldId,
                    ExtraFieldCollectionName = source.CollectionExtraFieldName,
                    PermissionsIds = PredefinedServicesPermissionsIdsToList(source.PredefinedServicesPermissionsIds),
                    PermissionsString = source.PredefinedServicesPermissionsString,
                    State = (source.State != "null") ? source.State : string.Empty
                };
            }
            return result;
        }

        public static IList<PredefinedServicesDto> ToPredefinedServicesDto(this IList<PredefinedServicesEditViewModel> source)
        {
            return source?.MapList(ps => ps.ToPredefinedServicesDto());
        }

        public static List<int> PredefinedServicesPermissionsIdsToList(string predefinedServicesPermissionsIds)
        {
            List<int> result = new List<int>();
            if (predefinedServicesPermissionsIds != null)
            {
                foreach (string id in predefinedServicesPermissionsIds.Split(","))
                {
                    result.Add(Convert.ToInt32(id));
                }
            }
            return result;
        }
    }
}