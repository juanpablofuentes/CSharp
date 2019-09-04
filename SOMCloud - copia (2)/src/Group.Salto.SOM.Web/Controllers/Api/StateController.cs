using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Contracts.States;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IStateService _stateService;

        public StateController(ILoggingService loggingService, IStateService stateService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _stateService = stateService ?? throw new ArgumentNullException($"{nameof(IStateService)} is null"); ;
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }

            var result = _stateService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }
}