namespace Group.Salto.Entities.Tenant
{
    public class ExternalServicesConfigurationSites
    {
        public int ExternalServicesConfigurationId { get; set; }
        public int FinalClientId { get; set; }
        public string ExtClientId { get; set; }
        public int? ExtSiteInitialChar { get; set; }
        public int? ExtSiteLength { get; set; }

        public ExternalServicesConfiguration ExternalServicesConfiguration { get; set; }
        public FinalClients FinalClient { get; set; }
    }
}
