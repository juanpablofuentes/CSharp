using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.PredefinedDay;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Expense;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Vehicle;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.BasicInfo
{
    public class BasicInfoDto
    {
        public ExpensesBasicFiltersInfoDto ExpenseBasicInfo { get; set; }
        public IEnumerable<VehicleBasicDto> UserVehicles { get; set; }
        public IEnumerable<PredefinedDayDto> JourneyTypes { get; set; }
        public double PriceKm { get; set; }
        public int BarcodeRetries { get; set; }
    }
}
