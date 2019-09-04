using Autofac;
using Group.Salto.Common.Cache;
using Group.Salto.ServiceLibrary.Common.Contracts;
using Group.Salto.ServiceLibrary.Helpers;
using Microsoft.AspNetCore.Http;

namespace Group.Salto.Ioc.Extensions
{
    public static class SetAdditionalServices
    {
        public static void SetInstancesHelper(this IContainer container)
        {
            TranslationHelper.SetTranslationInstance(container.Resolve<ITranslationService>());
            RolesActionGroupsActionsTenantHelper.SetRolesActionGroupsActionsTenantInstance(container.Resolve<ICache>());
            DateTimeZoneHelper.SetHttpContextAccessor(container.Resolve<IHttpContextAccessor>());
        }
    }
}