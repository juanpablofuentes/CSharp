using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class CompanyRepository : BaseRepository<Companies>, ICompanyRepository
    {
        public CompanyRepository(ITenantUnitOfWork uow) : base(uow)
        {

        }

        public IQueryable<Companies> GetAll()
        {
            return DbSet.Include(x => x.People).Where(x => !x.IsDeleted);
        }

        public Companies GetById(int id)
        {
            var company = DbSet.Include(c => c.Departments)
                            .ThenInclude(c => c.People)
                        .Include(x => x.People)
                        .SingleOrDefault(c => c.Id == id && !c.IsDeleted);
           return company;
        }

        public Companies GetByIdWithoutDeletedRelations(int id)
        {
            var company = DbSet.Include(c => c.Departments)
                                    .ThenInclude(c => c.People)
                                .Include(x => x.People)
                                .Include(x=>x.WorkCenters)
                                    .ThenInclude(x=>x.WorkCenterPeople)
                                .SingleOrDefault(c => c.Id == id && !c.IsDeleted);
            if (company?.Departments != null)
            {
                company.Departments = company.Departments.Where(x => !x.IsDeleted).ToList();
            }

            if (company?.WorkCenters != null)
            {
                company.WorkCenters = company.WorkCenters.Where(x => !x.IsDeleted).ToList();
            }
            return company;
        }

        public SaveResult<Companies> CreateCompany(Companies company)
        {
            company.UpdateDate = DateTime.UtcNow;
            Create(company);
            var result = SaveChange(company);
            return result;
        }

        public SaveResult<Companies> UpdateCompany(Companies company)
        {
            company.UpdateDate = DateTime.UtcNow;
            Update(company);
            var result = SaveChange(company);
            return result;
        }

        public SaveResult<Companies> DeleteCompany(Companies company)
        {
            company.UpdateDate = DateTime.UtcNow;
            Delete(company);
            SaveResult<Companies> result = SaveChange(company);
            result.Entity = company;
            return result;
        }

        public Dictionary<int, string> GetAllNotDeleteKeyValues()
        {
            return Filter(x => !x.IsDeleted)
                .Select(s => new { s.Id, s.Name })
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}