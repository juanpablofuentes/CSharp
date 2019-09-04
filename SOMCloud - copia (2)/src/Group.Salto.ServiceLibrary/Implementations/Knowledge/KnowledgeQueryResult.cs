using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Knowledge;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Knowledge
{
    public class KnowledgeQueryResult : IKnowledgeQueryResult
    {
        private IKnowledgeRepository _knowledgeRepository;

        public KnowledgeQueryResult(IKnowledgeRepository knowledgeRepository)
        {
            _knowledgeRepository = knowledgeRepository ?? throw new ArgumentNullException($"{nameof(IKnowledgeRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            Int32.TryParse(queryTypeParameters.Value, out int companyId);
            var data = _knowledgeRepository.GetAllKeyValues();
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}