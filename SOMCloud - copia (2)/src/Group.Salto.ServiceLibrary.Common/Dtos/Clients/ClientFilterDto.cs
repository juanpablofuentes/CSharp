using Group.Salto.Controls.Entities;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Clients
{
    public class ClientFilterDto : ISortableFilter
    {
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
        public string CorporateName { get; set; }
    }
}