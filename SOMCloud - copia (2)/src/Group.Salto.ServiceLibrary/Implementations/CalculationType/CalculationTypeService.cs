using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CalculationType;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations.CalculationType
{
    public class CalculationTypeService : BaseService, ICalculationTypeService
    {
        private readonly ICalculationTypeRepository _calculationTypeRepository;

        public CalculationTypeService(ILoggingService logginingService,
                                ICalculationTypeRepository iCalculationTypeRepository) : base(logginingService)
        {
            _calculationTypeRepository = iCalculationTypeRepository ?? throw new ArgumentNullException($"{nameof(ICalculationTypeRepository)} is null ");
        }

        public IList<BaseNameIdDto<Guid>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get CalculationType Key Value");
            var data = _calculationTypeRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<Guid>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}