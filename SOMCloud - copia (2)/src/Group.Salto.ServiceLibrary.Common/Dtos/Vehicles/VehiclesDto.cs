namespace Group.Salto.ServiceLibrary.Common.Dtos.Vehicles
{
    public class VehiclesDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UpdateDate { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public int?  DriverId { get; set; }
        public string Driver { get; set; }
    }
}