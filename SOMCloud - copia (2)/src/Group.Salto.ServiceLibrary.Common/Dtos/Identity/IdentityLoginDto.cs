namespace Group.Salto.ServiceLibrary.Common.Dtos.Identity
{
    public class IdentityLoginDto
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public int? UserConfigurationId { get; set; }
    }
}