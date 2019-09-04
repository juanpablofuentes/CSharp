using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.Permisions;
using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.ServiceLibrary.Common.Contracts.TasksTypes;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos.TasksTranslations;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class TasksController : BaseAPIController
    {
        private ITasksService _tasksService;
        private ITasksTypesService _tasksTypesService;
        private ILanguageService _languageService;
        private IPermissionsService _permissionsService;

        public TasksController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                ITasksTypesService tasksTypesService,
                                ILanguageService languageService,
                                IPermissionsService permissionsService,
                                ITasksService tasksService) : base(loggingService, configuration, accessor)
        {
            _tasksService = tasksService ?? throw new ArgumentNullException(nameof(ITasksService));
            _tasksTypesService = tasksTypesService ?? throw new ArgumentNullException(nameof(ITasksTypesService));
            _languageService = languageService ?? throw new ArgumentNullException(nameof(ILanguageService));
            _permissionsService = permissionsService ?? throw new ArgumentNullException(nameof(IPermissionsService));
        }

        [HttpGet("GetTask")]
        public IActionResult GetTask(int id)
        {
            TasksDetailDto result = _tasksService.GetByIdWithTranslations(id);

            var languages = _languageService.GetAll()?.Data;
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToDetailViewModel(languages));
        }

        [HttpGet("GetEmptyTask")]
        public IActionResult GetEmptyTask(int id)
        {
            TasksDetailDto result = new TasksDetailDto();
            result.PermissionsTasksSelected = new List<int>();
            result.TasksPlainTranslations = new List<TasksTranslationsDto>();
            var languages = _languageService.GetAll()?.Data;
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToDetailViewModel(languages));
        }

        [HttpPost("CreateTask")]
        public IActionResult CreateTask(TaskHeaderDetailViewModel model)
        {
            TasksDto taskDto = new TasksDto()
            {
                FlowId = model.FlowId,
                TaskId = model.Id,
                Name = model.Name,
                Description = model.Description,
                PermissionsTasksSelected = model.PermissionsTasksSelected,
                TasksPlainTranslations = model.TasksTranslationsList.ToDto()

            };
            var result = _tasksService.Create(taskDto);
            return Ok();
        }

        [HttpPost("UpdateTask")]
        public IActionResult UpdateTask(TaskHeaderDetailViewModel model)
        {
            TasksDto taskDto = new TasksDto()
            {
                FlowId = model.FlowId,
                TaskId = model.Id,
                Name = model.Name,
                Description = model.Description,
                PermissionsTasksSelected = model.PermissionsTasksSelected,
                TasksPlainTranslations = model.TasksTranslationsList.ToDto()
            };
            var result = _tasksService.Update(taskDto);
            return Ok();
        }

        [HttpGet("GetAllTasksTypesValues")]
        public IActionResult GetAllTriggersTypesValues()
        {
            var result = _tasksTypesService.GetAllKeyValues();

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetTasksForWorkOrder")]
        public IActionResult GetTasksForWorkOrder(int id)
        {
            IEnumerable<TaskApiDto> result = _tasksService.GetAvailableTasksFromWoId(base.GetConfigurationUserId(), id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToViewModel());
        }

        [HttpPost("ExecuteTask")]
        public IActionResult ExecuteTask(int id, int woId)
        {
            //TODO: Pending to execute
            IEnumerable<TaskApiDto> result = null;
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(true);
        }
    }
}