using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ActionsGroups;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.ActionsGroups
{
    public class ActionGroupService : BaseService, IActionGroupService
    {
        private IActionGroupRepository _actionGroupRepository;

        public ActionGroupService(ILoggingService logginingService,
            IActionGroupRepository actionGroupRepository) : base(logginingService)
        {
            _actionGroupRepository = actionGroupRepository ?? throw new ArgumentNullException($"{nameof(IActionGroupRepository)} is null");
        }

        public Dictionary<Guid, string> GetAllKeyValues()
        {
            LogginingService.LogInfo($"ActionGroupService - GetAllKeyValues");
            Dictionary<Guid, string> result = _actionGroupRepository.GetAllKeyValues();
            return result;
        }
    }
}