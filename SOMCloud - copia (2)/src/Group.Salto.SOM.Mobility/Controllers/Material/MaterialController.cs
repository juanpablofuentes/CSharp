using Group.Salto.ServiceLibrary.Common.Contracts.Material;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Mobility.Controllers.Material
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MaterialController : BaseController
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IConfiguration configuration,
                                  IMaterialService materialService) : base(configuration)
        {
            _materialService = materialService;
        }

        [HttpGet]
        [ActionName("GetMaterials")]
        public IActionResult GetMaterials()
        {
            var peopleConfigId = GetUserConfigId();
            var result = _materialService.GetMaterialsByPeopleConfigId(peopleConfigId);
            return Ok(result);
        }
    }
}
