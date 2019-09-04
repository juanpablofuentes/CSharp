using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class DepartmentRepository : BaseRepository<Departments>, IDepartmentRepository
    {
        public DepartmentRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public Departments GetById(int id)
        {
            return Find(d => d.Id == id && !d.IsDeleted);
        }

        public bool DeleteContextDepartment(Departments department)
        {
            department.UpdateDate = DateTime.UtcNow;
            Delete(department);
            return true;
        }

        public Dictionary<int, string> GetByCompanyKeyValues(int? companyId)
        {
            return All()
                .Where(x => x.CompanyId == companyId)
                .Select(s => new { s.Id, s.Name })
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}