namespace Group.Salto.ServiceLibrary.Common.Dtos
{
    public class MultiSelectItemDto
    {
        public MultiSelectItemDto() { }

        public string LabelName { get; set; }
        public string Value { get; set; }
        public bool IsChecked { get; set; }
    }
}