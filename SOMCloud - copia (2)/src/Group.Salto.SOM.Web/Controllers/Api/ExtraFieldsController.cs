using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ExtraFields;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Group.Salto.SOM.Web.Models.ExtraFields;


namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class ExtraFieldsController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IExtraFieldsService _extraFieldsService;

        public ExtraFieldsController(ILoggingService loggingService, IExtraFieldsService extraFieldsService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _extraFieldsService = extraFieldsService ?? throw new ArgumentNullException($"{nameof(IExtraFieldsService)} is null"); ;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            _loggingService.LogInfo($"Get ExtraFields Values by ExtraFields Id {id}");
            var extraField = _extraFieldsService.GetById(id).Data;

            if (extraField == null)
            {
                return NotFound("Not data found");
            }
            return Ok(extraField);
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _extraFieldsService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}