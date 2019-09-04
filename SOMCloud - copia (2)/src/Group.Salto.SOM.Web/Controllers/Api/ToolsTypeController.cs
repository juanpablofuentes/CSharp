using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Knowledge;
using Group.Salto.ServiceLibrary.Common.Contracts.ToolsType;
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
    public class ToolsTypeController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private IToolsTypeService _toolstypeService;

        public ToolsTypeController(ILoggingService loggingService,
                                IToolsTypeService toolstypeService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _toolstypeService = toolstypeService ?? throw new ArgumentNullException(nameof(IToolsTypeService));
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _toolstypeService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }
}