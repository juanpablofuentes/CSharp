namespace Group.Salto.Common.Entities.Settings
{
    public class TenantConfiguration
    {
        public string ConnectionStringFormat { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Hosting { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        public string TenantPrefix { get; set; }
    }
}