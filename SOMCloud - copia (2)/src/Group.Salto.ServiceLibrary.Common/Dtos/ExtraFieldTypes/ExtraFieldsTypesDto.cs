namespace Group.Salto.ServiceLibrary.Common.Dtos.ExtraFieldTypes
{
    public class ExtraFieldsTypesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMandatoryVisibility { get; set; }
        public bool AllowedValuesVisibility { get; set; }
        public bool MultipleChoiceVisibility { get; set; }
        public bool ErpSystemVisibility { get; set; }
    }
}