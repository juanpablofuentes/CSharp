using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IFlowsRepository : IRepository<Flows>
    {
        IQueryable<Flows> GetAll();
        Flows GetById(int id);
        SaveResult<Flows> UpdateFlows(Flows entity);
        SaveResult<Flows> CreateFlows(Flows entity);
    }
}