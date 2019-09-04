using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using Group.Salto.Entities.Tenant.QueryEntities;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IErpSystemInstanceQueryRepository : IRepository<ErpSystemInstanceQuery>
    {
        Dictionary<int, string> GetAllKeyValues();
        ErpSystemInstanceQuery GetByIdAndName(int erpId, string precioVenta);
        decimal GetItemPriceFromQuery(string sqlQuery, int itemId);
        IEnumerable<FieldMaterialForm> GetMaterialFormItemsFromPeople(string sqlQuery, int peopleId);
    }
}