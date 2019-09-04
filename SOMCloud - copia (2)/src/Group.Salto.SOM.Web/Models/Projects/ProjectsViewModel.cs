using System;

namespace Group.Salto.SOM.Web.Models.Projects
{
    public class ProjectsViewModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Serie { get; set; }
        public int Counter { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Observations { get; set; }
        public string ProjectManager { get; set; }
        public string ContractName { get; set; }
    }
}