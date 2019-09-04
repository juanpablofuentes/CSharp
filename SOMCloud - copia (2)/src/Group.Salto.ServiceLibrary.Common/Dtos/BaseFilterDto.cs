using Group.Salto.Controls.Entities;

namespace Group.Salto.ServiceLibrary.Common.Dtos
{
    public class BaseFilterDto : ISortableFilter, IExcel
    {
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public bool ExportAllToExcel { get; set; }
    }
}