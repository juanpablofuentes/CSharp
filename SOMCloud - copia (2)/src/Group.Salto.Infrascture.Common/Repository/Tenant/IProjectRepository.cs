using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IProjectRepository
    {
        Projects GetWithPeopleProjectById(int id);
        Projects GetById(int id);
        Projects GetByIdWithZoneProjectAndContract(int id);
        Projects GetByIdWithZoneProject(int id);
        Projects GetByIdWithIncludesToDelete(int id);
        Projects GetByIdWithIncludesPermissions(int id);
        Dictionary<int, string> GetAllKeyValues();
        Projects GetProjectByProjectName(string ProjectName);
        IQueryable<Projects> GetAllById(IList<int> ids);
        SaveResult<Projects> CreateProjects(Projects project);
        SaveResult<Projects> UpdateProjects(Projects project);
        SaveResult<Projects> DeleteProjects(Projects project);
        IQueryable<Projects> GetAllWithPeopleProjectsAndPeople();
        IQueryable<Projects> GetAllByFiltersWithPermisions(PermisionsFilterQueryDto filterQuery);
        List<Projects> GetProjectForAdvancedSearch(FilterQueryDto filterQuery);
        IQueryable<Projects> FilterByClient(string text, int?[] selected);
        IQueryable<Projects> GetByIds(IList<int> ids);
    }
}