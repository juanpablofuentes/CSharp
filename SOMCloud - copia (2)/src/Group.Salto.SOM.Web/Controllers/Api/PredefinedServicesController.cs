using System;
using Group.Salto.Common.Constants.PredefinedServices;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PredefinedServices;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class PredefinedServicesController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IPredefinedServiceService _predefinedServiceService;

        public PredefinedServicesController(ILoggingService loggingService, IPredefinedServiceService predefinedServiceService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _predefinedServiceService = predefinedServiceService ?? throw new ArgumentNullException($"{nameof(IWorkOrderStatusService)} is null"); ;
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            bool result = _predefinedServiceService.CanDelete(id);
            if (result)
            {
                return Ok(new { result = true, text = string.Empty });
            }
            else
            {
                return Ok(new { result = false, text = LocalizedExtensions.GetUILocalizedText(PredefinedServicesConstants.PredefinedServicesCanNotDelete) });
            }
        }
    }
}