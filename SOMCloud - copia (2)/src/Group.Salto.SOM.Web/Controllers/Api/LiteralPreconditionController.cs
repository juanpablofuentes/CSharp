using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.LiteralPrecondition;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.Precondition;
using Group.Salto.ServiceLibrary.Common.Contracts.Trigger;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class LiteralPreconditionController : BaseAPIController
    {
        private ILiteralPreconditionsService _literalPreconditionsService;
        private ILanguageService _languageService;

        public LiteralPreconditionController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                ILiteralPreconditionsService preconditionsService,
                                ILanguageService languageService) : base(loggingService, configuration, accessor)
        {
            _literalPreconditionsService = preconditionsService ?? throw new ArgumentNullException(nameof(ILiteralPreconditionsService));
            _languageService = languageService ?? throw new ArgumentNullException(nameof(ILanguageService));
        }

        [HttpGet("GetLiteralPrecondition")]
        public IActionResult GetLiteralPrecondition(int id)
        {
            var result = _literalPreconditionsService.GetLiteralPrecondition(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetLiteralValues")]
        public IActionResult GetLiteralValues(int id, int preconditionId, string type)
        {
            var languageId = GetLanguageId();
            FilterQueryParametersBase filterQueryParameters = null;
            filterQueryParameters = new FilterQueryParametersBase()
            {
                LiteralPreconditionId = id,
                PreconditionId = preconditionId,
                LiteralPreconditionTypeName = type,
                LanguageId = languageId,
                UserId = GetConfigurationUserId(),
            };
            if (type == LiteralPreconditionConstants.Billable ||
                type == LiteralPreconditionConstants.ActuationDate ||
                type == LiteralPreconditionConstants.AssignmentDate ||
                type == LiteralPreconditionConstants.ClientClosureDate ||
                type == LiteralPreconditionConstants.CollectionDate ||
                type == LiteralPreconditionConstants.CreateDate ||
                type == LiteralPreconditionConstants.SaltoClosureDate ||
                type == LiteralPreconditionConstants.WOClientClosureDate)
            {
                var result = _literalPreconditionsService.GetLiteralValues(filterQueryParameters);
                if (result == null)
                {
                    return NotFound("Not data found");
                }
                return Ok(result);
            }
            else
            {
                var result = _literalPreconditionsService.GetLiteralValuesList(filterQueryParameters);
                if (result == null)
                {
                    return NotFound("Not data found");
                }
                return Ok(result);
            }
        }

        [HttpPost("PostEditLiteral")]
        public IActionResult PostEditLiteral(LiteralPreconditionsDto literalPrecondition)
        {
            var result = _literalPreconditionsService.Update(literalPrecondition);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpPost("PostNewLiteral")]
        public IActionResult PostNewLiteral(LiteralPreconditionsDto literalPrecondition)
        {
            var result = _literalPreconditionsService.Create(literalPrecondition);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpDelete("DeleteLiteralPrecondition")]
        public IActionResult DeleteLiteralPrecondition(int id)
        {
            var result = _literalPreconditionsService.Delete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        /*TODO On BaseApiController*/
        private int GetLanguageId()
        {
            this.Request.Cookies.TryGetValue(AppConstants.CookieLanguageConstant, out var culture);
            int languageId = !string.IsNullOrEmpty(culture)
                ? _languageService.GetByCulture(culture).Data.Id
                : _languageService.GetByCulture(AppConstants.CultureTwoLettersSpanish).Data.Id;
            return languageId;
        }
    }
}