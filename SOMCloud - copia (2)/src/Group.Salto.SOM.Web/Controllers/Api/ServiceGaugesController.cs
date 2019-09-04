using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ServiceGauges;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.ServiceGauges;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class ServiceGaugesController : BaseAPIController
    {
        private readonly IServiceGaugesService _serviceGaugesService;
        public ServiceGaugesController(
            ILoggingService loggingService,
            IHttpContextAccessor accessor,
            IServiceGaugesService serviceGaugesService,
            IConfiguration configuration) : base(loggingService, configuration, accessor)
        { _serviceGaugesService = serviceGaugesService ?? throw new ArgumentNullException($"{nameof(IServiceGaugesService)} is null"); }

        [HttpPost("ServiceState")]
        public IActionResult ServiceState(ServiceGaugesFilterViewModel data)
        {
            var result = new ServiceGaugesResultFilterViewModel();
            var query = _serviceGaugesService.ServiceAnalysis(data.ToDto());
            result.AverageKilometers = query.AverageKilometers;
            result.MaxKilometers = query.MaxKilometers;
            result.AverageVisits = query.AverageVisits;
            result.MaxVisits = query.MaxVisits;
            result.AverageOnSiteTime = query.AverageOnSiteTime;
            result.MaxOnSiteTime = query.MaxOnSiteTime;
            result.AverageWaitTime = query.AverageWaitTime;
            result.MaxWaitTime = query.MaxWaitTime;
            result.AverageTravelTime = query.AverageTravelTime;
            result.MaxTravelTime = query.MaxTravelTime;
            result.TotalOts = query.TotalOts;
            result.IntervalKilometers = query.IntervalKilometers;
            result.IntervalOnSite = query.IntervalOnSite;
            result.IntervalTravel = query.IntervalTravel;
            result.IntervalVisits = query.IntervalVisits;
            result.IntervalWaitTime = query.IntervalWaitTime;
            result.Assets = query.Assets;
            result.GetMonthlyProjectCostList = query.GetMonthlyProjectCostList.ToListViewModel();
            result.GetMonthlyProjectMarginList = query.GetMonthlyProjectMarginList.ToListViewModel();
            result.GetMonthlyProjectRevenueList = query.GetMonthlyProjectRevenueList.ToListViewModel();
            result.AverageSLAResolution = query.AverageSLAResolution;
            result.MaxSLAResolution = query.MaxSLAResolution;
            result.IntervalSLAResolution = query.IntervalSLAResolution;
            result.AverageSLAResponse = query.AverageSLAResponse;
            result.MaxSLAResponse = query.MaxSLAResponse;
            result.IntervalSLAResponse = query.IntervalSLAResponse;
            result.ResolutionSla = query.ResolutionSla;
            result.ResponseSla = query.ResponseSla;
            result.TotalSla = query.TotalSla;

            return Ok(result);
        }
        
        [HttpPost("EconomicRep")]
        public IActionResult EconomicRep(ServiceGaugesFilterViewModel data)
        {
            var economic = new ServiceGaugesProjectReporViewModel();
            var economicquery = _serviceGaugesService.GetProjectReportByMonth(data.ToDto());
            economic.RevenueContract = economicquery.RevenueContract;
            economic.RevenueWorkForce = economicquery.RevenueWorkForce;
            economic.RevenueMaterials = economicquery.RevenueMaterials;
            economic.CostDirectWorkForce = economicquery.CostDirectWorkForce;
            economic.HoursDirectWorkForce = economicquery.HoursDirectWorkForce;
            economic.CostMaterials = economicquery.CostMaterials;
            economic.CostOutSource = economicquery.CostOutSource;
            economic.ExpensesOther = economicquery.ExpensesOther;
            economic.ExpensesTravel = economicquery.ExpensesTravel;
            economic.ExpensesWait = economicquery.ExpensesWait;
            economic.ExpensesKm = economicquery.ExpensesKm;
            economic.Margin = economicquery.Margin;
            return Ok(economic);
        }
    }
}