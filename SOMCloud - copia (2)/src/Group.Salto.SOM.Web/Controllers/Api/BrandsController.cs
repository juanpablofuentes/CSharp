using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Brand;
using Group.Salto.ServiceLibrary.Common.Contracts.Brands;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private IBrandsService _brandsService;

        public BrandsController(ILoggingService loggingService,
                                IBrandsService brandsService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _brandsService = brandsService ?? throw new ArgumentNullException(nameof(IBrandsService));
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _brandsService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }
}