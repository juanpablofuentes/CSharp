using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkCenter;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private ILanguageService _languageService;

        public LanguageController(ILoggingService loggingService,
                                    ILanguageService languageService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null");
            _languageService = languageService ?? throw new ArgumentNullException($"{nameof(ILanguageService)} is null");
        }

        public IActionResult Get()
        {
            _loggingService.LogInfo("Get All Languages");
            var languages = _languageService.GetAll().Data?.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });
            if (languages == null)
            {
                return NotFound("Not data found");
            }
            return Ok(languages);
        }
    }
}
