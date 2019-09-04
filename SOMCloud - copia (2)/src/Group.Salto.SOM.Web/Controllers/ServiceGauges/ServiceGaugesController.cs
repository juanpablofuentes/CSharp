using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ServiceGauges;
using Group.Salto.SOM.Web.Models.ServiceGauges;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.SOM.Web.Models.Extensions;

namespace Group.Salto.SOM.Web.Controllers.Analysis
{
    [Authorize]
    public class ServiceGaugesController : BaseController
    {
        private readonly IServiceGaugesService _serviceGaugesService;
        public ServiceGaugesController(
            ILoggingService loggingService,
            IHttpContextAccessor accessor,
            IServiceGaugesService serviceGaugesService,
            IConfiguration configuration) : base(loggingService, configuration, accessor)
        { _serviceGaugesService = serviceGaugesService ?? throw new ArgumentNullException($"{nameof(IServiceGaugesService)} is null"); }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}