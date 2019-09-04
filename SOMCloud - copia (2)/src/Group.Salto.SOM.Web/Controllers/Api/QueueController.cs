using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IQueueService _queueService;

        public QueueController(ILoggingService loggingService, IQueueService queueService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _queueService = queueService ?? throw new ArgumentNullException($"{nameof(IQueueService)} is null"); ;
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _queueService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}