using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Group.Salto.SOM.Mobility
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseAutofacMultitenantRequestServices(() => Startup.ApplicationContainer)
                .UseStartup<Startup>();
    }
}
