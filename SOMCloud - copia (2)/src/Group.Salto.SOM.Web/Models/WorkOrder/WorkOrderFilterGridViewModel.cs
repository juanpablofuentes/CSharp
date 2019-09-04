using System;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.MultiSelect;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderFilterGridViewModel : BaseFilter
    {
        public WorkOrderFilterGridViewModel()
        {
            WorkOrderStatus = new MultiSelectViewModel();
            WorkOrderQueue = new MultiSelectViewModel();
            Projects = new List<MultiComboViewModel<int, int>>();
            WorkOrderCategories = new List<MultiComboViewModel<int, int>>();
        }

        public string WorkOrderId { get; set; }
        public string InternalIdentifier { get; set; }
        public string SerialNumber { get; set; }
        public string LocationCode { get; set; }
        public string ResolutionDateSla { get; set; }
        public string CreationStartDate { get; set; }
        public string CreationEndDate { get; set; }
        public string CreationDate { get; set; }
        public string ActionDateStartDate { get; set; }
        public string ActionDateEndDate { get; set; }
        public string ActionDateDate { get; set; }
        public MultiSelectViewModel WorkOrderStatus { get; set; }
        public MultiSelectViewModel WorkOrderQueue { get; set; }
        public IList<MultiComboViewModel<int, int>> Projects { get; set; }
        public IList<MultiComboViewModel<int, int>> WorkOrderCategories { get; set; }
        public IList<MultiComboViewModel<int, int>> States { get; set; }
        public IList<MultiComboViewModel<int, int>> Responsibles { get; set; }
        public WorkOrderSearch WorkOrderSearch { get; set; }
    }
}