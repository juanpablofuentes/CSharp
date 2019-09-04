using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.PerformTask
{
    public class PerformTaskViewModel : IValidatableObject
    {
         public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameFieldModel { get; set; }
         public int? QueueId { get; set; }
         public int? WorkOrderStatusId { get; set; }
         public int? ExternalWorOrderStatusId { get; set; }
         public int? PeopleTechnicianId { get; set; }
         public int? PeopleManipulatorId { get; set; }
         public int? EnterValue { get; set; }
         public float? DecimalValue { get; set; }
        public string StringValue { get; set; }
        public bool? BooleanValue { get; set; }
        public DateTime? DateValue { get; set; }
         public int? ExtraFieldId { get; set; }
        public string MailSubjectToPrepend { get; set; }
        public string MailSubscribers { get; set; }
        public bool? AllowAdditionalSubscribers { get; set; }
        public bool? PeopleResponsibleTechniciansId { get; set; }
        public string ExternalCall { get; set; }
         public int? WebServiceCallId { get; set; }
         public int? MailTemplateId { get; set; }
        public bool? SendMailToTechnician { get; set; }
        public bool? SendMailToProjectResponsible { get; set; }
        public bool? SendMailToSiteUser { get; set; }
        public string TasksTypesId { get; set; }
        public string TriggerTypesId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //TODO: walter
            return null;
        }
    }
}
