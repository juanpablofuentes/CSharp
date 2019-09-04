using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant.AttributedEntities
{
    public class BaseAttributeValues
    {    
        public int ExtraFieldsId { get; set; }
        public int? IntValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public DateTime? DateTimeValue { get; set; }
        public string StringValue { get; set; }
        public ExtraFields ExtraFields { get; set; }
    }
}