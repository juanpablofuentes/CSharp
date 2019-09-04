namespace Group.Salto.ServiceLibrary.Common.Dtos.Grid
{
    public class SortDto
    {
        public bool IsAscending { get; set; } = true;
        public int ColumnOrder { get; set; }
        public bool DefaultOrder { get; set; }
    }
}