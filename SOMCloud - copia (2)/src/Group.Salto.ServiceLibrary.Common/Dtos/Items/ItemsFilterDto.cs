namespace Group.Salto.ServiceLibrary.Common.Dtos.Items
{
    public class ItemsFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string ERPReference { get; set; }
        public string InternalReference { get; set; }
        public bool Blocked { get; set; }
    }
}