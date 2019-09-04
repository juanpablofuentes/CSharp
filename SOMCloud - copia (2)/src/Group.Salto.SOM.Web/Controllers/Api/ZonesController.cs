using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ZoneProject;
using Group.Salto.SOM.Web.Models.Query;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.ZoneProject;
using Group.Salto.SOM.Web.Models.Zones;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.ZoneProjectPostalCode;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class ZonesController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private IZoneProjectService _zoneProjectService;

        public ZonesController(ILoggingService loggingService,
                                IZoneProjectService zoneProjectService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _zoneProjectService = zoneProjectService ?? throw new ArgumentNullException(nameof(IZoneProjectService));
        }

        [HttpGet("GetPostalCodesZone")]
        public IActionResult GetPostalCodesZone(int id)
        {

            var data = _zoneProjectService.GetAllPostalCodesByZoneProjectId(id);
            if (data.Data.Count == 0)
            {
                var resModel = new List <ZoneProjectPostalCodeViewModel>();
                return Ok(resModel);
            }

           var resultModel = data.Data.ToViewModel();
           var result = resultModel.FirstOrDefault().PostalCodes;
            return Ok(result);
        }
    }
}
