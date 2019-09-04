namespace Group.Salto.Entities.Tenant
{
    public class PeopleProjects
    {
        public int PeopleId { get; set; }
        public int ProjectId { get; set; }
        public bool IsManager { get; set; }

        public People People { get; set; }
        public Projects Projects { get; set; }
    }
}
