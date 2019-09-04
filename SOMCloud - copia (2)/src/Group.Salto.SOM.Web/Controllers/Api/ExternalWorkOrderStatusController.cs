using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class ExternalWorkOrderStatusController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IExternalWorkOrderStatusService _externalWorkOrderStatusService;

        public ExternalWorkOrderStatusController(ILoggingService loggingService, IExternalWorkOrderStatusService externalWorkOrderStatusService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _externalWorkOrderStatusService = externalWorkOrderStatusService ?? throw new ArgumentNullException($"{nameof(IWorkOrderStatusService)} is null"); ;
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _externalWorkOrderStatusService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}