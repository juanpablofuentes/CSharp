using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PeopleCollection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class PeopleCollectionController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly IPeopleCollectionService _peopleCollectionService;

        public PeopleCollectionController(ILoggingService loggingService, IPeopleCollectionService peopleCollectionService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _peopleCollectionService = peopleCollectionService ?? throw new ArgumentNullException($"{nameof(IPeopleCollectionService)} is null"); ;
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _peopleCollectionService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }

            return Ok(result.Data);
        }
    }
}