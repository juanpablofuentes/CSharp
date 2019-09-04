using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.SymptomCollection;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.SymptomCollection
{
    public interface ISymptomCollectionService
    {
        ResultDto<IList<SymptomCollectionBaseDto>> GetAllFiltered(SymptomCollectionFilterDto filter);
        ResultDto<SymptomCollectionDto> GetById(int id);
        ResultDto<SymptomCollectionDto> Create(SymptomCollectionDto model);
        ResultDto<SymptomCollectionDto> Update(SymptomCollectionDto model);
        ResultDto<bool> Delete(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}