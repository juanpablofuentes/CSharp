using Group.Salto.Entities.Tenant;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IServiceRepository
    {
        Services GetServiceByIdIncludeExtraFields(int id);
        IQueryable<Services> GetServiceWOForms(int Id);
        IQueryable<Services> GetGeneratedServiceWOForms(int Id);
    }
}