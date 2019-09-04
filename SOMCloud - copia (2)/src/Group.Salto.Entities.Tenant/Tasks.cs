using Group.Salto.Common;
using Group.Salto.Entities.Tenant.ContentTranslations;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Tasks : BaseEntity
    {
        public int FlowId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameFieldModel { get; set; }
        public int? QueueId { get; set; }
        public int? WorkOrderStatusId { get; set; }
        public int? ExternalWorOrderStatusId { get; set; }
        public int? PeopleTechnicianId { get; set; }
        public int? PeopleManipulatorId { get; set; }
        public int? EnterValue { get; set; }
        public double? DecimalValue { get; set; }
        public string StringValue { get; set; }
        public bool? BooleanValue { get; set; }
        public DateTime? DateValue { get; set; }
        public int? PredefinedServiceId { get; set; }
        public int? ExtraFieldId { get; set; }
        public string MailSubjectToPrepend { get; set; }
        public string MailSubscribers { get; set; }
        public bool? AllowAdditionalSubscribers { get; set; }
        public int? PeopleResponsibleTechniciansId { get; set; }
        public string ExternalCall { get; set; }
        public int? WebServiceCallId { get; set; }
        public int? MailTemplateId { get; set; }
        public bool SendMailToTechnician { get; set; }
        public bool SendMailToProjectResponsible { get; set; }
        public bool SendMailToSiteUser { get; set; }
        public Guid? TasksTypesId { get; set; }
        public Guid TriggerTypesId { get; set; }

        public ExternalWorOrderStatuses ExternalWorOrderStatus { get; set; }
        public ExtraFields ExtraField { get; set; }
        public Flows Flow { get; set; }
        public MailTemplate MailTemplate { get; set; }
        public People PeopleManipulator { get; set; }
        public PeopleCollections PeopleResponsibleTechnicians { get; set; }
        public People PeopleTechnician { get; set; }
        public PredefinedServices PredefinedService { get; set; }
        public Queues Queue { get; set; }
        public WorkOrderStatuses WorkOrderStatus { get; set; }
        public TasksTypes TasksTypes{ get; set; }
        public ICollection<Audits> Audits { get; set; }
        public ICollection<Bill> Bill { get; set; }
        public ICollection<BillingLineItems> BillingLineItems { get; set; }
        public ICollection<BillingRule> BillingRule { get; set; }
        public ICollection<DerivedServices> DerivedServices { get; set; }
        public ICollection<ExternalServicesConfiguration> ExternalServicesConfiguration { get; set; }
        public ICollection<PostconditionCollections> PostconditionCollections { get; set; }
        public ICollection<Preconditions> Preconditions { get; set; }
        public ICollection<PermissionsTasks> PermissionsTasks { get; set; }
        public ICollection<TaskWebServiceCallItems> TaskWebServiceCallItems { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
        public ICollection<TasksTranslations> TasksTranslations { get; set; }
    }
}
