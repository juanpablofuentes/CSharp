using Group.Salto.Common.Constants.Templates;
using Group.Salto.Common.Enums;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Contracts.Templates;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Notification;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.TemplateProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoSubscribersNotificationsTaskExecution : IWoSubscribersNotificationsTaskExecution
    {
        private readonly ITemplateProcessorService _templateProcessorService;
        private readonly INotificationFactory _notificationFactory;
        private readonly INotificationConfigurationService _notificationConfigurationServiceConfiguration;
        private readonly ITenantService _tenantService;

        public WoSubscribersNotificationsTaskExecution(ITemplateProcessorService templateProcessorService,
                                                       INotificationFactory notificationFactory,
                                                       INotificationConfigurationService notificationConfigurationServiceConfiguration,
                                                       ITenantService tenantService)
        {
            _templateProcessorService = templateProcessorService;
            _notificationFactory = notificationFactory;
            _notificationConfigurationServiceConfiguration = notificationConfigurationServiceConfiguration;
            _tenantService = tenantService;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            var subscribers = GetSubscribers(taskExecutionValues);
            if (subscribers.Any())
            {
                var subject = GetSubject(taskExecutionValues);
                var body = GetBody(taskExecutionValues);
                SendMail(subject, body, subscribers, taskExecutionValues.CustomerId);
            }
            return taskExecutionValues.Result;
        }

        private List<string> GetSubscribers(TaskExecutionValues taskExecutionValues)
        {
            var subscribers = new List<string>((taskExecutionValues.CurrentTask.MailSubscribers ?? string.Empty).Split(','));

            subscribers = subscribers.FindAll(subs => !string.IsNullOrEmpty(subs)).ToList();

            if (taskExecutionValues.CurrentTask.SendMailToTechnician)
            {
                if (taskExecutionValues.CurrentWorkOrder.PeopleResponsible != null && !string.IsNullOrWhiteSpace(taskExecutionValues.CurrentWorkOrder.PeopleResponsible.Email))
                {
                    subscribers.Add(taskExecutionValues.CurrentWorkOrder.PeopleResponsible.Email);
                }
            }

            if (taskExecutionValues.CurrentTask.SendMailToProjectResponsible)
            {
                if (taskExecutionValues.CurrentWorkOrder.Project != null && taskExecutionValues.CurrentWorkOrder.Project.PeopleProjects != null)
                {
                    var managers = taskExecutionValues.CurrentWorkOrder.Project.PeopleProjects.Where(m => m.IsManager).Select(m => m.People);
                    foreach (var manager in managers)
                    {
                        subscribers.Add(manager.Email);
                    }
                }
            }

            if (taskExecutionValues.CurrentTask.SendMailToSiteUser)
            {
                if (taskExecutionValues.CurrentWorkOrder.SiteUser != null && !string.IsNullOrWhiteSpace(taskExecutionValues.CurrentWorkOrder.SiteUser.Email))
                {
                    subscribers.Add(taskExecutionValues.CurrentWorkOrder.SiteUser.Email);
                }
            }

            return subscribers;
        }

        private string GetBody(TaskExecutionValues taskExecutionValues)
        {
            string mailBody;
            if (taskExecutionValues.CurrentTask.MailTemplate != null)
            {
                var htmlContent = WebUtility.HtmlDecode(taskExecutionValues.CurrentTask.MailTemplate.Content);
                var templateContent = new TemplateProcessorValuesDto
                {
                    Template = htmlContent,
                    WorkOrder = taskExecutionValues.CurrentWorkOrder,
                    Service = taskExecutionValues.CreatedService,
                    NullStringValue = string.Empty
                };
                htmlContent = _templateProcessorService.ProcessTemplate(templateContent);

                mailBody = htmlContent;
            }
            else
            {
                var woType = string.Empty;
                var currentType = taskExecutionValues.CurrentWorkOrder.WorkOrderTypes;
                while (currentType != null)
                {
                    woType = TemplateMailConstants.TemplateClose + currentType.Name + TemplateMailConstants.TemplateSpace + woType;
                    currentType = currentType.WorkOrderTypesFather;
                }
                if (woType.StartsWith(TemplateMailConstants.TemplateCloseEnd))
                {
                    woType = woType.Substring(2);
                }
                var content = new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(TemplateMailConstants.WorkOrderId, taskExecutionValues.CurrentWorkOrder.Id.ToString()),
                    new Tuple<string, string>(TemplateMailConstants.InternalId, taskExecutionValues.CurrentWorkOrder.InternalIdentifier),
                    new Tuple<string, string>(TemplateMailConstants.ExternalId, taskExecutionValues.CurrentWorkOrder.ExternalIdentifier),
                    new Tuple<string, string>(TemplateMailConstants.ParentWoId, taskExecutionValues.CurrentWorkOrder.WorkOrdersFatherId != null ? taskExecutionValues.CurrentWorkOrder.WorkOrdersFatherId.ToString() : string.Empty),
                    new Tuple<string, string>(TemplateMailConstants.Project, taskExecutionValues.CurrentWorkOrder.Project?.Name),
                    new Tuple<string, string>(TemplateMailConstants.WoType, woType),
                    new Tuple<string, string>(TemplateMailConstants.WorkOrderCategory, taskExecutionValues.CurrentWorkOrder.WorkOrderCategory?.Name),
                    new Tuple<string, string>(TemplateMailConstants.CreationDate, taskExecutionValues.CurrentWorkOrder.CreationDate.ToString(TemplateProcessorConstants.TemplateStringDateFormat)),
                    new Tuple<string, string>(TemplateMailConstants.SlaEndDate, taskExecutionValues.CurrentWorkOrder.ResolutionDateSla != null ? taskExecutionValues.CurrentWorkOrder.ResolutionDateSla.Value.ToString(TemplateProcessorConstants.TemplateStringDateFormat) : string.Empty),
                    new Tuple<string, string>(TemplateMailConstants.ClientClosingDate, taskExecutionValues.CurrentWorkOrder.ClientClosingDate != null ? taskExecutionValues.CurrentWorkOrder.ClientClosingDate.Value.ToString(TemplateProcessorConstants.TemplateStringDateFormat) : string.Empty),
                    new Tuple<string, string>(TemplateMailConstants.Queue, taskExecutionValues.CurrentWorkOrder.Queue?.Name),
                    new Tuple<string, string>(TemplateMailConstants.ActuationDate, taskExecutionValues.CurrentWorkOrder.ActionDate != null ? taskExecutionValues.CurrentWorkOrder.ActionDate.Value.ToString(TemplateProcessorConstants.TemplateStringDateFormat) : string.Empty),
                    new Tuple<string, string>(TemplateMailConstants.WoStatus, taskExecutionValues.CurrentWorkOrder.WorkOrderStatus?.Name ?? string.Empty),
                    new Tuple<string, string>(TemplateMailConstants.ExternalWoStatus, taskExecutionValues.CurrentWorkOrder.ExternalWorOrderStatus?.Name ?? string.Empty),
                    new Tuple<string, string>(TemplateMailConstants.SaltoClient, taskExecutionValues.CurrentWorkOrder.Project?.Contract?.Client?.CorporateName),
                    new Tuple<string, string>(TemplateMailConstants.SiteClient, taskExecutionValues.CurrentWorkOrder.FinalClient?.Name),
                    new Tuple<string, string>(TemplateMailConstants.Site, taskExecutionValues.CurrentWorkOrder.Location?.Name),
                    new Tuple<string, string>(TemplateMailConstants.Address, GetLocationString(taskExecutionValues.CurrentWorkOrder.Location)),
                    new Tuple<string, string>(TemplateMailConstants.Phone1, taskExecutionValues.CurrentWorkOrder.Location?.Phone1),
                    new Tuple<string, string>(TemplateMailConstants.Phone2, taskExecutionValues.CurrentWorkOrder.Location?.Phone2),
                    new Tuple<string, string>(TemplateMailConstants.Phone3, taskExecutionValues.CurrentWorkOrder.Location?.Phone3),
                    new Tuple<string, string>(TemplateMailConstants.Asset, GetAssetName(taskExecutionValues.CurrentWorkOrder.Asset)),
                    new Tuple<string, string>(TemplateMailConstants.AssetUbication, taskExecutionValues.CurrentWorkOrder.Asset?.Location?.Name?? taskExecutionValues.CurrentWorkOrder.Asset?.LocationClient.Name),
                    new Tuple<string, string>(TemplateMailConstants.AssetAddress, GetLocationString(taskExecutionValues.CurrentWorkOrder.Asset?.Location?? taskExecutionValues.CurrentWorkOrder.Asset?.LocationClient)),
                    new Tuple<string, string>(TemplateMailConstants.Operator, taskExecutionValues.CurrentWorkOrder.PeopleManipulator?.NameSurname),
                    new Tuple<string, string>(TemplateMailConstants.Technician, taskExecutionValues.CurrentWorkOrder.PeopleResponsible?.NameSurname),
                    new Tuple<string, string>(TemplateMailConstants.Reparation, taskExecutionValues.CurrentWorkOrder.TextRepair),
                    new Tuple<string, string>(TemplateMailConstants.Observations, taskExecutionValues.CurrentWorkOrder.Observations)
                };
                mailBody = _templateProcessorService.GetHtmlContent(content);
            }
            return mailBody;
        }

        private string GetSubject(TaskExecutionValues taskExecutionValues)
        {
            string subject;
            if (taskExecutionValues.CurrentTask.MailTemplate != null)
            {
                var htmlSubject = WebUtility.HtmlDecode(taskExecutionValues.CurrentTask.MailTemplate.Subject);
                var templateSubject = new TemplateProcessorValuesDto
                {
                    Template = htmlSubject,
                    WorkOrder = taskExecutionValues.CurrentWorkOrder,
                    Service = taskExecutionValues.CreatedService,
                    NullStringValue = string.Empty
                };
                htmlSubject = _templateProcessorService.ProcessTemplate(templateSubject);

                subject = (!string.IsNullOrWhiteSpace(taskExecutionValues.CurrentTask.MailSubjectToPrepend) ?
                    taskExecutionValues.CurrentTask.MailSubjectToPrepend + TemplateMailConstants.TemplateSpace : string.Empty) + Regex.Replace(htmlSubject, TemplateMailConstants.MailSubjectRegEx, string.Empty);
            }
            else
            {
                subject = taskExecutionValues.CurrentTask.MailSubjectToPrepend != null ? taskExecutionValues.CurrentTask.MailSubjectToPrepend + TemplateMailConstants.TemplateSpace : string.Empty;
                subject += TemplateMailConstants.TemplateWoStart + taskExecutionValues.CurrentWorkOrder.Id;
                if (!string.IsNullOrEmpty(taskExecutionValues.CurrentWorkOrder.InternalIdentifier))
                {
                    subject += TemplateMailConstants.TemplateWoClient + taskExecutionValues.CurrentWorkOrder.InternalIdentifier;
                }
                if (!string.IsNullOrEmpty(taskExecutionValues.CurrentWorkOrder.ExternalIdentifier))
                {
                    subject += TemplateMailConstants.TemplateWoClientSite + taskExecutionValues.CurrentWorkOrder.ExternalIdentifier;
                }
                subject += TemplateMailConstants.TemplateEnd + taskExecutionValues.CurrentTask.Name;
            }
            return subject;
        }

        private string GetLocationString(Locations location)
        {
            var locationString = "N/D";
            if (location != null)
            {
                var streetType = !string.IsNullOrEmpty(location.StreetType) && location.StreetType != "NULL" ? $"{location.StreetType} " : string.Empty;
                var street = !string.IsNullOrEmpty(location.Street) && location.Street != "NULL" ? $"{location.Street}, " : string.Empty;
                var number = location.Number != null ? $"{location.Number} " : string.Empty;
                var stairs = !string.IsNullOrEmpty(location.Escala) && location.Escala != "NULL" ? $"{location.Escala}, " : string.Empty;
                var doorNumber = !string.IsNullOrEmpty(location.GateNumber) && location.GateNumber != "NULL" ? $"{location.GateNumber}, " : string.Empty;

                locationString = streetType + street + number + stairs + doorNumber;
            }
            return locationString;
        }

        public string GetAssetName(Entities.Tenant.Assets assets)
        {
            var result = "N/D";
            if (assets != null)
            {
                if (!string.IsNullOrWhiteSpace(assets.SerialNumber))
                {
                    result = assets.SerialNumber;
                }
                else if (!string.IsNullOrWhiteSpace(assets.StockNumber))
                {
                    result = assets.StockNumber;
                }
                else if (!string.IsNullOrWhiteSpace(assets.AssetNumber))
                {
                    result = assets.AssetNumber;
                }
            }
            return result;
        }

        private void SendMail(string subject, string body, List<string> subscribers, Guid customerId)
        {
            var tenant = _tenantService.GetTenant(customerId);
            var connectionString = tenant.Data.ConnectionString;
            NotificationTypeEnum notificationType = _notificationConfigurationServiceConfiguration.GetNotificationTypeConfiguration(connectionString);
            if (notificationType != NotificationTypeEnum.Empty)
            {
                var notificationService = _notificationFactory.GetService(notificationType);

                var emailMessage = new EmailMessageDto()
                {
                    ConnectionString = connectionString,
                    Recipients = subscribers,
                    Subject = subject,
                    Body = body
                };
                notificationService.SendNotificationToMultipleRecipients(emailMessage);
            }
        }
    }
}
