using Group.Salto.ServiceLibrary.Common.Dtos.AssetsAudit;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Assets
{
    public class AssetDetailsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SerialNumber { get; set; }
        public string StockNumber { get; set; }
        public string AssetNumber { get; set; }
        public string Observations { get; set; }
        public GuaranteeDto Warranty { get; set; }
        public List<AssetsAuditDto> Audits { get; set; }
        public List<HiredServicesDto> HiredServices { get; set; }
        public int SelectedStatus { get; set; }
        public int? SelectedContract { get; set; }
        public KeyValuePair<int?,string> SelectedSubFamily { get; set; }
        public KeyValuePair<int?,string> SelectedFamily { get; set; }
        public KeyValuePair<int?,string> SelectedModel { get; set; }
        public KeyValuePair<int?,string> SelectedBrand { get; set; }
        public KeyValuePair<int?, string> SelectedSiteUser { get; set; }
        public KeyValuePair<int,string> SelectedSiteLocation { get; set; }
        public KeyValuePair<int?,string> SiteClient { get; set; }
        public KeyValuePair<int?,string> SelectedUsage { get; set; }
    }
}