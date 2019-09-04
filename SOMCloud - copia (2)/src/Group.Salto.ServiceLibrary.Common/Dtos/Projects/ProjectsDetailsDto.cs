using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using Group.Salto.ServiceLibrary.Common.Dtos.TechnicalCodes;
using Group.Salto.ServiceLibrary.Common.Dtos.PredefinedServices;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Projects
{
    public class ProjectsDetailsDto
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public string Name { get; set; }
        public string Serie { get; set; }
        public int Counter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? VisibilityPda { get; set; }
        public int ProjectManagerId { get; set; }
        public int QueueId { get; set; }
        public bool IsActive { get; set; }
        public int WorkOrderStatusesId { get; set; }
        public int CollectionsExtraFieldId { get; set; }
        public int CollectionsClosureCodesId { get; set; }
        public int CollectionsTypesWorkOrdersId { get; set; }
        public int WorkOrderCategoriesCollectionId { get; set; }
        public string Observations { get; set; }
        public string DefaultTechnicalCode { get; set; }
        public string BackOfficeResponsible { get; set; }
        public string TechnicalResponsible { get; set; }
        public IList<MultiSelectItemDto> Permissions { get; set; }
        public IList<ContactsDto> ContactsSelected { get; set; }
        public IList<TechnicalCodesDto> TechnicalCodesSelected { get; set; }
        public IList<PredefinedServicesDto> PredefinedServicesSelected { get; set; }
    }
}