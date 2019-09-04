using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.User;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.User;

namespace Group.Salto.SOM.Web.Models.User
{
    public static class UserExtensions
    {
        public static UserDto ToDto(this Users source)
        {
            UserDto user = null;
            if (source != null)
            {
                user = new UserDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    LanguageId = source.LanguageId,
                    ConfigurationWoFields = source.ConfigurationWoFields,
                    FirstSurname = source.FirstSurname,
                    NumberEntriesPerPage = source.NumberEntriesPerPage,
                    Observations = source.Observations,
                    OldUserId = source.OldUserId,
                    SecondSurname = source.SecondSurname,
                    UserConfigurationId = source.UserConfigurationId,
                };
            }

            return user;
        }
    }
}
