using Group.Salto.Log;
using Group.Salto.SOM.Web.Models.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Group.Salto.SOM.Web.Controllers.HiredServices
{
    [Authorize]
    public class HiredServicesController : BaseController
    {
        public HiredServicesController(
            ILoggingService loggingService,
            IHttpContextAccessor accessor,
            IConfiguration configuration) : base(loggingService, configuration, accessor)
        {}

        [HttpGet]
        public IActionResult HiredServices()
        {
            var model = new HiredServicesViewModel {};            
            return PartialView("_HiredServicesModal", model);
        }

        [HttpPost]
        public IActionResult HiredServices(HiredServicesViewModel model)
        {
            return PartialView("_HiredServicesModal", model);
        }
    }
}