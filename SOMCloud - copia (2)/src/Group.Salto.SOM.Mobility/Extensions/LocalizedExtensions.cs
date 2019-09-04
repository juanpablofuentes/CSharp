using System.Threading;
using Group.Salto.ServiceLibrary.Helpers;

namespace Group.Salto.SOM.Mobility.Extensions
{
    public class LocalizedExtensions
    {
        public static string GetUiLocalizedText(string key)
        {
            return TranslationHelper.GetText(key, Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
        }
    }
}
