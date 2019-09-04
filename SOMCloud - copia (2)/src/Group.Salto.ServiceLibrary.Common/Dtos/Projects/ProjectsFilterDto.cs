namespace Group.Salto.ServiceLibrary.Common.Dtos.Projects
{
    public class ProjectsFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string Serie { get; set; }
        public int Counter { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Observations { get; set; }
    }
}