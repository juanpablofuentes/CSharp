using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Assets;
using Group.Salto.ServiceLibrary.Common.Contracts.AssetStatuses;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private IAssetsService _assetsService;

        public AssetsController(ILoggingService loggingService,
                                IAssetsService assetsService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _assetsService = assetsService ?? throw new ArgumentNullException(nameof(IAssetsService));
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _assetsService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }

        [HttpGet("GetLocationById")]
        public IActionResult GetLocationById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Not data request");
            }
            var result = _assetsService.GetLocationsAndUserSiteById(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}