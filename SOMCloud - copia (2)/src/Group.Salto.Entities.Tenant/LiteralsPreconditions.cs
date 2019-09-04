using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class LiteralsPreconditions : BaseEntity
    {
        public int PreconditionId { get; set; }
        public string NomCampModel { get; set; }
        public string ComparisonOperator { get; set; }
        public int? ExtraFieldId { get; set; }
        public Guid PreconditionsTypeId { get; set; }

        public ExtraFields ExtraField { get; set; }
        public Preconditions Precondition { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
    }
}