using System.Linq;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.ClosureCode;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ClosureCodeDtoExtensions
    {
        public static ClosureCodeDto ToDto(this CollectionsClosureCodes source)
        {
            ClosureCodeDto result = null;
            if (source != null)
            {
                result = new ClosureCodeDto();
                source.ToBaseDto(result);
                result.ClosingCodes = source.ClosingCodes?.ToList().GetClosingCodesDtoTree(null);
            }

            return result;
        }

        public static CollectionsClosureCodes ToEntity(this ClosureCodeDto source)
        {
            CollectionsClosureCodes result = null;
            if (source != null)
            {
                result = new CollectionsClosureCodes();
                source.ToEntity(result);
                result.ClosingCodes = source.ClosingCodes.ToEntity(result);
            }

            return result;
        }
    }
}