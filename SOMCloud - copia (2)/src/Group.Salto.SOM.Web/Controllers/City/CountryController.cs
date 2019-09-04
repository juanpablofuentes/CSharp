using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts;
using Group.Salto.ServiceLibrary.Common.Contracts.Country;
using Group.Salto.ServiceLibrary.Common.Contracts.PostalCode;
using Group.Salto.SOM.Web.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.City
{
    [Authorize]
    public class CountryController : BaseController
    {
        private readonly ICountryService _countryService;
        private readonly IPostalCodeService _postalCodeService;

        public CountryController(ILoggingService loggingService,
                                ICountryService countryService,
                                IHttpContextAccessor accessor,
                                IConfiguration configuration,
                                IPostalCodeService postalCodeService) : base(loggingService, configuration, accessor)
        {
            _countryService = countryService ?? throw new ArgumentNullException(nameof(ICountryService));
            _postalCodeService = postalCodeService ?? throw new ArgumentNullException($"{nameof(IPostalCodeService)} is null");
        }

        [HttpGet]
        public IActionResult GetCity(int id)
        {
            var city = _countryService.GetCity(id);
            return Ok(city);
        }

        [HttpGet]
        public IActionResult GetMunicipality(int id)
        {
            var municipality = _countryService.GetMunicipality(id);
            return Ok(municipality);
        }

        [HttpGet]
        public IActionResult GetCities(int id)
        {
            var cities = _countryService.GetKeyValuesCities(id).ToKeyValuePair();
            return Ok(cities);
        }

        [HttpGet]
        public IActionResult GetMunicipalitiesByState(int id)
        {
            var municipalities = _countryService.GetKeyValuesMunicipalities(id).ToKeyValuePair();
            return Ok(municipalities);
        }

        [HttpGet]
        public IActionResult GetRegionsByCountry(int id)
        {
            var regions = _countryService.GetKeyValuesRegions(id).ToKeyValuePair();
            return Ok(regions);
        }

        [HttpGet]
        public IActionResult GetStatesByRegion(int id)
        {
            var states = _countryService.GetKeyValuesStates(id).ToKeyValuePair();
            return Ok(states);
        }

        [HttpGet]
        public IActionResult GetByPostalCodes(int id)
        {
            var data = _postalCodeService.GetAllKeyValuesByMunicipality(id);
            var result = data.Select(x => new { Key = x.Id, Value = x.Name });
            return Ok(result);
        }
    }
}