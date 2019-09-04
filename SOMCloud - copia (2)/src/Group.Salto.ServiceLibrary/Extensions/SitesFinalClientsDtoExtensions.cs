using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SitesFinalClientsDtoExtensions
    {
        public static LocationsFinalClients ToEntity(this SitesFinalClientsDto source)
        {
            LocationsFinalClients result = null;
            if (source != null)
            {
                result = new LocationsFinalClients()
                {
                    FinalClientId = source.FinalClientId,
                    LocationId = source.LocationId,
                    PeopleCommercialId = source.PeopleCommercialId,
                    OriginId = source.OriginId,
                    CompositeCode = source.CompositeCode,
                };
            }
            return result;
        }

        public static SitesFinalClientsDto ToDetailDto(this LocationsFinalClients source)
        {
            SitesFinalClientsDto result = null;
            if (source != null)
            {
                result = new SitesFinalClientsDto
                {
                    FinalClientId = source.FinalClientId,
                    LocationId = source.LocationId,
                    PeopleCommercialId = source.PeopleCommercialId,
                    OriginId = source.OriginId,
                    CompositeCode = source.CompositeCode,
                };
            }
            return result;
        }
    }
}