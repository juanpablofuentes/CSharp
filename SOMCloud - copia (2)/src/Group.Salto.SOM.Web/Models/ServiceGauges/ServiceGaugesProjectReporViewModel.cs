using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.ServiceGauges
{
    public class ServiceGaugesProjectReporViewModel
    {
        public double RevenueContract { get; set; }
        public double RevenueWorkForce { get; set; }
        public double RevenueMaterials { get; set; }
        public double CostDirectWorkForce { get; set; }
        public int HoursDirectWorkForce { get; set; }
        public double CostMaterials { get; set; }
        public double CostOutSource { get; set; }
        public double ExpensesOther { get; set; }
        public double ExpensesTravel { get; set; }
        public double ExpensesWait { get; set; }
        public double ExpensesKm { get; set; }
        public double Margin { get; set; }
    }
}