using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ClosingCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class ClosingCodeController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IClosingCodeService _closingCodeService;

        public ClosingCodeController(ILoggingService loggingService, IClosingCodeService closingCodeService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _closingCodeService = closingCodeService ?? throw new ArgumentNullException($"{nameof(IClosingCodeService)} is null"); ;
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _closingCodeService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}