using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.SubContract;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SubcontractsBaseDtoExtensions
    {
        public static SubContractBaseDto ToBaseDto(this SubContracts source)
        {
            SubContractBaseDto result = null;
            if (source != null)
            {
                result = new SubContractBaseDto();
                source.ToBaseDto(result);
            }

            return result;
        }

        public static void ToBaseDto(this SubContracts source, SubContractBaseDto target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Id = source.Id;
                target.Observations = source.Observations;
                target.Priority = source.Priority;
            }
        }

        public static void ToEntity(this SubContractBaseDto source, SubContracts target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Id = source.Id;
                target.Observations = source.Observations;
                target.Priority = source.Priority;
            }
        }

        public static IList<SubContractBaseDto> ToBaseDto(this IList<SubContracts> source)
        {
            return source?.MapList(s => s.ToBaseDto());
        }
    }
}