using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class ExtraFieldsValues : BaseEntity
    {
        public int? ServiceId { get; set; }
        public int? WorkOrderDeritativeId { get; set; }
        public int? DerivedServiceId { get; set; }
        public int? WorkOrderId { get; set; }
        public int ExtraFieldId { get; set; }
        public int? EnterValue { get; set; }
        public DateTime? DataValue { get; set; }
        public double? DecimalValue { get; set; }
        public bool? BooleaValue { get; set; }
        public string StringValue { get; set; }
        public byte[] Signature { get; set; }

        public DerivedServices DerivedService { get; set; }
        public ExtraFields ExtraField { get; set; }
        public Services Service { get; set; }
        public WorkOrdersDeritative WorkOrderDeritative { get; set; }
        public ICollection<MaterialForm> MaterialForm { get; set; }
    }
}
