using Group.Salto.Controls.Entities;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Vehicles
{
    public class VehiclesFilterDto : ISortableFilter
    {
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
    }
}