using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.PushNotifications
{
    public class PushRegistrationDto
    {
        public string Manufacturer { get; set; }
        public string DeviceModel { get; set; }
        public string Idiom { get; set; }
        public string PushToken { get; set; }
        public string DeviceId { get; set; }
        public DevicePlatformEnum Platform { get; set; }
        public string Version { get; set; }
    }
}
