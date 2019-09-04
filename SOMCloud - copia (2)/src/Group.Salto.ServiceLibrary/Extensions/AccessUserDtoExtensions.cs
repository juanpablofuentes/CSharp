using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.User;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class AccessUserDtoExtensions
    {
        public static Users ToEntity(this AccessUserDto source, PeopleDto peopleDto)
        {
            Users user = null;
            if (source != null)
            {
                user = new Users();
                SetData(source, peopleDto, user);
            }

            return user;
        }

        public static Users ToUpdateEntity(this AccessUserDto source, PeopleDto peopleDto, Users user)
        {
            if (source != null)
            {
                SetData(source, peopleDto, user);

                if (!string.IsNullOrEmpty(source.Password))
                {
                    user.PasswordHash = source.Password;
                }
            }

            return user;
        }
       

        private static void SetData(AccessUserDto source, PeopleDto peopleDto, Users user)
        {
            user.UserName = source.UserName;
            user.OldUserId = source.UserConfigurationId;
            user.UserConfigurationId = source.UserConfigurationId;
            user.Name = peopleDto.Name;
            user.FirstSurname = peopleDto.Surname;
            user.SecondSurname = peopleDto.SecondSurname;
            user.Email = source.UserName;
            user.NumberEntriesPerPage = source.NumberEntriesPerPage;
            user.LanguageId = source.LanguageId;
            user.CustomerId = source.CustomerId;
            user.IsActive = source.IsActive;
        }

        public static bool IsValid(this AccessUserDto source)
        {
            bool result = false;

            result = source != null
                   && !string.IsNullOrEmpty(source.UserName)
                   && ValidationsHelper.MaxLength(source.UserName, 256)
                   && ValidationsHelper.MaxLength(source.Password, 50);

            if (result && !string.IsNullOrEmpty(source.UserName))
                result = ValidationsHelper.IsEmailValid(source.UserName);

            return result;
        }
    }
}