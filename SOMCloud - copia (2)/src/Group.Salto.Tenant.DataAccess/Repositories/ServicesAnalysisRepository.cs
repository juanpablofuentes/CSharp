using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ServicesAnalysisRepository : BaseRepository<ServicesAnalysis>, IServicesAnalysisRepository
    {
        public ServicesAnalysisRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<ServicesAnalysis> GetServiceAnalysisWOOperations(int Id)
        {
            return Filter(s => s.WorkOrderCode == Id);               
        }
    }
}