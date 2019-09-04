using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClients;
using Group.Salto.SOM.Web.Models.FinalClients;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class FinalClientsViewModelExtensions
    {
        public static FinalClientsViewModel ToViewModel(this FinalClientsListDto source)
        {
            FinalClientsViewModel result = null;
            if (source != null)
            {
                result = new FinalClientsViewModel();
                source.ToViewModel(result);
            }
            return result;
        }

        public static IList<FinalClientsViewModel> ToViewModel(this IList<FinalClientsListDto> source)
        {
            return source?.MapList(c => c.ToViewModel());
        }

        public static void ToViewModel(this FinalClientsListDto source, FinalClientsViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Nif = source.Nif;
                target.Description = source.Description;
                target.Observations = source.Observations;
                target.Phone1 = source.Phone1;
                target.Fax = source.Fax;
            }
        }

        public static void ToDto(this FinalClientsViewModel source, FinalClientsListDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Nif = source.Nif;
                target.Description = source.Description;
                target.Observations = source.Observations;
                target.Phone1 = source.Phone1;
                target.Fax = source.Fax;
            }
        }
    }
}