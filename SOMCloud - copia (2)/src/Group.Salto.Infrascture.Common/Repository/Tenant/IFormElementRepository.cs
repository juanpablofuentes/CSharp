using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IFormElementRepository : IRepository<FormElements>
    {
        IQueryable<FormElements> GetValueByName(string name);
    }
}