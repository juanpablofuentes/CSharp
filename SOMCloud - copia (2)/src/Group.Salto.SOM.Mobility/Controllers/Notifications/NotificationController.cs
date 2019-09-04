using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Mobility.Controllers.Notifications
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly IPushNotificationService _pushNotificationService;

        public NotificationController(IConfiguration configuration,
                                      IPushNotificationService pushNotificationService) : base(configuration)
        {
            _pushNotificationService = pushNotificationService;
        }

        [HttpPost]
        [ActionName("RegisterPushUserDevice")]
        public IActionResult RegisterPushUserDevice(PushRegistrationDto pushRegDto)
        {
            var peopleConfigId = GetUserConfigId();
            var result = _pushNotificationService.RegisterPushUserDevice(pushRegDto, peopleConfigId);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("ChangePushState")]
        public IActionResult ChangePushState(PushChangeStateDto pushStateDto)
        {
            var result = _pushNotificationService.ChangePushState(pushStateDto);
            return Ok(result);
        }

        [HttpGet]
        [ActionName("GetUserNotifications")]
        public IActionResult GetUserNotifications()
        {
            var peopleConfigId = GetUserConfigId();
            var result = _pushNotificationService.GetUserNotifications(peopleConfigId);
            return Ok(result);
        }

        [HttpGet("{peopleId}")]
        [ActionName("SendRandomPush")]
        public IActionResult SendRandomPush(int peopleId)
        {
            //TODO: test notifications
            _pushNotificationService.SendRandomPush(peopleId);
            return Ok();
        }

        [HttpPost]
        [ActionName("ForceRegisterDevice")]
        public IActionResult ForceRegisterDevice(PushRegistrationDto pushRegDto)
        {
            var peopleConfigId = GetUserConfigId();
            var result = _pushNotificationService.ForceRegisterDevice(pushRegDto, peopleConfigId);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("GetIfDeviceIsValid")]
        public IActionResult GetIfDeviceIsValid(PushRegistrationDto pushRegDto)
        {
            var peopleConfigId = GetUserConfigId();
            var result = _pushNotificationService.GetIfDeviceIsValid(peopleConfigId, pushRegDto.DeviceId);
            return Ok(result);
        }
    }
}
