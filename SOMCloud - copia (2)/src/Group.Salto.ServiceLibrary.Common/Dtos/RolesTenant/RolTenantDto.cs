using Group.Salto.Common.Entities.Contracts;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.RolesTenant
{
    public class RolTenantDto : BaseComboDto, IKeyValue
    {
        public string Description { get; set; }
    }
}