using Group.Salto.Log;
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
    public class AssetStatusesController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private IAssetStatusesService _assetStatusesService;

        public AssetStatusesController(ILoggingService loggingService,
                                IAssetStatusesService assetStatusesService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _assetStatusesService = assetStatusesService ?? throw new ArgumentNullException(nameof(IAssetStatusesService));
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _assetStatusesService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }
}