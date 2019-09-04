namespace Group.Salto.ServiceLibrary.Common.Dtos.ExtraFields
{
    public class ExtraFieldsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsMandatory { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Observations { get; set; }
        public string AllowedStringValues { get; set; }
        public bool? MultipleChoice { get; set; }
        public int? ErpSystemInstanceQueryId { get; set; }
        public string ErpSystemInstanceQueryName { get; set; }
        public bool? DelSystem { get; set; }
        public string State { get; set; }
    }
}