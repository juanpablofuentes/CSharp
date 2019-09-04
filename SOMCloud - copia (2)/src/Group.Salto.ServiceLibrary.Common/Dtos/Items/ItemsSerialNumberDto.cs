namespace Group.Salto.ServiceLibrary.Common.Dtos.Items
{
    public class ItemsSerialNumberDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int? SerialNumberStatusId { get; set; }
        public string Observations { get; set; }
    }
}