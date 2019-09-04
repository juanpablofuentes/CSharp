using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IExtraFieldsValuesRepository
    {
        ExtraFieldsValues GetById(int id);
        IQueryable<ExtraFieldsValues> GetAllExtrafields(int id);
        SaveResult<ExtraFieldsValues> UpdateExtraFieldValues(List<ExtraFieldsValues> entities);
    }
}