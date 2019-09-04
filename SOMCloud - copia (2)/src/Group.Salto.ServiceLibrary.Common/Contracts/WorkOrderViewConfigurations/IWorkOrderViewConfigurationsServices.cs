using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderViewConfigurations
{
    public interface IWorkOrderViewConfigurationsServices
    {
        int GetDefaultConfigurationId(int userId);
        ResultDto<IList<WorkOrderViewConfigurationsDto>> GetAllViewsByUserId(int userId);
        ResultDto<ConfigurationViewDto> GetConfiguredViewById(int id, int languageId);
        ResultDto<ConfigurationViewDto> Create(ConfigurationViewDto configurationViewDto);
        ResultDto<ConfigurationViewDto> Update(ConfigurationViewDto configurationViewDto);
        ResultDto<bool> Delete(int id);
    }
}