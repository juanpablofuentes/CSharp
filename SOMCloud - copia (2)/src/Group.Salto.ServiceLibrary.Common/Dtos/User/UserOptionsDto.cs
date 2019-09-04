namespace Group.Salto.ServiceLibrary.Common.Dtos.User
{
    public class UserOptionsDto
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public int LanguageId { get; set; }
        public int NumberEntriesPerPage { get; set; }
    }
}