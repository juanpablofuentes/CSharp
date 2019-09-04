using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Permisions;
using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Tasks;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class PermissionsTasks : BaseAPIController
    {
        private IPermissionsService _permissionsService;

        public PermissionsTasks(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                IPermissionsService permissionsService) : base(loggingService, configuration, accessor)
        {
            _permissionsService = permissionsService ?? throw new ArgumentNullException(nameof(IPermissionsService));
        }

        [HttpGet("GetAllPermissions")]
        public IActionResult GetAllPermissions()
        {
            var result = _permissionsService.GetAllKeyValues();
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }
    }
}