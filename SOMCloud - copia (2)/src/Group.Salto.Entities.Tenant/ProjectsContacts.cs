namespace Group.Salto.Entities.Tenant
{
    public class ProjectsContacts
    {
        public int ProjectId { get; set; }
        public int ContactId { get; set; }

        public Contacts Contact { get; set; }
        public Projects Project { get; set; }
    }
}
