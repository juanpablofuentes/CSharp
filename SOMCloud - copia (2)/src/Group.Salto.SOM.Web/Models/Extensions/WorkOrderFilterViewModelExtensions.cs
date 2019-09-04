using Group.Salto.Common.Enums;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkOrderFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderFilterViewModelExtensions
    {
        public static WorkOrderFilterListViewModel ToViewModel(this WorkOrderViewConfigurationsDto source, int id)
        {
            WorkOrderFilterListViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderFilterListViewModel();
                source.ToViewModel(result, id);
            }

            return result;
        }

        public static void ToViewModel(this WorkOrderViewConfigurationsDto source, WorkOrderFilterListViewModel target, int id)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Load = source.Id == id;
            }
        }

        public static IList<WorkOrderFilterListViewModel> ToListViewModel(this IList<WorkOrderViewConfigurationsDto> source, int id)
        {
            return source?.MapList(x => x.ToViewModel(id));
        }

        public static WorkOrderFilterViewModel ToViewConfigurationViewModel(this ConfigurationViewDto source)
        {
            WorkOrderFilterViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderFilterViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    IsDefault = source.IsDefault,
                    AvailableColumns = source.AvailableColumns.ToAvailableColumn(),
                    SelectedColumns = source.SelectedColumns.ToSelectedColumn()
                };
            }
            return result;
        }

        public static WorkOrderFilterColumns ToWorkOrderFilterColumns(this BaseWorkOrderColumns source)
        {
            WorkOrderFilterColumns result = null;
            if (source != null)
            {
                result = new WorkOrderFilterColumns()
                {
                    Name = source.Name,
                    TranslatedName = source.TranslatedName,
                    ColumnId = source.ColumnId,
                    Modal = (int)source.EditType,
                    ToolTip = string.Empty
                };
            }
            return result;
        }

        public static WorkOrderFilterColumns ToWorkOrderSelectFilterColumns(this WorkOrderSelectedViewColumnsDto source)
        {
            WorkOrderFilterColumns result = ToWorkOrderFilterColumns(source);
            result.FilterValues = source.FilterValues;
            result.FilterStartDate = source.FilterStartDate;
            result.FilterEndDate = source.FilterEndDate;
            result.ToolTip = source.ToolTip;

            return result;
        }

        public static IList<WorkOrderFilterColumns> ToAvailableColumn(this IList<WorkOrderColumnsDto> source)
        {
            return source?.MapList(x => x.ToWorkOrderFilterColumns());
        }

        public static IList<WorkOrderFilterColumns> ToSelectedColumn(this IList<WorkOrderSelectedViewColumnsDto> source)
        {
            return source?.MapList(x => x.ToWorkOrderSelectFilterColumns());
        }
        
        public static ConfigurationViewDto ToEditDto(this WorkOrderFilterViewModel source, int languageId)
        {
            ConfigurationViewDto result = null;
            if (source != null)
            {
                result = new ConfigurationViewDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    IsDefault = source.IsDefault,
                    SelectedColumns = source.SelectedColumns.ToSelectColumnDto(),
                    Technician = source.Technicians.Items.ToDto(),
                    Groups = source.Groups.Items.ToDto(),
                    UserConfigurationId = source.UserId,
                    LanguageId = languageId
                };
            }
            return result;
        }

        public static IList<BaseNameIdDto<int>> ToViewModel(this IEnumerable<WorkOrderViewConfigurationsDto> source)
        {
            return source.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = x.Name,
                IsLocked = x.IsDefault
            }).ToList();
        }

        private static WorkOrderSelectedViewColumnsDto ToSelectColumnDto(this WorkOrderFilterColumns source, int position)
        {
            WorkOrderSelectedViewColumnsDto result = null;
            if (source != null)
            {
                result = new WorkOrderSelectedViewColumnsDto()
                {
                    ColumnId = source.ColumnId,
                    Name = source.Name,
                    TranslatedName = source.TranslatedName,
                    ColumnOrder = position,
                    FilterValues = source.FilterValues,
                    FilterStartDate = source.FilterStartDate,
                    FilterEndDate = source.FilterEndDate,
                };
            }
            return result;
        }

        private static IList<WorkOrderSelectedViewColumnsDto> ToSelectColumnDto(this IList<WorkOrderFilterColumns> source)
        {
            int position = 0;
            return source?.MapList(x => x.ToSelectColumnDto(position++));
        }
    }
}