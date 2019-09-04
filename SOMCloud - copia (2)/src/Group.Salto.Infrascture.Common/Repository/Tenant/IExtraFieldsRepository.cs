using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IExtraFieldsRepository : IRepository<ExtraFields>
    {
        ExtraFields GetByIdWithIncludeTranslations(int id);
        IList<ExtraFields> GetByIds(IEnumerable<int> ids);
        IQueryable<ExtraFields> GetAllWithIncludeTranslations();
        IQueryable<ExtraFields> GetAllByDelSystemWithIncludeTranslations(bool isSystem);
        SaveResult<ExtraFields> CreateExtraFields(ExtraFields entity);
        SaveResult<ExtraFields> UpdateExtraFields(ExtraFields entity);
        SaveResult<ExtraFields> DeleteExtraFields(ExtraFields entity);
        ExtraFields GetByIdWithCollectionsServices(int id);
        ExtraFields GetExtraFieldFromName(string extraFieldName);
    }
}