using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Log;
using Microsoft.AspNetCore.Authorization;
using Group.Salto.ServiceLibrary.Common.Contracts.PointRate;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Mvc;
using Group.Salto.SOM.Web.Extensions;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class PointRateController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IPointRateService _pointRateService;

        public PointRateController(ILoggingService loggingService, IPointRateService pointRateService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _pointRateService = pointRateService ?? throw new ArgumentNullException($"{nameof(IPointRateService)} is null"); ;
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _pointRateService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            if (!string.IsNullOrEmpty(result.Data.ErrorMessageKey))
            {
                var errorMessageKeyTranslation = LocalizedExtensions.GetUILocalizedText(result.Data.ErrorMessageKey);
                result.Data.ErrorMessageKey = errorMessageKeyTranslation;
            }
            return Ok(result.Data);
        }
    }
}