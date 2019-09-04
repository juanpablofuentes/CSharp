using System;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Customer;

namespace Group.Salto.ServiceLibrary.Common.Contracts
{
    public interface ITenantService
    {
        ResultDto<TenantIdsDTO> GetTenant(Guid tenantId);
    }
}