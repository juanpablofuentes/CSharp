using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.PreconditionLiteralValues
{
    public class PreconditionLiteralValuesDto
    {
        public int Id { get; set; }
        public int LiteralPreconditionId { get; set; }
        public string TypeName { get; set; }
        public int TypeId { get; set; }

        public bool? BooleanValue { get; set; }
        public double? DecimalValue { get; set; }
        public string StringValue { get; set; }
        public DateTime? DateValue { get; set; }
        public int? EnterValue { get; set; }
    }
}