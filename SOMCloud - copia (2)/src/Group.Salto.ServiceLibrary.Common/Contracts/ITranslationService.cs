using System.Collections.Generic;
using Group.Salto.Controls.Interfaces;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Common.Contracts
{
    public interface ITranslationService : ITranslationTable
    {
        TranslationDto GetTranslation(string key, string culture);
        IList<TranslationDto> GetLanguageTranslates(string culture);
    }
}
