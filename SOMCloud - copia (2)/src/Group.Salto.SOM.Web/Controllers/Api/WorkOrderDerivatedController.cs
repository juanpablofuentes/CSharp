using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderDerivated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class WorkOrderDerivatedController : BaseLanguageController
    {
        private readonly IWorkOrderDerivatedService _workOrderDerivatedService;

        public WorkOrderDerivatedController(
            ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            ILanguageService languageService,
            IWorkOrderDerivatedService workOrderDerivatedService) : base(loggingService, configuration, accessor, languageService)
        {
            _workOrderDerivatedService = workOrderDerivatedService ?? throw new ArgumentNullException($"{nameof(IWorkOrderDerivatedService)} is null");
        }

        [HttpGet("GetByTaskId")]
        public IActionResult GetByTaskId(int Id)
        {
            var result = _workOrderDerivatedService.GetByTaskId(Id, GetLanguageId());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}