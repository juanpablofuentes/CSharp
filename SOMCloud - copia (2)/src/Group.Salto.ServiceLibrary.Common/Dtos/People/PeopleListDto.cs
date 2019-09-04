namespace Group.Salto.ServiceLibrary.Common.Dtos.People
{
    public class PeopleListDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string FisrtSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
        public bool IsClientWorker { get; set; }
        public string UserName { get; set; }
        public int? UserConfigurationId { get; set; }
    }
}