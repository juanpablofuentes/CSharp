using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Knowledge;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Knowledge
{
    public interface IKnowledgeService
    {
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        ResultDto<IList<KnowledgeDto>> GetAll();
        ResultDto<KnowledgeDto> GetById(int id);
        ResultDto<KnowledgeDto> UpdateKnowledge(KnowledgeDto source);
        ResultDto<IList<KnowledgeDto>> GetAllFiltered(KnowledgeFilterDto filter);
        ResultDto<KnowledgeDto> CreateKnowledge(KnowledgeDto source);
        ResultDto<bool> DeleteKnowledge(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        
    }
}