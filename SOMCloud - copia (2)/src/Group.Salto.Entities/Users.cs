using Group.Salto.Common.Entities.Contracts;
using Microsoft.AspNetCore.Identity;
using System;

namespace Group.Salto.Entities
{
    public class Users : IdentityUser, ISoftDelete
    {
        public string Name { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Observations { get; set; }
        public string ConfigurationWoFields { get; set; }
        public int NumberEntriesPerPage { get; set; }
        public int OldUserId { get; set; }
        public Guid CustomerId { get; set; }
        public Customers Customer { get; set; }
        public int? UserConfigurationId { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}