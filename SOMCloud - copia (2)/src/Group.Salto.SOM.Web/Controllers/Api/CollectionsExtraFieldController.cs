using System;
using Group.Salto.Common.Constants.CollectionsExtraField;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CollectionsExtraField;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class CollectionsExtraFieldController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly ICollectionsExtraFieldService _collectionsExtraFieldService;

        public CollectionsExtraFieldController(ILoggingService loggingService, ICollectionsExtraFieldService collectionsExtraFieldService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _collectionsExtraFieldService = collectionsExtraFieldService ?? throw new ArgumentNullException($"{nameof(ICollectionsExtraFieldService)} is null");
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _collectionsExtraFieldService.CanDelete(id);
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