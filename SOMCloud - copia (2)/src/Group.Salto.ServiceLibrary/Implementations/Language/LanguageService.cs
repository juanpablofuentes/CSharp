using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Language;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Language
{
    public class LanguageService : BaseService, ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ICache _cacheService;

        public LanguageService(ILoggingService logginingService,
                               ILanguageRepository languageRepository,
                               ICache cacheService) 
            : base(logginingService)
        {
            _languageRepository = languageRepository ?? throw new ArgumentNullException($"{nameof(languageRepository)} is null");
            _cacheService = cacheService ?? throw new ArgumentNullException($"{nameof(cacheService)} is null");
        }

        public ResultDto<IList<LanguageDto>> GetAll()
        {
            LogginingService.LogInfo($"Get all Languages availables");

            ResultDto<IList<LanguageDto>> res = (ResultDto<IList<LanguageDto>>)_cacheService.GetData(AppCache.Languages, AppCache.LanguagesKey);

            if (res is null)
            {
                List<Entities.Language> result = _languageRepository.GetAll().ToList();
                res = ProcessResult(result.MapList(c => c.ToDto()));

                _cacheService.SetData(AppCache.Languages, AppCache.LanguagesKey, res);
            }

            return res;
        }

        public ResultDto<LanguageDto> GetByCulture(string culture)
        {
            LogginingService.LogInfo($"Get Language by culture");

            ResultDto<LanguageDto> res = (ResultDto<LanguageDto>)_cacheService.GetData(AppCache.Language, culture);

            if (res is null)
            {
                Entities.Language result = _languageRepository.GetByCulture(culture.ToLower());
                res = ProcessResult(result.ToDto());

                _cacheService.SetData(AppCache.Language, culture, res);
            }

            return res;
        }
    }
}
