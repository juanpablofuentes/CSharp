using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.SubContract;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SubContractKnowledgeDtoExtensions
    {
        public static SubContractKnowledgeDto ToSubContractKnowledgeDto(this KnowledgeSubContracts source)
        {
            SubContractKnowledgeDto result = null;
            if (source != null)
            {
                result = new SubContractKnowledgeDto()
                {
                    Name = source.Knowledge.Name,
                    Id = source.Knowledge.Id,
                    Priority = source.Maturity,
                };
            }

            return result;
        }

        public static IList<SubContractKnowledgeDto> ToSubContractKnowledgeDto(this IList<KnowledgeSubContracts> source)
        {
            return source?.MapList(sC => sC.ToSubContractKnowledgeDto());
        }
    }
}