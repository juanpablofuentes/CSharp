using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.TasksTypes;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.TasksTypes
{
    public class TasksTypesService : BaseService, ITasksTypesService
    {
        private readonly ITasksTypesRepository _tasksTypesRepository;

        public TasksTypesService(ILoggingService logginingService,
                             ITasksTypesRepository tasksTypesRepository) : base(logginingService)
        {
            _tasksTypesRepository = tasksTypesRepository ?? throw new ArgumentNullException($"{nameof(ITasksTypesRepository)} is null ");
        }

        public IList<BaseNameIdDto<Guid>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get TasksTypes Key Value");
            var data = _tasksTypesRepository.GetAll();

            return data.Select(x => new BaseNameIdDto<Guid>()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}