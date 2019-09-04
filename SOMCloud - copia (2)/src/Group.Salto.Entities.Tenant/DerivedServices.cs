using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class DerivedServices : BaseEntity
    {
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public int PredefinedServicesId { get; set; }
        public string InternalIdentifier { get; set; }
        public string ExternalIdentifier { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public int? PeopleResponsibleId { get; set; }
        public int? SubcontractResponsibleId { get; set; }
        public int? ServiceStatesId { get; set; }
        public int? ClosingCodesIdN1 { get; set; }
        public int? ClosingCodesIdN2 { get; set; }
        public int? ClosingCodesIdN3 { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? IcgId { get; set; }

        public ClosingCodes ClosingCodesIdN1Navigation { get; set; }
        public ClosingCodes ClosingCodesIdN2Navigation { get; set; }
        public ClosingCodes ClosingCodesIdN3Navigation { get; set; }
        public People PeopleResponsible { get; set; }
        public PredefinedServices PredefinedServices { get; set; }
        public Projects Project { get; set; }
        public SubContracts SubcontractResponsible { get; set; }
        public Tasks Task { get; set; }
        public ICollection<ExtraFieldsValues> ExtraFieldsValues { get; set; }
    }
}
