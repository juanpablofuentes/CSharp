using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Precondition;
using Group.Salto.ServiceLibrary.Common.Contracts.Trigger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class PreconditionController : BaseAPIController
    {
        private IPreconditionTypesService _preconditionTypesService;
        private IPreconditionsService _preconditionsService;

        public PreconditionController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                IPreconditionTypesService preconditionTypesService,
                                IPreconditionsService preconditionsService) : base(loggingService, configuration, accessor)
        {
            _preconditionTypesService = preconditionTypesService ?? throw new ArgumentNullException(nameof(IPreconditionTypesService));
            _preconditionsService = preconditionsService ?? throw new ArgumentNullException(nameof(IPreconditionsService));
        }

        [HttpGet("GetAllPreconditionByTaskId")]
        public IActionResult GetAllPreconditionByTaskId(int id)
        {
            var result = _preconditionsService.GetAllByTaskId(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetAllPreconditionTypes")]
        public IActionResult GetAllPreconditionTypes()
        {
            var result = _preconditionTypesService.GetAllKeyValues();
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetAllPreconditionTypesByPrecondition")]
        public IActionResult GetAllPreconditionTypesByPrecondition(int id)
        {
            var result = _preconditionTypesService.GetAllKeyValuesByPrecondition(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpPost("CreatePrecondition")]
        public IActionResult CreatePrecondition(int id, int? postconditionCollectionId)
        {
            var result = _preconditionsService.CreatePrecondition(id, postconditionCollectionId);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpDelete("DeletePrecondition")]
        public IActionResult DeletePrecondition(int id)
        {
            var result = _preconditionsService.Delete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpDelete("DeleteAllPrecondition")]
        public IActionResult DeleteAllPrecondition(int id)
        {
            var result = _preconditionsService.DeleteAllPreconditionsByTask(id);
            if (result.Errors != null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }
    }
}