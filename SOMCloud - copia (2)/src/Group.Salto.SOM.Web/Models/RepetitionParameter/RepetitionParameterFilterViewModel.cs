using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.RepetitionParameter
{
    public class RepetitionParameterFilterViewModel : BaseFilter
    {
        public RepetitionParameterFilterViewModel()
        {
            OrderBy = nameof(Days);
        }
        public int Days { get; set; }
    }
}