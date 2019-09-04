using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Families;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class FamiliesController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private IFamiliesService _familiesService;

        public FamiliesController(ILoggingService loggingService,
                                IFamiliesService familiesService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _familiesService = familiesService ?? throw new ArgumentNullException(nameof(IFamiliesService));
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _familiesService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }
}