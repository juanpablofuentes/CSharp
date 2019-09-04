using Group.Salto.Controls.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.RepetitionParameter
{
    public class RepetitionParameterFilterDto : ISortableFilter
    {
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
        public int Days { get; set; }
    }
}