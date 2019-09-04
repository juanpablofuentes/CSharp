using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Helpers;

namespace Group.Salto.ServiceLibrary.Extensions
{
   public static class WorkOrderDetailDtoExtensions
    {
        public static WorkOrderDetailDto ToDetailDto(this WorkOrders source)
        {
            WorkOrderDetailDto result = null;
            if(source != null)
            {
                result = new WorkOrderDetailDto
                {
                    Id = source.Id,
                    InternalIdentifier = source.InternalIdentifier,
                    WorkOrderStatus = source.WorkOrderStatus.Name,
                    ExternalWorOrderStatus = source.ExternalWorOrderStatus?.Name,
                    Queue = source.Queue?.Name,
                    Project = source.Project?.Name,
                    WorkOrderCategory = source.WorkOrderCategory?.Name,
                    WorkOrderCategoryURL = source.WorkOrderCategory?.Url,
                    FinalClient = source.FinalClient?.Name,
                    LocationCode = source.Location?.Code,
                    LocationName = source.Location?.Name,
                    LocationPhone = source.Location?.Phone1,
                    LocationAddress = source.Location?.Street,
                    PeopleResponsibleName = source.PeopleResponsible?.FullName,
                    PeopleResponsiblePhone = source.PeopleResponsible?.Telephone,
                    BrandURL = source.Asset?.Model?.Brand?.Url,
                    ActionDate = source.ActionDate.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.ActionDate.Value).ToString() : string.Empty,
                    ResolutionDateSla = source.ResolutionDateSla.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.ResolutionDateSla.Value).ToString() : string.Empty,
                    TextRepair = source.TextRepair,
                    Observations = source.Observations,
                    ExpectedWorkedTime = source.WorkOrderAnalysis?.ExpectedTimeWorked,
                    TotalWorkedTime = source.WorkOrderAnalysis?.TotalWorkedTime,
                    ExpectedVSTotalTime = source.WorkOrderAnalysis?.ExpectedvsWorkedTime,
                    TravelTime = source.WorkOrderAnalysis?.TravelTime,
                    WaitTime = source.WorkOrderAnalysis?.WaitTime,
                    OnSiteTime = source.WorkOrderAnalysis?.OnSiteTime,
                    Km = source.WorkOrderAnalysis?.Kilometers,
                    ShowHeader = source.WorkOrderAnalysis != null,
                    StatusColor = source.WorkOrderStatus?.Color,
                    AssetId = source.AssetId.HasValue ? source.AssetId.Value : 0,
                    FatherId = source.WorkOrdersFatherId.HasValue ? source.WorkOrdersFatherId.Value : 0,
                    ReferenceOtherServices = source.ReferenceOtherServices,
                    GeneratedService = source.ReferenceGeneratorService,
                    GeneratedServiceId = source.GeneratorServiceId.HasValue ? source.GeneratorServiceId.Value : 0,
                };
            }
            return result;
        }
    }
}