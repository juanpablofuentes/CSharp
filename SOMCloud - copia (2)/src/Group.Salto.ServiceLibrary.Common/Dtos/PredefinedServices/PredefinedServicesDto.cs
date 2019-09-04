using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.PredefinedServices
{
    public class PredefinedServicesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool LinkClosingCode { get; set; }
        public bool Billable { get; set; }
        public bool MustValidate { get; set; }
        public int? ExtraFieldCollectionId { get; set; }
        public string ExtraFieldCollectionName { get; set; }
        public string PermissionsString { get; set; }
        public List<int> PermissionsIds { get; set; }
        public string State { get; set; }
    }
}