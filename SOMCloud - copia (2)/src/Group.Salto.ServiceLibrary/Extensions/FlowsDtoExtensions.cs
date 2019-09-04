using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Flows;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class FlowsDtoExtensions
    {
        public static FlowsListDto ToListDto(this Flows source)
        {
            FlowsListDto result = null;
            if (source != null)
            {
                result = new FlowsListDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Published = source.Published
                };

            }
            return result;
        }

        public static IList<FlowsListDto> ToListDto(this IList<Flows> source)
        {
            return source?.MapList(x => x.ToListDto());
        }

        public static FlowsDto ToDto(this Flows source)
        {
            FlowsDto result = null;
            if (source != null)
            {
                result = new FlowsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Published = source.Published
                };
            }
            return result;
        }

        public static Flows ToEntity(this FlowsDto source)
        {
            Flows result = null;
            if (source != null)
            {
                result = new Flows()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Published = source.Published
                };
            }
            return result;
        }
    }
}