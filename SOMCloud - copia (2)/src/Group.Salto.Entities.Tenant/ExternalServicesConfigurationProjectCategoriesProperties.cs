namespace Group.Salto.Entities.Tenant
{
    public class ExternalServicesConfigurationProjectCategoriesProperties
    {
        public int ExternalServicesConfigurationProjectCategoriesId { get; set; }
        public string ColumnName { get; set; }
        public bool? NoApplies { get; set; }
        public string Value { get; set; }

        public ExternalServicesConfigurationProjectCategories ExternalServicesConfigurationProjectCategories { get; set; }
    }
}
