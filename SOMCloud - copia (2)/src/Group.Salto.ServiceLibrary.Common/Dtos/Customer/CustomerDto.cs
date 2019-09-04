using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Customer
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ConnString { get; set; }
        public string DatabaseName { get; set; }
        public int NumberWebUsers { get; set; }
        public int NumberAppUsers { get; set; }
        public int NumberTeamMembers { get; set; }        
        public string InvoicingContactName { get; set; }
        public string InvoicingContactFirstSurname { get; set; }
        public string InvoicingContactSecondSurname { get; set; }
        public string InvoicingContactEmail { get; set; }
        public string TechnicalAdministratorName { get; set; }
        public string TechnicalAdministratorFirstSurname { get; set; }
        public string TechnicalAdministratorSecondSurname { get; set; }
        public string TechnicalAdministratorEmail { get; set; }
        public int NumberEmployees { get; set; }
        public string Telephone { get; set; }
        public string CustomerCode { get; set; }
        public string Address { get; set; }
        public MunicipalityIdsDto MunicipalitySelected { get; set; }
        public string NIF { get; set; }        
        public bool IsActive { get; set; }
        public DateTime UpdateStatusDate { get; set; }
        public DateTime DateCreated { get; set; }
        public IList<Guid> ModulesAssociated { get; set; }
    }
}
