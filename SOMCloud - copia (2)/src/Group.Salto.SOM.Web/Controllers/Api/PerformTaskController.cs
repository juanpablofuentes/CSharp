using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class PerformTaskController : BaseAPIController
    {
        private readonly ITasksService _tasksService;
        public PerformTaskController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            ITasksService tasksService) : base(loggingService, configuration, accessor)
        {
            _tasksService = tasksService ?? throw new ArgumentNullException($"{nameof(ITasksService)} is null");
        }

        [HttpGet("GetInfo")]
        public IActionResult GetInfo(GetTaskDto getTaskDto)
        {
            var peopleConfigId = GetConfigurationUserId();
            var result = _tasksService.GetTaskEditInfo((int)peopleConfigId, getTaskDto);
            return Ok(result);
        }


        [HttpGet("GetTechnician")]
        public IActionResult GetTechnician(TaskInfoDto getTechnicianDto)
        {
            var peopleConfigId = GetConfigurationUserId();
            var getAllDto = _tasksService.GetSupplantTechnician((int)peopleConfigId, getTechnicianDto);
            return Ok(getAllDto.Technicians);
        }
    }
}