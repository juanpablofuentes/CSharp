using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.SubContract;
using Group.Salto.SOM.Web.Models.SubContract;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SubContractsViewModelExtensions
    {
        public static IList<SubContractViewModel> ToViewModel(this IList<SubContractBaseDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static SubContractViewModel ToViewModel(this SubContractBaseDto source)
        {
            SubContractViewModel result = null;
            if (source != null)
            {
                result = new SubContractViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this SubContractBaseDto source, SubContractViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
                target.Observations = source.Observations;
                target.Priority = source.Priority;
            }
        }

        public static void ToDto(this SubContractViewModel source, SubContractBaseDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
                target.Observations = source.Observations;
                target.Priority = source.Priority;
            }
        }
    }
}