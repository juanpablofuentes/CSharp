using Group.Salto.ServiceLibrary.Common.Contracts.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Mobility.Controllers.Service
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceController : BaseController
    {
        private readonly IFormService _formService;

        public ServiceController(IConfiguration configuration,
            IFormService formService) : base(configuration)
        {
            _formService = formService;
        }

        [HttpGet("{id}")]
        [ActionName("getfiles")]
        public IActionResult GetById(int id)
        {
            var result = _formService.GetFilesFromService(id);
            return Ok(result);
        }
    }
}
