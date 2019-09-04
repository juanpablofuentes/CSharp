using System;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.BasicInfo;
using Group.Salto.ServiceLibrary.Common.Dtos.PredefinedDay;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.BasicInfo;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Expense;
using Group.Salto.ServiceLibrary.Extensions;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.ServiceLibrary.Implementations.BasicInfo
{
    public class BasicInfoService : IBasicInfoService
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IExpenseTicketStatusRepository _expenseTicketStatusRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly IPredefinedDayStatesRepository _predefinedDayStatesRepository;
        private readonly IConfiguration _configuration;

        public BasicInfoService(IExpenseTypeRepository expenseTypeRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IExpenseTicketStatusRepository expenseTicketStatusRepository,
            IPeopleRepository peopleRepository,
            IVehiclesRepository vehiclesRepository,
            IPredefinedDayStatesRepository predefinedDayStatesRepository,
            IConfiguration configuration)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _expenseTicketStatusRepository = expenseTicketStatusRepository;
            _peopleRepository = peopleRepository;
            _vehiclesRepository = vehiclesRepository;
            _predefinedDayStatesRepository = predefinedDayStatesRepository;
            _configuration = configuration;
        }

        public BasicInfoDto GetAppBasicInfo(int peopleConfigId)
        {
            var dto = new BasicInfoDto();
            var expenseTypes = _expenseTypeRepository.GetAll().ToDto();
            var paymentTypes = _paymentMethodRepository.GetAll().ToDto();
            var expenseStatus = _expenseTicketStatusRepository.GetAll().ToDto();

            var dtoExpenseTypes = new ExpensesBasicFiltersInfoDto
            {
                ExpenseTypes = expenseTypes,
                PaymentTypes = paymentTypes,
                ExpenseStatus = expenseStatus
            };
            dto.ExpenseBasicInfo = dtoExpenseTypes;

            var currentPeople = _peopleRepository.GetByConfigId(peopleConfigId);
            if (currentPeople != null)
            {
                peopleConfigId = currentPeople.Id;
            }

            var allVehicles = _vehiclesRepository.GetAllNotDeleted().ToList().ToBasicDto();
            foreach (var vehicle in allVehicles.Where(v => v.PeopleId == peopleConfigId))
            {
                vehicle.IsAssigned = true;
            }

            dto.UserVehicles = allVehicles;

            var predefinedTypesIds = Enum.GetValues(typeof(PredefinedDayEnum)).Cast<PredefinedDayEnum>().Cast<int>().ToList();
            var journeyTypes = _predefinedDayStatesRepository.GetByIds(predefinedTypesIds).ToDto();
            dto.JourneyTypes = journeyTypes;
            dto.PriceKm = currentPeople?.CostKm ?? 1;
            dto.BarcodeRetries = _configuration.GetSection(AppsettingsKeys.MobileSettings).GetValue<int>(AppsettingsKeys.BarcodeRetries);

            return dto;
        }

        public string GetAppMinVersion()
        {
            var appMinVersion = _configuration.GetSection(AppsettingsKeys.MobileSettings).GetValue<string>(AppsettingsKeys.AppMinVersion);
            return appMinVersion;
        }
    }
}
