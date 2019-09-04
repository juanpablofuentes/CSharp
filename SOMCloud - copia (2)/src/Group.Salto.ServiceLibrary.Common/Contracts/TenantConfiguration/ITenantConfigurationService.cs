using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.TenantConfiguration
{
    public interface ITenantConfigurationService
    {
        IEnumerable<string> GetValueByGroup(string group);
    }
}