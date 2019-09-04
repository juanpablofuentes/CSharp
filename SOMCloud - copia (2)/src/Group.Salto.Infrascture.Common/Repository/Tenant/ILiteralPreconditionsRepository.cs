using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ILiteralPreconditionsRepository
    {
        LiteralsPreconditions GetLiteralPreconditions(int id);
        IQueryable<PreconditionsLiteralValues> GetLiteralValuesOfPrecondition(int preconditionId, Guid literalPreconditionType);
        SaveResult<LiteralsPreconditions> UpdateLiteralPreconditions(LiteralsPreconditions entity);
        SaveResult<LiteralsPreconditions> CreateLiteralPreconditions(LiteralsPreconditions entity);
        SaveResult<LiteralsPreconditions> DeleteLiteralPreconditions(LiteralsPreconditions entity);
        LiteralsPreconditions DeleteLiteralPreconditionsOnContext(LiteralsPreconditions entity);
    }
}