namespace Group.Salto.Common.Entities.Settings
{
    public class OAuthConfiguration
    {
        public string PrivateKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireTimeMinutes { get; set; }
    }
}