using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Items;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class ItemsController : BaseAPIController
    {
        private readonly IItemsService _itemsService;

        public ItemsController(ILoggingService loggingService,
                                    IHttpContextAccessor accessor,
                                    IConfiguration configuration,
                                    IItemsService itemsService) : base(loggingService, configuration, accessor)
        {
            _itemsService = itemsService ?? throw new ArgumentNullException(nameof(IItemsService));
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _itemsService.CanDelete(id);
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

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _itemsService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }
}