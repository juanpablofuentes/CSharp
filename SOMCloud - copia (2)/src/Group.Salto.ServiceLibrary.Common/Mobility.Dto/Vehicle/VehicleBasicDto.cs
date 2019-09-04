namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Vehicle
{
    public class VehicleBasicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAssigned { get; set; }
        public int? PeopleId { get; set; }
    }
}
