using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Sites;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Sites;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SitesDetailViewModelExtensions
    {
        public static GenericDetailViewModel ToGenericViewModel (this ResultDto<SitesDetailDto> source)
        {
            var response = new GenericDetailViewModel
            {
                IdSite = source.Data.Id,
                Name = source.Data?.Name,
                Code = source.Data?.Code,
                Observations = source.Data?.Observations,
                Phone1 = source.Data?.Phone1,
                Phone2 = source.Data?.Phone2,
                Phone3 = source.Data?.Phone3,
                Fax = source.Data?.Fax,
                FinalClientId = source.Data.FinalClientId,
                ContactsSelected = source.Data.ContactsSelected.ToContactsEditViewModel()
            };
            return response;
        }

        public static GeolocationDetailViewModel ToGeolocationViewModel(this ResultDto<SitesDetailDto> source, GeolocationDetailViewModel geo)
        {
            var response = new GeolocationDetailViewModel
            {
                Apikey = geo.Apikey,
                Latitude = source.Data?.Latitude,
                Longitude = source.Data?.Longitude,
                Street = source.Data?.Street,
                CountrySelected = source.Data?.CountryId,
                RegionSelected = source.Data?.RegionId,
                StateSelected = source.Data?.StateId,
                CitySelected = source.Data?.MunicipalityId,
                MunicipalitySelected = source.Data?.CityId,
                StreetType = source.Data?.StreetType,
                Gate = source.Data?.Gate,
                GateNumber = source.Data?.GateNumber,
                PostalCode = source.Data?.PostalCode,
                Area = source.Data?.Area,
                Zone = source.Data?.Zone,
                SubZone = source.Data?.SubZone
            };
            return response;
        }

        public static ResultViewModel<SitesDetailViewModel> ToViewModel(this ResultDto<SitesDetailDto> source)
        {
            var response = source != null ? new ResultViewModel<SitesDetailViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static SitesDetailViewModel ToViewModel(this SitesDetailDto source)
        {
            SitesDetailViewModel result = null;
            if (source != null)
            {
                result = new SitesDetailViewModel()
                {
                    GenericDetailViewModel = new GenericDetailViewModel
                    {
                        IdSite = source.Id,
                        Name = source.Name,
                        Code = source.Code,
                        Observations = source.Observations,
                        Phone1 = source.Phone1,
                        Phone2 = source.Phone2,
                        Phone3 = source.Phone3,
                        Fax = source.Fax,
                        FinalClientId = source.FinalClientId,
                    }
                };
            }
            return result;
        }

        public static SitesDetailDto ToDto(this SitesDetailViewModel source)
        {
            SitesDetailDto result = null;
            if (source != null)
            {
                result = new SitesDetailDto()
                {
                    Id = source.GenericDetailViewModel.IdSite,
                    Name = source.GenericDetailViewModel?.Name,
                    Code = source.GenericDetailViewModel?.Code,
                    Observations = source.GenericDetailViewModel?.Observations,
                    Phone1 = source.GenericDetailViewModel?.Phone1,
                    Phone2 = source.GenericDetailViewModel?.Phone2,
                    Phone3 = source.GenericDetailViewModel?.Phone3,
                    Fax = source.GenericDetailViewModel?.Fax,
                    FinalClientId = source.GenericDetailViewModel.FinalClientId,
                    CountryId = source.GeolocationDetailViewModel?.CountrySelected,
                    RegionId = source.GeolocationDetailViewModel?.RegionSelected,
                    StateId = source.GeolocationDetailViewModel?.StateSelected,
                    CityId = source.GeolocationDetailViewModel?.CitySelected,
                    MunicipalityId = source.GeolocationDetailViewModel?.MunicipalitySelected,
                    PostalCode = source.GeolocationDetailViewModel?.PostalCode,
                    Area = source.GeolocationDetailViewModel?.Area,
                    SubZone = source.GeolocationDetailViewModel?.SubZone,
                    Zone = source.GeolocationDetailViewModel?.Zone,
                    Street = source.GeolocationDetailViewModel?.Street,
                    StreetType = source.GeolocationDetailViewModel?.StreetType,
                    Gate = source.GeolocationDetailViewModel?.Gate,
                    GateNumber = source.GeolocationDetailViewModel?.GateNumber,
                    Latitude = source.GeolocationDetailViewModel?.Latitude,
                    Longitude = source.GeolocationDetailViewModel?.Longitude,
                    ContactsSelected = source.GenericDetailViewModel.ContactsSelected.ToContactsContractsDto(),
                };
            }
            return result;
        }
    }
}