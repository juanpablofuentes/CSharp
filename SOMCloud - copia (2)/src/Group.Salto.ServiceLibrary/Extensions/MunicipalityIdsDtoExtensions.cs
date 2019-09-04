using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Customer;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class MunicipalityIdsDtoExtensions
    {
        public static MunicipalityIdsDto ToIdsDto(this Municipalities source)
        {
            MunicipalityIdsDto result = null;
            if (source?.State?.Region?.Country != null)
            {
                result = new MunicipalityIdsDto()
                {
                    StateId = source.StateId,
                    CountryId = source.State.Region.CountryId,
                    MunicipalityId = source.Id,
                    RegionId = source.State.RegionId,
                };
            }
            return result;
        }
    }
}