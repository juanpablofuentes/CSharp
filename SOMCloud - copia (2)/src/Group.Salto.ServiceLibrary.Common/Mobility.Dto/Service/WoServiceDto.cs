using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.People;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service
{
    public class WoServiceDto
    {
        public int Id { get; set; }
        public int PredefinedServiceId { get; set; }
        public string IdentifyInternal { get; set; }
        public string IdentifyExternal { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public int? PeopleResponsibleId { get; set; }
        public int? SubcontractResponsibleId { get; set; }
        public int? ServiceStateId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime CreationDate { get; set; }
        public string FormState { get; set; }
        public string DeliveryNote { get; set; }
        public DateTime? DeliveryProcessInit { get; set; }
        public bool? Cancelled { get; set; }
        public int? ServicesCancelFormId { get; set; }
        public int? ClosingCodeId { get; set; }
        public IEnumerable<ServiceExtraFieldValueDto> ExtraFieldsValues { get; set; }
        public PredefinedServiceDto PredefinedService { get; set; }
        public IEnumerable<string> ClosingCodes { get; set; }
        public PeopleDto PeopleResponsible { get; set; }
    }
}
