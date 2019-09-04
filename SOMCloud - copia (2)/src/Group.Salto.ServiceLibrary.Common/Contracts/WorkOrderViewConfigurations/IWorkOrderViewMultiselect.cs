using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderViewConfigurations
{
    public interface IWorkOrderViewMultiselect
    {
        ResultDto<List<MultiSelectItemDto>> GetMultiSelect(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto);
    }
}