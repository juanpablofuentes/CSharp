using Group.Salto.Controls.Entities;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Knowledge
{
    public class KnowledgeFilterDto : ISortableFilter
    {
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public bool IsDeleted { get; set; }
    }
}
