namespace Group.Salto.ServiceLibrary.Common.Dtos.Billing
{
    public class BillDto
    {
        public int Id { get; set; }
        public int DeliveryNotesId { get; set; }
        public string ExternalSystemNumber { get; set; }
        public string Date { get; set; }
        public string Task { get; set; }
        public int DeliveryNotesLines { get; set; }
    }
}