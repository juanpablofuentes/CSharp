using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IFinalClientsRepository : IRepository<FinalClients>
    {
        IQueryable<FinalClients> GetAllByFilters(FilterAdditionalQueryDto filter);
        IQueryable<FinalClients> GetAll();
        FinalClients GetById(int id);
        SaveResult<FinalClients> CreateFinalClients(FinalClients finalClients);
        SaveResult<FinalClients> UpdateFinalClients(FinalClients finalClients);
        bool CheckUniqueCode(string code);
        bool DeleteFinalClients(FinalClients finalClients);
        FinalClients GetByIdIncludeReferencesToDelete(int id);
        FinalClients GetByIdCanDelete(int id);
        List<FinalClients> GetFinalClientsForAdvancedSearch(AdvancedSearchDto filterQuery);
        Dictionary<int, string> GetAllKeyValues();
        IQueryable<FinalClients> GetByIds(IList<int> ids);
    }
}