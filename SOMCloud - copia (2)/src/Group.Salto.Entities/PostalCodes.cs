using Group.Salto.Common;

namespace Group.Salto.Entities
{
    public class PostalCodes : BaseEntity
    {
        public string PostalCode { get; set; }
        public int CityId { get; set; }

        public Cities IdNavigation { get; set; }
    }
}
