using System.Linq;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Location;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class LocationDtoExtensions
    {
        public static LocationDto ToDto(this Locations dbModel)
        {
            var dto = new LocationDto
            {
                Name = dbModel.Name,
                Area = dbModel.Area,
                City = dbModel.City,
                CityId = dbModel.CityId,
                Code = dbModel.Code,
                Country = dbModel.Country,
                CountryId = dbModel.CountryId,
                Escala = dbModel.Escala,
                GateNumber = dbModel.GateNumber,
                HashCity = dbModel.HashCity,
                HashProvincie = dbModel.HashProvincie,
                Observations = dbModel.Observations,
                Latitude = dbModel.Latitude,
                Longitude = dbModel.Longitude,
                HashSubzone = dbModel.HashSubzone,
                HashZone = dbModel.HashZone,
                MunicipalityId = dbModel.MunicipalityId,
                Number = dbModel.Number,
                PeopleResponsibleLocationId = dbModel.PeopleResponsibleLocationId,
                Phone1 = dbModel.Phone1,
                Phone2 = dbModel.Phone2,
                Phone3 = dbModel.Phone3,
                PostalCode = dbModel.PostalCode,
                PostalCodeId = dbModel.PostalCodeId,
                Province = dbModel.Province,
                RegionId = dbModel.RegionId,
                StateId = dbModel.StateId,
                Street = dbModel.Street,
                StreetType = dbModel.StreetType,
                Subzone = dbModel.Subzone,
                Zone = dbModel.Zone,
                Contacts = dbModel.ContactsLocationsFinalClients?.ToContactsLocationsDto(),
                PeopleResponsible = dbModel.PeopleResponsibleLocation?.ToDto()
            };

            return dto;
        }
    }
}
