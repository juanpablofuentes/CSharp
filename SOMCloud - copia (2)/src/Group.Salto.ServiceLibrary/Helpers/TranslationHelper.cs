using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Common.Contracts;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Helpers
{
    public static class TranslationHelper
    {
        private static ITranslationService _translationService;

        private static Dictionary<string, IList<TranslationDto>> _tranlationsDictionary;

        public static void SetTranslationInstance(ITranslationService translationsService)
        {
            _translationService = translationsService ?? throw new ArgumentNullException(nameof(ITranslationService));
        }

        public static ITranslationService GetTranslationService()
        {
            return _translationService;
        }

        //TODO Add Logger
        public static string GetText(string key, string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentNullException($"Culture is null or empty");
            }
            var result = GetCacheText(key, culture);
            return !string.IsNullOrEmpty(result) ? result : $"{key} ({culture})";
        }

        private static string GetCacheText(string key, string culture)
        {
            string text = null;
            CheckTranslations(key, culture);
            IList<TranslationDto> translationsKey;
            if (_tranlationsDictionary != null && _tranlationsDictionary.TryGetValue(culture, out translationsKey))
            {
                text = translationsKey.SingleOrDefault(x => x.Key == key)?.Text;
            }
            return text;

        }

        private static void CheckTranslations(string key, string culture)
        {
            IList<TranslationDto> translationsKey = null;
            if (_tranlationsDictionary == null || !_tranlationsDictionary.TryGetValue(culture, out translationsKey))
            {
                GetTranslationsFor(culture);
            }
            if (translationsKey != null && !translationsKey.Any(x => x.Key == key))
            {
                UpdateTranslationsFor(key, culture);
            }
        }

        private static void UpdateTranslationsFor(string key, string culture)
        {
            var translation = _translationService.GetTranslation(key, culture);
            IList<TranslationDto> translations;
            if (translation != null  && _tranlationsDictionary.TryGetValue(culture, out translations))
            {
                _tranlationsDictionary.Remove(culture);
                translations.Add(translation);
                _tranlationsDictionary.Add(culture, translations);
            }
        }

        private static void GetTranslationsFor(string culture)
        {
            if (_tranlationsDictionary == null)
            {
                _tranlationsDictionary = new Dictionary<string, IList<TranslationDto>>();
            }
            var translations = _translationService.GetLanguageTranslates(culture);
            if (translations != null && translations.Any())
            {
                _tranlationsDictionary.Add(culture, translations);
            }
        }
    }
}
