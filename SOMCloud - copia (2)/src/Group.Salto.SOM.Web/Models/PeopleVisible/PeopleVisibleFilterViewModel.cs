namespace Group.Salto.SOM.Web.Models.PeopleVisible
{
    public class PeopleVisibleFilterViewModel : BaseFilter
    {
        public PeopleVisibleFilterViewModel()
        {
            OrderBy = nameof(Name);
        }

        public string Name { get; set; }
        public int KnowledgeId { get; set; }
        public string KnowledgeText { get; set; }
        public int CompanyId { get; set; }
        public string CompanyText { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentText { get; set; }
        public int WorkCenterId { get; set; }
        public string WorkCenterText { get; set; }
    }
}