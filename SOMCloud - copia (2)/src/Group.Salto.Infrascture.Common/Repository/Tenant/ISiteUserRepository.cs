using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ISiteUserRepository : IRepository<SiteUser>
    {
        IQueryable<SiteUser> GetAll(int Id);
        SiteUser GetById(int id);
        IQueryable<SiteUser> FilterByClientSite(string text, int?[] selected);
        SaveResult<SiteUser> UpdateSiteUser(SiteUser entity);
        SaveResult<SiteUser> CreateSiteUser(SiteUser entity);
        SiteUser GetSiteUserRelationshipsById(int id);
        bool DeleteSiteUser(SiteUser siteUser);
        SiteUser GetByIdIncludeReferencesToDelete(int id);
    }
}