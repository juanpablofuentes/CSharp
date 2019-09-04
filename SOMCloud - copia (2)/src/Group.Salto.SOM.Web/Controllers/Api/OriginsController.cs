using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Origins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class OriginsController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IOriginsService _originsService;

        public OriginsController(ILoggingService loggingService, IOriginsService originsService) {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null");
            _originsService = originsService ?? throw new ArgumentNullException($"{nameof(IOriginsService)} is null");

        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _originsService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}