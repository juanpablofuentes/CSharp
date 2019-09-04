using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ISitesRepository
    {
        IQueryable<Locations> GetAllByFilters(FilterAdditionalQueryDto filter);
        Locations GetById(int id);
        Locations GetByIdWithContacts(int id);
        Locations GetByIdCanDelete(int id);        
        IQueryable<Locations> GetAll();
        IQueryable<Locations> GetAllByClientSite(int clientId);
        SaveResult<Locations> CreateSite(Locations entity);
        SaveResult<Locations> UpdateSite(Locations entity);
        IQueryable<Locations> FilterByClientSite(string text, int?[] selected);
        bool ValidateCodeSite(Locations entity, int FinalClientId);
        Locations DeleteOnContext(Locations entity);
        bool DeleteLocation(Locations entity);
        Locations GetByIdWithFinalClient(int id);
        List<Locations> GetSitesForAdvancedSearch(AdvancedSearchDto filterQuery);
    }
}