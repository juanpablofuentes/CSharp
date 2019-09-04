using Group.Salto.ServiceLibrary.Common.Contracts.Journey;
using Group.Salto.ServiceLibrary.Common.Dtos.Journey;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Mobility.Controllers.Journey
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JourneyController : BaseController
    {
        private readonly IJourneyService _journeyService;

        public JourneyController(IConfiguration configuration,
            IJourneyService journeyService) : base(configuration)
        {
            _journeyService = journeyService;
        }

        [HttpGet]
        [ActionName("GetCurrentJourney")]
        public IActionResult GetBasicInfo()
        {
            var peopleConfigId = GetUserConfigId();
            var result = _journeyService.GetCurrentActiveJourney(peopleConfigId);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("AddOrUpdate")]
        public IActionResult AddOrUpdate(JourneyDto journeyDto)
        {
            var peopleConfigId = GetUserConfigId();
            var result = _journeyService.AddOrUpdate(peopleConfigId, journeyDto);
            return Ok(result);
        }
    }
}
