using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.People;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PeopleKnowledgeDtoExtensions
    {
        public static PeopleKnowledgeDto ToKnowledgePeopleDto(this KnowledgePeople source)
        {
            PeopleKnowledgeDto result = null;
            if (source != null)
            {
                result = new PeopleKnowledgeDto()
                {
                    Name = source.Knowledge.Name,
                    Id = source.Knowledge.Id,
                    Priority = source.Maturity,
                };
            }

            return result;
        }

        public static IList<PeopleKnowledgeDto> ToKnowledgePeopleDto(this IList<KnowledgePeople> source)
        {
            return source?.MapList(kp => kp.ToKnowledgePeopleDto());
        }
    }
}