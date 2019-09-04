using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.FinalClients;
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
    public class FinalClientsController : BaseAPIController
    {
        private readonly IFinalClientsServices _finalClientsService;

        public FinalClientsController(ILoggingService loggingService,
                                      IHttpContextAccessor accessor,
                                      IConfiguration configuration,
                                      IFinalClientsServices finalClientsService) : base(loggingService, configuration, accessor)
        {
            _finalClientsService = finalClientsService ?? throw new ArgumentNullException(nameof(IFinalClientsServices));
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _finalClientsService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _finalClientsService.CanDelete(id);
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

        [HttpGet("AdvancedSearch")]
        public IActionResult AdvancedSearch(AdvancedSearchQueryTypeViewModel queryRequest)
        {
            LoggingService.LogInfo($"FinalClients Post AdvancedSearch");
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }

            var result = _finalClientsService.GetAdvancedSearch(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}