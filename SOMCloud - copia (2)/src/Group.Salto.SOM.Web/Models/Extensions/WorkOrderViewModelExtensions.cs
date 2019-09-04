using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderDerivated;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderViewModelExtensions
    {
        public static WorkOrderEditViewModel ToEditViewModel(this WorkOrderEditDto source)
        {
            WorkOrderEditViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderEditViewModel();
                result.WorkOrderGenericEditViewModel = new WorkOrderGenericEditViewModel()
                {
                    Id = source.Id,
                    ProjectId = source.ProjectId,
                    ProjectName = source.ProjectName,
                    WorkOrderCategoryId = source.WorkOrderCategoryId,
                    WorkOrderTypesId = source.WorkOrderTypesId,
                    TextRepair = source.TextRepair,
                    Observations = source.Observations,
                    OriginId = source.OriginId,
                    QueueId = source.QueueId,
                    WorkOrderStatusId = source.WorkOrderStatusId,
                    ExternalWorkOrderStatusId = source.ExternalWorOrderStatusId,
                    TechnicianId = source.TechnicianId,
                    TechnicianName = source.TechnicianName,
                    IsResponibleFixed = source.IsResponsibleFixed,
                    ClientSiteId = source.ClientSiteId,
                    ClientSiteName = source.ClientSiteName,
                    SiteId = source.SiteId,
                    SiteName = source.SiteName,
                    AssetId = source.AssetId,
                    AssetName = source.AssetName,
                    UserSiteId = source.UserSiteId,
                    UserSiteName = source.UserSiteName,
                    InternalIdentifier = source.InternalIdentifier,
                    ClientSiteWorkOrder = source.ClientSiteWorkOrder,
                    CreationDate = source.CreationDate,
                    PickUpTime = source.PickUpTime,
                    AssignmentDate = source.AssignmentTime,
                    ActuationDate = source.ActuationDate,
                    ActuationEndDate = source.ActuationEndDate,
                    FinalClientClosingTime = source.FinalClientClosingTime,
                    InternalClosingTime = source.InternalClosingTime,
                    IsActuationDateFixed = source.IsActuationDateFixed,
                    DateResponseSLA = source.ResponseDateSla,
                    DateResolutionSLA = source.ResolutionDateSla,
                    DateUnansweredSLAPenalty = source.DateUnansweredPenaltySla,
                    DateWithoutPenaltyResolutionSLA = source.DatePenaltyWithoutResolutionSla
                };
            }
            return result;
        }

        public static ResultViewModel<WorkOrderEditViewModel> ToEditViewModel(this ResultDto<WorkOrderEditDto> source)
        {
            var response = source != null ? new ResultViewModel<WorkOrderEditViewModel>()
            {
                Data = source.Data.ToEditViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static WorkOrderEditDto ToEditDto(this WorkOrderEditViewModel source, int userConfigurationId, string customerId)
        {
            WorkOrderEditDto target = null;
            if (source != null)
            {
                target = new WorkOrderEditDto();
                source.ToEditDto(target);
                target.UserConfigurationId = userConfigurationId;
                target.CustomerId = new Guid(customerId);
            }
            return target;
        }

        public static WorkOrderDerivatedDto ToEditDerivatedDto(this WorkOrderEditViewModel source, int userConfigurationId, string customerId)
        {
            WorkOrderDerivatedDto target = null;
            if (source != null)
            {
                target = new WorkOrderDerivatedDto();
                source.ToEditDto(target);
                target.UserConfigurationId = userConfigurationId;
                target.CustomerId = new Guid(customerId);
                target.TaskId = source.WorkOrderGenericEditViewModel.TaskId;
                target.InheritProject = source.WorkOrderGenericEditViewModel.InheritProject;
                target.InheritTechnician = source.WorkOrderGenericEditViewModel.InheritTechnician;
                target.GeneratorServiceDuplicationPolicy = source.WorkOrderGenericEditViewModel.GeneratorServiceDuplicationPolicy;
                target.OtherServicesDuplicationPolicy = source.WorkOrderGenericEditViewModel.OtherServicesDuplicationPolicy;
            }
            return target;
        }

        public static WorkOrderEditViewModel ToEditDerivatedViewModel(this WorkOrderDerivatedDto source)
        {
            WorkOrderEditViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderEditViewModel
                {
                    WorkOrderGenericEditViewModel = new WorkOrderGenericEditViewModel()
                    {
                        Id = source.Id,
                        ProjectId = source.ProjectId,
                        ProjectName = source.ProjectName,
                        WorkOrderCategoryId = source.WorkOrderCategoryId,
                        WorkOrderTypesId = source.WorkOrderTypesId,
                        TextRepair = source.TextRepair,
                        Observations = source.Observations,
                        OriginId = source.OriginId,
                        QueueId = source.QueueId,
                        WorkOrderStatusId = source.WorkOrderStatusId,
                        ExternalWorkOrderStatusId = source.ExternalWorOrderStatusId,
                        TechnicianId = source.TechnicianId,
                        TechnicianName = source.TechnicianName,
                        IsResponibleFixed = source.IsResponsibleFixed,
                        ClientSiteId = source.ClientSiteId,
                        ClientSiteName = source.ClientSiteName,
                        SiteId = source.SiteId,
                        SiteName = source.SiteName,
                        AssetId = source.AssetId,
                        AssetName = source.AssetName,
                        UserSiteId = source.UserSiteId,
                        UserSiteName = source.UserSiteName,
                        InternalIdentifier = source.InternalIdentifier,
                        ClientSiteWorkOrder = source.ClientSiteWorkOrder,
                        CreationDate = source.CreationDate,
                        PickUpTime = source.PickUpTime,
                        AssignmentDate = source.AssignmentTime,
                        ActuationDate = source.ActuationDate,
                        ActuationEndDate = source.ActuationEndDate,
                        FinalClientClosingTime = source.FinalClientClosingTime,
                        InternalClosingTime = source.InternalClosingTime,
                        IsActuationDateFixed = source.IsActuationDateFixed,
                        DateResponseSLA = source.ResponseDateSla,
                        DateResolutionSLA = source.ResolutionDateSla,
                        DateUnansweredSLAPenalty = source.DateUnansweredPenaltySla,
                        DateWithoutPenaltyResolutionSLA = source.DatePenaltyWithoutResolutionSla
                    }
                };
            }
            return result;
        }

        public static ResultViewModel<WorkOrderEditViewModel> ToEditDerivatedViewModel(this ResultDto<WorkOrderDerivatedDto> source)
        {
            var response = source != null ? new ResultViewModel<WorkOrderEditViewModel>()
            {
                Data = source.Data.ToEditDerivatedViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static WorkOrderEditViewModel ToEditViewModel(this WorkOrderDerivatedDto source)
        {
            WorkOrderEditViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderEditViewModel();
                result.WorkOrderGenericEditViewModel = new WorkOrderGenericEditViewModel()
                {
                    Id = source.Id,
                    ProjectId = source.ProjectId,
                    ProjectName = source.ProjectName,
                    WorkOrderCategoryId = source.WorkOrderCategoryId,
                    WorkOrderTypesId = source.WorkOrderTypesId,
                    TextRepair = source.TextRepair,
                    Observations = source.Observations,
                    OriginId = source.OriginId,
                    QueueId = source.QueueId,
                    WorkOrderStatusId = source.WorkOrderStatusId,
                    ExternalWorkOrderStatusId = source.ExternalWorOrderStatusId,
                    TechnicianId = source.TechnicianId,
                    TechnicianName = source.TechnicianName,
                    IsResponibleFixed = source.IsResponsibleFixed,
                    ClientSiteId = source.ClientSiteId,
                    ClientSiteName = source.ClientSiteName,
                    SiteId = source.SiteId,
                    SiteName = source.SiteName,
                    AssetId = source.AssetId,
                    AssetName = source.AssetName,
                    UserSiteId = source.UserSiteId,
                    UserSiteName = source.UserSiteName,
                    InternalIdentifier = source.InternalIdentifier,
                    ClientSiteWorkOrder = source.ClientSiteWorkOrder,
                    CreationDate = source.CreationDate,
                    PickUpTime = source.PickUpTime,
                    AssignmentDate = source.AssignmentTime,
                    ActuationDate = source.ActuationDate,
                    ActuationEndDate = source.ActuationEndDate,
                    FinalClientClosingTime = source.FinalClientClosingTime,
                    InternalClosingTime = source.InternalClosingTime,
                    IsActuationDateFixed = source.IsActuationDateFixed,
                    DateResponseSLA = source.ResponseDateSla,
                    DateResolutionSLA = source.ResolutionDateSla,
                    DateUnansweredSLAPenalty = source.DateUnansweredPenaltySla,
                    DateWithoutPenaltyResolutionSLA = source.DatePenaltyWithoutResolutionSla,
                    TaskId = source.TaskId,
                    InheritProject = source.InheritProject,
                    InheritTechnician = source.InheritTechnician,
                    GeneratorServiceDuplicationPolicy = source.GeneratorServiceDuplicationPolicy,
                    OtherServicesDuplicationPolicy = source.OtherServicesDuplicationPolicy
                };
            }
            return result;
        }

        public static ResultViewModel<WorkOrderEditViewModel> ToEditViewModel(this ResultDto<WorkOrderDerivatedDto> source)
        {
            var response = source != null ? new ResultViewModel<WorkOrderEditViewModel>()
            {
                Data = source.Data.ToEditViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        private static void ToEditDto(this WorkOrderEditViewModel source, WorkOrderEditDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.WorkOrderGenericEditViewModel.Id;
                target.ProjectId = source.WorkOrderGenericEditViewModel.ProjectId;
                target.WorkOrderCategoryId = source.WorkOrderGenericEditViewModel.WorkOrderCategoryId;
                target.WorkOrderTypesId = GetWorkOrderType(source.WorkOrderGenericEditViewModel);
                target.TextRepair = source.WorkOrderGenericEditViewModel.TextRepair;
                target.Observations = source.WorkOrderGenericEditViewModel.Observations;
                target.OriginId = source.WorkOrderGenericEditViewModel.OriginId;
                target.QueueId = source.WorkOrderGenericEditViewModel.QueueId;
                target.WorkOrderStatusId = source.WorkOrderGenericEditViewModel.WorkOrderStatusId;
                target.ExternalWorOrderStatusId = source.WorkOrderGenericEditViewModel.ExternalWorkOrderStatusId;
                target.TechnicianId = source.WorkOrderGenericEditViewModel.TechnicianId;
                target.IsResponsibleFixed = source.WorkOrderGenericEditViewModel.IsResponibleFixed;
                target.ClientSiteId = source.WorkOrderGenericEditViewModel.ClientSiteId;
                target.SiteId = source.WorkOrderGenericEditViewModel.SiteId;
                target.AssetId = source.WorkOrderGenericEditViewModel.AssetId;
                target.UserSiteId = source.WorkOrderGenericEditViewModel.UserSiteId;
                target.InternalIdentifier = source.WorkOrderGenericEditViewModel.InternalIdentifier;
                target.ClientSiteWorkOrder = source.WorkOrderGenericEditViewModel.ClientSiteWorkOrder;
                target.CreationDate = source.WorkOrderGenericEditViewModel.CreationDate;
                target.PickUpTime = source.WorkOrderGenericEditViewModel.PickUpTime;
                target.AssignmentTime = source.WorkOrderGenericEditViewModel.AssignmentDate;
                target.ActuationDate = source.WorkOrderGenericEditViewModel.ActuationDate;
                target.ActuationEndDate = source.WorkOrderGenericEditViewModel.ActuationEndDate;
                target.FinalClientClosingTime = source.WorkOrderGenericEditViewModel.FinalClientClosingTime;
                target.InternalClosingTime = source.WorkOrderGenericEditViewModel.InternalClosingTime;
                target.IsActuationDateFixed = source.WorkOrderGenericEditViewModel.IsActuationDateFixed;
                target.ResponseDateSla = (source.WorkOrderGenericEditViewModel.DateResponseSLAVisible) ? source.WorkOrderGenericEditViewModel.DateResponseSLA : string.Empty;
                target.ResolutionDateSla = source.WorkOrderGenericEditViewModel.DateResolutionSLAVisible ? source.WorkOrderGenericEditViewModel.DateResolutionSLA : string.Empty;
                target.DateUnansweredPenaltySla = source.WorkOrderGenericEditViewModel.DateUnansweredSLAPenaltyVisible ? source.WorkOrderGenericEditViewModel.DateUnansweredSLAPenalty : string.Empty;
                target.DatePenaltyWithoutResolutionSla = source.WorkOrderGenericEditViewModel.DateWithoutPenaltyResolutionSLAVisible ? source.WorkOrderGenericEditViewModel.DateWithoutPenaltyResolutionSLA : string.Empty;
                target.GeneratorServiceId = 0;
                target.RefGeneratorService = false;
                target.RefOtherServices = false;
            }
        }

        private static int GetWorkOrderType(WorkOrderGenericEditViewModel workOrderGenericEditViewModel)
        {
            int WorkOrderTypesId = 0;
            if (workOrderGenericEditViewModel.WorkOrderType4Id != 0)
            {
                WorkOrderTypesId = workOrderGenericEditViewModel.WorkOrderType4Id;
            }
            else if (workOrderGenericEditViewModel.WorkOrderType3Id != 0)
            {
                WorkOrderTypesId = workOrderGenericEditViewModel.WorkOrderType3Id;
            }
            else if (workOrderGenericEditViewModel.WorkOrderType2Id != 0)
            {
                WorkOrderTypesId = workOrderGenericEditViewModel.WorkOrderType2Id;
            }
            else if (workOrderGenericEditViewModel.WorkOrderType1Id != 0)
            {
                WorkOrderTypesId = workOrderGenericEditViewModel.WorkOrderType1Id;
            }

            return WorkOrderTypesId;
        }
    }
}