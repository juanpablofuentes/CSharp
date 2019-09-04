using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.AdvancedSearch;
using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using Group.Salto.ServiceLibrary.Common.Dtos.Sites;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SitesDetailDtoExtensions
    {
        public static SitesDetailDto ToDetailDto(this Locations source)
        {
            SitesDetailDto result = null;
            if (source != null)
            {
                result = new SitesDetailDto
                {
                    Id = source.Id,
                    Name = source.Name,
                    Code = source.Code,
                    Observations = source.Observations,
                    Phone1 = source.Phone1,
                    Phone2 = source.Phone2,
                    Phone3 = source.Phone3,
                    Fax = source.Fax,
                    CountryId = source.CountryId,
                    RegionId = source.RegionId,
                    StateId = source.StateId,
                    CityId = source.CityId,
                    Street = source.Street,
                    PostalCode = source.PostalCode.HasValue ? source.PostalCode.Value.ToString() : string.Empty,
                    Area = source.Area,
                    Zone = source.Zone,
                    SubZone = source.Subzone,
                    ContactsSelected = source.ContactsLocationsFinalClients?.ToList()?.ToSitesContactsDto(),
                };
            }
            return result;
        }

        public static SitesDetailDto ToDetailDtoWithClient(this Locations source, int finalClientId)
        {
            SitesDetailDto result = null;
            if (source != null)
            {
                result = new SitesDetailDto
                {
                    Id = source.Id,
                    Name = source.Name,
                    Code = source.Code,
                    Observations = source.Observations,
                    Latitude = ((decimal?)source.Latitude)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    Longitude = ((decimal?)source.Longitude)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    Phone1 = source.Phone1,
                    Phone2 = source.Phone2,
                    Phone3 = source.Phone3,
                    Fax = source.Fax,
                    CountryId = source.CountryId,
                    RegionId = source.RegionId,
                    StateId = source.StateId,
                    CityId = source.CityId,
                    MunicipalityId = source.MunicipalityId,
                    FinalClientId = finalClientId,
                    Street = source.Street,
                    PostalCode = source.PostalCode.HasValue ? source.PostalCode.Value.ToString() : string.Empty,
                    Area = source.Area,
                    Zone = source.Zone,
                    SubZone = source.Subzone,
                    ContactsSelected = source.ContactsLocationsFinalClients?.ToList()?.ToSitesContactsDto(),
                };
            }
            return result;
        }

        public static Locations ToEntity(this SitesDetailDto source)
        {
            Locations result = null;
            if (source != null)
            {
                result = new Locations()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Code = source.Code,
                    Observations = source.Observations,
                    Phone1 = source.Phone1,
                    Phone2 = source.Phone2,
                    Phone3 = source.Phone3,
                    Fax = source.Fax,
                    CountryId = source.CountryId,
                    RegionId = source.RegionId,
                    StateId = source.StateId,
                    CityId = source.MunicipalityId,
                    MunicipalityId = source.CityId,
                    StreetType = source.StreetType,
                    Street = source.Street,
                    Escala = source.Gate,
                    GateNumber = source.GateNumber,
                    PostalCode = (!string.IsNullOrEmpty(source.PostalCode))? int.Parse(source.PostalCode) : 0,
                    Area = source.Area,
                    Zone = source.Zone,
                    Subzone = source.SubZone,
                };
            }
            return result;
        }

        public static Locations Update(this Locations target, SitesDetailDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Code = source.Code;
                target.Observations = source.Observations;
                target.Phone1 = source.Phone1;
                target.Phone2 = source.Phone2;
                target.Phone3 = source.Phone3;
                target.Fax = source.Fax;
                target.Street = source.Street;
                target.StreetType = source.StreetType;
                target.Escala = source.Gate;
                target.GateNumber= source.GateNumber;
                target.CountryId = source.CountryId;
                target.RegionId = source.RegionId;
                target.StateId = source.StateId;
                target.CityId = source.MunicipalityId;
                target.MunicipalityId = source.CityId;
                target.PostalCode = (!string.IsNullOrEmpty(source.PostalCode)) ? int.Parse(source.PostalCode) : 0;
                target.Area = source.Area;
                target.Zone = source.Zone;
                target.Subzone = source.SubZone;
                target.Latitude = (double?)source.Latitude?.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName);
                target.Longitude = (double?)source.Longitude?.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName);
            }
            return target;
        }

        public static Locations AssignSitesContacts(this Locations entity, IList<ContactsDto> contacts)
        {
            entity.ContactsLocationsFinalClients?.Clear();
            if (contacts != null && contacts.Any())
            {
                entity.ContactsLocationsFinalClients = entity.ContactsLocationsFinalClients ?? new List<ContactsLocationsFinalClients>();
                IList<Contacts> localContacts = contacts.ToEntity();
                foreach (Contacts localContact in localContacts)
                {
                    entity.ContactsLocationsFinalClients.Add(new ContactsLocationsFinalClients()
                    {
                        Contact = localContact
                    });
                }
            }
            return entity;
        }

        public static AdvancedSearchDto ToAdvancedSearchDto(this Locations source)
        {
            AdvancedSearchDto result = null;
            if (source != null)
            {
                result = new AdvancedSearchDto
                {
                    Id = source.Id,
                    Name = $"{source.Code} - {source.Name}",
                };
                result.Details.Add(source.Name);
                result.Details.Add(source.Code);
                result.Details.Add($"{source.PostalCodeId}|{source.MunicipalityId}");
            }

            return result;
        }

        public static IList<AdvancedSearchDto> ToAdvancedSearchListDto(this IList<Locations> source)
        {
            return source?.MapList(x => x.ToAdvancedSearchDto());
        }
    }
}