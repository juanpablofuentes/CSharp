using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Actions;
using Group.Salto.ServiceLibrary.Common.Contracts.ActionsGroups;
using Group.Salto.ServiceLibrary.Common.Contracts.CustomerModules;
using Group.Salto.ServiceLibrary.Common.Contracts.RolesActionGroupsActionsTenant;
using Group.Salto.ServiceLibrary.Common.Contracts.UserConfigurationRolesTenant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.RolesActionGroupsActionsTenant
{
    public class RolesActionGroupsActionsTenantAdapter : BaseService, IRolesActionGroupsActionsTenantAdapter
    {
        private readonly ICache _cacheService;
        private readonly IUserConfigurationRolesTenantService _userConfigurationRolesTenantService;
        private readonly IRolesActionGroupsActionsTenantService _rolesActionGroupsActionsTenantService;
        private readonly IActionService _actionService;
        private readonly IActionGroupService _actionGroupService;
        private readonly ICustomerModuleService _customerModuleService;

        public RolesActionGroupsActionsTenantAdapter(ILoggingService logginingService,
            IUserConfigurationRolesTenantService userConfigurationRolesTenantService,
            IRolesActionGroupsActionsTenantService rolesActionGroupsActionsTenantService,
            IActionService actionService,
            IActionGroupService actionGroupService,
            ICache cacheService,
            ICustomerModuleService customerModuleService) : base(logginingService)
        {
            _userConfigurationRolesTenantService = userConfigurationRolesTenantService ?? throw new ArgumentNullException($"{nameof(IUserConfigurationRolesTenantService)} is null");
            _rolesActionGroupsActionsTenantService = rolesActionGroupsActionsTenantService ?? throw new ArgumentNullException($"{nameof(IRolesActionGroupsActionsTenantService)} is null");
            _actionService = actionService ?? throw new ArgumentNullException($"{nameof(IActionService)} is null");
            _actionGroupService = actionGroupService ?? throw new ArgumentNullException($"{nameof(IActionGroupService)} is null");
            _cacheService = cacheService ?? throw new ArgumentNullException($"{nameof(ICache)} is null");
            _customerModuleService = customerModuleService ?? throw new ArgumentNullException($"{nameof(ICustomerModuleService)} is null");
        }

        public bool SetCacheRolesActionGroupsActionsByUserId(int userConfigurationId, string customerId)
        {
            LogginingService.LogInfo($"Get roles actions tenant by user");

            var moduleActionGroups = _customerModuleService.GetModulesByCustomer(Guid.Parse(customerId)).SelectMany(mc => mc.Module.ModuleActionGroups).Select(m => m.ActionGroupsId);
            string roleTenantId = _userConfigurationRolesTenantService.GetRoleIdByUserId(userConfigurationId);
            bool result = false;
            if (!string.IsNullOrEmpty(roleTenantId) && moduleActionGroups.Any())
            {
                Dictionary<string, List<string>> res = (Dictionary<string, List<string>>)_cacheService.GetData(AppCache.RoleActions, userConfigurationId.ToString());
                if (res is null)
                {
                    var rolesActionGroupsActionsTenant = _rolesActionGroupsActionsTenantService.GetRolesActionsByRolId(roleTenantId); 
                    if (rolesActionGroupsActionsTenant.Data.Any())
                    {
                        Dictionary<int, string> actions = _actionService.GetAllKeyValues();
                        Dictionary<Guid, string> actionGroups = _actionGroupService.GetAllKeyValues();
                        var rolesActionGroupsActionsTenantGroup = rolesActionGroupsActionsTenant.Data.Where(x => moduleActionGroups.Contains(x.ActionGroupId)).GroupBy(x => x.ActionGroupId);
                        
                        Dictionary<string, List<string>> rolesActionGroupsActionsTenantDictionary = new Dictionary<string, List<string>>();
                        foreach (var group in rolesActionGroupsActionsTenantGroup)
                        {
                            string actionGroupName = string.Empty;
                            actionGroups.TryGetValue(group.Key, out actionGroupName);

                            List<string> actionItem = new List<string>();
                            foreach (var item in group)
                            {
                                string actionName = string.Empty;
                                actions.TryGetValue(item.ActionId, out actionName);
                                if (!string.IsNullOrEmpty(actionName)) actionItem.Add(actionName);
                            }

                            rolesActionGroupsActionsTenantDictionary.Add(actionGroupName, actionItem);
                        }
                        _cacheService.SetData(AppCache.RoleActions, userConfigurationId.ToString(), rolesActionGroupsActionsTenantDictionary);
                        result = true;
                    }
                }
                result = true;
            }
            return result;
        }
    }
}