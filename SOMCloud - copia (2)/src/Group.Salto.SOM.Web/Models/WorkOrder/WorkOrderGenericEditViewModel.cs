using Group.Salto.Common.Constants;
using Group.Salto.Common.Enums;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderGenericEditViewModel : IValidatableObject
    {
        public WorkOrderGenericEditViewModel() { }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int WorkOrderCategoryId { get; set; }
        public int WorkOrderTypesId { get; set; }
        public int WorkOrderType1Id { get; set; }
        public int WorkOrderType2Id { get; set; }
        public int WorkOrderType3Id { get; set; }
        public int WorkOrderType4Id { get; set; }
        public string TextRepair { get; set; }
        public string Observations { get; set; }
        public int OriginId { get; set; }
        public IEnumerable<SelectListItem> OriginListItems { get; set; }
        public int QueueId { get; set; }
        public IEnumerable<SelectListItem> QueueListItems { get; set; }
        public int WorkOrderStatusId { get; set; }
        public IEnumerable<SelectListItem> WorkOrderStatusListItems { get; set; }
        public int ExternalWorkOrderStatusId { get; set; }
        public IEnumerable<SelectListItem> ExternalWorkOrderStatusListItems { get; set; }
        public int TechnicianId { get; set; }
        public string TechnicianName { get; set; }
        public bool IsResponibleFixed { get; set; }
        public int ClientSiteId { get; set; }
        public string ClientSiteName { get; set; }
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public int UserSiteId { get; set; }
        public string UserSiteName { get; set; }
        public string InternalIdentifier { get; set; }
        public string ClientSiteWorkOrder { get; set; }
        public string CreationDate { get; set; }
        public string PickUpTime { get; set; }
        public string AssignmentDate { get; set; }
        public string ActuationDate { get; set; }
        public string ActuationEndDate { get; set; }
        public string FinalClientClosingTime { get; set; }
        public string InternalClosingTime { get; set; }
        public bool IsActuationDateFixed { get; set; }
        public string DateResponseSLA { get; set; }
        public string DateResolutionSLA { get; set; }
        public string DateUnansweredSLAPenalty { get; set; }
        public string DateWithoutPenaltyResolutionSLA { get; set; }
        public bool DateResponseSLAVisible { get; set; }
        public bool DateResolutionSLAVisible { get; set; }
        public bool DateUnansweredSLAPenaltyVisible { get; set; }
        public bool DateWithoutPenaltyResolutionSLAVisible { get; set; }
        public bool IsDerived { get; set; }
        public int TaskId { get; set; }
        public int FlowId { get; set; }
        public bool InheritProject { get; set; }
        public bool InheritTechnician { get; set; }
        public int GeneratorServiceDuplicationPolicy { get; set; }
        public IEnumerable<SelectListItem> GeneratorServiceDuplicationPolicyItems { get; internal set; }
        public int OtherServicesDuplicationPolicy { get; set; }
        public IEnumerable<SelectListItem> OtherServicesDuplicationPolicyItems { get; internal set; }

        public ModeActionTypeEnum ModeActionType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (!IsDerived)
            {
                if (ProjectId == 0)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(ProjectId) }));
                }

                if (WorkOrderCategoryId == 0)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(WorkOrderCategoryId) }));
                }
            }

            if (string.IsNullOrEmpty(this.TextRepair))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(TextRepair) }));
            }

            if (OriginId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(OriginId) }));
            }

            if (QueueId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(QueueId) }));
            }

            if (WorkOrderStatusId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(WorkOrderStatusId) }));
            }

            if (ExternalWorkOrderStatusId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(ExternalWorkOrderStatusId) }));
            }

            if (ClientSiteId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(ClientSiteId) }));
            }

            if (SiteId == 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(SiteId) }));
            }

            if (DateResponseSLAVisible)
            {
                if (string.IsNullOrEmpty(this.DateResponseSLA))
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(DateResponseSLA) }));
                }

                if (!string.IsNullOrEmpty(DateResponseSLA))
                {
                    bool result = DateTime.TryParse(DateResponseSLA, out DateTime date);
                    if (!result)
                    {
                        errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DateTimeFormatInValid), new[] { nameof(DateResponseSLA) }));
                    }
                }
            }

            if (DateResolutionSLAVisible)
            {
                if (string.IsNullOrEmpty(this.DateResolutionSLA))
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(DateResolutionSLA) }));
                }

                if (!string.IsNullOrEmpty(DateResolutionSLA))
                {
                    bool result = DateTime.TryParse(DateResolutionSLA, out DateTime date);
                    if (!result)
                    {
                        errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DateTimeFormatInValid), new[] { nameof(DateResolutionSLA) }));
                    }
                }
            }

            if (DateUnansweredSLAPenaltyVisible)
            {
                if (string.IsNullOrEmpty(this.DateUnansweredSLAPenalty))
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(DateUnansweredSLAPenalty) }));
                }

                if (!string.IsNullOrEmpty(DateUnansweredSLAPenalty))
                {
                    bool result = DateTime.TryParse(DateUnansweredSLAPenalty, out DateTime date);
                    if (!result)
                    {
                        errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DateTimeFormatInValid), new[] { nameof(DateUnansweredSLAPenalty) }));
                    }
                }
            }

            if (DateWithoutPenaltyResolutionSLAVisible)
            {
                if (string.IsNullOrEmpty(this.DateWithoutPenaltyResolutionSLA))
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(DateWithoutPenaltyResolutionSLA) }));
                }

                if (!string.IsNullOrEmpty(DateWithoutPenaltyResolutionSLA))
                {
                    bool result = DateTime.TryParse(DateWithoutPenaltyResolutionSLA, out DateTime date);
                    if (!result)
                    {
                        errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DateTimeFormatInValid), new[] { nameof(DateWithoutPenaltyResolutionSLA) }));
                    }
                }
            }

            if (!string.IsNullOrEmpty(CreationDate))
            {
                bool result = DateTime.TryParse(CreationDate, out DateTime date);
                if (!result)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DateTimeFormatInValid), new[] { nameof(CreationDate) }));
                }
            }

            if (!string.IsNullOrEmpty(PickUpTime))
            {
                bool result = DateTime.TryParse(PickUpTime, out DateTime date);
                if (!result)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DateTimeFormatInValid), new[] { nameof(PickUpTime) }));
                }
            }

            if (!string.IsNullOrEmpty(AssignmentDate))
            {
                bool result = DateTime.TryParse(AssignmentDate, out DateTime date);
                if (!result)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DateTimeFormatInValid), new[] { nameof(AssignmentDate) }));
                }
            }

            if (!string.IsNullOrEmpty(ActuationDate))
            {
                bool result = DateTime.TryParse(ActuationDate, out DateTime date);
                if (!result)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DateTimeFormatInValid), new[] { nameof(ActuationDate) }));
                }
            }

            if (!string.IsNullOrEmpty(ActuationEndDate))
            {
                bool result = DateTime.TryParse(ActuationEndDate, out DateTime date);
                if (!result)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DateTimeFormatInValid), new[] { nameof(ActuationEndDate) }));
                }
            }

            if (!string.IsNullOrEmpty(FinalClientClosingTime))
            {
                bool result = DateTime.TryParse(FinalClientClosingTime, out DateTime date);
                if (!result)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DateTimeFormatInValid), new[] { nameof(FinalClientClosingTime) }));
                }
            }

            if (!string.IsNullOrEmpty(InternalClosingTime))
            {
                bool result = DateTime.TryParse(InternalClosingTime, out DateTime date);
                if (!result)
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.DateTimeFormatInValid), new[] { nameof(InternalClosingTime) }));
                }
            }

            return errors;
        }
    }
}