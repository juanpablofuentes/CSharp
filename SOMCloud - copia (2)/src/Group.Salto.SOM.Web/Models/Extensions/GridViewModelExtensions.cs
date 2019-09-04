using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.SOM.Web.Models.WorkOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class GridViewModelExtensions
    {
        public static GridDto ToDto(this WorkOrderGridRequestViewModel source)
        {
            GridDto result = null;
            if (source != null)
            {
                result = new GridDto()
                {
                    UserId = source.UserId,
                    LanguageId = source.LanguageId,
                    Pagination = new PaginationDto { Skip = source.PosStart, Take = source.Count },
                    Sort = new SortDto() { DefaultOrder = source.DefaultOrder, ColumnOrder = source.OrderBy != null ? Convert.ToInt32(source.OrderBy) : 0, IsAscending = source.IsAscending, },
                    WorkOrderFilters = ToFilterDto(source),
                    IsExcelMode = source.ExportToExcel,
                    ExportAllToExcel = source.ExportAllToExcel,
                    ConfigurationViewId = source.ConfigurationViewId,
                    IsQuickFilter = source.IsQuickFilter
                };
            }
            return result;
        }

        private static WorkOrderFiltersDto ToFilterDto(WorkOrderGridRequestViewModel source)
        {
            WorkOrderFiltersDto workOrderFilters = new WorkOrderFiltersDto
            {
                WorkOrderId = source.WorkOrderId,
                InternalIdentifier = source.InternalIdentifier,
                SerialNumber = source.SerialNumber,
                LocationCode = source.LocationCode,
                WorkOrderStatusIds = source.WorkOrderStatus,
                WorkOrderQueueIds = source.WorkOrderQueue,
                ProjectIds = source.ProjectIds,
                WorkOrderCategoryIds = source.WorkOrderCategoryIds,
                StateIds = source.StateIds,
                ResponsiblesIds = source.ResponsiblesIds,
                WorkOrderSearch = new ServiceLibrary.Common.Dtos.WorkOrders.WorkOrderSearch()
                {
                    SearchString = source.WorkOrderSearch.SearchString?.Trim(),
                    SearchType = source.WorkOrderSearch.SearchType
                }
            };

            if (!string.IsNullOrEmpty(source.ResolutionDateSla))
            {
                workOrderFilters.ResolutionDateSla = Convert.ToDateTime(source.ResolutionDateSla);
            }

            if (!string.IsNullOrEmpty(source.CreationDate))
            {
                workOrderFilters.CreationDate = Convert.ToDateTime(source.CreationDate);
            }

            if (!string.IsNullOrEmpty(source.CreationStartDate))
            {
                workOrderFilters.CreationStartDate = Convert.ToDateTime(source.CreationStartDate);
            }

            if (!string.IsNullOrEmpty(source.CreationEndDate))
            {
                workOrderFilters.CreationEndDate = Convert.ToDateTime(source.CreationEndDate);
            }

            if (!string.IsNullOrEmpty(source.ActionDateDate))
            {
                workOrderFilters.ActionDateDate = Convert.ToDateTime(source.ActionDateDate);
            }

            if (!string.IsNullOrEmpty(source.ActionDateStartDate))
            {
                workOrderFilters.ActionDateStartDate = Convert.ToDateTime(source.ActionDateStartDate);
            }

            if (!string.IsNullOrEmpty(source.ActionDateEndDate))
            {
                workOrderFilters.ActionDateEndDate = Convert.ToDateTime(source.ActionDateEndDate);
            }

            return workOrderFilters;
        }
    }
}