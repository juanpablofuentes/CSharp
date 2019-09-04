using Group.Salto.Common;

namespace Group.Salto.Entities
{
    public class Translation : BaseEntity
    {
        public string Key { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
