using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Grid
{
    public class GridDto
    {
        public GridDto()
        {
            Pagination = new PaginationDto();
            Sort = new SortDto();
            WorkOrderFilters = new WorkOrderFiltersDto();
        }

        public int UserId { get; set; }
        public int PeopleId { get; set; }
        public int LanguageId { get; set; }
        public PaginationDto Pagination { get; set; }
        public SortDto Sort { get; set; }
        public WorkOrderFiltersDto WorkOrderFilters { get; set; }
        public int[] SubContracts { get; set; }
        public bool IsExcelMode { get; set; }
        public bool ExportAllToExcel { get; set; }
        public int ConfigurationViewId { get; set; }
        public bool IsQuickFilter { get; set; }
    }
}