using Group.Salto.ServiceLibrary.Common.Contracts.BasicInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Mobility.Controllers.BasicInfo
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasicInfoController : BaseController
    {
        private readonly IBasicInfoService _basicInfoService;

        public BasicInfoController(IConfiguration configuration,
                                    IBasicInfoService basicInfoService) : base(configuration)
        {
            _basicInfoService = basicInfoService;
        }

        [HttpGet]
        [ActionName("GetBasicInfo")]
        public IActionResult GetBasicInfo()
        {
            var peopleConfigId = GetUserConfigId();
            var result = _basicInfoService.GetAppBasicInfo(peopleConfigId);
            return Ok(result);
        }

        [HttpGet]
        [ActionName("GetAppMinVersion")]
        public IActionResult GetAppMinVersion()
        {
            string result = _basicInfoService.GetAppMinVersion();
            return Ok(result);
        }
    }
}
