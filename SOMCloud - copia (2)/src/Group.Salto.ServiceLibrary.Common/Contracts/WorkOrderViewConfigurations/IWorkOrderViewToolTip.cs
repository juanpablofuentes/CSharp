using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderViewConfigurations
{
    public interface IWorkOrderViewToolTip
    {
        string GetToolTipMultiSelect(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto);
        string GetToolTipDates(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto);
    }
}