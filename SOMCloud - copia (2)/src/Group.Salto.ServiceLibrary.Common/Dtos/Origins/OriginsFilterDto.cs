using Group.Salto.Controls.Entities;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Origins
{
    public class OriginsFilterDto : ISortableFilter
    {
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
    }
}
