using DataAccess.Common.Context;
using Group.Salto.Common;
using Group.Salto.Common.Entities;
using Group.Salto.Common.Entities.Contracts;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Entities.Tenant.AttributedEntities;
using Group.Salto.Entities.Tenant.ContentTranslations;
using Group.Salto.Entities.Tenant.QueryEntities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Context
{
    public class SOMTenantContext : DbContext, IOwnerIdentifier
    {

        public string OwnerId { get; set; }

        public SOMTenantContext(DbContextOptions options) : base(options) { }

        public DbSet<AdvancedTechnicianListFilterPersons> AdvancedTechnicianListFilterPersons { get; set; }
        public DbSet<AdvancedTechnicianListFilters> AdvancedTechnicianListFilters { get; set; }
        public DbSet<AssetsAudit> AssetsAudit { get; set; }
        public DbSet<AssetsAuditChanges> AssetsAuditChanges { get; set; }
        public DbSet<AssetStatuses> AssetStatuses { get; set; }
        public DbSet<Audits> Audits { get; set; }
        public DbSet<BasicTechnicianListFilters> BasicTechnicianListFilters { get; set; }
        public DbSet<BasicTechnicianListFilterSkills> BasicTechnicianListFilterSkills { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillingItems> BillingItems { get; set; }
        public DbSet<BillingLineItems> BillingLineItems { get; set; }
        public DbSet<BillingRule> BillingRule { get; set; }
        public DbSet<BillingRuleItem> BillingRuleItem { get; set; }
        public DbSet<BillLine> BillLine { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<CalendarEvents> CalendarEvents { get; set; }
        public DbSet<CalendarPlanningViewConfiguration> CalendarPlanningViewConfiguration { get; set; }
        public DbSet<CalendarPlanningViewConfigurationPeople> CalendarPlanningViewConfigurationPeople { get; set; }
        public DbSet<CalendarPlanningViewConfigurationPeopleCollection> CalendarPlanningViewConfigurationPeopleCollection { get; set; }
        public DbSet<Calendars> Calendars { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<ClosingCodes> ClosingCodes { get; set; }
        public DbSet<CollectionsClosureCodes> CollectionsClosureCodes { get; set; }
        public DbSet<CollectionsExtraField> CollectionsExtraField { get; set; }
        public DbSet<CollectionsExtraFieldExtraField> CollectionsExtraFieldExtraField { get; set; }
        public DbSet<CollectionsTypesWorkOrders> CollectionsTypesWorkOrders { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<CompaniesCostHistorical> CompaniesCostHistorical { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<ContactsFinalClients> ContactsFinalClients { get; set; }
        public DbSet<ContactsLocationsFinalClients> ContactsLocationsFinalClients { get; set; }
        public DbSet<ContractContacts> ContractContacts { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<DerivedServices> DerivedServices { get; set; }
        public DbSet<ErpItemsSyncConfig> ErpItemsSyncConfig { get; set; }
        public DbSet<ErpSystemInstance> ErpSystemInstance { get; set; }
        public DbSet<ErpSystemInstanceQuery> ErpSystemInstanceQuery { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<ExpensesTicketFile> ExpensesTicketFile { get; set; }
        public DbSet<ExpensesTickets> ExpensesTickets { get; set; }        
        public DbSet<ExpenseTypes> ExpenseTypes { get; set; }
        public DbSet<ExternalServicesConfiguration> ExternalServicesConfiguration { get; set; }
        public DbSet<ExternalServicesConfigurationProjectCategories> ExternalServicesConfigurationProjectCategories { get; set; }
        public DbSet<ExternalServicesConfigurationProjectCategoriesProperties> ExternalServicesConfigurationProjectCategoriesProperties { get; set; }
        public DbSet<ExternalServicesConfigurationSites> ExternalServicesConfigurationSites { get; set; }
        public DbSet<ExternalSystemImportData> ExternalSystemImportData { get; set; }
        public DbSet<ExternalWorOrderStatuses> ExternalWorOrderStatuses { get; set; }
        public DbSet<ExtraFields> ExtraFields { get; set; }
        public DbSet<ExtraFieldsValues> ExtraFieldsValues { get; set; }
        public DbSet<Families> Families { get; set; }
        public DbSet<FinalClients> FinalClients { get; set; }
        public DbSet<FinalClientSiteCalendar> FinalClientSiteCalendar { get; set; }
        public DbSet<Flows> Flows { get; set; }
        public DbSet<FormConfigs> FormConfigs { get; set; }
        public DbSet<FormElements> FormElements { get; set; }
        public DbSet<Guarantee> Guarantee { get; set; }
        public DbSet<HiredServices> HiredServices { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<ItemsPointsRate> ItemsPointsRate { get; set; }
        public DbSet<ItemsPurchaseRate> ItemsPurchaseRate { get; set; }
        public DbSet<ItemsSalesRate> ItemsSalesRate { get; set; }
        public DbSet<ItemsSerialNumber> ItemsSerialNumber { get; set; }
        public DbSet<ItemsSerialNumberStatuses> ItemsSerialNumberStatuses { get; set; }
        public DbSet<Journeys> Journeys { get; set; }
        public DbSet<JourneysStates> JourneysStates { get; set; }
        public DbSet<Knowledge> Knowledge { get; set; }
        public DbSet<KnowledgePeople> KnowledgePeople { get; set; }
        public DbSet<KnowledgeSubContracts> KnowledgeSubContracts { get; set; }
        public DbSet<KnowledgeToolsType> KnowledgeToolsType { get; set; }
        public DbSet<KnowledgeWorkOrderTypes> KnowledgeWorkOrderTypes { get; set; }
        public DbSet<LiteralsPreconditions> LiteralsPreconditions { get; set; }
        public DbSet<LocationCalendar> LocationCalendar { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<LocationsFinalClients> LocationsFinalClients { get; set; }
        public DbSet<MailTemplate> MailTemplate { get; set; }
        public DbSet<MainOtstatics> MainOtstatics { get; set; }
        public DbSet<MainWoregistry> MainWoregistry { get; set; }
        public DbSet<MainWoViewConfigurationsColumns> MainWoViewConfigurationsColumns { get; set; }
        public DbSet<MainWoviewConfigurationsGroups> MainWoviewConfigurationsGroups { get; set; }
        public DbSet<MainWoViewConfigurationsPeople> MainWoViewConfigurationsPeople { get; set; }
        public DbSet<MaterialForm> MaterialForm { get; set; }
        public DbSet<Models> Models { get; set; }
        public DbSet<OptimizationFunctionWeights> OptimizationFunctionWeights { get; set; }
        public DbSet<PaymentMethods> PaymentMethods { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<PeopleCalendars> PeopleCalendars { get; set; }
        public DbSet<PeopleCollectionCalendars> PeopleCollectionCalendars { get; set; }
        public DbSet<PeopleCollections> PeopleCollections { get; set; }
        public DbSet<PeopleCollectionsAdmins> PeopleCollectionsAdmins { get; set; }
        public DbSet<PeopleCollectionsPeople> PeopleCollectionsPeople { get; set; }
        public DbSet<PeopleCollectionsPermissions> PeopleCollectionsPermissions { get; set; }
        public DbSet<PeopleCost> PeopleCost { get; set; }
        public DbSet<PeopleCostHistorical> PeopleCostHistorical { get; set; }
        public DbSet<PeopleRegisteredPda> PeopleRegisteredPda { get; set; }
        public DbSet<PeoplePermissions> PeoplePermissions { get; set; }
        public DbSet<PlanificationCriterias> PlanificationCriterias { get; set; }
        public DbSet<PlanificationProcessCalendarChangeTracker> PlanificationProcessCalendarChangeTracker { get; set; }
        public DbSet<PlanificationProcesses> PlanificationProcesses { get; set; }
        public DbSet<PlanificationProcessWorkOrderChangeTracker> PlanificationProcessWorkOrderChangeTracker { get; set; }
        public DbSet<PlanningPanelViewConfiguration> PlanningPanelViewConfiguration { get; set; }
        public DbSet<PlanningPanelViewConfigurationPeople> PlanningPanelViewConfigurationPeople { get; set; }
        public DbSet<PlanningPanelViewConfigurationPeopleCollection> PlanningPanelViewConfigurationPeopleCollection { get; set; }
        public DbSet<PointsRate> PointsRate { get; set; }
        public DbSet<PostconditionCollections> PostconditionCollections { get; set; }
        public DbSet<Postconditions> Postconditions { get; set; }
        public DbSet<Preconditions> Preconditions { get; set; }
        public DbSet<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
        public DbSet<PredefinedServices> PredefinedServices { get; set; }
        public DbSet<PredefinedServicesPermission> PredefinedServicesPermission { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<ProjectsCalendars> ProjectsCalendars { get; set; }
        public DbSet<ProjectsContacts> ProjectsContacts { get; set; }
        public DbSet<ProjectsPermissions> ProjectsPermissions { get; set; }
        public DbSet<PurchaseRate> PurchaseRate { get; set; }
        public DbSet<PushNotifications> PushNotifications { get; set; }
        public DbSet<PushNotificationsPeopleCollections> PushNotificationsPeopleCollections { get; set; }
        public DbSet<Queues> Queues { get; set; }
        public DbSet<PermissionsQueues> PermissionsQueues { get; set; }
        public DbSet<PermissionsTasks> PermissionsTasks { get; set; }
        public DbSet<SalesRate> SalesRate { get; set; }
        public DbSet<SaltoCsversion> SaltoCsversion { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<ServicesAnalysis> ServicesAnalysis { get; set; }
        public DbSet<ServicesViewConfigurations> ServicesViewConfigurations { get; set; }
        public DbSet<ServicesViewConfigurationsColumns> ServicesViewConfigurationsColumns { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
        public DbSet<SgsClosingInfo> SgsClosingInfo { get; set; }
        public DbSet<SiteUser> SiteUser { get; set; }
        public DbSet<Sla> Sla { get; set; }
        public DbSet<SomFiles> SomFiles { get; set; }
        public DbSet<StatesSla> StatesSla { get; set; }
        public DbSet<StopSlaReason> StopSlaReason { get; set; }
        public DbSet<SubContracts> SubContracts { get; set; }
        public DbSet<SubFamilies> SubFamilies { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<SymptomCollections> SymptomCollections { get; set; }
        public DbSet<SymptomCollectionSymptoms> SymptomCollectionSymptoms { get; set; }
        public DbSet<SynchronizationSessions> SynchronizationSessions { get; set; }
        public DbSet<SystemNotifications> SystemNotifications { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskTokens> TaskTokens { get; set; }
        public DbSet<TaskWebServiceCallItems> TaskWebServiceCallItems { get; set; }
        public DbSet<Assets> Assets { get; set; }
        public DbSet<AssetsContracts> AssetsContracts { get; set; }
        public DbSet<AssetsHiredServices> AssetsHiredServices { get; set; }
        public DbSet<AssetsWorkOrders> AssetsWorkOrders { get; set; }
        public DbSet<TechnicalCodes> TechnicalCodes { get; set; }
        public DbSet<TechnicianListFilters> TechnicianListFilters { get; set; }
        public DbSet<TenantConfiguration> TenantConfiguration { get; set; }
        public DbSet<Tools> Tools { get; set; }
        public DbSet<ToolsToolTypes> ToolsToolTypes { get; set; }
        public DbSet<ToolsType> ToolsType { get; set; }
        public DbSet<ToolsTypeWorkOrderTypes> ToolsTypeWorkOrderTypes { get; set; }
        public DbSet<Usages> Usages { get; set; }
        public DbSet<UsersMainWoviewConfigurations> UsersMainWoviewConfigurations { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<WorkOrderAnalysis> WorkOrderAnalysis { get; set; }
        public DbSet<WorkOrderCategories> WorkOrderCategories { get; set; }
        public DbSet<WorkOrderCategoriesCollectionCalendar> WorkOrderCategoriesCollectionCalendar { get; set; }
        public DbSet<WorkOrderCategoriesCollections> WorkOrderCategoriesCollections { get; set; }
        public DbSet<WorkOrderCategoryCalendar> WorkOrderCategoryCalendar { get; set; }
        public DbSet<WorkOrderCategoryKnowledge> WorkOrderCategoryKnowledge { get; set; }
        public DbSet<WorkOrderCategoryPermissions> WorkOrderCategoryPermissions { get; set; }
        public DbSet<WorkOrderCategoryRoles> WorkOrderCategoryRoles { get; set; }
        public DbSet<WorkOrders> WorkOrders { get; set; }
        public DbSet<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
        public DbSet<WorkOrderStatuses> WorkOrderStatuses { get; set; }
        public DbSet<WorkOrderTypes> WorkOrderTypes { get; set; }
        public DbSet<WsErpSystemInstance> WsErpSystemInstance { get; set; }
        public DbSet<ZoneProject> ZoneProject { get; set; }
        public DbSet<ZoneProjectPostalCode> ZoneProjectPostalCode { get; set; }
        public DbSet<Zones> Zones { get; set; }
        public DbSet<PeopleProjects> PeopleProjects { get; set; }
        public DbSet<UserConfiguration> UserConfigurations { get; set; }
        public DbSet<WorkCenters> WorkCenters { get; set; }
        public DbSet<WorkOrderStatusesTranslations> WorkOrderStatusesTranslations { get; set; }
        public DbSet<ExternalWorkOrderStatusesTranslations> ExternalWorkOrderStatusesTranslations { get; set; }
        public DbSet<QueuesTranslations> QueuesTranslations { get; set; }
        public DbSet<ExtraFieldsTranslations> ExtraFieldsTranslations { get; set; }
        public DbSet<RepetitionParameters> RepetitionParameters { get; set; }
        public DbSet<RolesTenant> RolesTenants { get; set; }
        public DbSet<UserConfigurationRolesTenant> UserConfigurationRolesTenants { get; set; }
        public DbSet<RolesActionGroupsActionsTenant> RolesActionGroupsActionsTenants { get; set; }
        public virtual DbSet<DnAndMaterialsAnalysis> DnAndMaterialsAnalysis { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<ExpensesTicketsFiles> ExpensesTicketsFiles { get; set; }
        public virtual DbSet<PushNotificationsPeople> PushNotificationsPeople { get; set; }
        public virtual DbSet<BasicTechnicianListFilterCalendarPlanningViewConfiguration> BasicTechnicianListFilterCalendarPlanningViewConfiguration { get; set; }
        public DbSet<FlowsProjects> FlowsProjects { get; set; }
        public DbSet<FlowsWOCategories> FlowsWOCategories { get; set; }
        public DbSet<ItemsSerialNumberAttributeValues> ItemsSerialNumberAttributeValues { get; set; }
        public DbSet<Warehouses> Warehouses { get; set; }
        public DbSet<WarehouseMovementEndpoints> WarehouseMovementEndpoints { get; set; }
        public DbSet<WarehouseMovements> WarehouseMovements { get; set; }
        public DbSet<PeopleNotification> PeopleNotifications { get; set; }
        public DbSet<PeoplePushRegistration> PeoplePushRegistrations { get; set; }
        public DbSet<NotificationTemplateType> NotificationTemplateTypes { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<NotificationTemplateTranslation> NotificationTemplateTranslation { get; set; }
        public DbSet<PeopleNotificationTranslation> PeopleNotificationTranslation { get; set; }
        public DbQuery<FieldMaterialForm> FieldMaterialForm { get; set; }

        public override int SaveChanges()
        {
            var entitiesTrackables = ChangeTracker.Entries().Where(e => e.Entity is ITrackable && e.State == EntityState.Modified).ToList();
            foreach (var entityTrackable in entitiesTrackables)
            {
                List<Tracker> trackerList = entityTrackable.GetPropertiesTrackablesModified(OwnerId);
                if (trackerList.Any()) trackerList.AddRange(trackerList);
            }

            var entitiesSoftDeleted = ChangeTracker.Entries().Where(e => e.Entity is ISoftDelete && e.State == EntityState.Deleted).ToList();
            foreach (var entitySoftDelete in entitiesSoftDeleted)
            {
                entitySoftDelete.State = EntityState.Modified;
                entitySoftDelete.Property(nameof(ISoftDelete.IsDeleted)).CurrentValue = true;
                entitySoftDelete.Property(nameof(ISoftDelete.IsDeleted)).IsModified = true;
            }

            return base.SaveChanges();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
        //    base.OnModelCreating(modelBuilder);
        //}

        //TODO: Extract to a files
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkCenters>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(c => c.Company)
                   .WithMany(w => w.WorkCenters);

                entity.HasOne(p => p.People)
                    .WithMany(w => w.WorkCenters);
            });

            modelBuilder.Entity<AdvancedTechnicianListFilterPersons>(entity =>
            {
                entity.HasKey(e => new { e.TechnicianListFilterId, e.PeopleId });

                entity.HasOne(d => d.People)
                    .WithMany(p => p.AdvancedTechnicianListFilterPersons)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ADVANCEDTECHNICIANLISTFILTERSPERSONS_PERSONS");

                entity.HasOne(d => d.TechnicianListFilter)
                    .WithMany(p => p.AdvancedTechnicianListFilterPersons)
                    .HasForeignKey(d => d.TechnicianListFilterId)
                    .HasConstraintName("FK_ADVANCEDTECHNICIANLISTFILTERSPERSONS_ADVANCEDTECHNICIANLISTFILTER");
            });

            modelBuilder.Entity<AdvancedTechnicianListFilters>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("UQ__Advanced__3214EC06FC66D039")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.AdvancedTechnicianListFilters)
                    .HasForeignKey<AdvancedTechnicianListFilters>(d => d.Id)
                    .HasConstraintName("FK__AdvancedTech__Id__3F9B6DFF");
            });

            modelBuilder.Entity<AssetsAudit>(entity =>
            {
                entity.Property(e => e.RegistryDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetsAudit)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AssetsAudit_Assets");
            });

            modelBuilder.Entity<AssetsAuditChanges>(entity =>
            {
                entity.HasKey(e => new { e.AssetAuditId, e.Property });

                entity.Property(e => e.Property)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NewValue)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.OldValue)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.HasOne(d => d.AssetAudit)
                    .WithMany(p => p.AssetsAuditChanges)
                    .HasForeignKey(d => d.AssetAuditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetsAuditChanges_AssetsAudit");
            });

            modelBuilder.Entity<AssetStatuses>(entity =>
            {
                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Audits>(entity =>
            {
                entity.Property(e => e.DataHora).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Audits)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_Audits_Tasks");

                entity.HasOne(d => d.UserConfiguration).WithMany(p => p.AuditsUser).OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.UserConfigurationSupplanter).WithMany(p => p.AuditsUserSupplanter).OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WorkOrder)
                    .WithMany(p => p.Audits)
                    .HasForeignKey(d => d.WorkOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auditoria_WorkOrders");
            });

            modelBuilder.Entity<BasicTechnicianListFilterCalendarPlanningViewConfiguration>(entity =>
            {
                entity.HasKey(e => new { e.TechnicianListFilterId, e.CalendarPlanningViewConfigurationId });

                entity.HasOne(d => d.CalendarPlanningViewConfiguration)
                    .WithMany(p => p.BasicTechnicianListFilterCalendarPlanningViewConfiguration)
                    .HasForeignKey(d => d.CalendarPlanningViewConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BasicTechniciansListCalendarPlanningViewConfiguration_CalendarPlanningViewConfiguration");

                entity.HasOne(d => d.TechnicianListFilter)
                    .WithMany(p => p.BasicTechnicianListFilterCalendarPlanningViewConfiguration)
                    .HasForeignKey(d => d.TechnicianListFilterId)
                    .HasConstraintName("FK_CalendarPlanningViewConfiguration_BasicTechnicianListFilters");
            });

            modelBuilder.Entity<BasicTechnicianListFilters>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("UQ__BasicTec__3214EC06B9ADA133")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.TechnicianListFilters)
                    .WithOne(p => p.BasicTechnicianListFilters)
                    .HasForeignKey<BasicTechnicianListFilters>(d => d.Id)
                    .HasConstraintName("FK__BasicTechnic__Id__4830B400");
            });

            modelBuilder.Entity<BasicTechnicianListFilterSkills>(entity =>
            {
                entity.HasKey(e => new { e.TechnicianListFilterId, e.SkillId });

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.BasicTechnicianListFilterSkills)
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("FK_BASICTECHNICIANLISTFILTERSSKILLS_SKILLS");

                entity.HasOne(d => d.TechnicianListFilter)
                    .WithMany(p => p.BasicTechnicianListFilterSkills)
                    .HasForeignKey(d => d.TechnicianListFilterId)
                    .HasConstraintName("FK_BASICTECHNICIANLISTFILTERSSKILLS_BASICTECHNICIANLISTFILTERS");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ErpSystemInstanceId).HasDefaultValueSql("((1))");

                entity.Property(e => e.ExternalSystemNumber)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Task)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.ErpSystemInstance)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.ErpSystemInstanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_ErpSystemInstance");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_People");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_Bill_Services");

                entity.HasOne(d => d.TaskNavigation)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_Bill_Task");

                entity.HasOne(d => d.Workorder)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.WorkorderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_WorkOrders");
            });

            modelBuilder.Entity<BillingItems>(entity =>
            {
                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BillingLineItem)
                    .WithMany(p => p.BillingItems)
                    .HasForeignKey(d => d.BillingLineItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillingItems_BillingLineItems");
            });

            modelBuilder.Entity<BillingLineItems>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PredefinedService)
                    .WithMany(p => p.BillingLineItems)
                    .HasForeignKey(d => d.PredefinedServiceId)
                    .HasConstraintName("FK_BillingLineItems_PredefinedServices");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.BillingLineItems)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillingLineItems_Projects");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.BillingLineItems)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_BillingLineItems_Tasks");

                entity.HasOne(d => d.WorkOrderCategory)
                    .WithMany(p => p.BillingLineItems)
                    .HasForeignKey(d => d.WorkOrderCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillingLineItems_WorkOrderCategories");
            });

            modelBuilder.Entity<BillingRule>(entity =>
            {
                entity.Property(e => e.Condition)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ErpSystemInstanceId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ErpSystemInstance)
                    .WithMany(p => p.BillingRule)
                    .HasForeignKey(d => d.ErpSystemInstanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillingRule_ErpSystemInstance");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.BillingRule)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillingRule_Tasks");
            });

            modelBuilder.Entity<BillingRuleItem>(entity =>
            {
                entity.Property(e => e.Units)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.BillingRule)
                    .WithMany(p => p.BillingRuleItem)
                    .HasForeignKey(d => d.BillingRuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillingRuleItem_BillingRule");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.BillingRuleItem)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillingRuleItem_Items");
            });

            modelBuilder.Entity<BillLine>(entity =>
            {
                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.BillLine)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemSerialNumber_Bill");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.BillLine)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemSerialNumber_Items");

                entity.HasOne(d => d.ItemsSerialNumber)
                    .WithMany(p => p.BillLine)
                    .HasForeignKey(d => new { d.ItemId, d.SerialNumber })
                    .HasConstraintName("FK_ItemsSerialNumber");
            });

            modelBuilder.Entity<Brands>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CalendarEvents>(entity =>
            {
                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CostHour).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeletedOccurrence).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description)
                    .HasMaxLength(140)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReplacedEventOccurrenceTs).HasColumnName("ReplacedEventOccurrenceTS");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.CalendarEvents)
                    .HasForeignKey(d => d.CalendarId)
                    .HasConstraintName("FK__CalendarE__Calen__5B438874");

                entity.HasOne(d => d.ParentEvent)
                    .WithMany(p => p.InverseParentEvent)
                    .HasForeignKey(d => d.ParentEventId)
                    .HasConstraintName("FK__CalendarE__Paren__5C37ACAD");
            });

            modelBuilder.Entity<CalendarPlanningViewConfiguration>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CalendarPlanningViewConfigurationNavigation)
                    .WithMany(p => p.CalendarPlanningViewConfiguration)
                    .HasForeignKey(d => d.CalendarPlanningViewConfigurationId)
                    .HasConstraintName("FK__CalendarP__Calen__5D2BD0E6");

                entity.HasOne(d => d.UserConfiguration).WithMany(p => p.CalendarPlanningViewConfiguration);
            });

            modelBuilder.Entity<CalendarPlanningViewConfigurationPeople>(entity =>
            {
                entity.HasKey(e => new { e.ViewId, e.PeopleId });

                entity.HasOne(d => d.People)
                    .WithMany(p => p.CalendarPlanningViewConfigurationPeople)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CalendarP__Peopl__5F141958");

                entity.HasOne(d => d.View)
                    .WithMany(p => p.CalendarPlanningViewConfigurationPeople)
                    .HasForeignKey(d => d.ViewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CalendarP__ViewI__60083D91");
            });

            modelBuilder.Entity<CalendarPlanningViewConfigurationPeopleCollection>(entity =>
            {
                entity.HasKey(e => new { e.ViewId, e.PeopleCollectionId });

                entity.HasOne(d => d.PeopleCollection)
                    .WithMany(p => p.CalendarPlanningViewConfigurationPeopleCollection)
                    .HasForeignKey(d => d.PeopleCollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CalendarP__Peopl__60FC61CA");

                entity.HasOne(d => d.View)
                    .WithMany(p => p.CalendarPlanningViewConfigurationPeopleCollection)
                    .HasForeignKey(d => d.ViewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CalendarP__ViewI__61F08603");
            });

            modelBuilder.Entity<Calendars>(entity =>
            {
                entity.Property(e => e.Color)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(140)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Alias)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankCity)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankPostalCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BranchNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ComercialName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContableCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ControlDigit)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CorporateName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdIcg).HasColumnName("IdICG");

                entity.Property(e => e.InternCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SwiftCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Web)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClosingCodes>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClosingCodesFather)
                    .WithMany(p => p.InverseClosingCodesFather)
                    .HasForeignKey(d => d.ClosingCodesFatherId)
                    .HasConstraintName("FK_ClosingCodes_ClosingCodesFather");

                entity.HasOne(d => d.CollectionsClosureCodes)
                    .WithMany(p => p.ClosingCodes)
                    .HasForeignKey(d => d.CollectionsClosureCodesId)
                    .HasConstraintName("FK_ClosingCodes_CollectionsClosureCodes");
            });

            modelBuilder.Entity<CollectionsClosureCodes>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CollectionsExtraField>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CollectionsExtraFieldExtraField>(entity =>
            {
                entity.HasKey(e => new { e.CollectionsExtraFieldId, e.ExtraFieldId });

                entity.Property(e => e.Position).HasDefaultValueSql("((2147483647))");

                entity.HasOne(d => d.CollectionsExtraField)
                    .WithMany(p => p.CollectionsExtraFieldExtraField)
                    .HasForeignKey(d => d.CollectionsExtraFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CollectionsExtraFieldExtraField_CollectionsExtraField");

                entity.HasOne(d => d.ExtraField)
                    .WithMany(p => p.CollectionsExtraFieldExtraField)
                    .HasForeignKey(d => d.ExtraFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CollectionsExtraFieldExtraField_ExtraField");
            });

            modelBuilder.Entity<CollectionsTypesWorkOrders>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CompaniesCostHistorical>(entity =>
            {
                entity.Property(e => e.Until)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompaniesCostHistorical)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Companies__Compa__66B53B20");
            });

            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstSurname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondSurname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ContactsFinalClients>(entity =>
            {
                entity.HasKey(e => new { e.FinalClientId, e.ContactId });

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactsFinalClients)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactsFinalClients_Contacts");

                entity.HasOne(d => d.FinalClient)
                    .WithMany(p => p.ContactsFinalClients)
                    .HasForeignKey(d => d.FinalClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactsFinalClients_FinalClients");
            });

            modelBuilder.Entity<ContactsLocationsFinalClients>(entity =>
            {
                entity.HasKey(e => new { e.LocationId, e.ContactId });

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactsLocationsFinalClients)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactsLocationsFinalClients_Contacts");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.ContactsLocationsFinalClients)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactsLocationsFinalClients_Locations");
            });

            modelBuilder.Entity<ContractContacts>(entity =>
            {
                entity.HasKey(e => new { e.ContractId, e.ContactId });

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContractContacts)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractContacts_Contacts");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractContacts)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractContacts_Contracts");
            });

            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.Property(e => e.ContractUrl)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Object)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Signer)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Contracts_Client");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.PeopleId)
                    .HasConstraintName("FK_Contracts_People");
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Departmen__Compa__6F4A8121");
            });

            modelBuilder.Entity<DerivedServices>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalIdentifier)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InternalIdentifier)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClosingCodesIdN1Navigation)
                    .WithMany(p => p.DerivedServicesClosingCodesIdN1Navigation)
                    .HasForeignKey(d => d.ClosingCodesIdN1)
                    .HasConstraintName("FK_DerivedServices_ClosingCodesN1");

                entity.HasOne(d => d.ClosingCodesIdN2Navigation)
                    .WithMany(p => p.DerivedServicesClosingCodesIdN2Navigation)
                    .HasForeignKey(d => d.ClosingCodesIdN2)
                    .HasConstraintName("FK_DerivedServices_ClosingCodesN2");

                entity.HasOne(d => d.ClosingCodesIdN3Navigation)
                    .WithMany(p => p.DerivedServicesClosingCodesIdN3Navigation)
                    .HasForeignKey(d => d.ClosingCodesIdN3)
                    .HasConstraintName("FK_DerivedServices_ClosingCodesN3");

                entity.HasOne(d => d.PeopleResponsible)
                    .WithMany(p => p.DerivedServices)
                    .HasForeignKey(d => d.PeopleResponsibleId)
                    .HasConstraintName("FK_DerivedServices_People");

                entity.HasOne(d => d.PredefinedServices)
                    .WithMany(p => p.DerivedServices)
                    .HasForeignKey(d => d.PredefinedServicesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DerivedServices_PredefinedServices");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.DerivedServices)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DerivedServices_Projects");

                entity.HasOne(d => d.SubcontractResponsible)
                    .WithMany(p => p.DerivedServices)
                    .HasForeignKey(d => d.SubcontractResponsibleId)
                    .HasConstraintName("FK_DerivedServices_Subcontracts");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.DerivedServices)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DerivedServices_Tasks");
            });

            modelBuilder.Entity<ErpItemsSyncConfig>(entity =>
            {
                entity.HasKey(e => e.TenantId);

                entity.Property(e => e.TenantId).ValueGeneratedNever();

                entity.HasOne(d => d.ErpSystemInstance)
                    .WithMany(p => p.ErpItemsSyncConfig)
                    .HasForeignKey(d => d.ErpSystemInstanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ErpItemsSyncConfig_ErpSystemInstance");
            });

            modelBuilder.Entity<ErpSystemInstance>(entity =>
            {
                entity.Property(e => e.DatabaseIpAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DatabaseName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DatabasePwd)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DatabaseUser)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ErpSystemIdentifier)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ErpSystemInstanceQuery>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SqlQuery)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.ErpSystemInstance)
                    .WithMany(p => p.ErpSystemInstanceQuery)
                    .HasForeignKey(d => d.ErpSystemInstanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ErpSystemInstanceQuery_ErpSystemInstance");
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExpenseTicket)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.ExpenseTicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expenses__Expens__79C80F94");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expenses__Paymen__7ABC33CD");

                entity.HasOne(d => d.ExpenseType)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.ExpenseTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expenses__ExpenseTypes_ExpenseTypeId");
            });

            modelBuilder.Entity<ExpensesTicketFile>(entity =>
            {
                entity.HasKey(e => new { e.ExpenseTicketId, e.SomFileId });

                entity.HasOne(d => d.ExpenseTicket)
                    .WithMany(p => p.ExpensesTicketFile)
                    .HasForeignKey(d => d.ExpenseTicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpensesTicket_File_ExpenseTicket");

                entity.HasOne(d => d.SomFile)
                    .WithMany(p => p.ExpensesTicketFile)
                    .HasForeignKey(d => d.SomFileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpensesTicket_File_SomFiles");
            });

            modelBuilder.Entity<ExpensesTickets>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentInformation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Pending')");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ValidationDate).HasColumnType("datetime");

                entity.Property(e => e.ValidationObservations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.People)
                    .WithMany(p => p.ExpensesTicketsPeople)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExpensesT__Peopl__7D98A078");

                entity.HasOne(d => d.PeopleValidator)
                    .WithMany(p => p.ExpensesTicketsPeopleValidator)
                    .HasForeignKey(d => d.PeopleValidatorId)
                    .HasConstraintName("FK__ExpensesT__Peopl__7E8CC4B1");

                entity.HasOne(d => d.WorkOrder)
                    .WithMany(p => p.ExpensesTickets)
                    .HasForeignKey(d => d.WorkOrderId)
                    .HasConstraintName("FK__ExpensesT__WorkO__7F80E8EA");

            });           

            modelBuilder.Entity<ExpenseTypes>(entity =>
            {
                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExternalServicesConfiguration>(entity =>
            {
                entity.HasIndex(e => e.ExternalService)
                    .HasName("UQ__External__E71D0598903D98C7")
                    .IsUnique();

                entity.Property(e => e.ExternalService)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.AssetQueue)
                    .WithMany(p => p.ExternalServicesConfigurationAssetQueue)
                    .HasForeignKey(d => d.AssetQueueId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_AssetQueue");

                entity.HasOne(d => d.AssetWoExternalStatus)
                    .WithMany(p => p.ExternalServicesConfigurationAssetWoExternalStatus)
                    .HasForeignKey(d => d.AssetWoExternalStatusId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_AssetWoExternalStatus");

                entity.HasOne(d => d.AssetWoStatus)
                    .WithMany(p => p.ExternalServicesConfigurationAssetWoStatus)
                    .HasForeignKey(d => d.AssetWoStatusId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_AssetWoStatus");

                entity.HasOne(d => d.FinalClient)
                    .WithMany(p => p.ExternalServicesConfiguration)
                    .HasForeignKey(d => d.FinalClientId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_FinalClient");

                entity.HasOne(d => d.Flow)
                    .WithMany(p => p.ExternalServicesConfiguration)
                    .HasForeignKey(d => d.FlowId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_Flow");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.ExternalServicesConfiguration)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_Location");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ExternalServicesConfiguration)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_Project");

                entity.HasOne(d => d.Queue)
                    .WithMany(p => p.ExternalServicesConfigurationQueue)
                    .HasForeignKey(d => d.QueueId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_Queue");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.ExternalServicesConfiguration)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_Task");

                entity.HasOne(d => d.WoCategory)
                    .WithMany(p => p.ExternalServicesConfiguration)
                    .HasForeignKey(d => d.WoCategoryId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_WoCategory");

                entity.HasOne(d => d.WoExternalStatus)
                    .WithMany(p => p.ExternalServicesConfigurationWoExternalStatus)
                    .HasForeignKey(d => d.WoExternalStatusId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_WoExternalStatus");

                entity.HasOne(d => d.WoStatus)
                    .WithMany(p => p.ExternalServicesConfigurationWoStatus)
                    .HasForeignKey(d => d.WoStatusId)
                    .HasConstraintName("FK_ExternalServicesConfiguration_WoStatus");
            });

            modelBuilder.Entity<ExternalServicesConfigurationProjectCategories>(entity =>
            {
                entity.Property(e => e.AssetSerialNumberProperty)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Configuration)
                    .WithMany(p => p.ExternalServicesConfigurationProjectCategories)
                    .HasForeignKey(d => d.ConfigurationId)
                    .HasConstraintName("FK__ExternalS__Confi__0BE6BFCF");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ExternalServicesConfigurationProjectCategories)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__ExternalS__Proje__0CDAE408");

                entity.HasOne(d => d.WoCategory)
                    .WithMany(p => p.ExternalServicesConfigurationProjectCategories)
                    .HasForeignKey(d => d.WoCategoryId)
                    .HasConstraintName("FK__ExternalS__WoCat__0DCF0841");
            });

            modelBuilder.Entity<ExternalServicesConfigurationProjectCategoriesProperties>(entity =>
            {
                entity.HasKey(e => new { e.ExternalServicesConfigurationProjectCategoriesId, e.ColumnName });

                entity.Property(e => e.ExternalServicesConfigurationProjectCategoriesId).HasColumnName("ExternalServicesConfiguration_ProjectCategoriesId");

                entity.Property(e => e.ColumnName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExternalServicesConfigurationProjectCategories)
                    .WithMany(p => p.ExternalServicesConfigurationProjectCategoriesProperties)
                    .HasForeignKey(d => d.ExternalServicesConfigurationProjectCategoriesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExternalS__Exter__0EC32C7A");
            });

            modelBuilder.Entity<ExternalServicesConfigurationSites>(entity =>
            {
                entity.HasKey(e => new { e.ExternalServicesConfigurationId, e.FinalClientId, e.ExtClientId });

                entity.Property(e => e.ExtClientId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExternalServicesConfiguration)
                    .WithMany(p => p.ExternalServicesConfigurationSites)
                    .HasForeignKey(d => d.ExternalServicesConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExternalServicesConfiguration_Sites_ExternalServicesConfiguration");

                entity.HasOne(d => d.FinalClient)
                    .WithMany(p => p.ExternalServicesConfigurationSites)
                    .HasForeignKey(d => d.FinalClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExternalServicesConfiguration_Sites_FinalClients");
            });

            modelBuilder.Entity<ExternalSystemImportData>(entity =>
            {
                entity.HasKey(e => new { e.ImportCode, e.Property });

                entity.Property(e => e.ImportCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Property)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExternalSystem)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.HasOne(d => d.WorkOrder)
                    .WithMany(p => p.ExternalSystemImportData)
                    .HasForeignKey(d => d.WorkOrderId)
                    .HasConstraintName("FK_ExternalSystemImportData_WorkOrder");
            });

            modelBuilder.Entity<ExternalWorOrderStatuses>(entity =>
            {
                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IdIcg)
                    .HasColumnName("IdICG")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExtraFields>(entity =>
            {
                entity.Property(e => e.AllowedStringValues)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(900)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.ErpSystemInstanceQuery)
                    .WithMany(p => p.ExtraFields)
                    .HasForeignKey(d => d.ErpSystemInstanceQueryId)
                    .HasConstraintName("FK_ExtraFields_ErpSystemInstanceQuery");
            });

            modelBuilder.Entity<ExtraFieldsValues>(entity =>
            {
                entity.Property(e => e.DataValue).HasColumnType("datetime");

                entity.Property(e => e.StringValue)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.HasOne(d => d.DerivedService)
                    .WithMany(p => p.ExtraFieldsValues)
                    .HasForeignKey(d => d.DerivedServiceId)
                    .HasConstraintName("FK_ExtraFieldsValues_DerivedServices");

                entity.HasOne(d => d.ExtraField)
                    .WithMany(p => p.ExtraFieldsValues)
                    .HasForeignKey(d => d.ExtraFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExtraFieldsValues_ExtraFields");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ExtraFieldsValues)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ExtraFieldsValues_Services");

                entity.HasOne(d => d.WorkOrderDeritative)
                    .WithMany(p => p.ExtraFieldsValues)
                    .HasForeignKey(d => d.WorkOrderDeritativeId)
                    .HasConstraintName("FK_ExtraFieldsValues_WorkOrdersDeritative");
            });

            modelBuilder.Entity<Families>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FinalClients>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdExtern)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nif)
                    .IsRequired()
                    .HasColumnName("NIF")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Phone1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PeopleCommercial)
                    .WithMany(p => p.FinalClients)
                    .HasForeignKey(d => d.PeopleCommercialId)
                    .HasConstraintName("FK_FinalClients_People");
            });

            modelBuilder.Entity<FinalClientSiteCalendar>(entity =>
            {
                entity.HasKey(e => new { e.FinalClientSiteId, e.CalendarId });

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.FinalClientSiteCalendar)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FinalClie__Calen__184C96B4");

                entity.HasOne(d => d.FinalClientSite)
                    .WithMany(p => p.FinalClientSiteCalendar)
                    .HasForeignKey(d => d.FinalClientSiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FinalClie__Final__1940BAED");
            });

            modelBuilder.Entity<Flows>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Published)
                    .IsRequired()
                    .HasDefaultValueSql("('true')");
            });

            modelBuilder.Entity<FormConfigs>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Page)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FormElements>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.HasOne(d => d.FormConfigs)
                    .WithMany(p => p.FormElements)
                    .HasForeignKey(d => d.FormConfigsId)
                    .HasConstraintName("FK_FormElements_FormConfigs");
            });

            modelBuilder.Entity<Guarantee>(entity =>
            {
                entity.Property(e => e.Armored)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BlnEndDate).HasColumnType("datetime");

                entity.Property(e => e.BlnStartDate).HasColumnType("datetime");

                entity.Property(e => e.ProEndDate).HasColumnType("datetime");

                entity.Property(e => e.ProStartDate).HasColumnType("datetime");

                entity.Property(e => e.Provider)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Standard)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StdEndDate).HasColumnType("datetime");

                entity.Property(e => e.StdStartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<HiredServices>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ErpReference)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.InternalReference)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ErpVersion).IsRowVersion();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ItemsPointsRate>(entity =>
            {
                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemsPointsRate)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsPointsRate_Items");

                entity.HasOne(d => d.PointsRate)
                    .WithMany(p => p.ItemsPointsRate)
                    .HasForeignKey(d => d.PointsRateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsPointsRate_PointsRate");
            });

            modelBuilder.Entity<ItemsPurchaseRate>(entity =>
            {
                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemsPurchaseRate)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsPurchaseRate_Items");

                entity.HasOne(d => d.PurchaseRate)
                    .WithMany(p => p.ItemsPurchaseRate)
                    .HasForeignKey(d => d.PurchaseRateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsPurchaseRate_PurchaseRate");
            });

            modelBuilder.Entity<ItemsSalesRate>(entity =>
            {
                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemsSalesRate)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsSalesRate_Items");

                entity.HasOne(d => d.SalesRate)
                    .WithMany(p => p.ItemsSalesRate)
                    .HasForeignKey(d => d.SalesRateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsSalesRate_SalesRate");
            });

            modelBuilder.Entity<ItemsSerialNumber>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.SerialNumber });

                //entity.HasIndex(e => e.SerialNumber)
                //    .HasName("UQ__ItemsSer__048A000827618D28")
                //    .IsUnique();

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                
                entity.Property(e => e.Observations)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemsSerialNumber)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsSerialNumber_Items");
                
                entity.HasOne(d => d.ItemsSerialNumberStatuses)
                    .WithMany(p => p.ItemsSerialNumber)
                    .HasForeignKey(d => d.ItemsSerialNumberStatusesId)
                    .HasConstraintName("FK_ItemsSerialNumber_ItemsSerialNumberStatuses");
            });

            modelBuilder.Entity<ItemsSerialNumberStatuses>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Journeys>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Observations)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.Journeys)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Journeys_People");
            });

            modelBuilder.Entity<JourneysStates>(entity =>
            {
                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.Observations)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Journey)
                    .WithMany(p => p.JourneysStates)
                    .HasForeignKey(d => d.JourneyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JourneysStates_Journeys");
            });

            modelBuilder.Entity<Knowledge>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KnowledgePeople>(entity =>
            {
                entity.HasKey(e => new { e.KnowledgeId, e.PeopleId });

                entity.HasOne(d => d.Knowledge)
                    .WithMany(p => p.KnowledgePeople)
                    .HasForeignKey(d => d.KnowledgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgePeople_Knowledge");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.KnowledgePeople)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgePeople_People");
            });

            modelBuilder.Entity<KnowledgeSubContracts>(entity =>
            {
                entity.HasKey(e => new { e.KnowledgeId, e.SubContractId });

                entity.HasOne(d => d.Knowledge)
                    .WithMany(p => p.KnowledgeSubContracts)
                    .HasForeignKey(d => d.KnowledgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeSubContracts_Knowledge");

                entity.HasOne(d => d.SubContract)
                    .WithMany(p => p.KnowledgeSubContracts)
                    .HasForeignKey(d => d.SubContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeSubContracts_SubContracts");
            });

            modelBuilder.Entity<KnowledgeToolsType>(entity =>
            {
                entity.HasKey(e => new { e.KnowledgeId, e.ToolsTypeId });

                entity.HasOne(d => d.Knowledge)
                    .WithMany(p => p.KnowledgeToolsType)
                    .HasForeignKey(d => d.KnowledgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeToolsType_Knowledge");

                entity.HasOne(d => d.ToolsType)
                    .WithMany(p => p.KnowledgeToolsType)
                    .HasForeignKey(d => d.ToolsTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeToolsType_ToolsType");
            });

            modelBuilder.Entity<KnowledgeWorkOrderTypes>(entity =>
            {
                entity.HasKey(e => new { e.KnowledgeId, e.WorkOrderTypeId });
                entity.ToTable("KnowledgeWorkOrderTypes");

                entity.HasOne(d => d.Knowledge)
                    .WithMany(p => p.KnowledgeWorkOrderTypes)
                    .HasForeignKey(d => d.KnowledgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeWorkOrderTypes_Knowledge");

                entity.HasOne(d => d.WorkOrderType)
                    .WithMany(p => p.KnowledgeWorkOrderTypes)
                    .HasForeignKey(d => d.WorkOrderTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KnowledgeWorkOrderTypes_WorkOrderTypes");
            });

            modelBuilder.Entity<LiteralsPreconditions>(entity =>
            {
                entity.Property(e => e.ComparisonOperator)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomCampModel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExtraField)
                    .WithMany(p => p.LiteralsPreconditions)
                    .HasForeignKey(d => d.ExtraFieldId)
                    .HasConstraintName("FK_LiteralsPreconditions_ExtraFields");

                entity.HasOne(d => d.Precondition)
                    .WithMany(p => p.LiteralsPreconditions)
                    .HasForeignKey(d => d.PreconditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LiteralsPreconditions_Preconditions");
            });

            modelBuilder.Entity<LocationCalendar>(entity =>
            {
                entity.HasKey(e => new { e.LocationId, e.CalendarId });

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.LocationCalendar)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LocationC__Calen__2D47B39A");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LocationCalendar)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LocationC__Locat__2E3BD7D3");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Escala)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GateNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Phone1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StreetType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Subzone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PeopleResponsibleLocation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.PeopleResponsibleLocationId)
                    .HasConstraintName("FK_Locations_People");
            });

            modelBuilder.Entity<LocationsFinalClients>(entity =>
            {
                entity.HasKey(e => new { e.FinalClientId, e.LocationId });

                entity.HasIndex(e => e.CompositeCode)
                    .HasName("UQ_LocationsFinalClients_CompositeCode")
                    .IsUnique();

                entity.Property(e => e.CompositeCode)
                    .IsRequired()
                    .HasMaxLength(101)
                    .IsUnicode(false);

                entity.HasOne(d => d.FinalClient)
                    .WithMany(p => p.LocationsFinalClients)
                    .HasForeignKey(d => d.FinalClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationsFinalClients_FinalClients");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LocationsFinalClients)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationsFinalClients_Locations");

                entity.HasOne(d => d.PeopleCommercial)
                    .WithMany(p => p.LocationsFinalClients)
                    .HasForeignKey(d => d.PeopleCommercialId)
                    .HasConstraintName("FK_LocationsFinalClients_People");
            });

            modelBuilder.Entity<MailTemplate>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MainOtstatics>(entity =>
            {
                entity.ToTable("MainOTStatics");

                entity.Property(e => e.ColumnName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Percentage).HasDefaultValueSql("((0.0))");
            });

            modelBuilder.Entity<MainWoregistry>(entity =>
            {
                entity.ToTable("MainWORegistry");

                entity.Property(e => e.ArchivedWo).HasColumnName("ArchivedWO");

                entity.Property(e => e.ArrivalTime).HasColumnType("datetime");

                entity.Property(e => e.ExportWo).HasColumnName("ExportWO");

                entity.Property(e => e.FilteredWo).HasColumnName("FilteredWO");

                entity.Property(e => e.VisibleWo).HasColumnName("VisibleWO");
            });

            modelBuilder.Entity<MainWoViewConfigurationsColumns>(entity =>
            {
                entity.HasKey(e => new { e.UserMainWoviewConfigurationId, e.ColumnId });

                entity.Property(e => e.UserMainWoviewConfigurationId).HasColumnName("UserMainWOViewConfigurationId");

                entity.Property(e => e.FilterEndDate).HasColumnType("datetime");

                entity.Property(e => e.FilterStartDate).HasColumnType("datetime");

                entity.Property(e => e.FilterValues)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.UserMainWoviewConfiguration)
                    .WithMany(p => p.MainWoViewConfigurationsColumns)
                    .HasForeignKey(d => d.UserMainWoviewConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MainWOViewConfigurationsColumns_UsersMainWOViewConfigurations");
            });

            modelBuilder.Entity<MainWoviewConfigurationsGroups>(entity =>
            {
                entity.HasKey(e => new { e.UserMainWoviewConfigurationId, e.PeopleCollectionId });

                entity.ToTable("MainWOViewConfigurationsGroups");

                entity.Property(e => e.UserMainWoviewConfigurationId).HasColumnName("UserMainWOViewConfigurationId");

                entity.HasOne(d => d.PeopleCollection)
                    .WithMany(p => p.MainWoviewConfigurationsGroups)
                    .HasForeignKey(d => d.PeopleCollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MainWOViewConfigurationsGroups_PeopleCollection");

                entity.HasOne(d => d.UserMainWoviewConfiguration)
                    .WithMany(p => p.MainWoviewConfigurationsGroups)
                    .HasForeignKey(d => d.UserMainWoviewConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MainWOViewConfigurationsGroups_UserMainWoViewConfiguration");
            });

            modelBuilder.Entity<MainWoViewConfigurationsPeople>(entity =>
            {
                entity.HasKey(e => new { e.UserMainWoViewConfigurationId, e.PeopleId });

                entity.HasOne(d => d.People)
                    .WithMany(p => p.MainWoViewConfigurationsPeople)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MainWoViewConfigurationsPeople_People");

                entity.HasOne(d => d.UserMainWoViewConfiguration)
                    .WithMany(p => p.MainWoViewConfigurationsPeople)
                    .HasForeignKey(d => d.UserMainWoViewConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MainWoViewConfigurationsPeople_UserMainWoViewConfiguration");
            });

            modelBuilder.Entity<MaterialForm>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExtraFieldValue)
                    .WithMany(p => p.MaterialForm)
                    .HasForeignKey(d => d.ExtraFieldValueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaterialForm_ExtraFieldsValues");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.MaterialForm)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK_MaterialForm_Assets");
            });

            modelBuilder.Entity<Models>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Models_Brands");
            });

            modelBuilder.Entity<OptimizationFunctionWeights>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FulfillSlaweight).HasColumnName("FulfillSLAWeight");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnattendedWopenalty).HasColumnName("UnattendedWOPenalty");
            });

            modelBuilder.Entity<PaymentMethods>(entity =>
            {
                entity.Property(e => e.Mode)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<People>(entity =>
            {
                entity.Property(e => e.Dni)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentationUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Extension)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FisrtSurname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SecondSurname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__People__CompanyI__3AA1AEB8");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__People__Departme__3B95D2F1");

                entity.HasOne(d => d.PointsRate)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.PointsRateId)
                    .HasConstraintName("FK__People__PointsRa__3C89F72A");

                entity.HasOne(d => d.Subcontract)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.SubcontractId)
                    .HasConstraintName("FK_People_SubContracts");

                entity.HasOne(d => d.WorkCenter)
                   .WithMany(p => p.WorkCenterPeople)
                   .HasForeignKey(d => d.WorkCenterId)
                   .HasConstraintName("FK__People__WorkCenter");

                entity.HasOne(d => d.Warehouses)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.WarehousesId)
                    .HasConstraintName("FK__People__Warehouses");

                entity.HasOne(d => d.UserConfiguration).WithMany(p => p.People);

                entity.Property(e => e.IsVisible).HasDefaultValue(true);
            });

            modelBuilder.Entity<PeopleCalendars>(entity =>
            {
                entity.HasKey(e => new { e.PeopleId, e.CalendarId });

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.PeopleCalendars)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PeopleCal__Calen__405A880E");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.PeopleCalendars)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PeopleCal__Peopl__414EAC47");
            });

            modelBuilder.Entity<PeopleCollectionCalendars>(entity =>
            {
                entity.HasKey(e => new { e.PeopleCollectionId, e.CalendarId });

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.PeopleCollectionCalendars)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeopleCollectionCalendars_Calendars");

                entity.HasOne(d => d.PeopleCollection)
                    .WithMany(p => p.PeopleCollectionCalendars)
                    .HasForeignKey(d => d.PeopleCollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeopleCollectionCalendars_PeopleCollections");
            });

            modelBuilder.Entity<PeopleCollections>(entity =>
            {
                entity.Property(e => e.Info)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PeopleCollectionsAdmins>(entity =>
            {
                entity.HasKey(e => new { e.PeopleCollectionId, e.PeopleId });

                entity.HasOne(d => d.PeopleCollection)
                    .WithMany(p => p.PeopleCollectionsAdmins)
                    .HasForeignKey(d => d.PeopleCollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeopleCollectionsAdmins_PeopleCollection");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.PeopleCollectionsAdmins)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeopleCollectionsAdmins_People");
            });

            modelBuilder.Entity<PeopleCollectionsPeople>(entity =>
            {
                entity.HasKey(e => new { e.PeopleId, e.PeopleCollectionId });

                entity.HasOne(d => d.PeopleCollection)
                    .WithMany(p => p.PeopleCollectionsPeople)
                    .HasForeignKey(d => d.PeopleCollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeopleCollectionsPeople_PeopleCollections");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.PeopleCollectionsPeople)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeopleCollectionsPeople_People");
            });

            modelBuilder.Entity<PeopleCollectionsPermissions>(entity =>
            {
                entity.HasKey(e => new { e.PermissionId, e.PeopleCollectionId });

                entity.HasOne(d => d.Permissions).WithMany(p => p.PeopleCollectionPermission).HasForeignKey(e => e.PermissionId);
                entity.HasOne(d => d.PeopleCollection).WithMany(p => p.PeopleCollectionPermission).HasForeignKey(e => e.PeopleCollectionId);
            });

            modelBuilder.Entity<PeopleCost>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.PeopleCost)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeopleCost_People");
            });

            modelBuilder.Entity<PeopleCostHistorical>(entity =>
            {
                entity.Property(e => e.Until)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.PeopleCostHistorical)
                    .HasForeignKey(d => d.PeopleId)
                    .HasConstraintName("FK_WorkOrderCategories_People");
            });

            modelBuilder.Entity<PeopleRegisteredPda>(entity =>
            {
                entity.HasKey(e => new { e.PeopleId, e.DeviceId });

                entity.HasIndex(e => e.DeviceId)
                    .HasName("UniqueDevice")
                    .IsUnique();

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceName)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.GcmId)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.People)
                    .WithMany(p => p.PeopleRegisteredPda)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeopleRegisteredPda_People");
            });

            modelBuilder.Entity<PeoplePermissions>(entity =>
            {
                entity.Property(e => e.AssignmentDate).HasColumnType("datetime").HasDefaultValueSql("(getdate())");

                entity.HasKey(e => new { e.PeopleId, e.PermissionId });
                entity.HasOne(d => d.Permission).WithMany(p => p.PeoplePermission).HasForeignKey(e => e.PermissionId);
                entity.HasOne(d => d.People).WithMany(p => p.PeoplePermissions).HasForeignKey(e => e.PeopleId);
            });

            modelBuilder.Entity<PlanificationCriterias>(entity =>
            {
                entity.Property(e => e.FullfillSlaweight).HasColumnName("FullfillSLAWeight");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UnattendedWopenalty).HasColumnName("UnattendedWOPenalty");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.PlanificationCriterias)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PLANIFICATIONCRITERIAS_PERSONES");
            });

            modelBuilder.Entity<PlanificationProcessCalendarChangeTracker>(entity =>
            {
                entity.Property(e => e.LastCheckTime).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PlanificationProcesses>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Horizon).HasDefaultValueSql("((3))");

                entity.Property(e => e.LastModification).HasColumnType("datetime");

                entity.Property(e => e.MinutesToSlaend).HasColumnName("MinutesToSLAEnd");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartCriteria)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExecutionCalendarNavigation)
                    .WithMany(p => p.PlanificationProcesses)
                    .HasForeignKey(d => d.ExecutionCalendar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Planifica__Execu__4F9CCB9E");

                entity.HasOne(d => d.HumanResourcesFilterNavigation)
                    .WithMany(p => p.PlanificationProcessesHumanResourcesFilterNavigation)
                    .HasForeignKey(d => d.HumanResourcesFilter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Planifica__Human__5090EFD7");

                entity.HasOne(d => d.WeightsNavigation)
                    .WithMany(p => p.PlanificationProcesses)
                    .HasForeignKey(d => d.Weights)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Planifica__Weigh__51851410");

                entity.HasOne(d => d.WorkOrdersFilterNavigation)
                    .WithMany(p => p.PlanificationProcessesWorkOrdersFilterNavigation)
                    .HasForeignKey(d => d.WorkOrdersFilter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Planifica__WorkO__52793849");
            });

            modelBuilder.Entity<PlanificationProcessWorkOrderChangeTracker>(entity =>
            {
                entity.Property(e => e.LastCheckTime).HasColumnType("datetime");

                entity.Property(e => e.LastModified).HasColumnType("datetime");
            });

            modelBuilder.Entity<PlanningPanelViewConfiguration>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PeopleOwner)
                    .WithMany(p => p.PlanningPanelViewConfiguration)
                    .HasForeignKey(d => d.PeopleOwnerId)
                    .HasConstraintName("FK_PlanningPanelViewConfiguration_People");

                entity.HasOne(d => d.UsersMainWoViewConfiguration)
                    .WithMany(p => p.PlanningPanelViewConfiguration)
                    .HasForeignKey(d => d.UsersMainWoViewConfigurationId)
                    .HasConstraintName("FK_PlanningPanelViewConfiguration_UsersMainWoViewConfiguration");
            });

            modelBuilder.Entity<PlanningPanelViewConfigurationPeople>(entity =>
            {
                entity.HasKey(e => new { e.PlanningPanelViewConfigurationId, e.PeopleId });

                entity.HasOne(d => d.People)
                    .WithMany(p => p.PlanningPanelViewConfigurationPeople)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlanningPanelViewConfiguration_People_People");

                entity.HasOne(d => d.PlanningPanelViewConfiguration)
                    .WithMany(p => p.PlanningPanelViewConfigurationPeople)
                    .HasForeignKey(d => d.PlanningPanelViewConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlanningPanelViewConfiguration_People_PlanningPanelViewConfiguration");
            });

            modelBuilder.Entity<PlanningPanelViewConfigurationPeopleCollection>(entity =>
            {
                entity.HasKey(e => new { e.PlanningPanelViewConfigurationId, e.PeopleCollectionId });

                entity.HasOne(d => d.PeopleCollection)
                    .WithMany(p => p.PlanningPanelViewConfigurationPeopleCollection)
                    .HasForeignKey(d => d.PeopleCollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlanningP__Peopl__573DED66");

                entity.HasOne(d => d.PlanningPanelViewConfiguration)
                    .WithMany(p => p.PlanningPanelViewConfigurationPeopleCollection)
                    .HasForeignKey(d => d.PlanningPanelViewConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlanningP__Plann__5832119F");
            });

            modelBuilder.Entity<PointsRate>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ErpReference)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PostconditionCollections>(entity =>
            {
                entity.HasOne(d => d.Task)
                    .WithMany(p => p.PostconditionCollections)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_PostconditionCollections_Tasks");
            });

            modelBuilder.Entity<Postconditions>(entity =>
            {
                entity.Property(e => e.DateValue).HasColumnType("datetime");

                entity.Property(e => e.NameFieldModel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StringValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExternalWorOrderStatus)
                    .WithMany(p => p.Postconditions)
                    .HasForeignKey(d => d.ExternalWorOrderStatusId)
                    .HasConstraintName("FK_Postconditions_ExternalWorOrderStatus");

                entity.HasOne(d => d.ExtraField)
                    .WithMany(p => p.Postconditions)
                    .HasForeignKey(d => d.ExtraFieldId)
                    .HasConstraintName("FK_Postconditions_ExtraFields");

                entity.HasOne(d => d.PeopleManipulator)
                    .WithMany(p => p.PostconditionsPeopleManipulator)
                    .HasForeignKey(d => d.PeopleManipulatorId)
                    .HasConstraintName("FK_Postconditions_PeopleManipulator");

                entity.HasOne(d => d.PeopleResponsibleTechniciansCollection)
                    .WithMany(p => p.Postconditions)
                    .HasForeignKey(d => d.PeopleResponsibleTechniciansCollectionId)
                    .HasConstraintName("FK_Postconditions_PeopleCollections");

                entity.HasOne(d => d.PeopleTechnicians)
                    .WithMany(p => p.PostconditionsPeopleTechnicians)
                    .HasForeignKey(d => d.PeopleTechniciansId)
                    .HasConstraintName("FK_Postconditions_PeopleTechnicians");

                entity.HasOne(d => d.PostconditionCollections)
                    .WithMany(p => p.Postconditions)
                    .HasForeignKey(d => d.PostconditionCollectionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Postconditions_PostconditionCollections");

                entity.HasOne(d => d.Queue)
                    .WithMany(p => p.Postconditions)
                    .HasForeignKey(d => d.QueueId)
                    .HasConstraintName("FK_Postconditions_Queues");

                entity.HasOne(d => d.WorkOrderStatus)
                    .WithMany(p => p.Postconditions)
                    .HasForeignKey(d => d.WorkOrderStatusId)
                    .HasConstraintName("FK_Postconditions_WorkOrderStatus");
            });

            modelBuilder.Entity<Preconditions>(entity =>
            {
                entity.HasOne(d => d.PeopleResponsibleTechniciansCollection)
                    .WithMany(p => p.Preconditions)
                    .HasForeignKey(d => d.PeopleResponsibleTechniciansCollectionId)
                    .HasConstraintName("FK_Preconditions_PeopleCollections");

                entity.HasOne(d => d.PostconditionCollection)
                    .WithMany(p => p.Preconditions)
                    .HasForeignKey(d => d.PostconditionCollectionId)
                    .HasConstraintName("FK_Postcondicions_PostconditionCollections");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Preconditions)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_Preconditions_Tasks");
            });

            modelBuilder.Entity<PreconditionsLiteralValues>(entity =>
            {
                entity.Property(e => e.DataValue).HasColumnType("datetime");

                entity.Property(e => e.StringValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkOrderTypesN1id).HasColumnName("WorkOrderTypesN1Id");

                entity.Property(e => e.WorkOrderTypesN2id).HasColumnName("WorkOrderTypesN2Id");

                entity.Property(e => e.WorkOrderTypesN3id).HasColumnName("WorkOrderTypesN3Id");

                entity.Property(e => e.WorkOrderTypesN4id).HasColumnName("WorkOrderTypesN4Id");

                entity.Property(e => e.WorkOrderTypesN5id).HasColumnName("WorkOrderTypesN5Id");

                entity.HasOne(d => d.ExternalWorOrderStatus)
                    .WithMany(p => p.PreconditionsLiteralValues)
                    .HasForeignKey(d => d.ExternalWorOrderStatusId)
                    .HasConstraintName("FK_PreconditionsLiteralValues_ExternalWorOrderStatuses");

                entity.HasOne(d => d.FinalClient)
                    .WithMany(p => p.PreconditionsLiteralValues)
                    .HasForeignKey(d => d.FinalClientId)
                    .HasConstraintName("FK_PreconditionsLiteralValues_FinalClients");

                entity.HasOne(d => d.LiteralPrecondition)
                    .WithMany(p => p.PreconditionsLiteralValues)
                    .HasForeignKey(d => d.LiteralPreconditionId)
                    .HasConstraintName("FK_PreconditionsLiteralValues_LiteralsPreconditions");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.PreconditionsLiteralValues)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_PreconditionsLiteralValues_Locations");

                entity.HasOne(d => d.PeopleManipulator)
                    .WithMany(p => p.PreconditionsLiteralValuesPeopleManipulator)
                    .HasForeignKey(d => d.PeopleManipulatorId)
                    .HasConstraintName("FK_PreconditionsLiteralValues_PeopleManipulator");

                entity.HasOne(d => d.PeopleResponsibleTechniciansCollection)
                    .WithMany(p => p.PreconditionsLiteralValues)
                    .HasForeignKey(d => d.PeopleResponsibleTechniciansCollectionId)
                    .HasConstraintName("FK_PreconditionsLiteralValues_PeopleCollections");

                entity.HasOne(d => d.PeopleTechnician)
                    .WithMany(p => p.PreconditionsLiteralValuesPeopleTechnician)
                    .HasForeignKey(d => d.PeopleTechnicianId)
                    .HasConstraintName("FK_PreconditionsLiteralValues_PeopleTechnicians");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.PreconditionsLiteralValues)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_PreconditionsLiteralValues_Projects");

                entity.HasOne(d => d.Queue)
                    .WithMany(p => p.PreconditionsLiteralValues)
                    .HasForeignKey(d => d.QueueId)
                    .HasConstraintName("FK_PreconditionsLiteralValues_Queues");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.PreconditionsLiteralValues)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK_PreconditionsLiteralValues_Assets");

                entity.HasOne(d => d.WorkOrderCategory)
                    .WithMany(p => p.PreconditionsLiteralValues)
                    .HasForeignKey(d => d.WorkOrderCategoryId)
                    .HasConstraintName("FK_ValorsLiteralsPrecondicio_WorkOrderCategories");

                entity.HasOne(d => d.WorkOrderStatus)
                    .WithMany(p => p.PreconditionsLiteralValues)
                    .HasForeignKey(d => d.WorkOrderStatusId)
                    .HasConstraintName("FK_PreconditionsLiteralValues_WorkOrderStatuses");

                entity.HasOne(d => d.WorkOrderTypesN1)
                    .WithMany(p => p.PreconditionsLiteralValuesWorkOrderTypesN1)
                    .HasForeignKey(d => d.WorkOrderTypesN1id)
                    .HasConstraintName("FK_PreconditionsLiteralValues_WorkOrderTypesN1");

                entity.HasOne(d => d.WorkOrderTypesN2)
                    .WithMany(p => p.PreconditionsLiteralValuesWorkOrderTypesN2)
                    .HasForeignKey(d => d.WorkOrderTypesN2id)
                    .HasConstraintName("FK_PreconditionsLiteralValues_WorkOrderTypesN2");

                entity.HasOne(d => d.WorkOrderTypesN3)
                    .WithMany(p => p.PreconditionsLiteralValuesWorkOrderTypesN3)
                    .HasForeignKey(d => d.WorkOrderTypesN3id)
                    .HasConstraintName("FK_PreconditionsLiteralValues_WorkOrderTypesN3");

                entity.HasOne(d => d.WorkOrderTypesN4)
                    .WithMany(p => p.PreconditionsLiteralValuesWorkOrderTypesN4)
                    .HasForeignKey(d => d.WorkOrderTypesN4id)
                    .HasConstraintName("FK_PreconditionsLiteralValues_WorkOrderTypesN4");

                entity.HasOne(d => d.WorkOrderTypesN5)
                    .WithMany(p => p.PreconditionsLiteralValuesWorkOrderTypesN5)
                    .HasForeignKey(d => d.WorkOrderTypesN5id)
                    .HasConstraintName("FK_PreconditionsLiteralValues_WorkOrderTypesN5");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.PreconditionsLiteralValues)
                    .HasForeignKey(d => d.ZoneId)
                    .HasConstraintName("FK_ValorsLiteralsPrecondicio_Zones");
            });

            modelBuilder.Entity<PredefinedServices>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CollectionExtraField)
                    .WithMany(p => p.PredefinedServices)
                    .HasForeignKey(d => d.CollectionExtraFieldId)
                    .HasConstraintName("FK_PredefinedServices_CollectionsExtraField");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.PredefinedServices)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PredefinedServices_Projects");
            });

            modelBuilder.Entity<PredefinedServicesPermission>(entity =>
            {
                entity.HasKey(e => new { e.PermissionId, e.PredefinedServiceId });
                entity.HasOne(d => d.Permission).WithMany(p => p.PredefinedServicesPermission).HasForeignKey(e => e.PermissionId);
                entity.HasOne(d => d.PredefinedService).WithMany(p => p.PredefinedServicesPermission).HasForeignKey(e => e.PredefinedServiceId);
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.Property(e => e.BackOfficeResponsible)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultTechnicalCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IdIcg).HasColumnName("IdICG");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Serie)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.TechnicalResponsible)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CollectionsClosureCodes)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CollectionsClosureCodesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Projects_CollectionsClosureCodes");

                entity.HasOne(d => d.CollectionsExtraField)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CollectionsExtraFieldId)
                    .HasConstraintName("FK_Projects_CollectionsExtraField");

                entity.HasOne(d => d.CollectionsTypesWorkOrders)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CollectionsTypesWorkOrdersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Projects_CollectionsTypesWorkOrders");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK_Projects_Contract");

                entity.HasOne(d => d.WorkOrderCategoriesCollection)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.WorkOrderCategoriesCollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Projects_WorkOrderCategoriesCollections");

                entity.HasOne(d => d.WorkOrderStatuses)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.WorkOrderStatusesId)
                    .HasConstraintName("FK_Projects_WorkOrderStatuses");

                entity.HasOne(d => d.Queues)
                   .WithMany(p => p.Projects)
                   .HasForeignKey(d => d.QueuetId)
                   .HasConstraintName("FK_Projects_Queues");
            });

            modelBuilder.Entity<ProjectsCalendars>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.CalendarId });

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.ProjectsCalendars)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectsCalendars_Calendars");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectsCalendars)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectsCalendars_Projects");
            });

            modelBuilder.Entity<ProjectsContacts>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.ContactId });

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ProjectsContacts)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectsContacts_Contact");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectsContacts)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectsContacts_Project");
            });

            modelBuilder.Entity<ProjectsPermissions>(entity =>
            {
                entity.HasKey(e => new { e.PermissionId, e.ProjectId });
                entity.HasOne(d => d.Permission).WithMany(p => p.ProjectPermission).HasForeignKey(e => e.PermissionId);
                entity.HasOne(d => d.Project).WithMany(p => p.ProjectPermission).HasForeignKey(e => e.ProjectId);
            });

            modelBuilder.Entity<PurchaseRate>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ErpReference)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PushNotifications>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Creator)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PushNotificationsPeopleCollections>(entity =>
            {
                entity.HasKey(e => new { e.NotificationId, e.PeopleCollectionsId });

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.PushNotificationsPeopleCollections)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PushNotificationsPeopleCollections_Notification");

                entity.HasOne(d => d.PeopleCollections)
                    .WithMany(p => p.PushNotificationsPeopleCollections)
                    .HasForeignKey(d => d.PeopleCollectionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PushNotificationsPeopleCollections_PeopleCollection");
            });

            modelBuilder.Entity<Queues>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PermissionsQueues>(entity =>
            {
                entity.HasKey(e => new { e.PermissionId, e.QueueId });
                entity.HasOne(d => d.Permission).WithMany(p => p.PermissionQueue).HasForeignKey(e => e.PermissionId);
                entity.HasOne(d => d.Queue).WithMany(p => p.PermissionQueue).HasForeignKey(e => e.QueueId);
            });

            modelBuilder.Entity<PermissionsTasks>(entity =>
            {
                entity.HasKey(e => new { e.PermissionId, e.TaskId });
                entity.HasOne(d => d.Permission).WithMany(p => p.PermissionTask).HasForeignKey(e => e.PermissionId);
                entity.HasOne(d => d.Task).WithMany(p => p.PermissionsTasks).HasForeignKey(e => e.TaskId);
            });

            modelBuilder.Entity<SalesRate>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ErpReference)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SaltoCsversion>(entity =>
            {
                entity.HasKey(e => e.Version);

                entity.ToTable("SaltoCSVersion");

                entity.Property(e => e.Version)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Services>(entity =>
            {
                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeliveryNote)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryProcessInit).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.FormState)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Billed')");

                entity.Property(e => e.IdentifyExternal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdentifyInternal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClosingCodeFirst)
                    .WithMany(p => p.ServicesClosingCodeFirst)
                    .HasForeignKey(d => d.ClosingCodeFirstId)
                    .HasConstraintName("FK_Services_ClosingCodesFirst");

                entity.HasOne(d => d.ClosingCode)
                    .WithMany(p => p.ServicesClosingCode)
                    .HasForeignKey(d => d.ClosingCodeId)
                    .HasConstraintName("FK__Services__Closin__0D99FE17");

                entity.HasOne(d => d.ClosingCodeSecond)
                    .WithMany(p => p.ServicesClosingCodeSecond)
                    .HasForeignKey(d => d.ClosingCodeSecondId)
                    .HasConstraintName("FK_Services_ClosingCodesSecond");

                entity.HasOne(d => d.ClosingCodeThird)
                    .WithMany(p => p.ServicesClosingCodeThird)
                    .HasForeignKey(d => d.ClosingCodeThirdId)
                    .HasConstraintName("FK_Services_ClosingCodesThird");

                entity.HasOne(d => d.PeopleResponsible)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.PeopleResponsibleId)
                    .HasConstraintName("FK_Services_People");

                entity.HasOne(d => d.PredefinedService)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.PredefinedServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Services_PredefinedServices");

                entity.HasOne(d => d.ServicesCancelForm)
                    .WithMany(p => p.InverseServicesCancelForm)
                    .HasForeignKey(d => d.ServicesCancelFormId)
                    .HasConstraintName("FK__Services__Servic__0E8E2250");

                entity.HasOne(d => d.SubcontractResponsible)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.SubcontractResponsibleId)
                    .HasConstraintName("FK_Services_Subcontracts");

                entity.HasOne(d => d.WorkOrder)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.WorkOrderId)
                    .HasConstraintName("FK_Services_Workorders");
            });

            modelBuilder.Entity<ServicesAnalysis>(entity =>
            {
                entity.HasKey(e => e.ServiceCode)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.ServiceCode).ValueGeneratedNever();

                entity.Property(e => e.ClosingCodeDesc1).HasMaxLength(50);

                entity.Property(e => e.ClosingCodeDesc2).HasMaxLength(50);

                entity.Property(e => e.ClosingCodeDesc3).HasMaxLength(50);

                entity.Property(e => e.ClosingCodeDesc4).HasMaxLength(50);

                entity.Property(e => e.ClosingCodeDesc5).HasMaxLength(50);

                entity.Property(e => e.ClosingCodeDesc6).HasMaxLength(50);

                entity.Property(e => e.ClosingCodeName1).HasMaxLength(50);

                entity.Property(e => e.ClosingCodeName2).HasMaxLength(50);

                entity.Property(e => e.ClosingCodeName3).HasMaxLength(50);

                entity.Property(e => e.ClosingCodeName4).HasMaxLength(50);

                entity.Property(e => e.ClosingCodeName5).HasMaxLength(50);

                entity.Property(e => e.ClosingCodeName6).HasMaxLength(50);

                entity.Property(e => e.CreationDateTime).HasColumnType("datetime");

                entity.Property(e => e.EndingTime).HasColumnType("datetime");

                entity.Property(e => e.Observacions)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceDescription)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.SubcontractorCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SubcontractorName).HasMaxLength(50);

                entity.HasOne(d => d.WorkOrderCodeNavigation)
                    .WithMany(p => p.ServicesAnalysis)
                    .HasForeignKey(d => d.WorkOrderCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SERVICEANALYSIS_ORDRES");

                entity.HasOne(d => d.Service)
                    .WithOne(p => p.ServicesAnalysis)
                    .HasForeignKey<ServicesAnalysis>(d => d.ServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SERVICEANALYSIS_SERVICE");
            });
            
            modelBuilder.Entity<ServicesViewConfigurations>(entity =>
            {
                entity.Property(e => e.IsDefault).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserConfiguration).WithMany(p => p.ServicesViewConfigurations);
            });

            modelBuilder.Entity<ServicesViewConfigurationsColumns>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ColumnId });

                entity.Property(e => e.FilterEndDate).HasColumnType("datetime");

                entity.Property(e => e.FilterStartDate).HasColumnType("datetime");

                entity.Property(e => e.FilterValues)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.ServicesViewConfigurationsColumns)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServicesViewConfigurationsColumns_ServicesViewConfigurations");
            });

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.Property(e => e.AndroidRelease)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateLastActivity).HasColumnType("datetime");

                entity.HasOne(d => d.UserConfiguration).WithMany(p => p.Sessions);
            });

            modelBuilder.Entity<SgsClosingInfo>(entity =>
            {
                entity.Property(e => e.ParametersSent)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Response)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.SentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.WorkOrder)
                    .WithMany(p => p.SgsClosingInfo)
                    .HasForeignKey(d => d.WorkOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SgsClosin__WorkO__190BB0C3");
            });

            modelBuilder.Entity<SiteUser>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstSurname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondSurname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.SiteUser)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SiteUser__Locati__19FFD4FC");
            });

            modelBuilder.Entity<Sla>(entity =>
            {
                entity.ToTable("SLA");

                entity.Property(e => e.MinutesPenaltyWithoutResolutionOtDefined)
                    .IsRequired()
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.MinutesPenaltyWithoutResponseOtDefined)
                    .IsRequired()
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.MinutesResolutionOtDefined)
                    .IsRequired()
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.MinutesResponseOtDefined)
                    .IsRequired()
                    .HasDefaultValueSql("('false')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimePenaltyWhithoutResolutionActive)
                    .IsRequired()
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.TimePenaltyWithoutResponseActive)
                    .IsRequired()
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.TimeResolutionActive)
                    .IsRequired()
                    .HasDefaultValueSql("('true')");

                entity.Property(e => e.TimeResponseActive)
                    .IsRequired()
                    .HasDefaultValueSql("('true')");
            });

            modelBuilder.Entity<SomFiles>(entity =>
            {
                entity.Property(e => e.Container)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ContentMd5)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Directory)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StatesSla>(entity =>
            {
                entity.Property(e => e.RowColor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Sla)
                    .WithMany(p => p.StatesSla)
                    .HasForeignKey(d => d.SlaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatesSla_Sla");
            });

            modelBuilder.Entity<StopSlaReason>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubContracts>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UK_Subcontracta")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.PurchaseRate)
                    .WithMany(p => p.SubContracts)
                    .HasForeignKey(d => d.PurchaseRateId);
            });

            modelBuilder.Entity<SubFamilies>(entity =>
            {
                entity.Property(e => e.Descripcio)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.SubFamilies)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubFamilies_Families");
            });

            modelBuilder.Entity<Symptom>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SymptomFather)
                    .WithMany(p => p.InverseSymptomFather)
                    .HasForeignKey(d => d.SymptomFatherId)
                    .HasConstraintName("FK_Symptom_SymptomFather");
            });

            modelBuilder.Entity<SymptomCollections>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                
                entity.Property(e => e.Element)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SymptomCollectionSymptoms>(entity =>
            {
                entity.HasKey(e => new { e.SymptomId, e.SymptomCollectionId });

                entity.HasOne(d => d.SymptomCollection)
                    .WithMany(p => p.SymptomCollectionSymptoms)
                    .HasForeignKey(d => d.SymptomCollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SymptomCollectionSymptoms_SymptomCollections");

                entity.HasOne(d => d.Symptom)
                    .WithMany(p => p.SymptomCollectionSymptoms)
                    .HasForeignKey(d => d.SymptomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SymptomCollectionSymptoms_Symptoms");
            });

            modelBuilder.Entity<SynchronizationSessions>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SystemNotifications>(entity =>
            {
                entity.Property(e => e.Creator)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PublicationDateTime).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VisibilityEndTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.Property(e => e.AllowAdditionalSubscribers).HasDefaultValueSql("((0))");

                entity.Property(e => e.DateValue).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalCall)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Empty')");

                entity.Property(e => e.MailSubjectToPrepend)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MailSubscribers)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameFieldModel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StringValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExternalWorOrderStatus)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ExternalWorOrderStatusId)
                    .HasConstraintName("FK_Tasks_ExternalWorOrderStatuses");

                entity.HasOne(d => d.ExtraField)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ExtraFieldId)
                    .HasConstraintName("FK_Tasks_ExtraFields");

                entity.HasOne(d => d.Flow)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.FlowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_Flows");

                entity.HasOne(d => d.MailTemplate)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.MailTemplateId)
                    .HasConstraintName("FK__Tasks__MailTempl__1DD065E0");

                entity.HasOne(d => d.PeopleManipulator)
                    .WithMany(p => p.TasksPeopleManipulator)
                    .HasForeignKey(d => d.PeopleManipulatorId)
                    .HasConstraintName("FK_Tasks_PeopleManipulator");

                entity.HasOne(d => d.PeopleResponsibleTechnicians)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.PeopleResponsibleTechniciansId)
                    .HasConstraintName("FK_Tasks_PeopleResponsibleTechnicians");

                entity.HasOne(d => d.PeopleTechnician)
                    .WithMany(p => p.TasksPeopleTechnician)
                    .HasForeignKey(d => d.PeopleTechnicianId)
                    .HasConstraintName("FK_Tasks_PeopleTechnician");

                entity.HasOne(d => d.PredefinedService)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.PredefinedServiceId)
                    .HasConstraintName("FK_Tasks_PredefinedServices");

                entity.HasOne(d => d.Queue)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.QueueId)
                    .HasConstraintName("FK_Tasks_Queues");

                entity.HasOne(d => d.WorkOrderStatus)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.WorkOrderStatusId)
                    .HasConstraintName("FK_Tasks_WorkOrderStatuses");

                entity.HasOne(d => d.TasksTypes)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TasksTypesId)
                    .HasConstraintName("FK_Tasks_TaskTypes");
            });

            modelBuilder.Entity<TaskTokens>(entity =>
            {
                entity.HasKey(e => e.Token)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TaskWebServiceCallItems>(entity =>
            {
                entity.HasKey(e => new { e.TaskId, e.ItemId });

                entity.Property(e => e.Value)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskWebServiceCallItems)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskWebSe__TaskI__2759D01A");
            });

            modelBuilder.Entity<Assets>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StockNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AssetNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AssetStatus)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.AssetStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Status_AssetStatuses");

                entity.HasOne(d => d.Guarantee)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.GuaranteeId)
                    .HasConstraintName("FK_Assets_Guarantee");

                entity.HasOne(d => d.LocationClient)
                    .WithMany(p => p.AssetsLocationClient)
                    .HasForeignKey(d => d.LocationClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assets_LocationsClient");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.AssetsLocation)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Assets_Locations");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_Assets_Model");

                entity.HasOne(d => d.SubFamily)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.SubFamilyId)
                    .HasConstraintName("FK_Assets_Subfamilies");

                entity.HasOne(d => d.Usage)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.UsageId)
                    .HasConstraintName("FK__Assets__UsageId__284DF453");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Assets__UserId__2942188C");
            });
            
            modelBuilder.Entity<AssetsContracts>(entity =>
            {
                entity.HasKey(e => new { e.AssetsId, e.ContractsId});

                entity.HasOne(d => d.Contracts)
                    .WithMany(p => p.AssetContracts)
                    .HasForeignKey(d => d.ContractsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetssContracts_Contracts");

                entity.HasOne(d => d.Assets)
                    .WithMany(p => p.AssetsContracts)
                    .HasForeignKey(d => d.AssetsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetsContracts_Assets");
            });

            modelBuilder.Entity<AssetsHiredServices>(entity =>
            {
                entity.HasKey(e => new { e.AssetId, e.HiredServiceId });

                entity.HasOne(d => d.HiredService)
                    .WithMany(p => p.AssetsHiredServices)
                    .HasForeignKey(d => d.HiredServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetsHiredServices_HiredServices");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetsHiredServices)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetsHiredServices_Assets");
            });

            modelBuilder.Entity<AssetsWorkOrders>(entity =>
            {
                entity.HasKey(e => new { e.WorkOrderId, e.AssetId });

                entity.HasOne(d => d.Assets)
                    .WithMany(p => p.AssetsWorkOrders)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetsWorkOrders_Assets");

                entity.HasOne(d => d.WorkOrder)
                    .WithMany(p => p.AssetsWorkOrders)
                    .HasForeignKey(d => d.WorkOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetsWorkOrders_WorkOrders");
            });

            modelBuilder.Entity<TechnicalCodes>(entity =>
            {
                entity.HasIndex(e => new { e.ProjectId, e.WorkOrderCategoryId, e.PeopleTechnicId })
                    .HasName("PK_PeopleTechnicalCodes")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PeopleTechnic)
                    .WithMany(p => p.TechnicalCodes)
                    .HasForeignKey(d => d.PeopleTechnicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TechnicalCodes_People");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TechnicalCodes)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_TechnicalCodes_Projects");

                entity.HasOne(d => d.WorkOrderCategory)
                    .WithMany(p => p.TechnicalCodes)
                    .HasForeignKey(d => d.WorkOrderCategoryId)
                    .HasConstraintName("FK__Technical__WorkO__33BFA6FF");
            });

            modelBuilder.Entity<TechnicianListFilters>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.People)
                    .WithMany(p => p.TechnicianListFilters)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TECHNICIANLISTFILTERS_PERSONES");
            });

            modelBuilder.Entity<TenantConfiguration>(entity =>
            {
                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Group)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tools>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Tools)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Tools_Vehicles");
            });

            modelBuilder.Entity<ToolsToolTypes>(entity =>
            {
                entity.HasKey(e => new { e.ToolId, e.ToolTypeId });

                entity.HasOne(d => d.Tool)
                    .WithMany(p => p.ToolsToolTypes)
                    .HasForeignKey(d => d.ToolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToolsToolTypes_Tools");

                entity.HasOne(d => d.ToolType)
                    .WithMany(p => p.ToolsToolTypes)
                    .HasForeignKey(d => d.ToolTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToolsToolTypes_ToolsType");
            });

            modelBuilder.Entity<ToolsType>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ToolsTypeWorkOrderTypes>(entity =>
            {
                entity.HasKey(e => new { e.WorkOrderTypesId, e.ToolsTypeId });

                entity.HasOne(d => d.ToolsType)
                    .WithMany(p => p.ToolsTypeWorkOrderTypes)
                    .HasForeignKey(d => d.ToolsTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToolsTypeWorkOrderTypes_ToolsType");

                entity.HasOne(d => d.WorkOrderTypes)
                    .WithMany(p => p.ToolsTypeWorkOrderTypes)
                    .HasForeignKey(d => d.WorkOrderTypesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToolsTypeWorkOrderTypes_WorkOrderTypes");
            });

            modelBuilder.Entity<Usages>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Usages__737584F6108C583C")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsersMainWoviewConfigurations>(entity =>
            {
                entity.ToTable("UsersMainWOViewConfigurations");

                entity.Property(e => e.IsDefault).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserConfiguration).WithMany(p => p.UsersMainWoviewConfigurations);
            });

            modelBuilder.Entity<Vehicles>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.PeopleDriver)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.PeopleDriverId)
                    .HasConstraintName("FK_Vehicles_People");
            });

            modelBuilder.Entity<Warehouses>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                                
                entity.Property(e => e.ErpReference)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WarehouseMovementEndpoints>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.WarehouseMovementEndpoints)
                    .HasForeignKey(d => d.WarehouseId)                    
                    .HasConstraintName("FK_WarehouseMovementEndpoints_Warehouses");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.WarehouseMovementEndpoints)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK_WarehouseMovementEndpoints_Assets");
            });

            modelBuilder.Entity<WarehouseMovements>(entity =>
            {                
                entity.HasOne(d => d.Items)
                    .WithMany(p => p.WarehouseMovements)
                    .HasForeignKey(d => d.ItemsId)        
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_WarehouseMovements_Items");
                
                entity.HasOne(d => d.WorkOrders)
                    .WithMany(p => p.WarehouseMovements)
                    .HasForeignKey(d => d.WorkOrdersId)                    
                    .HasConstraintName("FK_WarehouseMovements_WorkOrders");                

                entity.HasOne(d => d.Services)
                    .WithMany(p => p.WarehouseMovements)
                    .HasForeignKey(d => d.ServicesId)                    
                    .HasConstraintName("FK_WarehouseMovements_Services");

                entity.HasOne(d => d.ItemsSerialNumber)
                    .WithMany(p => p.WarehouseMovements)
                    .HasForeignKey(d => new { d.ItemsId, d.SerialNumber })                    
                    .HasConstraintName("FK_WarehouseMovements_ItemsSerialNumber");

                entity.HasOne(d => d.EndpointsFrom)
                    .WithMany(p => p.WarehouseMovementsFrom)
                    .HasForeignKey(d => d.EndpointsFromId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_WarehouseMovements_WarehouseMovementEndpointsFrom");

                entity.HasOne(d => d.EndpointsTo)
                    .WithMany(p => p.WarehouseMovementsTo)
                    .HasForeignKey(d => d.EndpointsToId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_WarehouseMovements_WarehouseMovementEndpointsTo");
            });

            modelBuilder.Entity<WorkOrderAnalysis>(entity =>
            {
                entity.HasKey(e => e.WorkOrderCode)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.WorkOrderCode).ValueGeneratedNever();

                entity.Property(e => e.AccountingClosingDate).HasColumnType("date");

                entity.Property(e => e.ActuationDate).HasColumnType("datetime");

                entity.Property(e => e.ClientCreationDate).HasColumnType("datetime");

                entity.Property(e => e.ClosingClientDate).HasColumnType("datetime");

                entity.Property(e => e.ClosingClientTime).HasColumnType("datetime");

                entity.Property(e => e.ClosingSystemDate).HasColumnType("datetime");

                entity.Property(e => e.ClosingWodate)
                    .HasColumnName("ClosingWODate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExternalOtstatus).HasColumnName("ExternalOTStatus");

                entity.Property(e => e.FinalClientName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InternalCreationDate).HasColumnType("datetime");

                entity.Property(e => e.InternalSystemTimeWhenOtclosed)
                    .HasColumnName("InternalSystemTimeWhenOTClosed")
                    .HasColumnType("datetime");

                entity.Property(e => e.LocationAddress).HasMaxLength(200);

                entity.Property(e => e.LocationCity).HasMaxLength(100);

                entity.Property(e => e.LocationCountry).HasMaxLength(100);

                entity.Property(e => e.LocationName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationObservation)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.LocationPostalCode).HasMaxLength(50);

                entity.Property(e => e.LocationRegion).HasMaxLength(100);

                entity.Property(e => e.LocationState).HasMaxLength(100);

                entity.Property(e => e.LocationTown).HasMaxLength(100);

                entity.Property(e => e.LocationClientCode).HasMaxLength(100);

                entity.Property(e => e.MeetResolutionSla).HasColumnName("MeetResolutionSLA");

                entity.Property(e => e.MeetResponseSla).HasColumnName("MeetResponseSLA");

                entity.Property(e => e.Otstatus).HasColumnName("OTStatus");

                entity.Property(e => e.SlaresolutionDate)
                    .HasColumnName("SLAResolutionDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.SlaresponseDate)
                    .HasColumnName("SLAResponseDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.TotalWoexpensesCost).HasColumnName("TotalWOExpensesCost");

                entity.Property(e => e.TotalWomaterialsCost).HasColumnName("TotalWOMaterialsCost");

                entity.Property(e => e.TotalWoproductionCost).HasColumnName("TotalWOProductionCost");

                entity.Property(e => e.TotalWosalesAmount).HasColumnName("TotalWOSalesAmount");

                entity.Property(e => e.TotalWosubcontractorCost).HasColumnName("TotalWOSubcontractorCost");

                entity.Property(e => e.TotalWotravelTimeCost).HasColumnName("TotalWOTravelTimeCost");

                entity.Property(e => e.WorkOrderCampainCode).HasMaxLength(50);

                entity.Property(e => e.WorkOrderClientCode).HasMaxLength(50);

                entity.Property(e => e.SlaResolutionPenaltyDate).HasColumnType("datetime");

                entity.Property(e => e.SlaResponsePenaltyDate).HasColumnType("datetime");

                entity.HasOne(d => d.WorkOrders)
                    .WithOne(p => p.WorkOrderAnalysis)
                    .HasForeignKey<WorkOrderAnalysis>(d => d.WorkOrderCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrderAnalysis_WorkOrders");
            });

            modelBuilder.Entity<WorkOrderCategories>(entity =>
            {
                entity.Property(e => e.BackOfficeResponsible)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DefaultTechnicalCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Info)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Serie)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TechnicalResponsible)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IsGhost).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Sla)
                    .WithMany(p => p.WorkOrderCategories)
                    .HasForeignKey(d => d.SlaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrderCategories_Sla");

                entity.HasOne(d => d.WorkOrderCategoriesCollection)
                    .WithMany(p => p.WorkOrderCategories)
                    .HasForeignKey(d => d.WorkOrderCategoriesCollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrderCategories_WorkOrderCategoriesCollections");
            });

            modelBuilder.Entity<WorkOrderCategoriesCollectionCalendar>(entity =>
            {
                entity.HasKey(e => new { e.WorkOrderCategoriesCollectionId, e.CalendarId });

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.WorkOrderCategoriesCollectionCalendar)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkOrder__Calen__420DC656");

                entity.HasOne(d => d.WorkOrderCategoriesCollection)
                    .WithMany(p => p.WorkOrderCategoriesCollectionCalendar)
                    .HasForeignKey(d => d.WorkOrderCategoriesCollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkOrder__WorkO__4301EA8F");
            });

            modelBuilder.Entity<WorkOrderCategoriesCollections>(entity =>
            {
                entity.Property(e => e.Info)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WorkOrderCategoryCalendar>(entity =>
            {
                entity.HasKey(e => new { e.WorkOrderCategoryId, e.CalendarId });

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.WorkOrderCategoryCalendar)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkOrder__Calen__43F60EC8");

                entity.HasOne(d => d.WorkOrderCategory)
                    .WithMany(p => p.WorkOrderCategoryCalendar)
                    .HasForeignKey(d => d.WorkOrderCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkOrder__WorkO__44EA3301");
            });

            modelBuilder.Entity<WorkOrderCategoryKnowledge>(entity =>
            {
                entity.HasKey(e => new { e.WorkOrderCategoryId, e.KnowledgeId });

                entity.HasOne(d => d.Knowledge)
                    .WithMany(p => p.WorkOrderCategoryKnowledge)
                    .HasForeignKey(d => d.KnowledgeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkOrder__Knowl__45DE573A");

                entity.HasOne(d => d.WorkOrderCategory)
                    .WithMany(p => p.WorkOrderCategoryKnowledge)
                    .HasForeignKey(d => d.WorkOrderCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkOrder__WorkO__46D27B73");
            });

            modelBuilder.Entity<WorkOrderCategoryPermissions>(entity =>
            {
                entity.HasKey(e => new { e.PermissionId, e.WorkOrderCategoryId });
                entity.HasOne(d => d.Permission).WithMany(p => p.WorkOrderCategoryPermission).HasForeignKey(e => e.PermissionId);
                entity.HasOne(d => d.WorkOrderCategory).WithMany(p => p.WorkOrderCategoryPermission).HasForeignKey(e => e.WorkOrderCategoryId);
            });

            modelBuilder.Entity<WorkOrderCategoryRoles>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.WorkOrderCategoryId });
                entity.HasOne(d => d.WorkOrderCategory).WithMany(p => p.WorkOrderCategoryRoles).HasForeignKey(e => e.WorkOrderCategoryId);
            });

            modelBuilder.Entity<WorkOrders>(entity =>
            {
                entity.Property(e => e.AccountingClosingDate).HasColumnType("date");

                entity.Property(e => e.ActionDate).HasColumnType("datetime");

                entity.Property(e => e.ActuationEndDate).HasColumnType("datetime");

                entity.Property(e => e.AssignmentTime).HasColumnType("datetime");

                entity.Property(e => e.ClientClosingDate).HasColumnType("datetime");

                entity.Property(e => e.ClosingOtdate)
                    .HasColumnName("ClosingOTDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DatePenaltyWithoutResolutionSla).HasColumnType("datetime");

                entity.Property(e => e.DateStopTimerSla).HasColumnType("datetime");

                entity.Property(e => e.DateUnansweredPenaltySla).HasColumnType("datetime");

                entity.Property(e => e.ExternalIdentifier)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalSystemId)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FinalClientClosingTime).HasColumnType("datetime");

                entity.Property(e => e.InternalClosingTime).HasColumnType("datetime");

                entity.Property(e => e.InternalCreationDate).HasColumnType("datetime");

                entity.Property(e => e.InternalIdentifier)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MeetSlaresolution).HasColumnName("MeetSLAResolution");

                entity.Property(e => e.MeetSlaresponse).HasColumnName("MeetSLAResponse");

                entity.Property(e => e.Observations)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.PickUpTime).HasColumnType("datetime");

                entity.Property(e => e.ResolutionDateSla).HasColumnType("datetime");

                entity.Property(e => e.ResponseDateSla).HasColumnType("datetime");

                entity.Property(e => e.SystemDateWhenOtclosed)
                    .HasColumnName("SystemDateWhenOTClosed")
                    .HasColumnType("datetime");

                entity.Property(e => e.TextRepair)
                    .IsRequired()
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.Overhead).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ExternalWorOrderStatus)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.ExternalWorOrderStatusId)
                    .HasConstraintName("FK_WorkOrders_ExternalWorOrderStatuses");

                entity.HasOne(d => d.FinalClient)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.FinalClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrders_FinalClients");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrders_Locations");

                entity.HasOne(d => d.PeopleIntroducedBy)
                    .WithMany(p => p.WorkOrdersPeopleIntroducedBy)
                    .HasForeignKey(d => d.PeopleIntroducedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrders_PeopleIntroducedBy");

                entity.HasOne(d => d.PeopleManipulator)
                    .WithMany(p => p.WorkOrdersPeopleManipulator)
                    .HasForeignKey(d => d.PeopleManipulatorId)
                    .HasConstraintName("FK_WorkOrders_PeopleManipulator");

                entity.HasOne(d => d.PeopleResponsible)
                    .WithMany(p => p.WorkOrdersPeopleResponsible)
                    .HasForeignKey(d => d.PeopleResponsibleId)
                    .HasConstraintName("FK_WorkOrders_PeopleResponsible");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdreTreball_Projects");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdreTreball_Projects");

                entity.HasOne(d => d.Queue)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.QueueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrders_Queues");

                entity.HasOne(d => d.SiteUser)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.SiteUserId)
                    .HasConstraintName("FK__WorkOrder__SiteU__4B973090");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK_WorkOrders_Assets");

                entity.HasOne(d => d.WorkOrderCategory)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.WorkOrderCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrders_WorkOrderCategories");

                entity.HasOne(d => d.WorkOrderStatus)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.WorkOrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrders_WorkOrderStatuses");

                entity.HasOne(d => d.WorkOrderTypes)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.WorkOrderTypesId)
                    .HasConstraintName("FK_WorkOrders_WorkOrderTypes");

                entity.HasOne(d => d.WorkOrdersFather)
                    .WithMany(p => p.InverseWorkOrdersFather)
                    .HasForeignKey(d => d.WorkOrdersFatherId)
                    .HasConstraintName("FK_WorkOrders_WorkOrdersFather");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.GeneratorServiceId)
                    .HasConstraintName("FK_WorkOrders_Services");
            });

            modelBuilder.Entity<WorkOrdersDeritative>(entity =>
            {
                entity.Property(e => e.ActionDate).HasColumnType("datetime");

                entity.Property(e => e.AssignmentTime).HasColumnType("datetime");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ExternalIdentifier)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FinalClientClosingTime).HasColumnType("datetime");

                entity.Property(e => e.InheritProject).HasDefaultValueSql("((0))");

                entity.Property(e => e.InheritTechnician).HasDefaultValueSql("((0))");

                entity.Property(e => e.InternalClosingTime).HasColumnType("datetime");

                entity.Property(e => e.InternalIdentifier)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.PickUpTime).HasColumnType("datetime");

                entity.Property(e => e.TextRepair)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExternalWorOrderStatus)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.ExternalWorOrderStatusId)
                    .HasConstraintName("FK_WorkOrdersDeritative_ExternalWorOrderStatuses");

                entity.HasOne(d => d.FinalClient)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.FinalClientId)
                    .HasConstraintName("FK_WorkOrdersDeritative_FinalClients");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_WorkOrdersDeritative_Locations");

                entity.HasOne(d => d.PeopleIntroducedBy)
                    .WithMany(p => p.WorkOrdersDeritativePeopleIntroducedBy)
                    .HasForeignKey(d => d.PeopleIntroducedById)
                    .HasConstraintName("FK_WorkOrdersDeritative_PeopleIntroducedBy");

                entity.HasOne(d => d.PeopleManipulator)
                    .WithMany(p => p.WorkOrdersDeritativePeopleManipulator)
                    .HasForeignKey(d => d.PeopleManipulatorId)
                    .HasConstraintName("FK_WorkOrdersDeritative_PeopleManipulator");

                entity.HasOne(d => d.PeopleResponsible)
                    .WithMany(p => p.WorkOrdersDeritativePeopleResponsible)
                    .HasForeignKey(d => d.PeopleResponsibleId)
                    .HasConstraintName("FK_WorkOrdersDeritative_PeopleResponsible");

                entity.HasOne(d => d.PeopleResponsibleTechniciansCollection)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.PeopleResponsibleTechniciansCollectionId)
                    .HasConstraintName("FK_WorkOrdersDeritative_PeopleResponsibleTechniciansCollection");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_OrdreTreballDerivades_Projects");

                entity.HasOne(d => d.Queue)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.QueueId)
                    .HasConstraintName("FK_WorkOrdersDeritative_Queues");

                entity.HasOne(d => d.SiteUser)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.SiteUserId)
                    .HasConstraintName("FK__WorkOrder__SiteU__59E54FE7");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrdersDeritative_Tasks");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK_WorkOrdersDeritative_Assets");

                entity.HasOne(d => d.WorkOrderCategory)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.WorkOrderCategoryId)
                    .HasConstraintName("FK_WorkOrdersDeritative_WorkOrderCategories");

                entity.HasOne(d => d.WorkOrderStatus)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.WorkOrderStatusId)
                    .HasConstraintName("FK_WorkOrdersDeritative_WorkOrderStatuses");

                entity.HasOne(d => d.WorkOrderType)
                    .WithMany(p => p.WorkOrdersDeritative)
                    .HasForeignKey(d => d.WorkOrderTypeId)
                    .HasConstraintName("FK_WorkOrdersDeritative_WorkOrderTypes");
            });

            modelBuilder.Entity<WorkOrderStatuses>(entity =>
            {
                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IdIcg)
                    .HasColumnName("IdICG")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsPlannable).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<WorkOrderTypes>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observations)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Serie)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CollectionsTypesWorkOrders)
                    .WithMany(p => p.WorkOrderTypes)
                    .HasForeignKey(d => d.CollectionsTypesWorkOrdersId)
                    .HasConstraintName("FK_WorkOrderTypes_CollectionsTypesWorkOrders");

                entity.HasOne(d => d.Sla)
                    .WithMany(p => p.WorkOrderTypes)
                    .HasForeignKey(d => d.SlaId)
                    .HasConstraintName("FK_WorkOrderTypes_Sla");

                entity.HasOne(d => d.WorkOrderTypesFather)
                    .WithMany(p => p.InverseWorkOrderTypesFather)
                    .HasForeignKey(d => d.WorkOrderTypesFatherId)
                    .HasConstraintName("FK_WorkOrderTypes_WorkOrderTypesFather");
            });

            modelBuilder.Entity<WsErpSystemInstance>(entity =>
            {
                entity.HasKey(e => e.ErpSystemInstanceId);

                entity.Property(e => e.ErpSystemInstanceId).ValueGeneratedNever();

                entity.Property(e => e.WebServiceIpAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WebServiceMethod)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WebServicePwd)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WebServiceUser)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ErpSystemInstance)
                    .WithOne(p => p.WsErpSystemInstance)
                    .HasForeignKey<WsErpSystemInstance>(d => d.ErpSystemInstanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WsErpSystemInstance_ErpSystemInstance");
            });

            modelBuilder.Entity<ZoneProject>(entity =>
            {
                entity.HasIndex(e => new { e.ZoneId, e.ProjectId })
                    .HasName("UQ_Zone_Project")
                    .IsUnique();

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ZoneProject)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__ZoneProje__Proje__6C040022");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.ZoneProject)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ZoneProje__ZoneI__6CF8245B");
            });

            modelBuilder.Entity<ZoneProjectPostalCode>(entity =>
            {
                entity.HasIndex(e => new { e.ZoneProjectId, e.PostalCode })
                    .HasName("UQ_ZoneProject_PostalCode")
                    .IsUnique();

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ZoneProject)
                    .WithMany(p => p.ZoneProjectPostalCode)
                    .HasForeignKey(d => d.ZoneProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ZoneProje__ZoneP__6DEC4894");
            });

            modelBuilder.Entity<Zones>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PeopleProjects>(entity =>
            {
                entity.HasKey(e => new { e.PeopleId, e.ProjectId });

                entity.HasOne(d => d.Projects)
                    .WithMany(p => p.PeopleProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeopleProjects_Projects");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.PeopleProjects)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeopleProjects_People");
            });

            modelBuilder.Entity<DnAndMaterialsAnalysis>(entity =>
            {
                entity.Property(e => e.ExternalSystemNumber)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName).HasMaxLength(100);

                entity.Property(e => e.ItemSerialNumber)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PVP).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalDeliveryNoteCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalDeliveryNoteSalePrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.BillLine)
                    .WithOne(p => p.DnAndMaterialsAnalysis)
                    .HasForeignKey<DnAndMaterialsAnalysis>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DnAndMaterialsAnalysis_BillLine");

                entity.HasOne(d => d.PeopleEntity)
                    .WithMany(b => b.DnAndMaterialsAnalysis)
                    .HasForeignKey(d => d.People)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DnAndMaterialsAnalysis_People");

                entity.HasOne(d => d.BillEntity)
                    .WithMany(b => b.DnAndMaterialsAnalysis)
                    .HasForeignKey(d => d.Bill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DnAndMaterialsAnalysis_Bill");

                entity.HasOne(d => d.Item)
                    .WithMany(b => b.DnAndMaterialsAnalysis)
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DnAndMaterialsAnalysis_Item");

                entity.HasOne(d => d.WorkOrderEntity)
                    .WithMany(b => b.DnAndMaterialsAnalysis)
                    .HasForeignKey(d => d.WorkOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DnAndMaterialsAnalysis_WorkOrder");
            });

            modelBuilder.Entity<PeopleNotification>(entity =>
            {
                entity.HasOne(d => d.People)
                    .WithMany(b => b.PeopleNotifications)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeopleNotification_People");
            });

            modelBuilder.Entity<PeoplePushRegistration>(entity =>
            {
                entity.HasOne(d => d.People)
                    .WithMany(b => b.PeoplePushRegistrations)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeoplePushRegistration_People");
            });

            modelBuilder.Entity<ExpensesTicketsFiles>(entity =>
            {
                entity.HasKey(e => new { e.ExpenseTicketId, e.Id });

                entity.HasOne(d => d.ExpenseTicket)
                    .WithMany(p => p.ExpensesTicketsFiles)
                    .HasForeignKey(d => d.ExpenseTicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpensesTicketsFiles_ExpenseTicket");
            });

            modelBuilder.Entity<PushNotificationsPeople>(entity =>
            {
                entity.HasKey(e => new { e.NotificationId, e.PeopleId });

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PushNotification)
                    .WithMany(p => p.PushNotificationsPeople)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PushNotificationsPeople_PushNotification");

                entity.HasOne(d => d.People)
                    .WithMany(p => p.PushNotificationsPeople)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PushNotificationsPeople_People");
            });

            modelBuilder.Entity<PeopleNotificationTranslation>(entity => 
            {
                entity.HasOne(d => d.PeopleNotification).WithMany(p => p.PeopleNotificationTranslations);
            });

            modelBuilder.Entity<NotificationTemplateTranslation>(entity =>
            {
                entity.HasOne(d => d.NotificationTemplate).WithMany(p => p.NotificationTemplateTranslations);
            });

            modelBuilder.Entity<WorkOrderStatusesTranslations>(entity =>
            {
                entity.HasOne(d => d.WorkOrderStatuses).WithMany(p => p.WorkOrderStatusesTranslations);
            });

            modelBuilder.Entity<ExternalWorkOrderStatusesTranslations>(entity =>
            {
                entity.HasOne(d => d.ExternalWorkOrderStatuses).WithMany(p => p.ExternalWorkOrderStatusesTranslations);
            });

            modelBuilder.Entity<QueuesTranslations>(entity =>
            {
                entity.HasOne(d => d.Queues).WithMany(p => p.QueuesTranslations);
            });
            
            modelBuilder.Entity<ExtraFieldsTranslations>(entity =>
            {
                entity.HasOne(d => d.ExtraFields).WithMany(p => p.ExtraFieldsTranslations);
            });

            modelBuilder.Entity<RepetitionParameters>(entity =>
            {
                entity.Property(e => e.Days).HasDefaultValue(30);
            });

            modelBuilder.Entity<RolesTenant>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<UserConfigurationRolesTenant>(entity =>
            {
                entity.HasKey(e => new { e.UserConfigurationId, e.RolesTenantId });

                entity.HasOne(uc => uc.UserConfiguration)
                    .WithMany(ur => ur.UserConfigurationRolesTenant)
                    .HasForeignKey(uc => uc.UserConfigurationId);

                entity.HasOne(rt => rt.RolesTenant)
                    .WithMany(ur => ur.UserConfigurationRolesTenant)
                    .HasForeignKey(rt => rt.RolesTenantId);
            });

            modelBuilder.Entity<RolesActionGroupsActionsTenant>(entity =>
            {
                entity.HasKey(x => new { x.RoleTenantId, x.ActionGroupId, x.ActionId });

                entity.HasOne(rt => rt.RolesTenant)
                    .WithMany(ur => ur.RolesActionGroupsActionsTenant)
                    .HasForeignKey(rt => rt.RoleTenantId);
            });

            modelBuilder.Entity<TasksTranslations>(entity =>
            {
                entity.HasOne(d => d.Tasks).WithMany(p => p.TasksTranslations);
            });

            modelBuilder.Entity<FlowsProjects>(entity =>
            {
                entity.HasKey(e => new { e.FlowId, e.ProjectId });

                entity.HasOne(p => p.Flows)
                    .WithMany(fp => fp.FlowsProjects)
                    .HasForeignKey(d => d.FlowId);

                entity.HasOne(p => p.Projects)
                    .WithMany(fp => fp.FlowsProjects)
                    .HasForeignKey(d => d.ProjectId);
            });

            modelBuilder.Entity<FlowsWOCategories>(entity =>
            {
                entity.HasKey(e => new { e.FlowId, e.WOCategoryId });

                entity.HasOne(f => f.Flows)
                    .WithMany(woc => woc.FlowsWOCategories)
                    .HasForeignKey(d => d.FlowId);

                entity.HasOne(f => f.WorkOrderCategories)
                    .WithMany(woc => woc.FlowsWOCategories)
                    .HasForeignKey(d => d.WOCategoryId);
            });

            modelBuilder.Entity<TasksTypes>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(250);
            });

            modelBuilder.Entity<ItemsSerialNumberAttributeValues>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.SerialNumber, e.ExtraFieldsId});

                entity.HasOne(d => d.ItemsSerialNumber)
                    .WithMany(p => p.ItemsSerialNumberAttributeValues)
                    .HasConstraintName("FK_ItemsSerialNumberAttributeValues_ItemsSerialNumber")
                    .HasForeignKey(d => new { d.ItemId, d.SerialNumber })
                    .IsRequired();

                entity.HasOne(d => d.ExtraFields)
                    .WithMany(p => p.ItemsSerialNumberAttributeValues)
                    .HasForeignKey(d => d.ExtraFieldsId)
                    .HasConstraintName("FK_ItemsSerialNumberAttributeValues_ExtraFields")
                    .IsRequired();

                entity.Property(e => e.StringValue)
                    .HasMaxLength(100)
                    .IsRequired(false)
                    .IsUnicode(false);
                
                entity.Property(e => e.DecimalValue)
                    .HasColumnType("decimal(18, 2)");

            });

            modelBuilder.Entity<NotificationTemplate>(entity =>
            {
                entity.HasOne(d => d.NotificationTemplateType)
                    .WithMany(p => p.NotificationTemplates)
                    .HasForeignKey(d => d.PeopleNotificationTemplateTypeId)
                    .HasConstraintName("FK_PeopleNotificationTemplate_TemplateType");

            });
        }
    }
}
