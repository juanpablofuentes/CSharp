using Group.Salto.SOM.Web.Models.ServiceGauges;
using Group.Salto.ServiceLibrary.Common.Dtos.ServiceGauges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ServiceGaugesResultFilterViewModelIExtencion
    {
        public static ServiceGaugesResultFilterDto ToDto(this ServiceGaugesResultFilterViewModel source)
        {
            ServiceGaugesResultFilterDto result = null;
            if (source != null)
            {
                result = new ServiceGaugesResultFilterDto()
                {
                    AverageKilometers = source.AverageKilometers,
                    MaxKilometers = source.MaxKilometers,
                    AverageVisits = source.AverageVisits,
                    MaxVisits = source.MaxVisits,
                    AverageOnSiteTime = source.AverageOnSiteTime,
                    MaxOnSiteTime = source.MaxOnSiteTime,
                    AverageWaitTime = source.AverageWaitTime,
                    MaxWaitTime = source.MaxWaitTime,
                    TotalOts = source.TotalOts,
                    IntervalKilometers = source.IntervalKilometers,
                    IntervalOnSite = source.IntervalOnSite,
                    IntervalTravel = source.IntervalTravel,
                    IntervalVisits = source.IntervalVisits,
                    IntervalWaitTime = source.IntervalWaitTime,
                    TotalClosedToday = source.TotalClosedToday,
                    TotalOpenedToday = source.TotalClosedToday,
                    Assets = source.Assets,
                    GetMonthlyProjectCostList = source.GetMonthlyProjectCostList.ToListDto(),
                    GetMonthlyProjectMarginList = source.GetMonthlyProjectMarginList.ToListDto(),
                    GetMonthlyProjectRevenueList = source.GetMonthlyProjectRevenueList.ToListDto(),
                    AverageSLAResolution = source.AverageSLAResolution,
                    MaxSLAResolution = source.MaxSLAResolution,
                    IntervalSLAResolution = source.IntervalSLAResolution,
                    AverageSLAResponse = source.AverageSLAResponse,
                    MaxSLAResponse = source.MaxSLAResponse,
                    IntervalSLAResponse = source.IntervalSLAResponse,
                    ResolutionSla = source.ResolutionSla,
                    ResponseSla = source.ResponseSla,
                    TotalSla = source.TotalSla
                };
            }
            return result;
        }

        public static ServiceGaugesResultFilterViewModel ToViewModel(this ServiceGaugesResultFilterDto source)
        {
            ServiceGaugesResultFilterViewModel result = null;
            if (source != null)
            {
                result = new ServiceGaugesResultFilterViewModel()
                {
                    AverageKilometers = source.AverageKilometers,
                    MaxKilometers = source.MaxKilometers,
                    AverageVisits = source.AverageVisits,
                    MaxVisits = source.MaxVisits,
                    AverageOnSiteTime = source.AverageOnSiteTime,
                    MaxOnSiteTime = source.MaxOnSiteTime,
                    AverageWaitTime = source.AverageWaitTime,
                    MaxWaitTime = source.MaxWaitTime,
                    TotalOts = source.TotalOts,
                    IntervalKilometers = source.IntervalKilometers,
                    IntervalOnSite = source.IntervalOnSite,
                    IntervalTravel = source.IntervalTravel,
                    IntervalVisits = source.IntervalVisits,
                    IntervalWaitTime = source.IntervalWaitTime,
                    TotalClosedToday = source.TotalClosedToday,
                    TotalOpenedToday = source.TotalClosedToday,
                    Assets = source.Assets,
                    GetMonthlyProjectCostList = source.GetMonthlyProjectCostList.ToListViewModel(),
                    GetMonthlyProjectMarginList = source.GetMonthlyProjectMarginList.ToListViewModel(),
                    GetMonthlyProjectRevenueList = source.GetMonthlyProjectRevenueList.ToListViewModel(),
                    AverageSLAResolution = source.AverageSLAResolution,
                    MaxSLAResolution = source.MaxSLAResolution,
                    IntervalSLAResolution = source.IntervalSLAResolution,
                    AverageSLAResponse = source.AverageSLAResponse,
                    MaxSLAResponse = source.MaxSLAResponse,
                    IntervalSLAResponse = source.IntervalSLAResponse,
                    ResolutionSla = source.ResolutionSla,
                    ResponseSla = source.ResponseSla,
                    TotalSla = source.TotalSla

                };
            }
            return result;
        }
    }
}