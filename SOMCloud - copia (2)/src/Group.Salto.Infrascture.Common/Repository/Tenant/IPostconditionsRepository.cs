using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPostconditionsRepository : IRepository<Postconditions>
    {
        Postconditions GetById(int id);
        IQueryable<Postconditions> GetByPostconditionCollectionId(int id);
        SaveResult<Postconditions> UpdatePostconditions(Postconditions entity);
        SaveResult<Postconditions> CreatePostconditions(Postconditions entity);
        SaveResult<Postconditions> DeletePostconditions(Postconditions entity);
        Postconditions DeletePostconditionsOnContext(Postconditions entity);
        SaveResult<IEnumerable<Postconditions>> DeleteRangePostconditions(IEnumerable<Postconditions> entities);
    }
}