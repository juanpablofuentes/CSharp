using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ContactsLocationsDtoExtensions
    {
        public static ContactsDto ToContactsLocationsDto(this ContactsLocationsFinalClients source)
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

        public static IEnumerable<ContactsDto> ToContactsLocationsDto(this IEnumerable<ContactsLocationsFinalClients> source)
        {
            return source?.MapList(cc => cc.ToContactsLocationsDto());
        }
    }
}
