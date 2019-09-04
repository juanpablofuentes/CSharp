using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class Customers : BaseEntity<Guid>, ITrackable
    {
        public string Name { get; set; }
        public string NIF { get; set; }
        public string ConnString { get; set; }
        public string DatabaseName { get; set; }
        public DateTime DateCreated { get; set; }
        [Trackable]
        public int NumberWebUsers { get; set; }
        [Trackable]
        public int NumberAppUsers { get; set; }
        [Trackable]
        public int NumberTeamMembers { get; set; }
        public int NumberEmployees { get; set; }
        public string InvoicingContactName { get; set; }
        public string InvoicingContactFirstSurname { get; set; }
        public string InvoicingContactSecondSurname { get; set; }
        public string InvoicingContactEmail { get; set; }
        public string TechnicalAdministratorName { get; set; }
        public string TechnicalAdministratorFirstSurname { get; set; }
        public string TechnicalAdministratorSecondSurname { get; set; }
        public string TechnicalAdministratorEmail { get; set; }
        public string Telephone { get; set; }
        public string CustomerCode { get; set; }
        public string Address { get; set; }
        public Municipalities Municipality { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdateStatusDate { get; set; }
        public ICollection<Users> Users { get; set; }
        //[Trackable]
        public ICollection<CustomerModule> ModulesAssigned { get; set; }
    }
}