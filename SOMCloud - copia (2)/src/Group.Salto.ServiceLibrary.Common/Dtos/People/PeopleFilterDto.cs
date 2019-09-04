using Group.Salto.Common.Enums;

namespace Group.Salto.ServiceLibrary.Common.Dtos.People
{
    public class PeopleFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public ActiveEnum Active { get; set; }
        public bool? IsVisible { get; set; }
        public int? SubcontractId { get; set; }
    }
}