using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PostalCode;
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
    public class PostalCodesController : BaseAPIController
    {
        private IPostalCodeService _postalCodesService;

        public PostalCodesController(
            ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IPostalCodeService postalCodesService) : base(loggingService, configuration, accessor)
        {
            _postalCodesService = postalCodesService ?? throw new ArgumentNullException(nameof(IPostalCodeService));
        }

        [HttpGet("CheckPostalCode")]
        public IActionResult CheckPostalCode(int id)
        {
            if (id == 0)
            {
                return BadRequest("Not data request");
            }
            var result = _postalCodesService.GetByCode(id.ToString());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetPostalCodeByCity")]
        public IActionResult GetPostalCodeByCity(int id)
        {
            var result = _postalCodesService.GetByCity(id);
            return Ok(result);
        }

        [HttpGet("GetPostalCodeById")]
        public JsonResult GetPostalCodeById(int id)
        {
            var result = _postalCodesService.GetById(id);
            return Json(result?.PostalCode);
        }

        [HttpPost]
        public IActionResult Post(string code)
        {
            if (code == null)
            {
                return BadRequest("Not data request");
            }
            var result = _postalCodesService.GetByCode(code);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }
    }
}