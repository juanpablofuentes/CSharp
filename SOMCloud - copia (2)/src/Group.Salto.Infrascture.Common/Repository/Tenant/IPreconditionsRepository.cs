using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPreconditionsRepository
    {
        IQueryable<Preconditions> GetAllByTaskId(int id);
        Preconditions GetById(int id);
        Preconditions CreatePrecondition(Preconditions entity);
        SaveResult<Preconditions> DeletePreconditions(Preconditions entity);
        Preconditions DeleteOnContextPreconditions(Preconditions entity);
    }
}