using System;

namespace Group.Salto.SOM.Web.Models.WorkOrderFilter
{
    public class WorkOrderFilterColumns
    {
        public int ColumnId { get; set; }
        public string Name { get; set; }
        public string TranslatedName { get; set; }
        public string FilterValues { get; set; }
        public DateTime? FilterStartDate { get; set; }
        public DateTime? FilterEndDate { get; set; }
        public int Modal { get; set; }
        public string ToolTip { get; set; }
    }
}