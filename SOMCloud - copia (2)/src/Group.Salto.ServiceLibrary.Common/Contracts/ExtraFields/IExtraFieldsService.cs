using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFields;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ExtraFields
{
    public interface IExtraFieldsService
    {
        ResultDto<IList<ExtraFieldsDetailDto>> GetAllFilteredByLanguage(ExtraFieldsFilterDto filter);
        ResultDto<IList<ExtraFieldsDto>> GetAllFiltered(ExtraFieldsFilterDto filter);
        ResultDto<ExtraFieldsDetailDto> GetById(int id);
        ResultDto<ExtraFieldsDetailDto> Update(ExtraFieldsDetailDto model);
        IList<BaseNameIdDto<int>> GetAllByDelSystemKeyValues(int languageId, bool isSystem);
        ResultDto<ExtraFieldsDetailDto> Create(ExtraFieldsDetailDto model);
        ResultDto<bool> Delete(int id);
        ResultDto<bool> CanDelete(int id);
        int GetExtraFieldIdFormName(string extraFieldName);
    }
}