using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IDepartmentRepository : IRepository<Departments>
    {
        Departments GetById(int id);
        bool DeleteContextDepartment(Departments department);
        Dictionary<int, string> GetByCompanyKeyValues(int? companyId);
    }
}