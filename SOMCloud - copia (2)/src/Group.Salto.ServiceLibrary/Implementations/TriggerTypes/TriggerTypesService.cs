using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.TriggerTypes;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Trigger;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.TriggerTypes
{
    public class TriggerTypesService : BaseService, ITriggerTypesService
    {
        private readonly ITriggerTypesRepository _triggerTypesRepository;

        public TriggerTypesService(ILoggingService logginingService,
                             ITriggerTypesRepository triggerTypesRepository) : base(logginingService)
        {
            _triggerTypesRepository = triggerTypesRepository ?? throw new ArgumentNullException($"{nameof(ITriggerTypesRepository)} is null ");
        }

        public IList<BaseNameIdDto<Guid>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get TriggerTypes Key Value");
            var data = _triggerTypesRepository.GetAll();

            return data.Select(x => new BaseNameIdDto<Guid>()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public TriggerTypeDto GetTriggerTypeById(Guid id)
        {
            var triggerType = _triggerTypesRepository.GetById(id);
            return triggerType.ToDto();
        }

        public TriggerTypeDto GetTriggerTypeByName(string name)
        {
            var triggerType = _triggerTypesRepository.GetByName(name);
            return triggerType.ToDto();
        }
    }
}