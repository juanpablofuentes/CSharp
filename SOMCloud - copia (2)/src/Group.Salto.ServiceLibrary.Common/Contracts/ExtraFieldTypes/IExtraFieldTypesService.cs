using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFieldTypes;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ExtraFieldTypes
{
    public interface IExtraFieldTypesService
    {
        ResultDto<ExtraFieldsTypesDto> GetById(int id);
        ResultDto<IList<ExtraFieldsTypesDto>> GetAll();
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}