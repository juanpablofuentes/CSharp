namespace Group.Salto.ServiceLibrary.Common.Dtos.PeopleVisible
{
    public class PeopleVisibleFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public int? WorkCenterId { get; set; }
        public int? DepartmentId { get; set; }
        public int? CompanyId { get; set; }
        public int? KnowledgeId { get; set; }
    }
}