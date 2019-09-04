using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Clients;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class ClientController : BaseAPIController
    {
        private readonly IClientService _clientService;
        public ClientController(
           ILoggingService loggingService,
           IConfiguration configuration,
           IHttpContextAccessor accessor,
           IClientService clientService) : base(loggingService, configuration, accessor)
        {
            _clientService = clientService ?? throw new ArgumentNullException($"{nameof(IClientService)} is null");
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }

            queryRequest.QueryTypeParameters.Value = GetConfigurationUserId().ToString();
            var result = _clientService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }
}
