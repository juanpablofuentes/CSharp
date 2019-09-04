using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class FinalClients : BaseEntity
    {
        public string IdExtern { get; set; }
        public int OriginId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Nif { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Fax { get; set; }
        public string Status { get; set; }
        public string Observations { get; set; }
        public int? PeopleCommercialId { get; set; }
        public int? IcgId { get; set; }
        public string Code { get; set; }

        public People PeopleCommercial { get; set; }
        public ICollection<ContactsFinalClients> ContactsFinalClients { get; set; }
        public ICollection<ExternalServicesConfiguration> ExternalServicesConfiguration { get; set; }
        public ICollection<ExternalServicesConfigurationSites> ExternalServicesConfigurationSites { get; set; }
        public ICollection<FinalClientSiteCalendar> FinalClientSiteCalendar { get; set; }
        public ICollection<LocationsFinalClients> LocationsFinalClients { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
        public ICollection<WorkOrders> WorkOrders { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
    }
}
