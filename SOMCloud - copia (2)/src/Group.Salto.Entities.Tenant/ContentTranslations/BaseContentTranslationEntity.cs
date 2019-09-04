using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class BaseContentTranslationEntity : BaseEntity<Guid>
    {
        public string NameText { get; set; }
        public string DescriptionText { get; set; }
        public int LanguageId { get; set; }
    }
}