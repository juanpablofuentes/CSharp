using System;

namespace Group.Salto.SOM.Web.Models.Projects
{
    public class ProjectsFilterViewModel : BaseFilter
    {
        public ProjectsFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Serie { get; set; }
    }
}