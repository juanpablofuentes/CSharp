using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.DaysType;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations
{
    public class DaysTypeService : BaseService, IDaysTypeService 
    {
        private readonly IDaysTypeRepository _daysTupeRepository;

        public DaysTypeService(ILoggingService logginingService,
                                IDaysTypeRepository iDaysTypeRepository) : base(logginingService)
        {
            _daysTupeRepository = iDaysTypeRepository ?? throw new ArgumentNullException($"{nameof(IDaysTypeRepository)} is null ");
        }

        public IList<BaseNameIdDto<Guid>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get Contract Key Value");
            var data = _daysTupeRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<Guid>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}