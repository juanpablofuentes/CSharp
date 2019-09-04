using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Language;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Language
{
    public interface ILanguageService
    {
        ResultDto<IList<LanguageDto>> GetAll();
        ResultDto<LanguageDto> GetByCulture(string culture);
    }
}
