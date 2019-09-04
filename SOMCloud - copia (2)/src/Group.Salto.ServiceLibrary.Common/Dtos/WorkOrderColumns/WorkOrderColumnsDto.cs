namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns
{
    public class WorkOrderColumnsDto : BaseWorkOrderColumns
    {
        public int With { get; set; }
        public string Align { get; set; }
        public string Type { get; set; }
        public string Sort { get; set; }
        public bool ExportToExcel { get; set; }
        public bool IsExcelMode { get; set; }
    }
}