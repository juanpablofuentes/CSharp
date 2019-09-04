using DataAccess.Common.Repositories;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Common.Enums;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PeopleRepository : ExplicitBaseRepository<People>, IPeopleRepository
    {
        public PeopleRepository(ITenantUnitOfWork uow) : base(uow) { }

        //TODO: Revisar el naming de este método
        //TODO: && x.PeopleProjects.DefaultIfEmpty().Any(p => !p.IsManager) no acaba de funcionar correctamente
        //TODO Javier : Conditions has been removed because some people cannot be loaded on People Index (For ex. Id 56)
        public People GetById(int id)
        {
            return DbSet
                .Include(x => x.KnowledgePeople)
                    .ThenInclude(x => x.Knowledge)
                .Include(x => x.PeopleProjects)
                .Include(x => x.PeoplePermissions)
                .Where(x => x.Id == id && !x.IsDeleted)
                .FirstOrDefault();
        }

        public People GetByIdWithSubContractCompanyAndCost(int id)
        {
            return Filter(x => x.Id == id)
                .Include(x => x.Subcontract)
                .Include(x => x.PeopleCost)
                .Include(x => x.Company)
                .FirstOrDefault();
        }

        public People GetByIdIncludeReferencesToDelete(int id)
        {
            return Find(x => x.Id == id, GetIncludeDeleteRefenreces());
        }

        public People GetByIdWithoutIncludes(int id)
        {
            return this.Find(p => p.Id == id);
        }

        public IList<People> GetByIds(PeopleFilterRepositoryDto filters)
        {
            IQueryable<People> query = this.GetAllFiltered(filters.Name, filters.Active);
            query = query.Where(x => filters.Ids.Contains(x.UserConfigurationId.Value));

            return query.ToList();
        }

        public IQueryable<People> GetByPeopleIds(IList<int> ids)
        {
            IQueryable<People> query = this.Filter(x => ids.Contains(x.Id));
            return query;
        }

        public IQueryable<People> GetAllFiltered(string name, ActiveEnum active)
        {
            IQueryable<People> query = All()
                .Where(x => !x.IsDeleted)
                .Select(s => new People { Id = s.Id, Name = s.Name, FisrtSurname = s.FisrtSurname, SecondSurname = s.SecondSurname, Dni = s.Dni, Telephone = s.Telephone, Email = s.Email, IsClientWorker = s.IsClientWorker, IsActive = s.IsActive, UserConfigurationId = s.UserConfigurationId });

            query = FilterQuery(name, active, query);

            return query;
        }

        public IQueryable<People> GetAllByFilters(string name, ActiveEnum active)
        {
            IQueryable<People> query = Filter(x => !x.IsDeleted);
            query = FilterQuery(name, active, query);
            return query;
        }

        public People UpdateOnContext(People person)
        {
            person.UpdateDate = DateTime.UtcNow;
            Update(person);

            return person;
        }

        public People GetByIdIncludeIncludeContractInfo(int id)
        {
            return Filter(x => x.Id == id)
                .Include(x => x.Subcontract)
                .Include(x => x.PeopleCost)
                .Include(x => x.Company)
                .Include(x => x.Subcontract)
                .FirstOrDefault();
        }

        private static IQueryable<People> FilterQuery(string name, ActiveEnum active, IQueryable<People> query)
        {
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x =>
                    (x.Name.Contains(name) || x.FisrtSurname.Contains(name) || x.SecondSurname.Contains(name)));
            }

            if (active == ActiveEnum.Active)
            {
                query = query.Where(x => x.IsActive.Value == true);
            }
            else if (active == ActiveEnum.Disabled)
            {
                query = query.Where(x => !x.IsActive.HasValue || x.IsActive.Value == false);
            }

            return query;
        }

        public List<People> GetAllVisibleFiltered(PeopleVisibleFilterRepositoryDto filter)
        {
            IQueryable<People> query = DbSet
                                .Include(c => c.Company)
                                .Include(c => c.Department)
                                .Include(c => c.WorkCenter)
                        .Where(x => !x.IsDeleted && x.IsVisible);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => (x.Name.Contains(filter.Name) || x.FisrtSurname.Contains(filter.Name) || x.SecondSurname.Contains(filter.Name)));
            }

            if (filter.CompanyId.HasValue)
            {
                query = query.Where(x => x.Company.Id == filter.CompanyId.Value);
            }

            if (filter.DeparmentId.HasValue)
            {
                query = query.Where(x => x.Department.Id == filter.DeparmentId);
            }

            if (filter.KnowledgeId.HasValue)
            {
                query = query.Where(x => x.KnowledgePeople.Any(s => s.Knowledge.Id == filter.KnowledgeId.Value));
            }

            if (filter.WorkCenterId.HasValue)
            {
                query = query.Where(x => x.WorkCenter.Id == filter.WorkCenterId);
            }

            return query.ToList();
        }

        //TODO: Revisar el naming de este método
        public Dictionary<int, string> GetByCompanyAllKeyValues(int companyId, int id)
        {
            return All()
                .Where(x => x.Id != id && x.CompanyId == companyId && !x.IsDeleted)
                .Select(s => new { s.Id, Name = $"{s.Name} {s.FisrtSurname} {s.SecondSurname}" })
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        //TODO. Falta saber como filtrarlo
        public Dictionary<int, string> GetAllCommercialKeyValues()
        {
            return All()
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.FullName);
        }

        public Dictionary<int, string> GetActiveByCompanyKeyValue(int companyId)
        {
            var people = this.Filter(w => w.CompanyId == companyId && !w.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.FullName);
            return people;
        }

        public Dictionary<int, string> GetAllKeyValue()
        {
            return this.Filter(x => !x.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.FullName);
        }

        public Dictionary<int, string> GetAllActiveKeyValue()
        {
            return this.Filter(x => !x.IsDeleted && x.IsActive.HasValue && x.IsActive.Value)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.FullName);
        }

        public People GetByConfigId(int userId)
        {
            return Find(p => p.UserConfigurationId == userId);
        }

        public int GetPeopleIdByUserId(int userId)
        {
            return Filter(p => p.UserConfigurationId == userId).Select(x => x.Id).FirstOrDefault();
        }

        public People GetByConfigIdIncludingCompany(int userId)
        {
            return Filter(p => p.UserConfigurationId == userId)
                .Include(p => p.Company).FirstOrDefault();
        }

        public Dictionary<int, string> GetPeopleByPermissionKeyValues(PermissionTypeEnum permission, PeopleFilterDto filter)
        {
            IQueryable<People> data = GetPeopleByPermissions(permission, filter);
            return data.ToDictionary(t => t.Id, t => t.FullName);
        }

        public IQueryable<People> GetBySameSubcontract(int? peopleSubcontractId)
        {
            return Filter(p => !p.IsDeleted && p.IsActive != null && (p.IsActive.Value && p.SubcontractId == null || p.SubcontractId == peopleSubcontractId));
        }

        public SaveResult<People> CreatePeople(People people)
        {
            people.UpdateDate = DateTime.UtcNow;
            Create(people);
            SaveResult<People> result = SaveChange(people);
            result.Entity = people;

            return result;
        }

        public SaveResult<People> UpdatePeople(People people)
        {
            people.UpdateDate = DateTime.UtcNow;
            Update(people);
            SaveResult<People> result = SaveChange(people);
            result.Entity = people;

            return result;
        }

        public bool DeletePeople(People people)
        {
            Delete(people);
            SaveResult<People> result = SaveChange(people);
            result.Entity = people;

            return result.IsOk;
        }

        public SaveResult<People> CreatePeople(People person, string connectionString)
        {
            CreateInstance(connectionString);
            var result = CreatePeople(person);
            DestroyInstance();
            return result;
        }

        public Dictionary<int, string> GetAllDriversFiltered()
        {
            return All()
                .Where(x => x.IsActive.Value && !x.IsDeleted)
                .Select(s => new { s.Id, Name = $"{s.Name} {s.FisrtSurname} {s.SecondSurname}" })
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<People> GetAll()
        {
            return All();
        }

        public People GetByConfigIdIncludePermissions(int userId)
        {
            return Filter(p => p.UserConfigurationId == userId)
                .Include(p => p.UserConfiguration)
                .Include(p => p.PeoplePermissions).ThenInclude(p => p.Permission.ProjectPermission)
                .Include(p => p.PeoplePermissions).ThenInclude(p => p.Permission.PermissionQueue)
                .Include(p => p.PeoplePermissions).ThenInclude(p => p.Permission.WorkOrderCategoryPermission)
                .Include(p => p.PeoplePermissions).ThenInclude(p => p.Permission.PermissionTask).ThenInclude(p => p.Task)
                .SingleOrDefault();
        }

        public int[] GetSubContracts(int peopleId)
        {
            int[] subContractPeople = null;
            People people = Find(x => x.Id == peopleId && !x.IsDeleted);

            if (people != null && people.SubcontractId.HasValue && people.SubcontractorResponsible.HasValue && people.SubcontractorResponsible.Value)
            {
                subContractPeople = Filter(p => p.SubcontractId == people.SubcontractId.Value && !p.IsDeleted)
                    .Select(p => p.Id)
                    .ToArray();
            }
            return subContractPeople ?? new int[0];
        }

        private IQueryable<People> GetPeopleByPermissions(PermissionTypeEnum permission, PeopleFilterDto filter)
        {
            IQueryable<People> data = this.Filter(x => !x.IsDeleted && x.IsActive.HasValue && x.IsActive.Value)
                            .Include(p => p.PeoplePermissions).ThenInclude(p => p.Permission)
                                .Where(x => x.PeoplePermissions.Where(y => y.Permission.Tasks.Substring((int)permission, 1) == "1").Any())
                            .OrderBy(o => o.Name);

            if (filter != null && !string.IsNullOrEmpty(filter.Name))
            {
                data = data.Where(x => (x.Name.Contains(filter.Name) || x.FisrtSurname.Contains(filter.Name) || x.SecondSurname.Contains(filter.Name)));
            }

            if (filter != null && filter.IsVisible.HasValue)
            {
                data = data.Where(x => x.IsVisible == filter.IsVisible.Value);
            }

            if (filter != null && filter.SubcontractId.HasValue)
            {
                data = data.Where(x => x.SubcontractId == filter.SubcontractId.Value);
            }

            return data;
        }
        private List<Expression<Func<People, object>>> GetIncludeDeleteRefenreces()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(KnowledgePeople),
                                                            typeof(PeopleCollectionsAdmins),
                                                            typeof(PeopleCollectionsAdmins),
                                                            typeof(PeoplePermissions),
                                                            typeof(Vehicles)});
        }

        private List<Expression<Func<People, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<People, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(KnowledgePeople))
                {
                    includesPredicate.Add(p => p.KnowledgePeople);
                }
                if (element == typeof(PeopleCollectionsAdmins))
                {
                    includesPredicate.Add(p => p.PeopleCollectionsAdmins);
                }
                if (element == typeof(PeopleCollectionsPeople))
                {
                    includesPredicate.Add(p => p.PeopleCollectionsPeople);
                }
                if (element == typeof(Vehicles))
                {
                    includesPredicate.Add(p => p.Vehicles);
                }
                if (element == typeof(PeoplePermissions))
                {
                    includesPredicate.Add(p => p.PeoplePermissions);
                }
            }
            return includesPredicate;
        }
    }
}