using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.User;
using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.User;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class UsersExtensions
    {
        public static UserDto ToUserDto(this Users source)
        {
            UserDto user = null;
            if (source != null)
            {
                user = new UserDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    LanguageId= source.LanguageId,
                    ConfigurationWoFields = source.ConfigurationWoFields,
                    FirstSurname = source.FirstSurname,
                    NumberEntriesPerPage = source.NumberEntriesPerPage,
                    Observations = source.Observations,
                    OldUserId = source.OldUserId,
                    SecondSurname = source.SecondSurname,
                    UserConfigurationId = source.UserConfigurationId                    
                };
            }

            return user;
        }

        public static UserApiDto ToApiDto(this Users source)
        {
            UserApiDto user = null;
            if (source != null)
            {
                user = new UserApiDto()
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
                    TennantId = source.Customer.Id
                };
            }

            return user;
        }

        public static PeopleListDto ToListDto(this Users source)
        {
            PeopleListDto result = null;
            if (source != null)
            {
                result = new PeopleListDto()
                {
                    UserId = source.Id,
                    UserName = source.UserName,
                    UserConfigurationId = source.UserConfigurationId,
                };
            }

            return result;
        }

        public static IList<PeopleListDto> ToListDto(this IList<Users> source)
        {
            return source?.MapList(c => c.ToListDto());
        }

        public static IEnumerable<PeopleListDto> ToListDto(this IEnumerable<Users> source)
        {
            return source?.MapList(c => c.ToListDto());
        }

        public static AccessUserDto ToAccessUserDto(this Users source, string rolname)
        {
            AccessUserDto result = null;
            if (source != null)
            {
                result = new AccessUserDto()
                {
                    Id = new Guid(source.Id),
                    UserName = source.UserName,
                    UserConfigurationId = source.UserConfigurationId.Value,
                    NumberEntriesPerPage = source.NumberEntriesPerPage,
                    SelectedRol = rolname,
                    LanguageId = source.LanguageId,
                    CustomerId = source.CustomerId,
                    IsActive = source.IsActive
                };
            }

            return result;
        }
    }
}