using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using System;
using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.ServiceLibrary.Extensions
{ 
    public static class ContactsFinalClientsDtoExtensions
    {
        public static ContactsDto ToFinalClientsContactsDto(this ContactsFinalClients source)
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

        public static IList<ContactsDto> ToFinalClientsContactsDto(this IList<ContactsFinalClients> source)
        {
            return source?.MapList(cc => cc.ToFinalClientsContactsDto());
        }
    }
}