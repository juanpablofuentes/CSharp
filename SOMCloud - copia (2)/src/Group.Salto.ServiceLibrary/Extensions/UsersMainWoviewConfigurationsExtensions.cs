using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class UsersMainWoviewConfigurationsExtensions
    {
        public static WorkOrderViewConfigurationsDto ToDto(this UsersMainWoviewConfigurations source)
        {
            WorkOrderViewConfigurationsDto result = null;
            if (source != null)
            {
                result = new WorkOrderViewConfigurationsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    IsDefault = source.IsDefault ?? false
                };
            }
            return result;
        }

        public static IList<WorkOrderViewConfigurationsDto> ToListDto(this IList<UsersMainWoviewConfigurations> source)
        {
            return source?.MapList(x => x.ToDto());
        }

        public static ConfigurationViewDto ToViewDto(this UsersMainWoviewConfigurations source, IList<WorkOrderColumnsDto> columns)
        {
            ConfigurationViewDto result = new ConfigurationViewDto();
            if (source != null)
            {
                result = new ConfigurationViewDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    IsDefault = source.IsDefault ?? false,
                    SelectedColumns = source.MainWoViewConfigurationsColumns?.OrderBy(x => x.ColumnOrder).ToList()?.ToWorkOrderSelectedViewColumns(columns),
                    TechnicianValues = source.MainWoViewConfigurationsPeople?.Select(x => x.PeopleId).ToList(),
                    GroupsValues = source.MainWoviewConfigurationsGroups?.Select(x => x.PeopleCollectionId).ToList()
                };
            }
            result.AvailableColumns = columns.ToAvailableColumnsDto(result.SelectedColumns);
            return result;
        }

        public static UsersMainWoviewConfigurations ToEntity(this ConfigurationViewDto source)
        {
            UsersMainWoviewConfigurations result = null;
            if (source != null)
            {
                result = new UsersMainWoviewConfigurations()
                {
                    Name = source.Name,
                    IsDefault = source.IsDefault,
                    UserConfigurationId = source.UserConfigurationId
                };
            }
            return result;
        }

        public static void UpdateConfigurationView(this UsersMainWoviewConfigurations target, UsersMainWoviewConfigurations source)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.IsDefault = source.IsDefault;
                target.UserConfigurationId = source.UserConfigurationId;
            }
        }

        public static UsersMainWoviewConfigurations AssignColumns(this UsersMainWoviewConfigurations entity, IList<WorkOrderSelectedViewColumnsDto> columns)
        {
            entity.MainWoViewConfigurationsColumns?.Clear();
            if (columns != null && columns.Any())
            {
                entity.MainWoViewConfigurationsColumns = entity.MainWoViewConfigurationsColumns ?? new List<MainWoViewConfigurationsColumns>();
                IList<MainWoViewConfigurationsColumns> localColumns = columns.ToEntity();
                foreach (MainWoViewConfigurationsColumns localColumn in localColumns)
                {
                    entity.MainWoViewConfigurationsColumns.Add(localColumn);
                }
            }
            return entity;
        }

        public static UsersMainWoviewConfigurations AssignTechnician(this UsersMainWoviewConfigurations entity, IEnumerable<int> technicianIds)
        {
            entity.MainWoViewConfigurationsPeople?.Clear();
            if (technicianIds != null && technicianIds.Any())
            {
                entity.MainWoViewConfigurationsPeople = entity.MainWoViewConfigurationsPeople ?? new List<MainWoViewConfigurationsPeople>();
                foreach (var element in technicianIds)
                {
                    entity.MainWoViewConfigurationsPeople.Add(new MainWoViewConfigurationsPeople()
                    {
                        PeopleId = element,
                    });
                }
            }

            return entity;
        }

        public static UsersMainWoviewConfigurations AssignGroup(this UsersMainWoviewConfigurations entity, IEnumerable<int> groupIds)
        {
            entity.MainWoviewConfigurationsGroups?.Clear();
            if (groupIds != null && groupIds.Any())
            {
                entity.MainWoviewConfigurationsGroups = entity.MainWoviewConfigurationsGroups ?? new List<MainWoviewConfigurationsGroups>();
                foreach (var element in groupIds)
                {
                    entity.MainWoviewConfigurationsGroups.Add(new MainWoviewConfigurationsGroups()
                    {
                        PeopleCollectionId = element,
                    });
                }
            }

            return entity;
        }

        private static MainWoViewConfigurationsColumns ToEntity(this WorkOrderSelectedViewColumnsDto source)
        {
            MainWoViewConfigurationsColumns result = null;
            if (source != null)
            {
                result = new MainWoViewConfigurationsColumns()
                {
                    ColumnId = source.ColumnId,
                    ColumnOrder = source.ColumnOrder,
                    FilterValues = source.FilterValues ?? string.Empty,
                    FilterStartDate = source.FilterStartDate,
                    FilterEndDate = source.FilterEndDate
                };
            }

            return result;
        }

        private static IList<MainWoViewConfigurationsColumns> ToEntity(this IList<WorkOrderSelectedViewColumnsDto> source)
        {
            return source?.MapList(e => e.ToEntity());
        }
    }
}