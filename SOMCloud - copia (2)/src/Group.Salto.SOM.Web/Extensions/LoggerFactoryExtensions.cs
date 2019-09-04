using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Group.Salto.SOM.Web.Extensions
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddLogConfiguration(this ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            loggerFactory.AddConsole(configuration);
            loggerFactory.AddDebug();
            loggerFactory.AddLog4Net();
            return loggerFactory;
        }
    }
}
