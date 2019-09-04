using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Actions
{
    public class ActionsViewModel
    {
        public MultiItemViewModel<ActionViewModel, int> Actions { get; set; }
        public ActionsFilterViewModel ActionsFilters { get; set; }
    }
}
