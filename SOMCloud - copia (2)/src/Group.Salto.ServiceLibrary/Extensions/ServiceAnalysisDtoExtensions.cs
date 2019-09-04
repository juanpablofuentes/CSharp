using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.ServiceAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ServiceAnalysisDtoExtensions
    {
        public static ServiceAnalysisDto ToServicesListDto (this ServicesAnalysis source, IQueryable<People> technician)
        {
            ServiceAnalysisDto result = null;
            if(source != null)
            {
                result = new ServiceAnalysisDto()
                {
                    Id = source.ServiceCode,
                    Description = source.ServiceDescription,
                    CreationDate = source.CreationDateTime.ToString(),
                    Technician = technician.Where(x => x.Id == source.Technician).Select(x => x.FullName).FirstOrDefault(),
                    Subcontract = source.SubcontractorName,
                    WorkedTime = source.WorkedTime,
                    TimeOnSite = source.OnSiteTime,
                    WaitTime = source.WaitTime,
                    TravelTime = source.TravelTime,
                    Kilometers = source.Kilometers,
                };
            }
            return result;
        }

        public static IList<ServiceAnalysisDto> ToServicesListDto (this IList<ServicesAnalysis> source, IQueryable<People> technician)
        {
            return source?.MapList(x => x.ToServicesListDto(technician));
        }

        public static ServiceAnalysisDto GetServiceAnalysisTotals(this IList<ServiceAnalysisDto> source)
        {
            ServiceAnalysisDto result = null;
            if (source != null)
            {
                result = new ServiceAnalysisDto()
                {
                    WorkedTime = source.Sum(wt => wt.WorkedTime),
                    TimeOnSite = source.Sum(ost => ost.TimeOnSite),
                    WaitTime = source.Sum(wt => wt.WaitTime),
                    TravelTime = source.Sum(tt => tt.TravelTime),
                    Kilometers = source.Sum(km => km.Kilometers),
                };
            }
            return result;
        }

    }
}