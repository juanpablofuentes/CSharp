using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Task
{
    public class PostConditionCollectionViewModel
    {
        public MultiItemViewModel<PreConditionViewModel, int> PostPreConditionsList { get; set; }
        public MultiItemViewModel<PostConditionViewModel, int> PostConditionsList { get; set; }
    }
}