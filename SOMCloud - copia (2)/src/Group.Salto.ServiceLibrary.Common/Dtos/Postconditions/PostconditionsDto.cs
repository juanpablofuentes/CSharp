using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Postconditions
{
    public class PostconditionsDto
    {
        public int Id { get; set; }
        public int PostconditionCollectionsId { get; set; }
        public string NameFieldModel { get; set; }
        public string TypeName { get; set; }
        public int TypeId { get; set; }

        public Guid PostconditionTypeId { get; set; }
        public string PostconditionTypeName { get; set; }

        public bool? BooleanValue { get; set; }
        public double? DecimalValue { get; set; }
        public string StringValue { get; set; }
        public DateTime? DateValue { get; set; }
        public int? EnterValue { get; set; }

    }
}