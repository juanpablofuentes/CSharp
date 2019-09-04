using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private IPeopleService _peopleService;

        public PeopleController(ILoggingService loggingService,
                                IPeopleService peopleService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _peopleService = peopleService ?? throw new ArgumentNullException(nameof(IPeopleService));
        }
        
        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if(queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _peopleService.Filter(queryRequest.ToDto());
            if(result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }

        [HttpPost("PeopleTechnicians")]
        public IActionResult PeopleTechnicians(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _peopleService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }
}