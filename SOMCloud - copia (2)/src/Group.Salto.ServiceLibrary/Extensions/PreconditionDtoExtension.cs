using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Preconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.PreconditionsTypes;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PreconditionDtoExtension
    {
        public static PreconditionsDto ToDto(this Preconditions source, List<PreconditionsTypesDto> types)
        {
            PreconditionsDto result = null;
            if (source != null)
            {
                result = new PreconditionsDto()
                {
                    Id = source.Id,
                    TaskId = source.TaskId,
                    PostconditionCollectionId = source.PostconditionCollectionId,
                    PeopleResponsibleTechniciansCollectionId = source.PeopleResponsibleTechniciansCollectionId,
                    LiteralsPreconditions = source.LiteralsPreconditions.ToDto(types)
                };
            }

            return result;
        }

        public static Preconditions ToEntity(this PreconditionsDto source)
        {
            Preconditions result = null;
            if (source != null)
            {
                result = new Preconditions()
                {
                    Id = source.Id,
                    TaskId = source.TaskId,
                    PostconditionCollectionId = source.PostconditionCollectionId,
                    PeopleResponsibleTechniciansCollectionId = source.PeopleResponsibleTechniciansCollectionId
                };
            }

            return result;
        }

        public static IList<PreconditionsDto> ToDto(this IList<Preconditions> source, List<PreconditionsTypesDto> types)
        {
            return source?.MapList(x => x.ToDto(types)).ToList();
        }
    }
}