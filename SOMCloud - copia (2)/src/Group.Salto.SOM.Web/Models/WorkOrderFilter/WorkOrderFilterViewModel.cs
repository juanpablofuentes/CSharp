using Group.Salto.Common.Enums;
using Group.Salto.SOM.Web.Models.MultiSelect;
using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WorkOrderFilter
{
    public class WorkOrderFilterViewModel
    {
        public WorkOrderFilterViewModel()
        {
            AvailableColumns = new List<WorkOrderFilterColumns>();
            SelectedColumns = new List<WorkOrderFilterColumns>();
            Technicians = new MultiSelectViewModel();
            Groups = new MultiSelectViewModel();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TranslatedName { get; set; }
        public bool IsDefault { get; set; }
        public int UserId { get; set; }
        public IList<WorkOrderFilterColumns> AvailableColumns { get; set; }
        public IList<WorkOrderFilterColumns> SelectedColumns { get; set; }
        public MultiSelectViewModel Technicians { get; set; }
        public MultiSelectViewModel Groups { get; set; }
        public ModeActionTypeEnum ModeActionType { get; set; }
    }
}