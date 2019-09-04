using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ProjectsContactsDtoExtensions
    {
        public static ContactsDto ToProjectsContactsDto(this ProjectsContacts source)
        {
            ContactsDto result = null;
            if (source != null)
            {
                result = new ContactsDto()
                {
                    Id = source.Contact.Id,
                    Name = source.Contact.Name,
                    FirstSurname = source.Contact.FirstSurname,
                    SecondSurname = source.Contact.SecondSurname,
                    Position = source.Contact.Position,
                    Email = source.Contact.Email,
                    Telephone = source.Contact.Telephone,
                    FullName = $"{source.Contact.Name} {source.Contact.FirstSurname ?? string.Empty} {source.Contact.SecondSurname ?? string.Empty}"
                };
            }
            return result;
        }

        public static IList<ContactsDto> ToProjectsContactsDto(this IList<ProjectsContacts> source)
        {
            return source?.MapList(cc => cc.ToProjectsContactsDto());
        }
    }
}