using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Knowledge;
using Group.Salto.ServiceLibrary.Implementations;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class KnowledgeController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private IKnowledgeService _knowledgeService;

        public KnowledgeController(ILoggingService loggingService,
                                IKnowledgeService knowledgeService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _knowledgeService = knowledgeService ?? throw new ArgumentNullException(nameof(IKnowledgeService));
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _knowledgeService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }
}