namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Account
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string UserName { get; set; }
    }
}
