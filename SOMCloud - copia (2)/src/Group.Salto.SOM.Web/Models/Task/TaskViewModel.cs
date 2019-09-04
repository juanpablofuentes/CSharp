using Group.Salto.Controls.Table.Models;
using System.Collections;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Task
{
    public class TaskViewModel
    {
        public TaskHeaderViewModel TaskHeader { get; set; }
        public MultiItemViewModel<PreConditionViewModel, int> PreConditionsList { get; set; }
        public MultiItemViewModel<PostConditionCollectionViewModel, int> PostConditionsCollectionList { get; set; }
    }
}