namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations
{
    public class MultiSelectConfigurationViewDto
    {
        public int UserId { get; set; }
        public int ColumnId { get; set; }
        public int LanguageId { get; set; }
        public string SelectedFilter { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}