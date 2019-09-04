using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Maturity;
using Group.Salto.ServiceLibrary.Common.Contracts.Priority;
using Group.Salto.SOM.Web.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Web.Controllers.Maturity
{
    [Authorize]
    public class MaturityController : BaseController
    {
        private readonly IMaturityService _maturityService;

        public MaturityController(ILoggingService loggingService, 
                                    IConfiguration configuration, 
                                    IMaturityService maturityService,
                                    IHttpContextAccessor accessor) : base(loggingService, configuration, accessor)
        {
            _maturityService = maturityService ?? throw new ArgumentNullException(nameof(IMaturityService));
        }

        [HttpGet]
        public IActionResult GetMaturities()
        {
            //TODO ImplementService Maturities
            var maturities = _maturityService.GetBaseMaturities().ToSelectList();
            return Ok(maturities);
        }
    }
}