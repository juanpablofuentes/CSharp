using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.AdvancedSearch;
using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClients;
using Group.Salto.ServiceLibrary.Common.Dtos.Origins;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class FinalClientsDtoExtensions
    {
        public static FinalClientsListDto ToDto(this FinalClients source)
        {
            FinalClientsListDto result = null;
            if (source != null)
            {
                result = new FinalClientsListDto();
                result.Id = source.Id;
                result.Name = source.Name;
                result.Description = source.Description;
                result.Nif = source.Nif;
                result.Phone1 = source.Phone1;
                result.Fax = source.Fax;
                result.Observations = source.Observations;
                //TODO
                //result.Origin = origins.;
            }
            return result;
        }

        public static IList<FinalClientsListDto> ToDto(this IList<FinalClients> source)
        {
            return source?.MapList(c => c.ToDto());
        }

        public static FinalClientsDetailDto ToDetailDto(this FinalClients source)
        {
            FinalClientsDetailDto result = null;
            if (source != null)
            {
                result = new FinalClientsDetailDto();
                result.Id = source.Id;
                result.IdExtern = source.IdExtern;
                result.OriginId = source.OriginId;
                result.Name = source.Name;
                result.Description = source.Description;
                result.Nif = source.Nif;
                result.Phone1 = source.Phone1;
                result.Phone2 = source.Phone2;
                result.Phone3 = source.Phone3;
                result.Fax = source.Fax;
                result.Status = source.Status;
                result.Observations = source.Observations;
                result.PeopleCommercialId = source.PeopleCommercialId;
                result.Code = source.Code;
                if (source.ContactsFinalClients?.Any()==true)
                {
                    result.Contacts = ContactsFinalClientsDtoExtensions.ToFinalClientsContactsDto(source.ContactsFinalClients.ToList());
                }
            }
            return result;
        }

        public static FinalClients ToEntity(this FinalClientsDetailDto source)
        {
            FinalClients result = null;
            if (source != null)
            {
                result = new FinalClients();
                result.Id = source.Id;
                result.IdExtern = source.IdExtern;
                result.OriginId = source.OriginId;
                result.Name = source.Name;
                result.Description = source.Description;
                result.Nif = source.Nif;
                result.Phone1 = source.Phone1;
                result.Phone2 = source.Phone2;
                result.Phone3 = source.Phone3;
                result.Fax = source.Fax;
                result.Status = source.Status;
                result.Observations = source.Observations;
                result.PeopleCommercialId = source.PeopleCommercialId;
                result.Code = source.Code;
            }
            return result;
        }

        public static void UpdateFinalClients(this FinalClients target, FinalClientsDetailDto source)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.IdExtern = source.IdExtern;
                target.OriginId = source.OriginId;
                target.Name = source.Name;
                target.Description = source.Description;
                target.Nif = source.Nif;
                target.Phone1 = source.Phone1;
                target.Phone2 = source.Phone2;
                target.Phone3 = source.Phone3;
                target.Fax = source.Fax;
                target.Status = source.Status;
                target.Observations = source.Observations;
                target.PeopleCommercialId = source.PeopleCommercialId;
                target.Code = source.Code;
            }
        }

        public static FinalClients AssignFinalClientContacts(this FinalClients entity, IList<ContactsDto> contacts)
        {
            entity?.ContactsFinalClients.Clear();
            if (contacts != null && contacts.Any())
            {
                entity.ContactsFinalClients = entity.ContactsFinalClients ?? new List<ContactsFinalClients>();
                IList<Contacts> localContacts = contacts.ToEntity();
                foreach (Contacts localContact in localContacts)
                {
                    entity.ContactsFinalClients.Add(new ContactsFinalClients()
                    {
                        Contact = localContact
                    });
                }
            }
            return entity;
        }


        public static AdvancedSearchDto ToAdvancedSearchDto(this FinalClients source)
        {
            AdvancedSearchDto result = null;
            if (source != null)
            {
                result = new AdvancedSearchDto
                {
                    Id = source.Id,
                    Name = $"{source.Code ?? string.Empty} - {source.Name}",
                };
                result.Details.Add(source.Name);
                result.Details.Add(source.Description ?? string.Empty);
            }

            return result;
        }

        public static IList<AdvancedSearchDto> ToAdvancedSearchListDto(this IList<FinalClients> source)
        {
            return source?.MapList(x => x.ToAdvancedSearchDto());
        }
    }
}