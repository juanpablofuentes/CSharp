using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IServicesAnalysisRepository : IRepository<ServicesAnalysis>
    {
        IQueryable<ServicesAnalysis> GetServiceAnalysisWOOperations(int Id);
    }
}