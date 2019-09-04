using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Constants;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.ServiceLibrary.Common.Contracts;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Microsoft.AspNetCore.Http;

namespace Group.Salto.ServiceLibrary.Implementations
{
    public class TranslationService : ITranslationService
    {
        private readonly ITranslationRepository _translationsRepository;
        private readonly IHttpContextAccessor _accessor;

        public TranslationService(ITranslationRepository translationsRepository, IHttpContextAccessor accessor)
        {
            _translationsRepository = translationsRepository ?? throw new ArgumentNullException(nameof(ITranslationRepository));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(IHttpContextAccessor));
        }

        public TranslationDto GetTranslation(string key, string culture)
        {
            return _translationsRepository.GetTranslation(key, culture)?.ToDto();
        }

        public IList<TranslationDto> GetLanguageTranslates(string culture)
        {
            return _translationsRepository.GetAll(culture).ToDto().ToList();
        }

        public string GetTranslationText(string key, string culture)
        {
            var data = _translationsRepository.GetTranslation(key, culture)?.Text;
            return string.IsNullOrEmpty(data) ? $"{key} ({culture})" : data;
        }

        public string GetTranslationText(string key)
        {
            var culture = string.Empty;
            var result = $"{key} ({culture})";
            if (_accessor.HttpContext.Request.Cookies.TryGetValue(AppConstants.CookieLanguageConstant, out culture))
            {
                var data = _translationsRepository.GetTranslation(key, culture)?.Text;
                result = string.IsNullOrEmpty(data) ? $"{key} ({culture})" : data;
            }

            return result;
        }
    }
}
