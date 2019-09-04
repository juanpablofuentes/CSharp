using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ContractContactsDtoExtensions
    {
        public static ContactsDto ToContractContactsDto(this ContractContacts source)
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
                    Email  = source.Contact.Email,
                    Telephone = source.Contact.Telephone,
                    FullName = $"{source.Contact.Name} {source.Contact.FirstSurname ?? string.Empty} {source.Contact.SecondSurname ?? string.Empty}"
                };
            }

            return result;
        }

        public static IList<ContactsDto> ToContractContactsDto(this IList<ContractContacts> source)
        {
            return source?.MapList(cc => cc.ToContractContactsDto());
        }

        public static Contacts ToEntity(this ContactsDto source)
        {
            Contacts result = null;
            if (source != null)
            {
                result = new Contacts()
                {
                    Name = source.Name,
                    FirstSurname = source.FirstSurname,
                    SecondSurname = source.SecondSurname,
                    Email = source.Email,
                    Telephone = source.Telephone,
                    Position = source.Position
                };
            }

            return result;
        }

        public static IList<Contacts> ToEntity(this IList<ContactsDto> source)
        {
            return source?.MapList(e => e.ToEntity());
        }
    }
}