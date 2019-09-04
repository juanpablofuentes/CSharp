using System;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.User
{
    public class UserApiDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Observations { get; set; }
        public string ConfigurationWoFields { get; set; }
        public string Email { get; set; }
        public int LanguageId { get; set; }
        public int NumberEntriesPerPage { get; set; }
        public int OldUserId { get; set; }
        public int? UserConfigurationId { get; set; }
        public string UserName { get; set; }
        public Guid TennantId { get; set; }
        public Common.Dtos.Rols.RolDto Rol { get; set; }
    }
}
