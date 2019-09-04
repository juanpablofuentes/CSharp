using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.User
{
    public class AccessUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SelectedRol { get; set; }
        public Guid CustomerId { get; set; }
        public int UserConfigurationId { get; set; }
        public int NumberEntriesPerPage { get; set; }
        public int LanguageId { get; set; }
        public bool IsActive { get; set; }
        public IList<MultiSelectItemDto> Permissions { get; set; }

        public bool AccessUserWithData()
        {
            return ((!string.IsNullOrEmpty(UserName)) || (!string.IsNullOrEmpty(Password)));
        }
    }
}