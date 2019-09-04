using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPreconditionsLiteralValuesRepository
    {
        SaveResult<PreconditionsLiteralValues> UpdatePreconditionsLiteralValues(PreconditionsLiteralValues entity);
        SaveResult<PreconditionsLiteralValues> CreatePreconditionsLiteralValues(PreconditionsLiteralValues entity);
        PreconditionsLiteralValues DeletePreconditionsLiteralValues(PreconditionsLiteralValues entity);
        PreconditionsLiteralValues DeleteOnContextPreconditionsLiteralValues(PreconditionsLiteralValues entity);
        PreconditionsLiteralValues GetById(int id);
    }
}