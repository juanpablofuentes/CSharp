using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Agent;
using Group.Salto.Log;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using PushSharp.Apple;

namespace Group.Salto.Agents.PushNotification
{
    public class IosPushNotificationAgent : IIosPushNotificationAgent
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggingService _logginingService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public IosPushNotificationAgent(IConfiguration configuration,
                                        ILoggingService logginingService,
                                        IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _logginingService = logginingService;
            _hostingEnvironment = hostingEnvironment;
        }

        public void SendNotifications(IEnumerable<string> deviceTokens, string title, string body, bool openWoPage)
        {
            try
            {
                var cetPath = _configuration.GetSection(AppsettingsKeys.PushNotificationSettings).GetValue<string>(AppsettingsKeys.IosCertificateFilePath);
                var cetPass = _configuration.GetSection(AppsettingsKeys.PushNotificationSettings).GetValue<string>(AppsettingsKeys.IosCertificatePass);
                var rootDir = Path.Combine(_hostingEnvironment.ContentRootPath, cetPath);
                var file = File.ReadAllBytes(rootDir);
                var cert = new X509Certificate2(file, cetPass, X509KeyStorageFlags.MachineKeySet);

                var environment = ApnsConfiguration.ApnsServerEnvironment.Production;
                if (cetPath.Contains("Dev"))
                {
                    environment = ApnsConfiguration.ApnsServerEnvironment.Sandbox;
                }

                var config = new ApnsConfiguration(environment, cert);

                foreach (var deviceToken in deviceTokens)
                {
                    Task.Run(() => IosSendPushToSingleUser(config, deviceToken, title, body, openWoPage));
                }
            }
            catch (Exception ex)
            {
                _logginingService.LogException(ex);
            }
        }

        private void IosSendPushToSingleUser(ApnsConfiguration config, string deviceToken, string title, string body, bool openWoPage)
        {
            try
            {
                var apnsBroker = new ApnsServiceBroker(config);

                apnsBroker.OnNotificationFailed += (notification, exception) =>
                {
                    _logginingService.LogVerbose(exception.Message);
                };

                apnsBroker.OnNotificationSucceeded += (notification) =>
                {
                    Debug.WriteLine(notification.ToString());
                };

                apnsBroker.Start();
                string message;
                if (openWoPage)
                {
                    message = "{\"aps\": {\"alert\": { \"title\": \"" + title + "\",\"body\": \"" + body + "\"}},\"OpenWoPage\" : \"" + openWoPage + "\" }";
                }
                else
                {
                    message = "{\"aps\": {\"alert\": { \"title\": \"" + title + "\",\"body\": \"" + body + "\"}}}";
                }

                apnsBroker.QueueNotification(new ApnsNotification
                {

                    DeviceToken = deviceToken,
                    Payload = JObject.Parse(message)
                });

                apnsBroker.Stop();
            }
            catch (Exception ex)
            {
                _logginingService.LogException(ex);
            }
        }
    }
}
