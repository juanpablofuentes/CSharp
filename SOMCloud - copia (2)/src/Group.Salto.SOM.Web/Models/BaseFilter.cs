using Group.Salto.Controls.Entities;
using Group.Salto.Controls.Table.Filter;

namespace Group.Salto.SOM.Web.Models
{
    public abstract class BaseFilter : ISortableFilter, IPagedFilter, IExcel
    {
        protected BaseFilter()
        {
            Size = 50;
            FilterHeader = new FilterHeaderViewModel();
        }

        public int Page { get; set; } = 1;
        public int Size { get; set; }
        public int PagesCount { get; set; }
        public string OrderBy { get; set; }
        public bool Asc { get; set; } = true;
        public FilterHeaderViewModel FilterHeader { get; set; }
        public int TotalValues { get; set; }
        public bool ExportAllToExcel { get; set; }
    }
}