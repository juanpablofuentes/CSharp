using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Municipality;
using System;

namespace Group.Salto.ServiceLibrary.Implementations.Municipality
{
    public class MunicipalityService : BaseService, IMunicipalityService
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly ICache _cacheService;

        public MunicipalityService(ILoggingService logginingService,
                                   ICache cacheService,
                                   IMunicipalityRepository municipalityRepository) : base(logginingService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException($"{nameof(ICache)} is null");
            _municipalityRepository = municipalityRepository ?? throw new ArgumentNullException($"{nameof(IMunicipalityRepository)} is null ");
        }
        
        public Municipalities GetByIdWithStatesRegionsCountriesIncludes(int id)
        {
            LogginingService.LogInfo($"Get Municipalities by Id {id}");
            string municipalityKey = $"{AppCache.MunicipalityKey}{id}";

            Municipalities res = (Municipalities)_cacheService.GetData(AppCache.MunicipalityId, municipalityKey);
            if (res is null)
            {
                res = _municipalityRepository.GetByIdWithStatesRegionsCountriesIncludes(id);
                _cacheService.SetData(AppCache.MunicipalityId, municipalityKey, res);
            }
            return res;
        }
    }
}