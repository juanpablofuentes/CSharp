using Group.Salto.Common.Constants.WorkOrder;
using Group.Salto.Common.Constants.WorkOrderColumns;
using Group.Salto.Common.Enums;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderColumnsDtoExtensions
    {
        public static WorkOrderColumnsDto ToDto(this MainWoViewConfigurationsColumns source, IList<WorkOrderColumnsDto> columns)
        {
            WorkOrderColumnsDto result = null;
            if (source != null)
            {
                result = columns.Where(x => x.Id == source.ColumnId).FirstOrDefault();
                result.ColumnOrder = source.ColumnOrder;
            }
            return result;
        }

        public static IList<WorkOrderColumnsDto> ToListDto(this IList<MainWoViewConfigurationsColumns> source, IList<WorkOrderColumnsDto> columns, bool exportToExcel)
        {
            List<WorkOrderColumnsDto> data = new List<WorkOrderColumnsDto>();
            AddCheckboxColumn(data);
            AddNumWOsInSiteColumn(data);
            NormalizePosition(source, data.Count);
            data.AddRange(source?.MapList(x => x.ToDto(columns)));
            AddSLAColumn(data);
            if (exportToExcel)
            {
                data = data.Where(x => x.ExportToExcel).ToList();
            }
            return data;
        }

        public static WorkOrderColumnsDto ToDto(this Entities.WorkOrderDefaultColumns source, IList<WorkOrderColumnsDto> columns)
        {
            WorkOrderColumnsDto result = null;
            if (source != null)
            {
                result = columns.Where(x => x.Id == source.WorkOrderColumnId).FirstOrDefault();
                result.ColumnOrder = source.Position;
            }
            return result;
        }

        public static IList<WorkOrderColumnsDto> ToListDto(this IList<Entities.WorkOrderDefaultColumns> source, IList<WorkOrderColumnsDto> columns, bool exportToExcel)
        {
            List<WorkOrderColumnsDto> data = new List<WorkOrderColumnsDto>();
            AddCheckboxColumn(data);
            AddNumWOsInSiteColumn(data);
            NormalizePosition(source, data.Count);
            data.AddRange(source?.MapList(x => x.ToDto(columns)).ToList());
            AddSLAColumn(data);
            if (exportToExcel)
            { 
                data = data.Where(x => x.ExportToExcel).ToList();
            }
            return data;
        }

        public static WorkOrderSelectedViewColumnsDto ToWorkOrderSelectedViewColumns(this MainWoViewConfigurationsColumns source, IList<WorkOrderColumnsDto> columns)
        {
            WorkOrderSelectedViewColumnsDto result = null;
            if (source != null)
            {
                WorkOrderColumnsDto column = columns.Where(x => x.Id == source.ColumnId).FirstOrDefault();
                result = new WorkOrderSelectedViewColumnsDto
                {
                    Id = source.UserMainWoviewConfigurationId,
                    ColumnId = source.ColumnId,
                    Name = column.Name,
                    TranslatedName = column.TranslatedName,
                    ColumnOrder = source.ColumnOrder,
                    FilterValues = source.FilterValues,
                    FilterStartDate = source.FilterStartDate,
                    FilterEndDate = source.FilterEndDate,
                    EditType = (EditTypeEnum)column.EditType
                };
            }
            return result;
        }

        public static IList<WorkOrderSelectedViewColumnsDto> ToWorkOrderSelectedViewColumns(this IList<MainWoViewConfigurationsColumns> source, IList<WorkOrderColumnsDto> columns)
        {
            return source?.MapList(x => x.ToWorkOrderSelectedViewColumns(columns));
        }

        public static IList<WorkOrderColumnsDto> ToAvailableColumnsDto(this IList<WorkOrderColumnsDto> source, IList<WorkOrderSelectedViewColumnsDto> selectedColumns)
        {
            IList<WorkOrderColumnsDto> availableColumns = new List<WorkOrderColumnsDto>();
            int position = 0;
            foreach (WorkOrderColumnsDto column in source)
            {
                if (!selectedColumns.Any(x => x.ColumnId == column.Id))
                {
                    column.ColumnId = column.Id;
                    column.ColumnOrder = position++;
                    availableColumns.Add(column);
                }
            }
            return availableColumns;
        }

        public static WorkOrderColumnsDto ToDto(this Entities.WorkOrderColumns source, string translatedName)
        {
            WorkOrderColumnsDto result = new WorkOrderColumnsDto();
            if (source != null)
            {
                SetColumnValue(result, source);
                result.TranslatedName = translatedName;
            }
            return result;
        }

        private static void NormalizePosition(IList<Entities.WorkOrderDefaultColumns> data, int position)
        {
            foreach (Entities.WorkOrderDefaultColumns column in data)
            {
                column.Position = position++;
            }
        }

        private static void NormalizePosition(IList<MainWoViewConfigurationsColumns> data, int position)
        {
            foreach (MainWoViewConfigurationsColumns column in data)
            {
                column.ColumnOrder = position++;
            }
        }

        private static void SetColumnValue(WorkOrderColumnsDto result, Entities.WorkOrderColumns column)
        {
            result.Id = column.Id;
            result.Name = column.Name;
            result.With = System.Convert.ToInt32(column.Width);
            result.Align = column.Align;
            result.Type = WorkOrderColumnsConstants.ReadOnlyType;
            result.Sort = column.CanSort ? WorkOrderColumnsConstants.ServerSort : WorkOrderColumnsConstants.NoSort;
            result.ExportToExcel = true;
            result.EditType = column.EditType != null ? (EditTypeEnum)column.EditType : EditTypeEnum.Empty;
        }

        private static void AddSLAColumn(IList<WorkOrderColumnsDto> columnsDtos)
        {
            if (columnsDtos.Any(x => x.Name == WorkOrderColumnsEnum.ResolutionDateSla.ToString()))
            {
                columnsDtos.Add(new WorkOrderColumnsDto() { Id = 999, Name = WorkOrderConstants.SlaId, Align = WorkOrderConstants.WorkOrderGridAlignLeft, With = 0, ColumnOrder = columnsDtos.Count() + 1, Type = WorkOrderColumnsConstants.ReadOnlyType, Sort = WorkOrderColumnsConstants.NoSort, ExportToExcel = false });
            }
        }

        private static void AddNumWOsInSiteColumn(IList<WorkOrderColumnsDto> columnsDtos)
        {
            if (!columnsDtos.Any(x => x.Name == WorkOrderColumnsEnum.NumWOsInSite.ToString()))
            {
                columnsDtos.Add(new WorkOrderColumnsDto() { Id = (int)WorkOrderColumnsEnum.NumWOsInSite, Name = WorkOrderColumnsEnum.NumWOsInSite.ToString(), Align = WorkOrderConstants.WorkOrderGridAlignLeft, With = 50, ColumnOrder = columnsDtos.Count(), Type = WorkOrderColumnsConstants.ReadOnlyType, Sort = WorkOrderColumnsConstants.NoSort, ExportToExcel = true });
            }
        }

        private static void AddCheckboxColumn(IList<WorkOrderColumnsDto> columnsDtos)
        {
            columnsDtos.Add(new WorkOrderColumnsDto() { Id = (int)WorkOrderColumnsEnum.SelectCheckbox, Name = WorkOrderColumnsEnum.SelectCheckbox.ToString(), Align = WorkOrderConstants.WorkOrderGridAlignCenter, With = 50, ColumnOrder = columnsDtos.Count(), Type = WorkOrderColumnsConstants.CheckboxType, Sort = WorkOrderColumnsConstants.NoSort, ExportToExcel = false });
        }
    }
}