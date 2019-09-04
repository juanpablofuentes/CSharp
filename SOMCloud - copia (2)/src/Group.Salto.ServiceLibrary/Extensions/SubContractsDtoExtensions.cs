using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.SubContract;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SubContractsDtoExtensions
    {
        public static SubContractDto ToDto(this SubContracts source)
        {
            SubContractDto result = null;
            if (source != null)
            {
                result = new SubContractDto();
                source.ToBaseDto(result);
                result.KnowledgeSelected = source.KnowledgeSubContracts?.ToList()?.ToSubContractKnowledgeDto();
                result.PeopleSelected = source.People
                    ?.ToList()
                    ?.ToPeopleSelectableDto();
                result.PurchaseRateId = source.PurchaseRateId;
            }

            return result;
        }

        public static SubContracts ToEntity(this SubContractDto source)
        {
            SubContracts result = null;
            if (source != null)
            {
                result = new SubContracts();
                source.ToEntity(result);
            }
            return result;
        }

        public static void Update(this SubContracts target, SubContractDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Observations = source.Observations;
                target.Priority = source.Priority;
            }
        }
        
        public static IList<SubContractDto> ToDto(this IList<SubContracts> source)
        {
            return source?.MapList(s => s.ToDto());
        }
    }
}
