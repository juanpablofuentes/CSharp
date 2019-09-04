using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.User;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class UserConfigurationsExtensions
    {
        public static UserConfigurationsDto ToDto(this UserConfiguration source)
        {
            UserConfigurationsDto result = null;

            if (source != null)
            {
                result = new UserConfigurationsDto()
                {
                    Id = source.Id,
                    GuidId = source.GuidId
                };
            }

            return result;
        }

        public static UserConfiguration ToEntity(this UserConfigurationsDto source)
        {
            UserConfiguration result = null;

            if (source != null)
            {
                result = new UserConfiguration()
                {
                    GuidId = source.GuidId
                };
            }

            return result;
        }
    }
}