using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Country;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Country
{
    public class CountryService : BaseService, ICountryService
    {
        private readonly ICache _cache;
        private readonly ICountryRepository _countryRepository;


        public CountryService(ILoggingService logginingService, 
            ICache cache, 
            ICountryRepository countryRepository): base(logginingService)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(ICache));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(ICountryRepository));
        }

        //TODO: Rename to ¿GetAllIdName?
        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            return GetCacheData().Select(x=>new BaseNameIdDto<int>()
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();
        }
        
        public IList<BaseNameIdDto<int>> GetKeyValuesRegions(int countryId)
        {
            return GetCacheData().SingleOrDefault(x=>x.Id == countryId)?
                                 .Regions?.Select(x => new BaseNameIdDto<int>()
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetKeyValuesStates(int regionId)
        {
            return GetCacheData().SelectMany(x => x.Regions)
                                ?.SingleOrDefault(x=>x.Id == regionId)
                                ?.States?.Select(x => new BaseNameIdDto<int>()
                                {
                                    Name = x.Name,
                                    Id = x.Id,
                                }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetKeyValuesMunicipalities(int stateId)
        {
            return GetCacheData().SelectMany(x => x.Regions)?.SelectMany(x=>x.States)
                ?.SingleOrDefault(x => x.Id == stateId)
                ?.Municipalities?.Select(x => new BaseNameIdDto<int>()
                {
                    Name = x.Name,
                    Id = x.Id,
                }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetKeyValuesCities(int muniId)
        {
            var countries = _countryRepository.GetAll();
            var cities = countries.SelectMany(x => x.Regions)?
                    .SelectMany(x => x.States)?
                    .SelectMany(x => x.Municipalities)?
                    .SelectMany(x => x.Cities)?
                    .Where(x => x.MunicipalityId == muniId).Select(x => new BaseNameIdDto<int>()
                    {
                        Name = x.Name,
                        Id = x.Id,
                    }).ToList();

            return cities;
        }


        public Municipalities GetMunicipality(int municipalityId)
        {
            var countries = _countryRepository.GetAll();
            var municipality = countries.SelectMany(x => x.Regions)?
                    .SelectMany(x => x.States)?
                    .SelectMany(x => x.Municipalities)?
                    .SingleOrDefault(x => x.Id == municipalityId);
            return municipality;

        }

        public Cities GetCity(int cityId)
        {
            var countries = _countryRepository.GetAll();
            var city =  countries.SelectMany(x => x.Regions)?
                    .SelectMany(x => x.States)?
                    .SelectMany(x => x.Municipalities)?
                    .SelectMany(x => x.Cities)?
                    .SingleOrDefault(x => x.Id == cityId);
            return city;
        }

        private IList<CountryDto> GetCacheData()
        {
            LogginingService.LogInfo("CountriesService Get Countries from Cache");
            var countries = _cache.GetData(AppCache.Countries, AppCache.Countries) as IList<CountryDto>;
            if (countries == null)
            {
                LogginingService.LogInfo("CountriesService set Countries on Cache");
                countries = _countryRepository.GetAll().ToList().ToDto();
                _cache.SetData(AppCache.Countries, AppCache.Countries, countries);
            }
            return countries;
        }
    }
}
