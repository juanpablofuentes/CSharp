using Group.Salto.Common.Enums;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Identity
{
    public class IdentityResultDto<TDto>
    {
        public IdentityResultEnum Result { get; set; }
        public TDto Data { get; set; }
        public string LanguageCode { get; set; }
    }
}