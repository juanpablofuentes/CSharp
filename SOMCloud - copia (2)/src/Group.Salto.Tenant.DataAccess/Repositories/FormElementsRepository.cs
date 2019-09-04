using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class FormElementsRepository : BaseRepository<FormElements>, IFormElementRepository
    {
        public FormElementsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<FormElements> GetValueByName(string name)
        {
            var result = Filter(x => x.Name == name);
            return result;
        }
    }
}