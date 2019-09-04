using Group.Salto.Common.Enums;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns
{
    public class BaseWorkOrderColumns
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ColumnOrder { get; set; }
        public int ColumnId { get; set; }
        public EditTypeEnum EditType { get; set; }
        public string TranslatedName { get; set; }
    }
}