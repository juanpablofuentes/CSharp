using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.ServiceLibrary.Common.Contracts.Trigger;
using Group.Salto.ServiceLibrary.Common.Contracts.TriggerTypes;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos.Trigger;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.Trigger
{
    public class TriggerService : BaseService, ITriggerService
    {
        private readonly ITriggerTypesService _triggerTypesService;
        private readonly ITasksService _tasksService;
        private readonly ITriggerFactory _triggerFactory;

        public TriggerService(
                        ITriggerTypesService triggerTypesService,
                        ITasksService tasksService,
                        ITriggerFactory triggerFactory,
                        ILoggingService logginingService) : base(logginingService)
        {
            _triggerTypesService = triggerTypesService ?? throw new ArgumentNullException($"{nameof(ITriggerTypesService)} is null");
            _tasksService = tasksService ?? throw new ArgumentNullException($"{nameof(ITasksService)} is null");
            _triggerFactory = triggerFactory ?? throw new ArgumentNullException($"{nameof(ITriggerFactory)} is null");
        }

        public TriggerDto GetTriggerByTaskId(int id)
        {
            TriggerDto triggerDto = null;
            var task = _tasksService.GetById(id);
            if (task != null)
            {
                var typeId = task.TriggerTypesId;
                var triggerType = _triggerTypesService.GetTriggerTypeById(typeId);

                triggerDto = new TriggerDto()
                {
                    TaskId = task.TaskId,
                    TypeId = triggerType.Id,
                    TypeName = triggerType.Name,
                    Value = task.TypeValue,
                    ValueId = task.TypeId,
                };
            }

            return triggerDto;
        }

        public IList<BaseNameIdDto<int>> GetTriggerValues(string triggerTypeName, FilterQueryParametersBase filterQueryParameters)
        {
            var languageId = filterQueryParameters.LanguageId;
            TriggerTypeDto triggerType = _triggerTypesService.GetTriggerTypeByName(triggerTypeName);

            var query = _triggerFactory.GetQuery(triggerType.Description);

            IList<BaseNameIdDto<int>> values = query.GetAllKeyValues(filterQueryParameters);

            return values;
        }

        public ResultDto<TasksDto> Update(TriggerDto model)
        {
            ResultDto<TasksDto> res = new ResultDto<TasksDto>();
            var triggerType = _triggerTypesService.GetTriggerTypeById(model.TypeId);
            res = _tasksService.UpdateTrigger(model, triggerType);
            return res;
        }
    }
}