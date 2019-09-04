using Group.Salto.Common.Constants;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.Postcondition;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.Postconditions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class PostconditionController : BaseAPIController
    {
        private IPostconditionTypesService _postconditionTypesService;
        private IPostconditionsService _postconditionService;
        private ILanguageService _languageService;

        public PostconditionController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                IPostconditionTypesService postconditionTypesService,
                                ILanguageService languageService,
                                IPostconditionsService postconditionsService) : base(loggingService, configuration, accessor)
        {
            _postconditionTypesService = postconditionTypesService ?? throw new ArgumentNullException(nameof(IPostconditionTypesService));
            _postconditionService = postconditionsService ?? throw new ArgumentNullException(nameof(IPostconditionsService));
            _languageService = languageService ?? throw new ArgumentNullException(nameof(ILanguageService));
        }

        [HttpGet("GetAllPostconditionTypesByPostcondition")]
        public IActionResult GetAllPostconditionTypesByPostcondition(int id)
        {
            var result = _postconditionTypesService.GetAllKeyValuesByPostcondition(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetAllPostconditionTypes")]
        public IActionResult GetAllPostconditionTypes()
        {
            var result = _postconditionTypesService.GetAllKeyValues();
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetAllPostconditionByTaskId")]
        public IActionResult GetAllPostconditionByTaskId(int id)
        {
            var result = _postconditionService.GetAllByTaskId(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetTypeOtnValues")]
        public IActionResult GetTypeOtnValues(int id)
        {
            var languageId = GetLanguageId();
            FilterQueryParametersBase filterQueryParameters = null;
            filterQueryParameters = new FilterQueryParametersBase()
            {
                LanguageId = languageId,
            };

            var result = _postconditionService.GetTypeOtnValues(id, filterQueryParameters);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetPostconditionValues")]
        public IActionResult GetPostconditionValues(string postconditionTypeName)
        {
            var languageId = GetLanguageId();
            FilterQueryParametersBase filterQueryParameters = null;
            filterQueryParameters = new FilterQueryParametersBase()
            {
                LanguageId = languageId,
            };

            var result = _postconditionService.GetPostconditionValues(postconditionTypeName, filterQueryParameters);

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpPost("CanDeletePostconditionCollection")]
        public IActionResult CanDeletePostconditionCollection(int id)
        {
            var result = _postconditionService.CanDeletePostconditionCollection(id);
            
            return Ok(result);
        }

        [HttpPost("PostNewPostcondition")]
        public IActionResult PostNewPostcondition(PostconditionsDto postcondition)
        {
            var result = _postconditionService.Create(postcondition);

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpPost("PostNewPostconditionColection")]
        public IActionResult PostNewPostconditionColection(int taskId)
        {
            var result = _postconditionService.CreatePostconditionCollection(taskId);

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpPost("PostUpdatePostcondition")]
        public IActionResult PostUpdatePostcondition(PostconditionsDto postcondition)
        {
            var result = _postconditionService.Update(postcondition);

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpDelete("DeletePostcondition")]
        public IActionResult DeletePostcondition(int id)
        {
            var result = _postconditionService.Delete(id);

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpDelete("DeleteAllPostcondition")]
        public IActionResult DeleteAllPostcondition(int id)
        {
            var result = _postconditionService.DeleteAllPostconditions(id);

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpDelete("DeletePostconditionCollection")]
        public IActionResult DeletePostconditionCollection(int id)
        {
            var result = _postconditionService.DeletePostconditionCollection(id);

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