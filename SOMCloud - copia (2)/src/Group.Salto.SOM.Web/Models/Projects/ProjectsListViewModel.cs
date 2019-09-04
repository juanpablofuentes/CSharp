using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Projects
{
    public class ProjectsListViewModel
    {
        public MultiItemViewModel<ProjectsViewModel, int> ProjectsList { get; set; }
        public ProjectsFilterViewModel Filters { get; set; }
    }
}