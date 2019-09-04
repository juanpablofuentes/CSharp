using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Mobility.Controllers.Tasks
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TasksController : BaseController
    {
        private readonly ITasksService _tasksService;

        public TasksController(IConfiguration configuration,
                                ITasksService tasksService) : base(configuration)
        {
            _tasksService = tasksService;
        }
         
        [HttpGet("{woId}")]
        [ActionName("GetByWoId")]
        public IActionResult GetByWoId(int woId)
        {
            var peopleConfigId = GetUserConfigId();
            var result = _tasksService.GetAvailableTasksFromWoId(peopleConfigId, woId);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("TaskExecute")]
        public IActionResult TaskExecute(TaskExecuteDto taskExecuteDto)
        {
            var peopleConfigId = GetUserConfigId();
            var customerId = GetCustomerId();
            var result = _tasksService.TaskExecute(taskExecuteDto, peopleConfigId, customerId);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("GetInfo")]
        public IActionResult GetById(GetTaskDto getTaskDto)
        {
            var peopleConfigId = GetUserConfigId();
            var result = _tasksService.GetTaskEditInfo(peopleConfigId, getTaskDto);
            return Ok(result);
        }
    }
}
