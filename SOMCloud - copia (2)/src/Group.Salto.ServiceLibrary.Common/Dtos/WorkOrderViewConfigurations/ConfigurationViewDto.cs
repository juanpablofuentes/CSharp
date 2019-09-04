using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations
{
    public class ConfigurationViewDto : WorkOrderViewConfigurationsDto
    {
        public ConfigurationViewDto()
        {
            SelectedColumns = new List<WorkOrderSelectedViewColumnsDto>();
            TechnicianValues = new List<int>();
            GroupsValues = new List<int>();
        }

        public IList<WorkOrderColumnsDto> AvailableColumns { get; set; }
        public IList<WorkOrderSelectedViewColumnsDto> SelectedColumns { get; set; }
        public IList<MultiSelectItemDto> Technician { get; set; }
        public IList<MultiSelectItemDto> Groups { get; set; }
        public List<int> TechnicianValues { get; set; }
        public List<int> GroupsValues { get; set; }
        public int LanguageId { get; set; }
    }
}