using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderDerivated;
using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Helpers;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderDerivatedDtoExtensions
    {
        public static WorkOrderDerivatedDto ToEditDto(this WorkOrdersDeritative source)
        {
            WorkOrderDerivatedDto result = null;
            if (source != null)
            {
                result = new WorkOrderDerivatedDto()
                {
                    Id = source.Id,
                    InternalIdentifier = source.InternalIdentifier,
                    ClientSiteWorkOrder = source.ExternalIdentifier,
                    CreationDate = source.CreationDate.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.CreationDate.Value).ToString() : string.Empty,
                    TextRepair = source.TextRepair,
                    Observations = source.Observations,
                    PickUpTime = source.PickUpTime.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.PickUpTime.Value).ToString() : string.Empty,
                    FinalClientClosingTime = source.FinalClientClosingTime.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.FinalClientClosingTime.Value).ToString() : string.Empty,
                    InternalClosingTime = source.InternalClosingTime.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.InternalClosingTime.Value).ToString() : string.Empty,
                    AssignmentTime = source.AssignmentTime.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.AssignmentTime.Value).ToString() : string.Empty,
                    ActuationDate = source.ActionDate.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.ActionDate.Value).ToString() : string.Empty,
                    TechnicianId = source.PeopleResponsibleId.HasValue ? source.PeopleResponsibleId.Value : 0,
                    TechnicianName = source.PeopleResponsibleId.HasValue ? source.PeopleResponsible?.FullName : string.Empty,
                    OriginId = source.OriginId.HasValue ? source.OriginId.Value : 0,
                    WorkOrderTypesId = source.WorkOrderTypeId.HasValue ? source.WorkOrderTypeId.Value : 0,
                    WorkOrderTypeName = source.WorkOrderType?.Name,
                    ProjectId = source.ProjectId.HasValue ? source.ProjectId.Value : 0,
                    ProjectName = source.Project?.Name,
                    QueueId = source.QueueId.HasValue ? source.QueueId.Value : 0,
                    QueueName = source.Queue?.Name,
                    WorkOrderCategoryId = source.WorkOrderCategoryId.HasValue ? source.WorkOrderCategoryId.Value : 0,
                    WorkOrderCategoryName = source.WorkOrderCategory?.Name,
                    WorkOrderStatusId = source.WorkOrderStatusId.HasValue ? source.WorkOrderStatusId.Value : 0,
                    WorkOrderStatusName = source.WorkOrderStatus?.Name,
                    ExternalWorOrderStatusId = source.ExternalWorOrderStatusId.HasValue ? source.ExternalWorOrderStatusId.Value : 0,
                    ExternalWorOrderStatusName = source.ExternalWorOrderStatus?.Name,
                    SiteId = source.LocationId.HasValue ? source.LocationId.Value : 0,
                    SiteName = source.Location?.Name,
                    ClientSiteId = source.FinalClientId.HasValue ? source.FinalClientId.Value : 0,
                    ClientSiteName = source.FinalClient?.Name,
                    AssetId = source.AssetId.HasValue ? source.AssetId.Value : 0,
                    AssetName = source.AssetId.HasValue ? source.Asset?.SerialNumber : string.Empty,
                    UserSiteId = source.SiteUserId.HasValue ? source.SiteUserId.Value : 0,
                    UserSiteName = source.SiteUserId.HasValue ? source.SiteUser?.Name : string.Empty,
                    TaskId = source.TaskId,
                    InheritProject = source.InheritProject.HasValue ? source.InheritProject.Value : false,
                    InheritTechnician = source.InheritTechnician.HasValue ? source.InheritTechnician.Value : false,
                    GeneratorServiceDuplicationPolicy = source.GeneratorServiceDuplicationPolicy,
                    OtherServicesDuplicationPolicy = source.OtherServicesDuplicationPolicy
                };
            }
            return result;
        }

        public static IList<WorkOrderDerivatedDto> ToEditDto(this IList<WorkOrdersDeritative> source)
        {
            return source?.MapList(sC => sC.ToEditDto());
        }

        public static WorkOrdersDeritative ToEntity(this WorkOrderDerivatedDto source)
        {
            WorkOrdersDeritative result = null;
            if (source != null)
            {
                result = new WorkOrdersDeritative()
                {
                    TaskId = source.TaskId,
                    InternalIdentifier = source.InternalIdentifier,
                    ExternalIdentifier = source.ClientSiteWorkOrder,
                    TextRepair = source.TextRepair,
                    Observations = source.Observations,
                    CreationDate = !string.IsNullOrEmpty(source.CreationDate) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.CreationDate)) : (DateTime?)null,
                    PickUpTime = !string.IsNullOrEmpty(source.PickUpTime) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.PickUpTime)) : (DateTime?)null,
                    FinalClientClosingTime = !string.IsNullOrEmpty(source.FinalClientClosingTime) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.FinalClientClosingTime)) : (DateTime?)null,
                    InternalClosingTime = !string.IsNullOrEmpty(source.InternalClosingTime) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.InternalClosingTime)) : (DateTime?)null,
                    AssignmentTime = !string.IsNullOrEmpty(source.AssignmentTime) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.AssignmentTime)) : (DateTime?)null,
                    ActionDate = !string.IsNullOrEmpty(source.ActuationDate) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.ActuationDate)) : (DateTime?)null,
                    PeopleResponsibleId = (!source.InheritTechnician && source.TechnicianId != 0) ? source.TechnicianId : (int?)null,
                    OriginId = source.OriginId != 0 ? source.OriginId : (int?)null,
                    PeopleIntroducedById = source.PeopleIntroducedById,
                    WorkOrderStatusId = source.WorkOrderStatusId != 0 ? source.WorkOrderStatusId : (int?)null,
                    ExternalWorOrderStatusId = source.ExternalWorOrderStatusId != 0 ? source.ExternalWorOrderStatusId : (int?)null,
                    ProjectId = (!source.InheritProject && source.ProjectId != 0) ? source.ProjectId : (int?)null,
                    WorkOrderCategoryId = (!source.InheritProject && source.WorkOrderCategoryId != 0) ? source.WorkOrderCategoryId : (int?)null,
                    WorkOrderTypeId = (!source.InheritProject && source.WorkOrderTypesId != 0) ? source.WorkOrderTypesId : (int?)null,
                    QueueId = source.QueueId != 0 ? source.QueueId : (int?)null,
                    LocationId = source.SiteId != 0 ? source.SiteId : (int?)null,
                    FinalClientId = source.ClientSiteId != 0 ? source.ClientSiteId : (int?)null,
                    AssetId = source.AssetId != 0 ? source.AssetId : (int?)null,
                    SiteUserId = source.UserSiteId != 0 ? source.UserSiteId : (int?)null,
                    InheritProject = source.InheritProject,
                    InheritTechnician = source.InheritTechnician,
                    GeneratorServiceDuplicationPolicy = source.GeneratorServiceDuplicationPolicy,
                    OtherServicesDuplicationPolicy = source.OtherServicesDuplicationPolicy,
                    PeopleManipulatorId = null,
                    PeopleResponsibleTechniciansCollectionId = null
                };
            }
            return result;
        }

        public static void UpdateWorkOrdersDeritative(this WorkOrdersDeritative target, WorkOrdersDeritative source)
        {
            if (source != null && target != null)
            {
                target.InternalIdentifier = source.InternalIdentifier;
                target.ExternalIdentifier = source.ExternalIdentifier;
                target.CreationDate = source.CreationDate;
                target.TextRepair = source.TextRepair;
                target.Observations = source.Observations;
                target.PickUpTime = source.PickUpTime;
                target.FinalClientClosingTime = source.FinalClientClosingTime;
                target.InternalClosingTime = source.InternalClosingTime;
                target.AssignmentTime = source.AssignmentTime;
                target.ActionDate = source.ActionDate;
                target.PeopleResponsibleId = source.PeopleResponsibleId;
                target.OriginId = source.OriginId;
                target.WorkOrderTypeId = source.WorkOrderTypeId;
                target.LocationId = source.LocationId;
                target.ProjectId = source.ProjectId;
                target.FinalClientId = source.FinalClientId;
                target.WorkOrderStatusId = source.WorkOrderStatusId;
                target.ExternalWorOrderStatusId = source.ExternalWorOrderStatusId;
                target.AssetId = source.AssetId;
                target.QueueId = source.QueueId;
                target.WorkOrderCategoryId = source.WorkOrderCategoryId;
                target.SiteUserId = source.SiteUserId;
                target.TaskId = source.TaskId;
                target.InheritProject = source.InheritProject;
                target.InheritTechnician = source.InheritTechnician;
                target.GeneratorServiceDuplicationPolicy = source.GeneratorServiceDuplicationPolicy;
                target.OtherServicesDuplicationPolicy = source.OtherServicesDuplicationPolicy;
            }
        }
    }
}