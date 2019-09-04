using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Translation
{
    public class BaseContentTranslationDto
    {
        public Guid Id { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
    }
}