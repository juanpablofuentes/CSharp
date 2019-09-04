using Group.Salto.Common;
using Group.Salto.Common.Constants.TenantConfiguration;
using Group.Salto.Common.Enums;
using Group.Salto.Common.Helpers;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Group.Salto.ServiceLibrary.Common.Dtos.Notification;
using Group.Salto.ServiceLibrary.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Email
{
    public class NotificationConfigurationService : BaseService, INotificationConfigurationService
    {
        private IConfiguration _configuration;
        private ITenantConfigurationRepository _tenantConfigurationRepository;

        public NotificationConfigurationService(ILoggingService logginingService, IConfiguration configuration, ITenantConfigurationRepository tenantConfigurationRepository)
            : base(logginingService)
        {
            _configuration = configuration ?? throw new ArgumentNullException($"{nameof(configuration)} is null ");
            _tenantConfigurationRepository = tenantConfigurationRepository ?? throw new ArgumentNullException($"{nameof(tenantConfigurationRepository)} is null ");
        }

        public NotificationTypeEnum GetNotificationTypeConfiguration(string connectionString)
        {
            NotificationTypeEnum notificationType = NotificationTypeEnum.Empty;
            var tenantConfiguration = _tenantConfigurationRepository.GetByKey(TenantConfigurationConstants.NotificationType, connectionString);
            if(tenantConfiguration != null)
            {
                notificationType = tenantConfiguration.Value.ParseEnum<NotificationTypeEnum>();
            }
            return notificationType;
        }

        public EmailSendGridConfigurationDto GetSendGridConfiguration(string connectionString)
        {
            string sendGridKey = _configuration.GetSection(AppsettingsKeys.EmailConfiguration).GetValue<string>(AppsettingsKeys.SendGridKey);
            string emailFrom = _tenantConfigurationRepository.GetByKey(TenantConfigurationConstants.SendGridFrom, connectionString).Value;

            return new EmailSendGridConfigurationDto()
            {
                SendGridKey = sendGridKey,
                EmailFrom = emailFrom
            };
        }

        public EmailSmtpConfigurationDto GetSmtpConfiguration(string connectionString)
        {
            var tenantConfig = _tenantConfigurationRepository.GetByGroup(TenantConfigurationConstants.SmtpConfiguration, connectionString);
            return tenantConfig.ToEmailSmtpConfigurationDto();
        }
    }
}