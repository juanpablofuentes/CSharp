using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.RepetitionParameter
{
    public class RepetitionParametersViewModel
    {
        public MultiItemViewModel<RepetitionParameterViewModel, Guid> RepetitionParameter { get; set; }
        public RepetitionParameterFilterViewModel RepetitionParameterFilter { get; set; }
    }
}
