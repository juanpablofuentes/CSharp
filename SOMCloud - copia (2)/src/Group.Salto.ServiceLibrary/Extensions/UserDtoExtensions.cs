using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.User;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class UserDtoExtensions
    {

        public static Users ToEntity(this UserDto source)
        {
            Users user = null;
            if (source != null)
            {
                user = new Users()
                {
                    UserName = source.UserName,
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