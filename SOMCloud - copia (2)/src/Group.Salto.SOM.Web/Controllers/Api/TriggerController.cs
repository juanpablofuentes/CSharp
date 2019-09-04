using Group.Salto.Common.Constants;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.Trigger;
using Group.Salto.ServiceLibrary.Common.Contracts.TriggerTypes;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.Trigger;
using Group.Salto.SOM.Web.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class TriggerController : BaseAPIController
    {
        private ITriggerService _triggerService;
        private ITriggerTypesService _triggerTypesService;
        private ILanguageService _languageService;

        public TriggerController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                ITriggerTypesService triggerTypesService,
                                ITriggerService triggerService,
                                ILanguageService languageService) : base(loggingService, configuration, accessor)
        {
            _triggerService = triggerService ?? throw new ArgumentNullException(nameof(ITriggerService));
            _triggerTypesService = triggerTypesService ?? throw new ArgumentNullException(nameof(ITriggerTypesService));
            _languageService = languageService ?? throw new ArgumentNullException(nameof(ILanguageService));
        }

        [HttpGet("GetTriggerTypes")]
        public IActionResult GetTriggerTypes()
        {
            var result = _triggerTypesService.GetAllKeyValues();

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetTriggerValues")]
        public IActionResult GetTriggerValues(string triggerTypeName)
        {
            var languageId = GetLanguageId();
            FilterQueryParametersBase filterQueryParameters = null;
            filterQueryParameters = new FilterQueryParametersBase()
            {
                LanguageId = languageId,
            };

            var result = _triggerService.GetTriggerValues(triggerTypeName, filterQueryParameters);

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetTriggerTypeByTaskId")]
        public IActionResult GetTriggerTypeByTaskId(int id)
        {
            Guid comparerId = new Guid();
            if(!id.Equals(comparerId))
            {
                var result = _triggerService.GetTriggerByTaskId(id);

                if (result == null)
                {
                    return NotFound("Not data found");
                }
                return Ok(result);
            }
            else
            {
                return Ok(null);
            }
        }

        [HttpPost("PostTrigger")]
        public IActionResult PostTrigger(TriggerDto triggerDto)
        {
            var result = _triggerService.Update(triggerDto);
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