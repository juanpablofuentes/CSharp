using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Task
{
    public class TaskListViewModel
    {
        public MultiItemViewModel<TaskViewModel, int> TasksList { get; set; }
    }
}