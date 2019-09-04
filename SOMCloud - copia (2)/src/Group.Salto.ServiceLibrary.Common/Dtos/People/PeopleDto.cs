using Group.Salto.ServiceLibrary.Common.Dtos.Company;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.People
{
    public class PeopleDto
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PhoneEX { get; set; }
        public int LanguageId { get; set; }
        public int? UserConfigurationId { get; set; }
        public bool IsVisible { get; set; }
        public int? CompanyId { get; set; }
        public int? ResponsibleId { get; set; }
        public int? DepartmentId { get; set; }
        public int? PointRateId { get; set; }
        public int? SubcontractId { get; set; }
        public bool SubcontractorResponsible { get; set; }
        public int? ProjectId { get; set; }
        public double? CostKm { get; set; }
        public string DocumentationUrl { get; set; }
        public int? PriorityId { get; set; }
        public bool IsActive { get; set; }
        public int? AnnualHours { get; set; }
        public int? WorkCenterId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? WorkRadiusKm { get; set; }
        public IList<PeopleKnowledgeDto> KnowledgeSelected { get; set; }
        public CompanyDto Company { get; set; }
    }
}