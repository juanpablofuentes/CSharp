using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkCenter;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Mvc;
using Group.Salto.SOM.Web.Models.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class WorkCenterController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IWorkCenterService _workCenterService;

        public WorkCenterController(ILoggingService loggingService,
            IWorkCenterService workCenterService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null");
            _workCenterService = workCenterService ?? throw new ArgumentNullException($"{nameof(IWorkCenterService)} is null");
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _workCenterService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }
}