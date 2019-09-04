using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ServiceGauges
{
    public class ServiceGaugesResultFilterDto 
    {
        public double AverageVisits { get; set; }
        public double MaxVisits { get; set; }
        public Dictionary<double, double> IntervalVisits { get; set; }
        public double AverageOnSiteTime { get; set; }
        public double MaxOnSiteTime { get; set; }
        public Dictionary<double, double> IntervalOnSite { get; set; }
        public double AverageTravelTime { get; set; }
        public double MaxTravelTime { get; set; }
        public Dictionary<double, double> IntervalTravel { get; set; }
        public double AverageKilometers { get; set; }
        public double MaxKilometers { get; set; }
        public Dictionary<double, double> IntervalKilometers { get; set; }
        public double AverageWaitTime { get; set; }
        public double MaxWaitTime { get; set; }
        public Dictionary<double, double> IntervalWaitTime { get; set; }
        public double TotalOts { get; set; }
        public int TotalClosedToday { get; set; }
        public int TotalOpenedToday { get; set; }
        public List<Dictionary<string, int>> Assets { get; set; }
        public IList<ServiceGaugesEconomicDto> GetMonthlyProjectCostList { get; set; }
        public IList<ServiceGaugesEconomicDto> GetMonthlyProjectRevenueList { get; set; }
        public IList<ServiceGaugesEconomicDto> GetMonthlyProjectMarginList { get; set; }
        public double AverageSLAResponse { get; set; }
        public double MaxSLAResponse { get; set; }
        public Dictionary<double, double> IntervalSLAResponse { get; set; }
        public double AverageSLAResolution { get; set; }
        public double MaxSLAResolution { get; set; }
        public Dictionary<double, double> IntervalSLAResolution { get; set; }
        public int ResponseSla { get; set; }  
        public int ResolutionSla { get; set; }
        public int TotalSla { get; set; }
    }
}