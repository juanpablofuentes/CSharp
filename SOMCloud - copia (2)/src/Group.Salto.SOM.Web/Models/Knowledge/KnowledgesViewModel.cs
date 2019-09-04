using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.Knowledge
{
    public class KnowledgesViewModel
    {
        public MultiItemViewModel<KnowledgeViewModel, int> Knowledge { get; set; }

        public KnowledgesFilterViewModel KnowledgeFilters { get; set; }
    }
}
