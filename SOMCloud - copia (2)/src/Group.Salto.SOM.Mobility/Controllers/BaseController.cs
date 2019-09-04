using System;
using System.Linq;
using System.Security.Claims;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.ApiSettings;
using Group.Salto.Common.Entities.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Mobility.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly OAuthConfiguration _configuration;

        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration.GetSection(ApiSettingsKeys.OAuthConfiguration)
                                 .Get<OAuthConfiguration>() ??
                             throw new ArgumentNullException(nameof(OAuthConfiguration));
        }

        protected int GetUserConfigId()
        {
            var peopleIdString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(peopleIdString, out var peopleConfigId);
            return peopleConfigId;
        }

        protected Guid GetCustomerId()
        {
            var customerId = User.Claims.FirstOrDefault(c => c.Type == AppIdentityClaims.TenantId)?.Value;
            Guid.TryParse(customerId, out var result);
            return result;
        }
    }
}
