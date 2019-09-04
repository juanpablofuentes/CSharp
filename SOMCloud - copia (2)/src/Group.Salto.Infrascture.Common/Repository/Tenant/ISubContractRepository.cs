using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ISubContractRepository : IRepository<SubContracts>
    {
        IQueryable<SubContracts> GetAll();
        SubContracts GetById(int id);
        SaveResult<SubContracts> CreateSubContract(SubContracts entity);
        SaveResult<SubContracts> UpdateSubContract(SubContracts entity);
        Dictionary<int, string> GetAllKeyValues();
        SaveResult<SubContracts> DeleteSubContractor(SubContracts localSubcontractor);
    }
}