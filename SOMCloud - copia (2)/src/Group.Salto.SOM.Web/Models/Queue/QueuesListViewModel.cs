using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Queue
{
    public class QueuesListViewModel
    {
        public MultiItemViewModel<QueueListViewModel, int> QueueList { get; set; }
        public QueueFilterViewModel QueueFilter { get; set; }
    }
}