using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Helpers
{
    public static class RolesActionGroupsActionsTenantHelper
    {
        private static ICache _cacheService;

        public static void SetRolesActionGroupsActionsTenantInstance(ICache cacheService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(ICache));
        }

        public static bool CanDoAction(string userConfigurationId, string actionGroup, string action)
        {
            Dictionary<string, List<string>> actionGroupActionCache = (Dictionary<string, List<string>>) _cacheService.GetData(AppCache.RoleActions, userConfigurationId);
            var existAction = string.Empty;
            if (actionGroupActionCache != null && actionGroupActionCache.ContainsKey(actionGroup))
            {
                actionGroupActionCache.TryGetValue(actionGroup, out var actions);
                existAction = actions.Find(x => x == action);
            }
            return !string.IsNullOrEmpty(existAction);
        }
    }
}