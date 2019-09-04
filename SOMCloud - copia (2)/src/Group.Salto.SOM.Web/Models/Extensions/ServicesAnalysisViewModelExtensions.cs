using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.ServiceAnalysis;
using Group.Salto.SOM.Web.Models.ServicesAnalysis;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ServicesAnalysisViewModelExtensions
    {
        public static ServiceAnalysisViewModel ToServicesViewModel(this ServiceAnalysisDto source)
        {
            ServiceAnalysisViewModel result = new ServiceAnalysisViewModel();
            if(source != null)
            {
                result.Id = source.Id.ToString();
                result.Description = source.Description;
                result.CreationDate = source.CreationDate;
                result.Technician = source.Technician;
                result.Subcontract = source.Subcontract;
                result.WorkedTime = source.WorkedTime;
                result.TimeOnSite = source.TimeOnSite;
                result.WaitTime = source.WaitTime;
                result.TravelTime = source.TravelTime;
                result.Kilometers = source.Kilometers;
            }
            return result;
        }

        public static IList<ServiceAnalysisViewModel> ToListViewModel(this IList<ServiceAnalysisDto> source)
        {
            return source?.MapList(x => x.ToServicesViewModel());
        }
    }
}