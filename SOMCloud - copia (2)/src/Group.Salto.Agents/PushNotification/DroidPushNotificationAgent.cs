using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Agent;
using Group.Salto.Log;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using PushSharp.Google;

namespace Group.Salto.Agents.PushNotification
{
    public class DroidPushNotificationAgent : IDroidPushNotificationAgent
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggingService _logginingService;

        public DroidPushNotificationAgent(IConfiguration configuration,
                                          ILoggingService logginingService)
        {
            _configuration = configuration;
            _logginingService = logginingService;
        }

        public void SendNotifications(IEnumerable<string> deviceTokens, string title, string body, bool openWoPage)
        {
            try
            {
                var androidApiKey = _configuration.GetSection(AppsettingsKeys.PushNotificationSettings).GetValue<string>(AppsettingsKeys.AndroidAuthToken);
                var config = new GcmConfiguration(null, androidApiKey, null);
                var broker = new GcmServiceBroker(config);
                broker.OnNotificationFailed += (notification, exception) =>
                {
                    _logginingService.LogVerbose(exception.Message);
                };
                broker.OnNotificationSucceeded += (notification) =>
                {
                    Debug.WriteLine(notification.ToString());
                };

                broker.Start();
                var message = "{" + "\"body\" : \"" + body + "\"," + "\"title\" : \"" + title + "\"," + "\"sound\" : \"" + "default" + "\"," + "}";
                string data = null;
                if (openWoPage)
                {
                    data = "{\"OpenWoPage\" : \"" + openWoPage + "\"}";
                }

                broker.QueueNotification(new GcmNotification
                {
                    RegistrationIds = deviceTokens.ToList(),
                    Notification = JObject.Parse(message),
                    Data = !string.IsNullOrEmpty(data) ? JObject.Parse(data) : null,
                });

                broker.Stop();
            }
            catch (Exception ex)
            {
                _logginingService.LogException(ex);
            }
        }
    }
}
