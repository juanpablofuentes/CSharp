using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Knowledge;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class KnowledgeDtoExtensions
    {
        public static KnowledgeDto ToDto(this Knowledge source)
        {
            KnowledgeDto result = null;
            if (source != null)
            {
                result = new KnowledgeDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    UpdateDate = !ValidationsHelper.IsMinDateValue(source.UpdateDate) ? source.UpdateDate.ToShortDateString() : string.Empty,
                    Observations = source.Observations,
                };
            }
            return result;
        }

        public static IList<KnowledgeDto> ToDto(this IList<Knowledge> source)
        {
            return source.MapList(x => x.ToDto());
        }

        public static Knowledge Update(this Knowledge target, KnowledgeDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Observations = source.Observations;
            }

            return target;
        }

        public static Knowledge ToEntity(this KnowledgeDto source)
        {
            Knowledge result = null;
            if (source != null)
            {
                result = new Knowledge()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations
                };
            }

            return result;
        }
    }
}