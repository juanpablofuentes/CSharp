using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
           CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.Sources.Clear();
                    config.AddJsonFile("appsettings.json", optional: true);
                })
                .UseAutofacMultitenantRequestServices(() => Startup.ApplicationContainer)
                .UseStartup<Startup>();
    }
}