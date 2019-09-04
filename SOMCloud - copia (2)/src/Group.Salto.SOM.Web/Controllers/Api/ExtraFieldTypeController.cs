using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ExtraFieldTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class ExtraFieldTypeController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IExtraFieldTypesService _extraFieldTypesService;

        public ExtraFieldTypeController(ILoggingService loggingService, 
            IExtraFieldTypesService extraFieldTypesService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null");
            _extraFieldTypesService = extraFieldTypesService ?? throw new ArgumentNullException($"{nameof(IExtraFieldTypesService)} is null"); ;
        }

        public IActionResult Get(int id)
        {
            _loggingService.LogInfo("Get Extra Field Type By Id");
            var extraFieldType = _extraFieldTypesService.GetById(id);
            if (extraFieldType.Data == null)
            {
                return NotFound("Not data found");
            }
            return Ok(extraFieldType.Data);
        }
    }
}