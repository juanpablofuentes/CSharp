using Group.Salto.Common.Enums;
using Group.Salto.ServiceLibrary.Common.Contracts.Notification;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.Notification
{
    public class NotificationFactory : INotificationFactory
    {
        private IDictionary<NotificationTypeEnum, Func<INotificationService>> _servicesNotification;
        private readonly IServiceProvider _services;

        public NotificationFactory(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException($"{nameof(services)} is null ");
            _servicesNotification = new Dictionary<NotificationTypeEnum, Func<INotificationService>>();
            _servicesNotification.Add(NotificationTypeEnum.SendGrid, () => _services.GetService<IEmailSendGridNotification>());
            _servicesNotification.Add(NotificationTypeEnum.Smtp, () => _services.GetService<IEmailSmtpNotification>());
            _servicesNotification.Add(NotificationTypeEnum.PushMobile, () => _services.GetService<IPushSendNotification>());
        }

        public INotificationService GetService(NotificationTypeEnum typeNotification)
        {
            return _servicesNotification[typeNotification]() ?? throw new NotImplementedException();
        }
    }
}