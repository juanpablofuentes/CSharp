using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWorkCenterRepository
    {
        WorkCenters GetById(int id);
        IQueryable<WorkCenters> GetAllAvailablesWithIncludes();
        WorkCenters GetByIdWithPeopleCompaniesIncludes(int? id);
        SaveResult<WorkCenters> CreateWorkCenter(WorkCenters workCenter);
        SaveResult<WorkCenters> UpdateWorkCenter(WorkCenters workCenter);
        SaveResult<WorkCenters> DeleteWorkCenter(WorkCenters workcCenter);
        WorkCenters DeleteWorkCenterContext(WorkCenters workCenter);
        Dictionary<int, string> GetActiveByCompanyKeyValue(int companyId);
    }
}