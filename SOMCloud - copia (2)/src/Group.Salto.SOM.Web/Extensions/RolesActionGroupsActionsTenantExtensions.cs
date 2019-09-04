using Group.Salto.Common.Enums;
using Group.Salto.ServiceLibrary.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Extensions
{
    public static class RolesActionGroupsActionsTenantExtensions
    {
        public static bool CanDoAction(this IHtmlHelper context, string userConfigurationId, ActionGroupEnum actionGroup, ActionEnum action)
        {
            return RolesActionGroupsActionsTenantHelper.CanDoAction(userConfigurationId, actionGroup.ToString(), action.ToString());
        }
    }
}