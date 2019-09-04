using Group.Salto.Common.Enums;
using System.Collections.Generic;
using Group.Salto.Controls.Table.Models;
using Group.Salto.SOM.Web.Models.Task;
using Group.Salto.SOM.Web.Models.MultiSelect;

namespace Group.Salto.SOM.Web.Models.Flows
{
    public class FlowsDetailViewModel : FlowsViewModel
    {
        public TasksListViewModel TasksList { get; set; }
        public TaskHeaderDetailViewModel FlowTaskEditViewModel { get; set; }
        //The next multiselect if for Literal Preconditions
        public MultiSelectViewModel ModalMultiselect { get; set; }
    }
}