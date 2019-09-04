using Autofac.Multitenant;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public class IOCConfiguration
    {
        public IConfiguration Configuration { get; set; }
        public IServiceCollection Services { get; set; }
        public MultitenantContainer ApplicationContainer { get; set; }
    }
}
