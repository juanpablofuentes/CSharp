using Autofac.Multitenant;
using Group.Salto.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Group.Salto.Ioc
{
    public class UserStrategy : ITenantIdentificationStrategy
    {
        public IHttpContextAccessor Accessor { get; private set; }
        public IServiceProvider provider { get; set; }

        public UserStrategy(IHttpContextAccessor accessor,IServiceProvider provider)
        {
            Accessor = accessor;
            this.provider = provider;
        }

        public bool TryIdentifyTenant(out object tenantId)
        {
            
                tenantId = null;
                return false;
            


        }

    }

}
