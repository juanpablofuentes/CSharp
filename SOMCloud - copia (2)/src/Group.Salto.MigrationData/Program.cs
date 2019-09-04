using Dapper;
using Group.Salto.DataAccess.Context;
using Group.Salto.DataAccess.Tenant.Context;
using Group.Salto.Entities;
using Group.Salto.Entities.Tenant;
using Group.Salto.MigrationData.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Group.Salto.MigrationData
{
    public class Program
    {
        private static IConfigurationRoot Configuration;
        private static SOMContext ContextInfrastructure;
        private static SOMTenantContext ContextTenant;
        private static SqlConnection SqlConnectionOldDatabase;
        private static SqlConnection SqlConnectionTenantDatabase;
        private static MigrationInfrastructure migrationInfrastructure;
        private static MigrationTenantWithoutRelations migrationTenantWithoutRelations;
        private static MigrationTenantWithRelationsInfrastructure migrationTenantWithRelationsInfrastructure;
        private static MigrationTenant migrationTenant;
        private static UpdateData updateData;

        public static void Main(string[] args)
        {
            ConfigurationDatabases configurationDatabases = new ConfigurationDatabases();
            Configuration = configurationDatabases.ConfigureSettings();
            string databaseType = Configuration.GetSection("Database").Value;
            string nameTenant = Configuration.GetSection("DataTenant").GetValue<string>("NameTenant");
            
            ContextInfrastructure = configurationDatabases.CreateEFContextInfrastructure(Configuration);
            ContextTenant = configurationDatabases.CreateEFContextTenant(Configuration);
            SqlConnectionTenantDatabase = configurationDatabases.CreateTenantSqlConnection(Configuration);
            SqlConnectionOldDatabase = configurationDatabases.CreateOldSqlConnection(Configuration);

            if (!databaseType.ToLower().Contains("update"))
            {                                
                migrationInfrastructure = new MigrationInfrastructure(ContextInfrastructure, SqlConnectionOldDatabase);
                migrationTenantWithoutRelations = new MigrationTenantWithoutRelations(SqlConnectionOldDatabase, SqlConnectionTenantDatabase, ContextInfrastructure, ContextTenant);
                migrationTenant = new MigrationTenant(SqlConnectionOldDatabase, SqlConnectionTenantDatabase, ContextInfrastructure, ContextTenant);
                migrationTenantWithRelationsInfrastructure = new MigrationTenantWithRelationsInfrastructure(SqlConnectionOldDatabase, SqlConnectionTenantDatabase, ContextInfrastructure, ContextTenant);
            }

            Type[] asmTypes = GetTypeEntitiesFromAssembly();

            if (databaseType.ToLower().Contains("update"))
            {
                updateData = new UpdateData(ContextTenant, SqlConnectionOldDatabase, SqlConnectionTenantDatabase);
                updateData.UpdateDataMigration(databaseType, asmTypes, Configuration.GetSection("CsvPath").Value);
            }
            else
            {
                if (databaseType.ToLower() == "masters") InsertCustomer();
                else if (databaseType.ToLower() == "tenants") migrationTenantWithRelationsInfrastructure.InsertUserConfig(Configuration);
                
                var valuesList = ReadQueriesFromCsv();
                string valkey = string.Empty;

                foreach (KeyValuePair<string, string> val in valuesList)
                {
                    valkey = val.Key;

                    Type type = asmTypes.FirstOrDefault(t => t.Name.Equals(valkey, StringComparison.InvariantCultureIgnoreCase));
                    if (type != null)
                    {
                        string[] espcialK = new string[5] { "Services", "WorkOrders", "Audits", "ExtraFieldsValues", "ExternalSystemImportData" };

                        if (valkey == "PeopleProjects")
                        {
                            migrationTenant.MapPeopleProjects(val);
                        }
                        else if (valkey == "RepetitionParameters")
                        {
                            migrationTenantWithRelationsInfrastructure.MapRepetitionParameters(val);
                        }
                        else if (valkey == "Sla")
                        {
                            migrationTenantWithRelationsInfrastructure.MapSla(val);
                        }
                        else if (valkey == "TenantConfiguration")
                        {
                            migrationTenantWithoutRelations.MapTenantConfiguration(val);
                        }
                        else if (valkey == "Language")
                        {
                            migrationInfrastructure.MapLanguage(val);
                        }
                        else if (valkey == "Module")
                        {
                            migrationInfrastructure.MapModules(val);
                        }
                        else if (valkey == "ReferenceTimeSla")
                        {
                            migrationInfrastructure.MapReferenceTimeSla(val);
                        }
                        else if (valkey == "ExpenseTicketStatus")
                        {
                            migrationInfrastructure.MapExpenseTicketStatus(val);
                        }
                        else if (valkey == "ExtraFieldTypes")
                        {
                            migrationInfrastructure.MapExtraFieldTypes(val);
                        }
                        else if (valkey == "CalculationType")
                        {
                            migrationInfrastructure.MapCalculationType(val);
                        }
                        else if (valkey == "DamagedEquipment")
                        {
                            migrationInfrastructure.MapDamagedEquipment(val);
                        }
                        else if (valkey == "DaysType")
                        {
                            migrationInfrastructure.MapDaysType(val);
                        }
                        else if (valkey == "WorkOrderColumns")
                        {
                            migrationInfrastructure.MapWorkOrderColumns(val);
                        }
                        else if (valkey == "WorkOrderDefaultColumns")
                        {
                            migrationInfrastructure.MapWorkOrderDefaultColumns(val);
                        }
                        else if (!espcialK.Contains(valkey))
                        {
                            List<object> dataList = GetDataFromOldDatabase(type, val.Value).ToList();
                            if (dataList.Any())
                            {
                                if (Configuration.GetSection("Database").Value == "Masters") MigrateInfrastructure(dataList, type, val, nameTenant);
                                else MigrateTenant(dataList, type, val);
                            }
                            dataList.Clear();
                        }
                        else
                        {
                            int skip = Configuration.GetSection("Pagination").GetValue<int>("Skip");
                            int taken = 0;

                            if (valkey == "Services") { taken = Configuration.GetSection("Pagination").GetValue<int>("ServicePagination"); }
                            else if (valkey == "WorkOrders") { taken = Configuration.GetSection("Pagination").GetValue<int>("WorkOrdersPagination"); }
                            else if (valkey == "ExtraFieldsValues") { taken = Configuration.GetSection("Pagination").GetValue<int>("ExtraFieldValuesPagination"); }
                            else if (valkey == "Audits") { taken = Configuration.GetSection("Pagination").GetValue<int>("AuditPagination"); }
                            else if (valkey == "ExternalSystemImportData") { taken = Configuration.GetSection("Pagination").GetValue<int>("ExternalSystemImportDataPagination"); }

                            List<object> dataList = GetDataFromOldDatabase(type, val.Value, skip, taken).ToList();

                            while (dataList.Any())
                            {
                                MigrateTenant(dataList, type, val, skip);

                                skip += taken;
                                dataList = GetDataFromOldDatabase(type, val.Value, skip, taken).ToList();
                            }
                        }
                    }
                    else Console.WriteLine($"La tabla {valkey} no existe en las entidades.");
                }

                if (databaseType.ToLower() == "masters") migrationInfrastructure.MapUserRoles(nameTenant);
            }           

            Console.WriteLine("--------------- Fin ------------------");
            Console.ReadLine();
        }

        private static void MigrateInfrastructure(IEnumerable<object> dataList, Type type, KeyValuePair<string, string> value, string nameCustomer)
        {
            switch (type.Name)
            {
                case "CalendarEventCategories":
                    migrationInfrastructure.MapCalendarEventCategories(dataList, value);
                    break;
                case "ContractType":
                    migrationInfrastructure.MapContractType(dataList, value);
                    break;
                case "AvailabilityCategories":
                    migrationInfrastructure.MapAvailabilityCategories(dataList, value);
                    break;
                case "PredefinedDayStates":
                    migrationInfrastructure.MapPredefinedDaysStates(dataList, value);
                    break;
                case "PredefinedReasons":
                    migrationInfrastructure.MapPredefinedReasons(dataList, value);
                    break;
                case "Origins":
                    migrationInfrastructure.MapOrigins(dataList, value);
                    break;
                case "Users":
                    migrationInfrastructure.MapUsers(dataList, value, nameCustomer);
                    break;
                case "Roles":
                    migrationInfrastructure.MapRoles(dataList, value);
                    break;
                case "ActionsRoles":
                    migrationInfrastructure.MapActionsRoles(dataList, value);
                    break;
                case "Actions":
                    migrationInfrastructure.MapActions(dataList, value);
                    break;
                case "Cities":
                    migrationInfrastructure.MapCities(dataList, value);
                    break;
                case "CitiesOtherNames":
                    migrationInfrastructure.MapCitiesOtherNames(dataList, value);
                    break;
                case "Countries":
                    migrationInfrastructure.MapCountries(dataList, value);
                    break;
                case "Municipalities":
                    migrationInfrastructure.MapMunicipalities(dataList, value);
                    break;
                case "PostalCodes":
                    migrationInfrastructure.MapPostalCodes(dataList, value);
                    break;
                case "Regions":
                    migrationInfrastructure.MapRegions(dataList, value);
                    break;
                case "States":
                    migrationInfrastructure.MapStates(dataList, value);
                    break;
                case "ServiceStates":
                    migrationInfrastructure.MapServiceStates(dataList, value);
                    break;
                case "RepetitionTypes":
                    migrationInfrastructure.MapRepetitionTypes(dataList, value);
                    break;
                case "StatesOtherNames":
                    migrationInfrastructure.MapStatesOtherNames(dataList, value);
                    break;
            }
        }

        private static void MigrateTenant(IEnumerable<object> dataList, Type type, KeyValuePair<string, string> val)
        {
            switch (type.Name)
            {
                case "Permissions":
                    migrationTenantWithoutRelations.MapPermissions(dataList, val);
                    break;
                case "People":
                    migrationTenantWithRelationsInfrastructure.MapPeople(dataList, val);
                    break;
                case "Projects":
                    migrationTenant.MapProjects(dataList, val);
                    break;
                case "Contracts":
                    migrationTenantWithRelationsInfrastructure.MapContracts(dataList, val);
                    break;
                case "SalesRate":
                    migrationTenantWithoutRelations.MapSalesRate(dataList, val);
                    break;
                case "Companies":
                    migrationTenantWithoutRelations.MapCompanies(dataList, val);
                    break;
                case "CollectionsExtraField":
                    migrationTenantWithoutRelations.MapCollectionsExtraField(dataList, val);
                    break;
                case "AssetStatuses":
                    migrationTenantWithoutRelations.MapAssetStatuses(dataList, val);
                    break;
                case "Calendars":
                    migrationTenantWithoutRelations.MapCalendars(dataList, val);
                    break;
                case "CollectionsClosureCodes":
                    migrationTenantWithoutRelations.MapCollectionsClosureCodes(dataList, val);
                    break;
                case "CollectionsTypesWorkOrders":
                    migrationTenantWithoutRelations.MapCollectionsTypesWorkOrders(dataList, val);
                    break;
                case "Knowledge":
                    migrationTenantWithoutRelations.MapKnowledge(dataList, val);
                    break;
                case "Contacts":
                    migrationTenantWithoutRelations.MapContacts(dataList, val);
                    break;
                case "Queues":
                    migrationTenantWithoutRelations.MapQueues(dataList, val);
                    break;
                case "ErpSystemInstance":
                    migrationTenantWithoutRelations.MapErpSystemInstance(dataList, val);
                    break;
                case "WorkOrderStatuses":
                    migrationTenantWithoutRelations.MapWorkOrderStatuses(dataList, val);
                    break;
                case "ExternalWorOrderStatuses":
                    migrationTenantWithoutRelations.MapExternalWorOrderStatuses(dataList, val);
                    break;
                case "ExpenseTypes":
                    migrationTenantWithoutRelations.MapExpenseTypes(dataList, val);
                    break;
                case "Families":
                    migrationTenantWithoutRelations.MapFamilies(dataList, val);
                    break;
                case "Flows":
                    migrationTenantWithoutRelations.MapFlows(dataList, val);
                    break;
                case "FormConfigs":
                    migrationTenantWithoutRelations.MapFormConfigs(dataList, val);
                    break;
                case "Guarantee":
                    migrationTenantWithoutRelations.MapGuarantee(dataList, val);
                    break;
                case "Items":
                    migrationTenantWithoutRelations.MapItems(dataList, val);
                    break;
                case "MailTemplate":
                    migrationTenantWithoutRelations.MapMailTemplate(dataList, val);
                    break;
                case "MainOtstatics":
                    migrationTenantWithoutRelations.MapMainOtstatics(dataList, val);
                    break;
                case "MainWoregistry":
                    migrationTenantWithoutRelations.MapMainWoregistry(dataList, val);
                    break;
                case "Brands":
                    migrationTenantWithoutRelations.MapBrands(dataList, val);
                    break;
                case "OptimizationFunctionWeights":
                    migrationTenantWithoutRelations.MapOptimizationFunctionWeights(dataList, val);
                    break;
                case "PaymentMethods":
                    migrationTenantWithoutRelations.MapPaymentMethods(dataList, val);
                    break;
                case "PeopleCollections":
                    migrationTenantWithoutRelations.MapPeopleCollections(dataList, val);
                    break;
                case "PlanificationProcessCalendarChangeTracker":
                    migrationTenantWithoutRelations.MapPlanificationProcessCalendarChangeTracker(dataList, val);
                    break;
                case "PlanificationProcessWorkOrderChangeTracker":
                    migrationTenantWithoutRelations.MapPlanificationProcessWorkOrderChangeTracker(dataList, val);
                    break;
                case "PointsRate":
                    migrationTenantWithoutRelations.MapPointsRate(dataList, val);
                    break;
                case "PurchaseRate":
                    migrationTenantWithoutRelations.MapPurchaseRate(dataList, val);
                    break;
                case "PushNotifications":
                    migrationTenantWithoutRelations.MapPushNotifications(dataList, val);
                    break;
                case "HiredServices":
                    migrationTenantWithoutRelations.MapHiredServices(dataList, val);
                    break;
                case "SomFiles":
                    migrationTenantWithoutRelations.MapSomFiles(dataList, val);
                    break;
                case "StopSlaReason":
                    migrationTenantWithoutRelations.MapStopSlaReason(dataList, val);
                    break;
                case "SystemNotifications":
                    migrationTenantWithoutRelations.MapSystemNotifications(dataList, val);
                    break;
                case "TaskTokens":
                    migrationTenantWithoutRelations.MapTaskTokens(dataList, val);
                    break;
                case "ToolsType":
                    migrationTenantWithoutRelations.MapToolsType(dataList, val);
                    break;
                case "Usages":
                    migrationTenantWithoutRelations.MapUsages(dataList, val);
                    break;
                case "WorkOrderCategoriesCollections":
                    migrationTenantWithoutRelations.MapWorkOrderCategoriesCollections(dataList, val);
                    break;
                case "Zones":
                    migrationTenantWithoutRelations.MapZones(dataList, val);
                    break;
                case "CalendarEvents":
                    migrationTenantWithRelationsInfrastructure.MapCalendarEvents(dataList, val);
                    break;
                case "CalendarPlanningViewConfiguration":
                    migrationTenantWithRelationsInfrastructure.MapCalendarPlanningViewConfiguration(dataList, val);
                    break;
                case "ExtraFields":
                    migrationTenant.MapExtraFields(dataList, val);
                    break;
                case "CollectionsExtraFieldExtraField":
                    migrationTenant.MapCollectionsExtraFieldExtraField(dataList, val);
                    break;
                case "CompaniesCostHistorical":
                    migrationTenant.MapCompaniesCostHistorical(dataList, val);
                    break;
                case "KnowledgeSubContracts":
                    migrationTenant.MapKnowledgeSubContracts(dataList, val);
                    break;
                case "KnowledgeToolsType":
                    migrationTenant.MapKnowledgeToolsType(dataList, val);
                    break;
                case "KnowledgePeople":
                    migrationTenant.MapKnowledgePeople(dataList, val);
                    break;
                case "KnowledgeWorkOrderTypes":
                    migrationTenant.MapKnowledgeWorkOrderTypes(dataList, val);
                    break;
                case "ClosingCodes":
                    migrationTenant.MapClosingCodes(dataList, val);
                    break;
                case "SubContracts":
                    migrationTenant.MapSubContracts(dataList, val);
                    break;
                case "Departments":
                    migrationTenant.MapDepartments(dataList, val);
                    break;
                case "ErpSystemInstanceQuery":
                    migrationTenant.MapErpSystemInstanceQuery(dataList, val);
                    break;
                case "StatesSla":
                    migrationTenant.MapStatesSla(dataList, val);
                    break;
                case "FormElements":
                    migrationTenant.MapFormElements(dataList, val);
                    break;
                case "ItemsPointsRate":
                    migrationTenant.MapItemsPointsRate(dataList, val);
                    break;
                case "ItemsPurchaseRate":
                    migrationTenant.MapItemsPurchaseRate(dataList, val);
                    break;
                case "ItemsSalesRate":
                    migrationTenant.MapItemsSalesRate(dataList, val);
                    break;
                case "ItemsSerialNumber":
                    migrationTenant.MapItemsSerialNumber(dataList, val);
                    break;
                case "UsersMainWoviewConfigurations":
                    migrationTenantWithRelationsInfrastructure.MapUsersMainWoviewConfigurations(dataList, val);
                    break;
                case "MainWoViewConfigurationsColumns":
                    migrationTenant.MapMainWoViewConfigurationsColumns(dataList, val);
                    break;
                case "MainWoviewConfigurationsGroups":
                    migrationTenant.MapMainWoviewConfigurationsGroups(dataList, val);
                    break;
                case "Models":
                    migrationTenant.MapModels(dataList, val);
                    break;
                case "PeopleCollectionCalendars":
                    migrationTenant.MapPeopleCollectionCalendars(dataList, val);
                    break;
                case "PlanificationProcesses":
                    migrationTenant.MapPlanificationProcesses(dataList, val);
                    break;
                case "PushNotificationsPeopleCollections:":
                    migrationTenant.MapPushNotificationsPeopleCollections(dataList, val);
                    break;
                case "ServicesViewConfigurations":
                    migrationTenantWithRelationsInfrastructure.MapServicesViewConfigurations(dataList, val);
                    break;
                case "ServicesViewConfigurationsColumns":
                    migrationTenant.MapServicesViewConfigurationsColumns(dataList, val);
                    break;
                case "SubFamilies":
                    migrationTenant.MapSubFamilies(dataList, val);
                    break;
                case "WorkOrderTypes":
                    migrationTenant.MapWorkOrderTypes(dataList, val);
                    break;
                case "WorkOrderCategories":
                    migrationTenant.MapWorkOrderCategories(dataList, val);
                    break;
                case "WorkOrderCategoriesCollectionCalendar":
                    migrationTenant.MapWorkOrderCategoriesCollectionCalendar(dataList, val);
                    break;
                case "WorkOrderCategoryCalendar":
                    migrationTenant.MapWorkOrderCategoryCalendar(dataList, val);
                    break;
                case "WorkOrderCategoryKnowledge":
                    migrationTenant.MapWorkOrderCategoryKnowledge(dataList, val);
                    break;
                case "ZoneProject":
                    migrationTenant.MapZoneProject(dataList, val);
                    break;
                case "ZoneProjectPostalCode":
                    migrationTenant.MapZoneProjectPostalCode(dataList, val);
                    break;
                case "WsErpSystemInstance":
                    migrationTenant.MapWsErpSystemInstance(dataList, val);
                    break;
                case "PredefinedServices":
                    migrationTenant.MapPredefinedServices(dataList, val);
                    break;
                case "Tasks":
                    migrationTenant.MapTasks(dataList, val);
                    break;
                case "Locations":
                    migrationTenantWithRelationsInfrastructure.MapLocations(dataList, val);
                    break;
                case "SiteUser":
                    migrationTenant.MapSiteUser(dataList, val);
                    break;
                case "FinalClients":
                    migrationTenantWithRelationsInfrastructure.MapFinalClients(dataList, val);
                    break;
                case "Assets":
                    migrationTenant.MapAssets(dataList, val);
                    break;
                case "JourneysStates":
                    migrationTenantWithRelationsInfrastructure.MapJourneysStates(dataList, val);
                    break;
                case "AssetsAudit":
                    migrationTenant.MapAssetsAudit(dataList, val);
                    break;
                case "AssetsAuditChanges":
                    migrationTenant.MapAssetsAuditChanges(dataList, val);
                    break;
                case "Bill":
                    migrationTenant.MapBill(dataList, val);
                    break;
                case "BillingItems":
                    migrationTenant.MapBillingItems(dataList, val);
                    break;
                case "BillingLineItems":
                    migrationTenant.MapBillingLineItems(dataList, val);
                    break;
                case "BillLine":
                    migrationTenant.MapBillLine(dataList, val);
                    break;
                case "PostconditionCollections":
                    migrationTenant.MapPostconditionCollections(dataList, val);
                    break;
                case "Postconditions":
                    migrationTenant.MapPostconditions(dataList, val);
                    break;
                case "Journeys":
                    migrationTenant.MapJourneys(dataList, val);
                    break;
                case "Vehicles":
                    migrationTenant.MapVehicles(dataList, val);
                    break;
                case "Tools":
                    migrationTenant.MapTools(dataList, val);
                    break;
                case "ExpensesTickets":
                    migrationTenantWithRelationsInfrastructure.MapExpensesTickets(dataList, val);
                    break;
                case "Expenses":
                    migrationTenant.MapExpenses(dataList, val);
                    break;
                case "ExpensesTicketFile":
                    migrationTenant.MapExpensesTicketFile(dataList, val);
                    break;
                case "ExternalServicesConfiguration":
                    migrationTenant.MapExternalServicesConfiguration(dataList, val);
                    break;
                case "ExternalServicesConfigurationProjectCategories":
                    migrationTenant.MapExternalServicesConfigurationProjectCategories(dataList, val);
                    break;
                case "ExternalServicesConfigurationProjectCategoriesProperties":
                    migrationTenant.MapExternalServicesConfigurationProjectCategoriesProperties(dataList, val);
                    break;
                case "ExternalServicesConfigurationSites":
                    migrationTenant.MapExternalServicesConfigurationSites(dataList, val);
                    break;
                case "BillingRule":
                    migrationTenant.MapBillingRule(dataList, val);
                    break;
                case "BillingRuleItem":
                    migrationTenant.MapBillingRuleItem(dataList, val);
                    break;
                case "Preconditions":
                    migrationTenant.MapPreconditions(dataList, val);
                    break;
                case "LiteralsPreconditions":
                    migrationTenant.MapLiteralsPreconditions(dataList, val);
                    break;
                case "PeopleCost":
                    migrationTenant.MapPeopleCost(dataList, val);
                    break;
                case "PeopleCostHistorical":
                    migrationTenant.MapPeopleCostHistorical(dataList, val);
                    break;
                case "PlanningPanelViewConfiguration":
                    migrationTenant.MapPlanningPanelViewConfiguration(dataList, val);
                    break;
                case "SaltoCsversion":
                    migrationTenant.MapSaltoCsversion(dataList, val);
                    break;
                case "Sessions":
                    migrationTenantWithRelationsInfrastructure.MapSessions(dataList, val);
                    break;
                case "SynchronizationSessions":
                    migrationTenant.MapSynchronizationSessions(dataList, val);
                    break;
                case "SgsClosingInfo":
                    migrationTenant.MapSgsClosingInfo(dataList, val);
                    break;
                case "TechnicalCodes":
                    migrationTenant.MapTechnicalCodes(dataList, val);
                    break;
                case "PreconditionsLiteralValues":
                    migrationTenant.MapPreconditionsLiteralValues(dataList, val);
                    break;
                case "CalendarPlanningViewConfigurationPeople":
                    migrationTenant.MapCalendarPlanningViewConfigurationPeople(dataList, val);
                    break;
                case "CalendarPlanningViewConfigurationPeopleCollection":
                    migrationTenant.MapCalendarPlanningViewConfigurationPeopleCollection(dataList, val);
                    break;
                case "FinalClientSiteCalendar":
                    migrationTenant.MapFinalClientSiteCalendar(dataList, val);
                    break;
                case "ContactsFinalClients":
                    migrationTenant.MapContactsFinalClients(dataList, val);
                    break;
                case "ContactsLocationsFinalClients":
                    migrationTenant.MapContactsLocationsFinalClients(dataList, val);
                    break;
                case "ContractContacts":
                    migrationTenant.MapContractContacts(dataList, val);
                    break;
                case "ToolsToolTypes":
                    migrationTenant.MapToolsToolTypes(dataList, val);
                    break;
                case "AssetsWorkOrders":
                    migrationTenant.MapAssetsWorkOrders(dataList, val);
                    break;
                case "AssetsHiredServices":
                    migrationTenant.MapAssetsHiredServices(dataList, val);
                    break;
                case "MainWoViewConfigurationsPeople":
                    migrationTenant.MapMainWoViewConfigurationsPeople(dataList, val);
                    break;
                case "PeopleCalendars":
                    migrationTenant.MapPeopleCalendars(dataList, val);
                    break;
                case "PeopleCollectionsAdmins":
                    migrationTenant.MapPeopleCollectionsAdmins(dataList, val);
                    break;
                case "PeopleCollectionsPeople":
                    migrationTenant.MapPeopleCollectionsPeople(dataList, val);
                    break;
                case "PlanningPanelViewConfigurationPeople":
                    migrationTenant.MapPlanningPanelViewConfigurationPeople(dataList, val);
                    break;
                case "PeoplePermissions":
                    migrationTenant.MapPeoplePermissions(dataList, val);
                    break;
                case "PeopleCollectionsPermissions":
                    migrationTenant.MapPeopleCollectionsPermissions(dataList, val);
                    break;
                case "PlanningPanelViewConfigurationPeopleCollection":
                    migrationTenant.MapPlanningPanelViewConfigurationPeopleCollection(dataList, val);
                    break;
                case "PredefinedServicesPermission":
                    migrationTenant.MapPredefinedServicesPermission(dataList, val);
                    break;
                case "ProjectsCalendars":
                    migrationTenant.MapProjectsCalendars(dataList, val);
                    break;
                case "ProjectsContacts":
                    migrationTenant.MapProjectsContacts(dataList, val);
                    break;
                case "ProjectsPermissions":
                    migrationTenant.MapProjectsPermissions(dataList, val);
                    break;
                case "PermissionsQueues":
                    migrationTenant.MapPermissionsQueues(dataList, val);
                    break;
                case "PermissionsTasks":
                    migrationTenant.MapPermissionsTasks(dataList, val);
                    break;
                case "LocationCalendar":
                    migrationTenant.MapLocationCalendar(dataList, val);
                    break;
                case "ToolsTypeWorkOrderTypes":
                    migrationTenant.MapToolsTypeWorkOrderTypes(dataList, val);
                    break;
                case "WorkOrderCategoryPermissions":
                    migrationTenant.MapWorkOrderCategoryPermissions(dataList, val);
                    break;
                case "WorkOrderCategoryRoles":
                    migrationTenant.MapWorkOrderCategoryRoles(dataList, val);
                    break;
                case "WorkOrdersDeritative":
                    migrationTenant.MapWorkOrdersDeritative(dataList, val);
                    break;
                case "DerivedServices":
                    migrationTenant.MapDerivedServices(dataList, val);
                    break;
                case "MaterialForm":
                    migrationTenant.MapMaterialForm(dataList, val);
                    break;                
                case "LocationsFinalClients":
                    migrationTenant.MapLocationsFinalClients(dataList, val);
                    break;
                case "PeopleRegisteredPda":
                    migrationTenant.MapPeopleRegisteredPda(dataList, val);
                    break;
                case "TaskWebServiceCallItems":
                    migrationTenant.MapTaskWebServiceCallItems(dataList, val);
                    break;
                case "AdvancedTechnicianListFilterPersons":
                    migrationTenant.MapAdvancedTechnicianListFilterPersons(dataList, val);
                    break;
                case "AdvancedTechnicianListFilters":
                    migrationTenant.MapAdvancedTechnicianListFilters(dataList, val);
                    break;
                case "BasicTechnicianListFilters":
                    migrationTenant.MapBasicTechnicianListFilters(dataList, val);
                    break;
                case "BasicTechnicianListFilterSkills":
                    migrationTenant.MapBasicTechnicianListFilterSkills(dataList, val);
                    break;
                case "DnAndMaterialsAnalysis":
                    migrationTenant.MapDnAndMaterialsAnalysis(dataList, val);
                    break;
                case "PlanificationCriterias":
                    migrationTenant.MapPlanificationCriterias(dataList, val);
                    break;
                case "ServicesAnalysis":
                    migrationTenant.MapServicesAnalysis(dataList, val);
                    break;
                case "ErpItemsSyncConfig":
                    migrationTenant.MapErpItemsSyncConfig(dataList, val);
                    break;
                case "ExpensesTicketsFiles":
                    migrationTenant.MapExpensesTicketsFiles(dataList, val);
                    break;
                case "PushNotificationsPeople":
                    migrationTenant.MapPushNotificationsPeople(dataList, val);
                    break;
                case "WorkOrderAnalysis":
                    migrationTenant.MapWorkOrderAnalysis(dataList, val);
                    break;
                case "TechnicianListFilters":
                    migrationTenant.MapTechnicianListFilters(dataList, val);
                    break;
                case "Clients":
                    migrationTenantWithoutRelations.MapClients(dataList, val);
                    break;
                default:
                    break;
            }
        }

        private static void MigrateTenant(IEnumerable<object> dataList, Type type, KeyValuePair<string, string> val, int skip)
        {
            switch (type.Name)
            {
                case "Services":
                    migrationTenantWithRelationsInfrastructure.MapServices(dataList, val, skip);
                    break;
                case "WorkOrders":
                    migrationTenantWithRelationsInfrastructure.MapWorkOrders(dataList, val, skip);
                    break;
                case "Audits":
                    migrationTenantWithRelationsInfrastructure.MapAudits(dataList, val, skip);
                    break;
                case "ExtraFieldsValues":
                    migrationTenant.MapExtraFieldsValues(dataList, val, skip);
                    break;
                case "ExternalSystemImportData":
                    migrationTenant.MapExternalSystemImportData(dataList, val, skip);
                    break;
            }
        }

        private static void InsertCustomer()
        {
            string nameTenant = Configuration.GetSection("DataTenant").GetValue<string>("NameTenant");
            Console.WriteLine($" Insert Customer - Nombre tenant {nameTenant} ");
            bool existCustomer = ContextInfrastructure.Customers.Any(c => c.Name == nameTenant);

            if (!existCustomer)
            {
                var newCustomer = new Customers
                {
                    Name = nameTenant,
                    ConnString = Configuration.GetSection("DataTenant").GetValue<string>("ConnectionStringTenant"),                    
                    NumberWebUsers = 0,
                    NumberAppUsers = 0,
                    DateCreated = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    InvoicingContactEmail = string.Empty,
                    InvoicingContactName = string.Empty,
                    InvoicingContactFirstSurname = string.Empty,
                    InvoicingContactSecondSurname = string.Empty,
                    TechnicalAdministratorEmail = string.Empty,
                    TechnicalAdministratorName = string.Empty,
                    TechnicalAdministratorFirstSurname = string.Empty,
                    TechnicalAdministratorSecondSurname = string.Empty,
                    Telephone = string.Empty,
                    NumberEmployees = 0,
                    CustomerCode = string.Empty,
                    Address = string.Empty,
                    Municipality = null,
                    NIF = string.Empty,
                    NumberTeamMembers = 0,                                       
                    UpdateStatusDate = DateTime.UtcNow,
                    IsActive = true,
                    DatabaseName = Configuration.GetSection("DataTenant").GetValue<string>("DatabaseName"),
                };

                ContextInfrastructure.Add(newCustomer);
                ContextInfrastructure.SaveChanges();
            }
            else Console.WriteLine($"El tenant {nameTenant} ya existe en la base de datos.");
        }

        private static Type[] GetTypeEntitiesFromAssembly()
        {
            Console.WriteLine("GetTypeEntitiesFromAssembly");
            Assembly asm;

            if (Configuration.GetSection("Database").Value == "Masters") asm = Assembly.Load("Group.Salto.Entities");
            else asm = Assembly.Load("Group.Salto.Entities.Tenant");

            Type[] asmTypes = asm.GetTypes();

            return asmTypes;
        }

        private static Dictionary<string, string> ReadQueriesFromCsv()
        {
            Console.WriteLine("ReadQueriesFromCsv");
            StreamReader reader = new StreamReader(Configuration.GetSection("CsvPath").Value);
            var valuesList = new Dictionary<string, string>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(';');

                if (values.Any() && values.Length > 1 && !string.IsNullOrEmpty(values[1]))
                {
                    if (!values[0].StartsWith("--"))
                    {
                        valuesList.Add(values[0], values[1]);
                    }
                }
            }

            return valuesList;
        }

        private static IEnumerable<object> GetDataFromOldDatabase(Type type, string sql)
        {
            Console.Write($"GetDataFromOldDatabase: {type.Name} - {Environment.NewLine}");
            var results = SqlConnectionOldDatabase.Query(type, sql);

            return results;
        }

        private static IEnumerable<object> GetDataFromOldDatabase(Type type, string sql, int skip, int taken)
        {
            sql += $" OFFSET {skip} ROWS FETCH NEXT {taken} ROWS ONLY";

            Console.Write($"GetDataFromOldDatabase: {type.Name} - {Environment.NewLine}");

            var results = SqlConnectionOldDatabase.Query(type, sql);
            return results;
        }
    }
}