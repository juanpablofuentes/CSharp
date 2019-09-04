using Group.Salto.Common;

namespace Group.Salto.Entities
{
    public class ExtraFieldTypes : BaseEntity
    {
        public string Name { get; set; }
        public bool IsMandatoryVisibility { get; set; }
        public bool AllowedValuesVisibility { get; set; }
        public bool MultipleChoiceVisibility { get; set; }
        public bool ErpSystemVisibility { get; set; }
    }
}