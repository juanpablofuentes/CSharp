namespace Group.Salto.Entities.Tenant
{
    public class PeopleRegisteredPda
    {
        public int PeopleId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string GcmId { get; set; }

        public People People { get; set; }
    }
}
