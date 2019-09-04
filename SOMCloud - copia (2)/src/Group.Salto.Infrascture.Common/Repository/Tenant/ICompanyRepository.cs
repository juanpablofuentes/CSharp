using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ICompanyRepository : IRepository<Companies>
    {
        IQueryable<Companies> GetAll();
        Companies GetById(int id);
        Companies GetByIdWithoutDeletedRelations(int id);
        SaveResult<Companies> CreateCompany(Companies company);
        SaveResult<Companies> UpdateCompany(Companies company);
        SaveResult<Companies> DeleteCompany(Companies company);
        Dictionary<int, string> GetAllNotDeleteKeyValues();
    }
}
