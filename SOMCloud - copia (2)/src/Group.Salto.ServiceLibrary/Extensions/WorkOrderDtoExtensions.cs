using System;
using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.WorkOrder;
using Group.Salto.ServiceLibrary.Helpers;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderDtoExtensions
    {
        public static DateTime EPOCH = new DateTime(1970, 1, 1);

        public static WorkOrderBasicInfoDto ToBasicInfoDto(this WorkOrders dbModel)
        {
            var dto = new WorkOrderBasicInfoDto
            {
                Id = dbModel.Id,
                ClientId = dbModel.InternalIdentifier,
                Site = dbModel.Location?.Name,
                State = dbModel.WorkOrderStatus?.Name,
                Color = dbModel.WorkOrderStatus?.Color,
                Category = dbModel.WorkOrderCategory?.Name,
                Type = dbModel.WorkOrderTypes?.Name,
                ActionDate = dbModel.ActionDate,
                City = dbModel.Location?.City,
                ResolutionDateSla = dbModel.ResolutionDateSla,
                Equipment = $"{dbModel.Asset?.Name} {dbModel.Asset?.AssetNumber} {dbModel.Asset?.SerialNumber}",
                LocationId = dbModel.LocationId,
                Location = dbModel.Location?.ToDto(),
                IsWoClosed = dbModel.WorkOrderStatus?.IsWoclosed != null && dbModel.WorkOrderStatus.IsWoclosed.Value,
                TextRepair = dbModel.TextRepair,
                Sla = dbModel.WorkOrderCategory?.Sla?.ToSlaBasicInfoDto()
            };

            return dto;
        }

        public static IEnumerable<WorkOrderBasicInfoDto> ToBasicInfoDto(this IEnumerable<WorkOrders> dbModelList)
        {
            var dtoList = new List<WorkOrderBasicInfoDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToBasicInfoDto());
            }

            return dtoList;
        }

        public static WorkOrderFullInfoDto ToFullInfoDto(this WorkOrders dbModel)
        {
            var dto = new WorkOrderFullInfoDto
            {
                Id = dbModel.Id,
                WorkOrderCategory = dbModel.WorkOrderCategory?.ToListDto(),
                WorkOrderStatus = dbModel.WorkOrderStatus?.ToDto(),
                Queue = dbModel.Queue?.ToDto(),
                ActionDate = dbModel.ActionDate,
                Observations = dbModel.Observations,
                ResolutionDateSla = dbModel.ResolutionDateSla,
                InternalIdentifier = dbModel.InternalIdentifier,
                AccountingClosingDate = dbModel.AccountingClosingDate,
                ActuationEndDate = dbModel.ActuationEndDate,
                Archived = dbModel.Archived,
                AssignmentTime = dbModel.AssignmentTime,
                Billable = dbModel.Billable,
                ClientClosingDate = dbModel.ClientClosingDate,
                ClosingOtdate = dbModel.ClosingOtdate,
                CreationDate = dbModel.CreationDate,
                DatePenaltyWithoutResolutionSla = dbModel.DatePenaltyWithoutResolutionSla,
                DateStopTimerSla = dbModel.DateStopTimerSla,
                DateUnansweredPenaltySla = dbModel.DateUnansweredPenaltySla,
                ExternalIdentifier = dbModel.ExternalIdentifier,
                ExternalWorkOrderStatus = dbModel.ExternalWorOrderStatus?.ToDto(),
                FinalClientClosingTime = dbModel.FinalClientClosingTime,
                InternalClosingTime = dbModel.InternalClosingTime,
                InternalCreationDate = dbModel.InternalCreationDate,
                IsActuationDateFixed = dbModel.IsActuationDateFixed,
                IsResponsibleFixed = dbModel.IsResponsibleFixed,
                LastModified = dbModel.LastModified,
                PickUpTime = dbModel.PickUpTime,
                ReferenceGeneratorService = dbModel.ReferenceGeneratorService,
                ReferenceOtherServices = dbModel.ReferenceOtherServices,
                ResponseDateSla = dbModel.ResponseDateSla,
                SystemDateWhenOtclosed = dbModel.SystemDateWhenOtclosed,
                TextRepair = dbModel.TextRepair,
                PeopleResponsible = dbModel.PeopleResponsible?.ToDto(),
                ClientSite = dbModel.Location?.ToDto(),
                Equipment = dbModel.Asset?.ToDto(),
                WorkOrderTypes = dbModel.WorkOrderTypes?.ToWorkOrderFatherListDto(),
                ClientSiteName = dbModel.FinalClient?.Name,
                WoServices = dbModel.Services?.ToWoServiceDto(),
                BackOfficeResponsiblePhone = !string.IsNullOrWhiteSpace(dbModel.WorkOrderCategory?.BackOfficeResponsible) ? dbModel.WorkOrderCategory.BackOfficeResponsible : dbModel.Project?.BackOfficeResponsible,
                TechnicalResponsiblePhone = !string.IsNullOrWhiteSpace(dbModel.WorkOrderCategory?.TechnicalResponsible) ? dbModel.WorkOrderCategory.TechnicalResponsible : dbModel.Project?.TechnicalResponsible,
            };
            return dto;
        }

        public static IEnumerable<WorkOrderFullInfoDto> ToFullInfoDto(this IEnumerable<WorkOrders> dbModelList)
        {
            var dtoList = new List<WorkOrderFullInfoDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToFullInfoDto());
            }

            return dtoList;
        }

        public static WorkOrderEditDto ToEditDto(this WorkOrders source)
        {
            WorkOrderEditDto result = null;
            if (source != null)
            {
                result = new WorkOrderEditDto()
                {
                    Id = source.Id,
                    ProjectId = source.ProjectId,
                    ProjectName = source.Project?.Name,
                    WorkOrderCategoryId = source.WorkOrderCategoryId,
                    WorkOrderTypesId = source.WorkOrderTypesId.HasValue ? source.WorkOrderTypesId.Value : 0,
                    TextRepair = source.TextRepair,
                    Observations = source.Observations,
                    OriginId = source.OriginId,
                    QueueId = source.QueueId,
                    WorkOrderStatusId = source.WorkOrderStatusId,
                    ExternalWorOrderStatusId = source.ExternalWorOrderStatusId.HasValue ? source.ExternalWorOrderStatusId.Value : 0,
                    TechnicianId = source.PeopleResponsibleId.HasValue ? source.PeopleResponsibleId.Value : 0,
                    TechnicianName = source.PeopleResponsibleId.HasValue ? source.PeopleResponsible?.FullName : string.Empty,
                    IsResponsibleFixed = source.IsResponsibleFixed,
                    ClientSiteId = source.FinalClientId,
                    ClientSiteName = source.FinalClient?.Name,
                    SiteId = source.LocationId,
                    SiteName = source.Location?.Name,
                    AssetId = source.AssetId.HasValue ? source.AssetId.Value : 0,
                    AssetName = source.AssetId.HasValue ? source.Asset?.SerialNumber : string.Empty,
                    UserSiteId = source.SiteUserId.HasValue ? source.SiteUserId.Value : 0,
                    UserSiteName = source.SiteUserId.HasValue ? source.SiteUser?.Name : string.Empty,
                    InternalIdentifier = source.InternalIdentifier,
                    ClientSiteWorkOrder = source.ExternalIdentifier,
                    CreationDate = DateTimeZoneHelper.ToLocalTimeByUser(source.CreationDate).ToString(),
                    PickUpTime = source.PickUpTime.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.PickUpTime.Value).ToString() : string.Empty,
                    AssignmentTime = source.AssignmentTime.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.AssignmentTime.Value).ToString() : string.Empty,
                    ActuationDate = source.ActionDate.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.ActionDate.Value).ToString() : string.Empty,
                    ActuationEndDate = source.ActuationEndDate.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.ActuationEndDate.Value).ToString() : string.Empty,
                    FinalClientClosingTime = source.FinalClientClosingTime.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.FinalClientClosingTime.Value).ToString() : string.Empty,
                    InternalClosingTime = source.InternalClosingTime.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.InternalClosingTime.Value).ToString() : string.Empty,
                    IsActuationDateFixed = source.IsActuationDateFixed,
                    ResponseDateSla = source.ResponseDateSla.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.ResponseDateSla.Value).ToString() : string.Empty,
                    ResolutionDateSla = source.ResolutionDateSla.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.ResolutionDateSla.Value).ToString() : string.Empty,
                    DateUnansweredPenaltySla = source.DateUnansweredPenaltySla.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.DateUnansweredPenaltySla.Value).ToString() : string.Empty,
                    DatePenaltyWithoutResolutionSla = source.DatePenaltyWithoutResolutionSla.HasValue ? DateTimeZoneHelper.ToLocalTimeByUser(source.DatePenaltyWithoutResolutionSla.Value).ToString() : string.Empty,
                };
            }
            return result;
        }

        public static WorkOrders ToEntity(this WorkOrderEditDto source)
        {
            WorkOrders result = null;
            if (source != null)
            {
                result = new WorkOrders()
                {
                    CreationDate = !string.IsNullOrEmpty(source.CreationDate) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.CreationDate)) : DateTime.UtcNow,
                    InternalCreationDate = DateTime.UtcNow,
                    PickUpTime = !string.IsNullOrEmpty(source.PickUpTime) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.PickUpTime)) : DateTime.UtcNow,
                    AssignmentTime = !string.IsNullOrEmpty(source.AssignmentTime) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.AssignmentTime)) : DateTime.UtcNow,
                    GeneratorServiceId = source.GeneratorServiceId > 0 ? (int?)source.GeneratorServiceId : null,
                    ReferenceGeneratorService = source.RefGeneratorService ?? false,
                    ReferenceOtherServices = source.RefOtherServices ?? false,
                    PeopleIntroducedById = source.PeopleIntroducedById,
                    ProjectId = source.ProjectId,
                    WorkOrderCategoryId = source.WorkOrderCategoryId,
                    WorkOrderTypesId = source.WorkOrderTypesId != 0 ? source.WorkOrderTypesId : (int?)null,
                    TextRepair = source.TextRepair,
                    Observations = source.Observations,
                    OriginId = source.OriginId,
                    QueueId = source.QueueId,
                    WorkOrderStatusId = source.WorkOrderStatusId,
                    ExternalWorOrderStatusId = source.ExternalWorOrderStatusId != 0 ? source.ExternalWorOrderStatusId : (int?)null,
                    PeopleResponsibleId = source.TechnicianId != 0 ? source.TechnicianId : (int?)null,
                    IsResponsibleFixed = source.IsResponsibleFixed,
                    FinalClientId = source.ClientSiteId,
                    LocationId = source.SiteId,
                    AssetId = source.AssetId != 0 ? source.AssetId : (int?)null,
                    SiteUserId = source.UserSiteId != 0 ? source.UserSiteId : (int?)null,
                    InternalIdentifier = source.InternalIdentifier,
                    ExternalIdentifier = source.ClientSiteWorkOrder,
                    ActionDate = !string.IsNullOrEmpty(source.ActuationDate) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.ActuationDate)) : (DateTime?)null,
                    ActuationEndDate = !string.IsNullOrEmpty(source.ActuationEndDate) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.ActuationEndDate)) : (DateTime?)null,
                    FinalClientClosingTime = !string.IsNullOrEmpty(source.FinalClientClosingTime) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.FinalClientClosingTime)) : (DateTime?)null,
                    InternalClosingTime = !string.IsNullOrEmpty(source.InternalClosingTime) ? DateTimeZoneHelper.ToUtcByUser(Convert.ToDateTime(source.InternalClosingTime)) : (DateTime?)null,
                    IsActuationDateFixed = source.IsActuationDateFixed,
                    ResponseDateSla = !string.IsNullOrEmpty(source.ResponseDateSla) ? Convert.ToDateTime(source.ResponseDateSla).ToUniversalTimeConvert() : (DateTime?)null,
                    ResolutionDateSla = !string.IsNullOrEmpty(source.ResolutionDateSla) ? Convert.ToDateTime(source.ResolutionDateSla).ToUniversalTimeConvert() : (DateTime?)null,
                    DateUnansweredPenaltySla = !string.IsNullOrEmpty(source.DateUnansweredPenaltySla) ? Convert.ToDateTime(source.DateUnansweredPenaltySla).ToUniversalTimeConvert() : (DateTime?)null,
                    DatePenaltyWithoutResolutionSla = !string.IsNullOrEmpty(source.DatePenaltyWithoutResolutionSla) ? Convert.ToDateTime(source.DatePenaltyWithoutResolutionSla).ToUniversalTimeConvert() : (DateTime?)null,
                    Audits = new List<Audits>()
                };
            }
            return result;
        }

        public static void UpdateWorkOrder(this WorkOrders target, WorkOrders source)
        {
            if (source != null && target != null)
            {
                target.ProjectId = source.ProjectId;
                target.WorkOrderCategoryId = source.WorkOrderCategoryId;
                target.WorkOrderTypesId = source.WorkOrderTypesId;
                target.TextRepair = source.TextRepair;
                target.Observations = source.Observations;
                target.OriginId = source.OriginId;
                target.QueueId = source.QueueId;
                target.WorkOrderStatusId = source.WorkOrderStatusId;
                target.ExternalWorOrderStatusId = source.ExternalWorOrderStatusId;
                target.PeopleResponsibleId = source.PeopleResponsibleId;
                target.IsResponsibleFixed = source.IsResponsibleFixed;
                target.FinalClientId = source.FinalClientId;
                target.LocationId = source.LocationId;
                target.AssetId = source.AssetId;
                target.SiteUserId = source.SiteUserId;
                target.InternalIdentifier = source.InternalIdentifier;
                target.ExternalIdentifier = source.ExternalIdentifier;
                target.CreationDate = source.CreationDate;
                target.PickUpTime = source.PickUpTime;
                target.AssignmentTime = source.AssignmentTime;
                target.ActionDate = source.ActionDate;
                target.ActuationEndDate = source.ActuationEndDate;
                target.FinalClientClosingTime = source.FinalClientClosingTime;
                target.InternalClosingTime = source.InternalClosingTime;
                target.IsActuationDateFixed = source.IsActuationDateFixed;
                target.ResponseDateSla = source.ResponseDateSla;
                target.ResolutionDateSla = source.ResolutionDateSla;
                target.DateUnansweredPenaltySla = source.DateUnansweredPenaltySla;
                target.DatePenaltyWithoutResolutionSla = source.DatePenaltyWithoutResolutionSla;
                target.GeneratorServiceId = source.GeneratorServiceId;
                target.ReferenceGeneratorService = source.ReferenceGeneratorService;
                target.ReferenceOtherServices = source.ReferenceOtherServices;
            }
        }

        public static bool IsValid(this WorkOrderEditDto source)
        {
            bool result = false;
            result = source != null
                && source.WorkOrderStatusId > 0
                && source.ExternalWorOrderStatusId > 0
                && source.PeopleIntroducedById > 0
                && source.ProjectId > 0
                && source.WorkOrderCategoryId > 0
                && source.ClientSiteId > 0
                && source.SiteId > 0
                && source.QueueId > 0
                && source.OriginId > 0;
            return result;
        }

        private static DateTime ToUniversalTimeConvert(this DateTime date)
        {
            if (date != EPOCH)
            {
                return DateTimeZoneHelper.ToUtcByUser(date);
            }
            else
            {
                return date;
            }
        }
    }
}