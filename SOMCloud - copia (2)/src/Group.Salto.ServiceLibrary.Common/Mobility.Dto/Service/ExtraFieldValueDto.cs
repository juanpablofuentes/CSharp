namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service
{
    public class ExtraFieldValueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsMandatory { get; set; }
        public ExtraFieldValueTypeEnum Type { get; set; }
        public string Observations { get; set; }
        public string AllowedStringValues { get; set; }
        public bool? MultipleChoice { get; set; }
        public int? ErpSystemInstanceQueryId { get; set; }
        public bool DelSystem { get; set; }
    }
}
