using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IBillLineRepository
    {
        List<BillLine> GetAllByBillId(int id);
    }
}