using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IClosingCodeRepository : IRepository<ClosingCodes>
    {
        ClosingCodes GetById(int id);
        ClosingCodes GetByIdIncludeFathers(int id);
    }
}