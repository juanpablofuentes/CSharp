using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class WorkOrderStatusController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IWorkOrderStatusService _workOrderStatusService;

        public WorkOrderStatusController(ILoggingService loggingService, IWorkOrderStatusService workOrderStatusService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _workOrderStatusService = workOrderStatusService ?? throw new ArgumentNullException($"{nameof(IWorkOrderStatusService)} is null"); ;
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _workOrderStatusService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}