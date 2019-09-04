using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.PreconditionLiteralValues;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions
{
    public interface ILiteralPreconditionsService
    {
        LiteralPreconditionsDto GetLiteralPrecondition(int id);
        List<MultiSelectItemDto> GetLiteralValuesList(FilterQueryParametersBase filterQueryParameters);
        IList<PreconditionLiteralValuesDto> GetLiteralValues(FilterQueryParametersBase filterQueryParameters);
        LiteralPreconditionsDto Update(LiteralPreconditionsDto model);
        LiteralPreconditionsDto Create(LiteralPreconditionsDto model);
        ResultDto<bool> Delete(int id);
        LiteralsPreconditions DeleteOnContext(int id);
    }
}