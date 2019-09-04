using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Symptoms;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Symptom
{
    public interface ISymptomService
    {
        ResultDto<IList<SymptomBaseDto>> GetAllFiltered(SymptomFilterDto filter);
        ResultDto<SymptomBaseDto> GetById(int id);
        ResultDto<SymptomBaseDto> Create(SymptomBaseDto model);
        ResultDto<SymptomBaseDto> Update(SymptomBaseDto model);
        ResultDto<bool> Delete(int id);
        IList<BaseNameIdDto<int>> GetOrphansKeyValue();
    }
}