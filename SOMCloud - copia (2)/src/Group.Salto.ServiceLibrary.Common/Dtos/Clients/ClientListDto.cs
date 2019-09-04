namespace Group.Salto.ServiceLibrary.Common.Dtos.Clients
{
    public class ClientListDto
    {
        public int Id { get; set; }
        public string CorporateName { get; set; }
        public bool UnListed { get; set; }
        public string Phone { get; set; }
        public string Observations { get; set; }
    }
}