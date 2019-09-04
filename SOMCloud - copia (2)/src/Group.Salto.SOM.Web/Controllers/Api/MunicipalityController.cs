using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Municipality;
using Group.Salto.ServiceLibrary.Common.Contracts.PostalCode;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
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
    public class MunicipalityController : BaseAPIController
    {
        private readonly IMunicipalityService _municipalityService;

        public MunicipalityController(
            ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IMunicipalityService municipalityService) : base(loggingService, configuration, accessor)
        {
            _municipalityService = municipalityService ?? throw new ArgumentNullException(nameof(IMunicipalityService));
        }

        [HttpGet("GetMunicipalityById")]
        public JsonResult GetMunicipalityById(int id)
        {
            var result = _municipalityService.GetByIdWithStatesRegionsCountriesIncludes(id);
            return Json(result?.Name);
        }
    }
}