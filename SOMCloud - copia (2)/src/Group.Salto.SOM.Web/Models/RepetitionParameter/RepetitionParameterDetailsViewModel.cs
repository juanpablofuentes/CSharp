using Group.Salto.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.RepetitionParameter
{
    public class RepetitionParameterDetailsViewModel : RepetitionParameterViewModel
    {
        public RepetitionParameterViewModel RepParam { get; set; }
        public Guid IdDamagedEquipment { get; set; }
        public IEnumerable<SelectListItem> DamagedEquipmentItems { get; set; }
        public Guid IdCalculationType { get; set; }
        public IEnumerable<SelectListItem> CalculationTypeItems { get; set; }
        public Guid IdDaystype { get; set; }
        public IEnumerable<SelectListItem> DaystypeItemsItems { get; set; }
    }
}