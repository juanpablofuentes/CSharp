using Group.Salto.ServiceLibrary.Common.Dtos.ServiceGauges;
using Group.Salto.SOM.Web.Models.ServiceGauges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ServiceGaugesProjectTReporViewModelExtenCion
    {
        public static ServiceGaugesProjectReportDto ToDto(this ServiceGaugesProjectReporViewModel source)
        {
            ServiceGaugesProjectReportDto result = null;
            if (source != null)
            {
                result = new ServiceGaugesProjectReportDto()
                {
                    CostDirectWorkForce = source.CostDirectWorkForce,
                    CostMaterials = source.CostMaterials,
                    CostOutSource = source.CostOutSource,
                    ExpensesKm = source.ExpensesKm,
                    ExpensesOther = source.ExpensesOther,
                    ExpensesTravel = source.ExpensesTravel,
                    ExpensesWait = source.ExpensesWait,
                    HoursDirectWorkForce = source.HoursDirectWorkForce,
                    RevenueContract = source.RevenueContract,
                    RevenueMaterials = source.RevenueMaterials,
                    RevenueWorkForce = source.RevenueWorkForce,
                    Margin = source.Margin
                };
            }
            return result;
        }

        public static ServiceGaugesProjectReporViewModel ToViewModel(this ServiceGaugesProjectReportDto source)
        {
            ServiceGaugesProjectReporViewModel result = null;
            if (source != null)
            {
                result = new ServiceGaugesProjectReporViewModel()
                {
                    CostDirectWorkForce = source.CostDirectWorkForce,
                    CostMaterials = source.CostMaterials,
                    CostOutSource = source.CostOutSource,
                    ExpensesKm = source.ExpensesKm,
                    ExpensesOther = source.ExpensesOther,
                    ExpensesTravel = source.ExpensesTravel,
                    ExpensesWait = source.ExpensesWait,
                    HoursDirectWorkForce = source.HoursDirectWorkForce,
                    RevenueContract = source.RevenueContract,
                    RevenueMaterials = source.RevenueMaterials,
                    RevenueWorkForce = source.RevenueWorkForce,
                    Margin = source.Margin
                };
            }
            return result;
        }
    }
}