namespace Group.Salto.ServiceLibrary.Common.Dtos.Identity
{
    public class IdentityResetPasswordDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}