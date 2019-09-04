using Group.Salto.Common.Enums;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.PerformTask;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.PerformTask
{
    public class PerformTaskController : BaseController
    {
        private readonly ITasksService _tasksService;
        public PerformTaskController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            ITasksService tasksService) : base(loggingService, configuration, accessor)
        {
            _tasksService = tasksService ?? throw new ArgumentNullException($"{nameof(ITasksService)} is null");
        }

        [HttpPost("PerformTask")]
        [CustomAuthorization(ActionGroupEnum.PerformTask, ActionEnum.Create)]
        public IActionResult Create(PerformTaskViewModel genricTask)
        {
            //TODO: walter
            LoggingService.LogInfo("Perform Task - Create");

            return PartialView("_PerfomTaskModal");
        }

        [HttpPost("PerformTaskForm")]
        [CustomAuthorization(ActionGroupEnum.PerformTask, ActionEnum.Create)]
        public IActionResult CreateForm(PerformTaskFormsViewModel formTask)
        {
            //TODO: walter
            LoggingService.LogInfo("Perform Task - CreateForm");

            return PartialView("_PerfomTaskModal");
        }
    }
}
