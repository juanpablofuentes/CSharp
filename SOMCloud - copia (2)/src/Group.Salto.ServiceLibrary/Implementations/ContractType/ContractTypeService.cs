using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ContractType;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.ContractType
{
    public class ContractTypeService : BaseService, IContractTypeService
    {
        private readonly IContractTypeRepository _contractTypeRepository;

        public ContractTypeService(ILoggingService logginingService,
                                   IContractTypeRepository contractTypeRepository) : base(logginingService)
        {
            _contractTypeRepository = contractTypeRepository ?? throw new ArgumentNullException(nameof(IContractTypeRepository));
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get Companies Key Value");
            var data = _contractTypeRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}