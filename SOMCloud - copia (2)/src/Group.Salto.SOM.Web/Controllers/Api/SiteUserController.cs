using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.SiteUser;
using Group.Salto.ServiceLibrary.Implementations.SiteUser;
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
    public class SiteUserController: BaseController
    {
        private readonly ISiteUserService _siteUserService;
        public SiteUserController(ILoggingService loggingService,
                                  IConfiguration configuration,
                                  IHttpContextAccessor accessor,
                                  ISiteUserService siteUserService) : base(loggingService, configuration, accessor)
        {
            _siteUserService = siteUserService ?? throw new ArgumentNullException($"{nameof(SiteUserService)} is null");
        }

        [HttpPost("Filter")]
        public IActionResult Filter(QueryCascadeViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _siteUserService.FilterByClientSite(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _siteUserService.CanDelete(id);
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