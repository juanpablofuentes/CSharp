namespace Group.Salto.ServiceLibrary.Common.Dtos.Identity
{
    public class IdentityForgotPasswordDto
    {
        public string Subject { get; set; }
        public string EmailTo { get; set; }
        public string CallbackUrl { get; set; }
    }
}