using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PostalCode;
using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Linq;
using Group.Salto.Entities;
using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;

namespace Group.Salto.ServiceLibrary.Implementations.PostalCode
{
    public class PostalCodeService : BaseService, IPostalCodeService
    {
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly ICache _cacheService;

        public PostalCodeService(
            ILoggingService logginingService,
            ICache cacheService,
            IPostalCodeRepository postalCodeRepository) : base(logginingService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException($"{nameof(ICache)} is null");
            _postalCodeRepository = postalCodeRepository ?? throw new ArgumentNullException($"{nameof(IPostalCodeRepository)} is null");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValuesByMunicipality(int municipalityId)
        {
            LogginingService.LogInfo($"Get PostalCode Key Value");
            Dictionary<int, string> data = new Dictionary<int, string>();
            if (municipalityId != 0)
            {
                data = _postalCodeRepository.GetAllKeyValuesByMunicipality(municipalityId);
            }

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public PostalCodes GetById(int id)
        {
            LogginingService.LogInfo($"Get PostalCode by Id {id}");
            string postalCodeKey = $"{AppCache.PostalCodeKey}{id}";

            PostalCodes res = (PostalCodes)_cacheService.GetData(AppCache.PostalCodeId, postalCodeKey);
            if (res is null)
            {
                res = _postalCodeRepository.GetById(id);
                _cacheService.SetData(AppCache.PostalCodeId, postalCodeKey, res);
            }
            return res;
        }

        public PostalCodes GetByCode(string code)
        {
            LogginingService.LogInfo($"Get PostalCode by Code {code}");
            PostalCodes res = _postalCodeRepository.GetByCode(code);
            return res;
        }

        public PostalCodes GetByCity(int city)
        {
            LogginingService.LogInfo($"Get PostalCode by City {city}");
            PostalCodes res = _postalCodeRepository.GetByCity(city);
            return res;
        }

        public bool ValidateCodeAndCity(string code, int city) {
            return _postalCodeRepository.ExistCodeAndCity(code, city);
        }
    }
}