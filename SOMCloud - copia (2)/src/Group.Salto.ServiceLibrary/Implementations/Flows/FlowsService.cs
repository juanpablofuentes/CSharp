using Group.Salto.Common;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Flows;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Flows;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Flows
{
    public class FlowsService : BaseService, IFlowsService
    {
        private readonly IFlowsRepository _flowsRepository;
        private readonly ITaskRepository _taskRepository;

        public FlowsService(ILoggingService logginingService,
                             IFlowsRepository flowsRepository, 
                             ITaskRepository taskRepository) : base(logginingService)
        {
            _flowsRepository = flowsRepository ?? throw new ArgumentNullException($"{nameof(IFlowsRepository)} is null ");
            _taskRepository = taskRepository ?? throw new ArgumentNullException($"{nameof(ITaskRepository)} is null ");
        }

        public ResultDto<FlowsDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get Queue by id {id}");
            var result = _flowsRepository.GetById(id)?.ToDto();
            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<FlowsWithTasksDictionaryDto> GetFlowsWithTasksInfoById(int id, int languageId)
        {
            LogginingService.LogInfo($"Get Flows by id {id}");
            var flow = _flowsRepository.GetById(id);
            var tasksWithTranslations = _taskRepository.GetTasksByFlowIdIncludeTranslations(flow.Id);
            List<BaseNameIdDto<int>> translationsTask = new List<BaseNameIdDto<int>>();
            foreach (var task in tasksWithTranslations)
            {
                var taskId = task.Id;
                var nameTask = task.Name;
                var translation = task.TasksTranslations.FirstOrDefault(x => x.LanguageId == languageId);
                if (translation != null && !string.IsNullOrEmpty(translation.NameText)) nameTask = translation.NameText;

                translationsTask.Add(new BaseNameIdDto<int>()
                {
                    Id = taskId,
                    Name = nameTask
                });
            }
            var result = new FlowsWithTasksDictionaryDto()
            {
                Id = flow.Id,
                Name = flow.Name,
                Description = flow.Description,
                Published = flow.Published,
                FlowTasksDictionary = translationsTask
            };


            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<IList<FlowsListDto>> GetAllFiltered(FlowsFilterDto filter)
        {
            LogginingService.LogInfo($"Get Flows filtered");
            var query = _flowsRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToListDto());
        }

        public ResultDto<FlowsDto> Create(FlowsDto model)
        {
            LogginingService.LogInfo($"Create Queue");
            var entity = model.ToEntity();
            var resultSave = _flowsRepository.CreateFlows(entity);
            return ProcessResult(resultSave.Entity.ToDto(), resultSave);
        }

        public ResultDto<FlowsDto> Update(FlowsDto model) {
            LogginingService.LogInfo($"Update Flows with id {model.Id}");

            ResultDto<FlowsDto> result = null;
            var localModel = _flowsRepository.GetById(model.Id);
            if (localModel != null)
            {
                localModel.Name = model.Name;
                localModel.Description = model.Description;
                localModel.Published = model.Published;
                var resultSave = _flowsRepository.UpdateFlows(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<FlowsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }

        private IQueryable<Entities.Tenant.Flows> OrderBy(IQueryable<Entities.Tenant.Flows> data, FlowsFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }
    }
}