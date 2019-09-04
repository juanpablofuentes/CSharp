using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Sites;
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
    public class SitesController : BaseAPIController
    {
        private readonly ISitesService _sitesService;

        public SitesController(ILoggingService loggingService,
                               IHttpContextAccessor accessor,
                               IConfiguration configuration,
                               ISitesService sitesService) : base(loggingService, configuration, accessor)
        {
            _sitesService = sitesService ?? throw new ArgumentNullException(nameof(ISitesService));
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _sitesService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }

        [HttpPost("Filter")]
        public IActionResult Filter(QueryCascadeViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _sitesService.FilterByClientSite(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _sitesService.CanDelete(id);
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
            LoggingService.LogInfo($"Site Post AdvancedSearch");
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }

            var result = _sitesService.GetAdvancedSearch(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}