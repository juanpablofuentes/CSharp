using Group.Salto.Common;
using Group.Salto.Common.Enums;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPeopleRepository : IRepository<People>
    {
        People GetById(int Id);
        People GetByIdWithSubContractCompanyAndCost(int id);
        People GetByIdWithoutIncludes(int id);
        IList<People> GetByIds(PeopleFilterRepositoryDto filters);
        IQueryable<People> GetAllFiltered(string name, ActiveEnum isActive);
        List<People> GetAllVisibleFiltered(PeopleVisibleFilterRepositoryDto filter);
        SaveResult<People> CreatePeople(People people);
        SaveResult<People> UpdatePeople(People people);
        bool DeletePeople(People people);
        SaveResult<People> CreatePeople(People person, string connectionString);
        Dictionary<int, string> GetByCompanyAllKeyValues(int companyId, int id);
        Dictionary<int, string> GetAllCommercialKeyValues();
        Dictionary<int, string> GetActiveByCompanyKeyValue(int companyId);
        IQueryable<People> GetByPeopleIds(IList<int> ids);
        IQueryable<People> GetAllByFilters(string name, ActiveEnum active);
        Dictionary<int, string> GetAllKeyValue();
        People GetByConfigId(int userId);
        People GetByConfigIdIncludingCompany(int userId);
        Dictionary<int,string> GetAllDriversFiltered();
        People GetByConfigIdIncludePermissions(int peopleConfigId);
        Dictionary<int, string> GetAllActiveKeyValue();
        People GetByIdIncludeReferencesToDelete(int id);
        Dictionary<int, string> GetPeopleByPermissionKeyValues(PermissionTypeEnum permission, PeopleFilterDto filter);
        IQueryable<People> GetBySameSubcontract(int? peopleSubcontractId);
        int[] GetSubContracts(int peopleId);
        People UpdateOnContext(People person);
        People GetByIdIncludeIncludeContractInfo(int id);
        IQueryable<People> GetAll();
        int GetPeopleIdByUserId(int userId);
    }
}