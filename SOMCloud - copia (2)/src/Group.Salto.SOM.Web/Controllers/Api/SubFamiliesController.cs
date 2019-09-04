using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.SubFamilies;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class SubFamiliesController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private ISubFamiliesService _subFamiliesService;

        public SubFamiliesController(ILoggingService loggingService,
                                ISubFamiliesService subFamiliesService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _subFamiliesService = subFamiliesService ?? throw new ArgumentNullException(nameof(ISubFamiliesService));
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _subFamiliesService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }

        [HttpPost("Filter")]
        public IActionResult Filter(QueryCascadeViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _subFamiliesService.FilterByClientSite(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }

}