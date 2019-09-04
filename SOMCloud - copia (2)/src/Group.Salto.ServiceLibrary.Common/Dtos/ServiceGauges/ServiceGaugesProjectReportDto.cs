using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ServiceGauges
{
    public class ServiceGaugesProjectReportDto
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