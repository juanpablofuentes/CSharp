using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class PeoplePushRegistration : BaseEntity
    {
        public int PeopleId { get; set; }
        public int Platform { get; set; }
        public string Manufacturer { get; set; }
        public string DeviceModel { get; set; }
        public string Version { get; set; }
        public string Idiom { get; set; }
        public string PushToken { get; set; }
        public string DeviceId { get; set; }
        public bool Enabled { get; set; }

        public People People { get; set; }
    }
}
