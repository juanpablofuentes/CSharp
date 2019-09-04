using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Task
{
    public class PreConditionViewModel
    {
        public MultiItemViewModel<LiteralPreconditionViewModel, int> LiteralPreconditionsList { get; set; }
    }
}