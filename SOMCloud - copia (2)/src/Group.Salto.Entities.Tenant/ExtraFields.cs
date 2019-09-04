using Group.Salto.Common.Entities;
using Group.Salto.Entities.Tenant.AttributedEntities;
using Group.Salto.Entities.Tenant.ContentTranslations;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class ExtraFields : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsMandatory { get; set; }
        public int Type { get; set; }
        public string Observations { get; set; }
        public string AllowedStringValues { get; set; }
        public bool? MultipleChoice { get; set; }
        public int? ErpSystemInstanceQueryId { get; set; }
        public bool DelSystem { get; set; }

        public ErpSystemInstanceQuery ErpSystemInstanceQuery { get; set; }
        public ICollection<CollectionsExtraFieldExtraField> CollectionsExtraFieldExtraField { get; set; }
        public ICollection<ExtraFieldsValues> ExtraFieldsValues { get; set; }
        public ICollection<LiteralsPreconditions> LiteralsPreconditions { get; set; }
        public ICollection<Postconditions> Postconditions { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<ExtraFieldsTranslations> ExtraFieldsTranslations { get; set; }
        public ICollection<ItemsSerialNumberAttributeValues> ItemsSerialNumberAttributeValues { get; set; }
    }
}
