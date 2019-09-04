using Group.Salto.Common.Constants.TenantConfiguration;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Notification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class EmailConfigurationDtoExtensions
    {
        public static EmailSmtpConfigurationDto ToEmailSmtpConfigurationDto (this IList<TenantConfiguration> source)
        {
            EmailSmtpConfigurationDto result = null;
            if(source != null)
            {
                result = new EmailSmtpConfigurationDto()
                {
                    SmtpServer = source.FirstOrDefault(x => x.Key == TenantConfigurationConstants.SmtpServer).Value,
                    SmtpPort = Convert.ToInt32(source.FirstOrDefault(x => x.Key == TenantConfigurationConstants.SmtpPort).Value),
                    SmtpUserName = source.FirstOrDefault(x => x.Key == TenantConfigurationConstants.SmtpUserName).Value,
                    SmtpPassword = source.FirstOrDefault(x => x.Key == TenantConfigurationConstants.SmtpPassword).Value,
                    SmtpEnableSSL = Convert.ToBoolean(source.FirstOrDefault(x => x.Key == TenantConfigurationConstants.SmtpEnableSSL).Value),
                    EmailFrom = source.FirstOrDefault(x => x.Key == TenantConfigurationConstants.SmtpFrom).Value
                };
            }
            return result;
        }
    }
}