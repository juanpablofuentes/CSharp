using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Routing
{



    class MultitenancyRouting : IRouter
    {
        private readonly IRouter _defaulRouter;

        public MultitenancyRouting(IRouter defaulRouter)
        {
            _defaulRouter = defaulRouter;
        }

        public Task RouteAsync(RouteContext context) => _defaulRouter.RouteAsync(context);

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            if (context.HttpContext.User?.Identity?.IsAuthenticated == true)
            {
                context.Values.TryGetValue("tenantId", out var tnid);
                if (tnid == null)
                    context.Values["tenaniId"] = context.HttpContext.User?.Claims.FirstOrDefault(c => c.Type == "tenantId");

            }
            return _defaulRouter.GetVirtualPath(context);
        }
    }



}
