using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ClosureCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class ClosureCodeController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IClosureCodeService _closureCodeService;

        public ClosureCodeController(ILoggingService loggingService, IClosureCodeService closureCodeService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _closureCodeService = closureCodeService ?? throw new ArgumentNullException($"{nameof(IClosureCodeService)} is null"); ;
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _closureCodeService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}