using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Warehouses;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class WarehousesController : BaseAPIController
    {
        private readonly IWarehousesService _warehousesService;

        public WarehousesController(ILoggingService loggingService,
                                    IHttpContextAccessor accessor,
                                    IConfiguration configuration,
                                    IWarehousesService WarehousesService) : base(loggingService, configuration, accessor)
        {
            _warehousesService = WarehousesService ?? throw new ArgumentNullException(nameof(IWarehousesService));
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _warehousesService.CanDelete(id);
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