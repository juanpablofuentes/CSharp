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
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.MigrationData
{
    public class MigrationTenantWithRelationsInfrastructure
    {
        private SqlConnection _sqlConnectionOldDatabase;//SaltoCS03
        private SqlConnection _sqlConnectionTenantDatabase;
        private SOMContext _contextInfrastructure; //Som SqlServer
        private SOMTenantContext _contextTenant; //SomTenant localdb
        private MigrationTenantCommon _migrationTenantCommon; //somTeant localdb para grabar

        public MigrationTenantWithRelationsInfrastructure(SqlConnection sqlConnectionOldDatabase, SqlConnection sqlConnectionTenantDatabase, SOMContext contextInfrastructure, SOMTenantContext contextTenant)
        {
            _sqlConnectionOldDatabase = sqlConnectionOldDatabase;
            _sqlConnectionTenantDatabase = sqlConnectionTenantDatabase;
            _contextInfrastructure = contextInfrastructure;
            _contextTenant = contextTenant;
            _migrationTenantCommon = new MigrationTenantCommon(contextTenant, sqlConnectionTenantDatabase.ConnectionString);
        }

        public void InsertUserConfig(IConfigurationRoot Configuration)
        {
            string nameTenant = Configuration.GetSection("DataTenant").GetValue<string>("NameTenant");
            Customers customer = _contextInfrastructure.Customers.FirstOrDefault(c => c.Name == nameTenant);

            if (customer != null)
            {
                List<Users> usersByCustomer = _contextInfrastructure.Users.Where(u => u.Customer.Id == customer.Id).ToList();
                List<UserConfiguration> userConfigurationList = new List<UserConfiguration>();
                int cont = 0;
                if (_contextTenant.UserConfigurations.Any())
                {
                    cont = _contextTenant.UserConfigurations.Max(x => x.Id);
                }

                foreach (Users user in usersByCustomer)
                {
                    bool existUserConfiguration = _contextTenant.UserConfigurations.Where(u => u.Id == user.UserConfigurationId).Any();

                    if (!existUserConfiguration)
                    {
                        cont++;
                        userConfigurationList.Add(new UserConfiguration() { GuidId = Guid.NewGuid(), Id = cont });
                        user.UserConfigurationId = cont;
                    }
                }

                if (userConfigurationList.Any())
                {
                    _migrationTenantCommon.InsertToNewDatabaseTenant(userConfigurationList, "UserConfigurations");
                    _contextInfrastructure.UpdateRange(usersByCustomer);
                    _contextInfrastructure.SaveChanges();
                }
            }
            else Console.WriteLine($"El tenant {nameTenant} no existe en la base de datos.");
        }

        public void MapServices(IEnumerable<object> dataList, KeyValuePair<string, string> val, int skip)
        {
            Console.WriteLine($"MapServices - Registros a tratar: {dataList.Count()} apartir del {skip}");
            object localLockObject = new object();

            List<Services> list = dataList.Cast<Services>().ToList();
            List<Services> listFilter = new List<Services>();

            List<ServiceStates> oldListContextServiceStates = _sqlConnectionOldDatabase.Query<ServiceStates>("SELECT IdEstatServei As Id, Nom AS [Name] FROM EstatsServei").ToList();
            List<ServiceStates> listContextServiceStates = _contextInfrastructure.ServiceStates.ToList();

            Parallel.ForEach(list,
                () => { return new List<Services>(); },
                (item, state, localList) =>
                {
                    if (item.ServiceStateId != null)
                    {
                        var serviceState = listContextServiceStates.Find(x => x.Id == item.ServiceStateId);
                        var oldServiceState = oldListContextServiceStates.Find(x => x.Name == serviceState.Name);
                        if (serviceState.Id != oldServiceState.Id) item.ServiceStateId = serviceState.Id;
                    }

                    item.CreationDate = item.CreationDate.ToUniversalTime();
                    item.DeliveryProcessInit = item.DeliveryProcessInit.HasValue ? item.DeliveryProcessInit.Value.ToUniversalTime() : item.DeliveryProcessInit.GetValueOrDefault();
                    item.UpdateDate = DateTime.UtcNow;
                    localList.Add(item);

                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            list.Clear();

            if (listFilter.Any())
            {
                string[] copyParameters = new[]
                {
                    nameof(Services.Id),
                    nameof(Services.UpdateDate),
                    nameof(Services.PredefinedServiceId),
                    nameof(Services.IdentifyInternal),
                    nameof(Services.IdentifyExternal),
                    nameof(Services.Description),
                    nameof(Services.Observations),
                    nameof(Services.PeopleResponsibleId),
                    nameof(Services.SubcontractResponsibleId),
                    nameof(Services.WorkOrderId),
                    nameof(Services.ServiceStateId),
                    nameof(Services.ClosingCodeFirstId),
                    nameof(Services.ClosingCodeSecondId),
                    nameof(Services.ClosingCodeThirdId),
                    nameof(Services.Latitude),
                    nameof(Services.Longitude),
                    nameof(Services.IcgId),
                    nameof(Services.CreationDate),
                    nameof(Services.FormState),
                    nameof(Services.DeliveryNote),
                    nameof(Services.DeliveryProcessInit),
                    nameof(Services.Cancelled),
                    nameof(Services.ServicesCancelFormId),
                    nameof(Services.ClosingCodeId)
                };

                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters);
                listFilter.Clear();
            }
        }

        public void MapWorkOrders(IEnumerable<object> dataList, KeyValuePair<string, string> val, int skip)
        {
            Console.WriteLine($"MapWorkOrders - Registros a tratar: {dataList.Count()} apartir del {skip}");
            object localLockObject = new object();

            List<WorkOrders> list = dataList.Cast<WorkOrders>().ToList();
            List<WorkOrders> listFilter = new List<WorkOrders>();

            List<Origins> oldListContextOrigins = _sqlConnectionOldDatabase.Query<Origins>("SELECT IdProcedencia As Id, Nom As [Name] FROM Procedencies").ToList();
            List<Origins> listContextOrigins = _contextInfrastructure.Origins.ToList();

            Parallel.ForEach(list,
                () => { return new List<WorkOrders>(); },
                (item, state, localList) =>
                {
                    Origins origin = listContextOrigins.Find(x => x.Id == item.OriginId);
                    Origins oldOrigin = oldListContextOrigins.Find(x => x.Name == origin.Name);
                    if (origin.Id != oldOrigin.Id) item.OriginId = origin.Id;
                    item.CreationDate = item.CreationDate.ToUniversalTime();
                    item.PickUpTime = item.PickUpTime.HasValue ? item.PickUpTime.Value.ToUniversalTime() : item.PickUpTime.GetValueOrDefault();
                    item.FinalClientClosingTime = item.FinalClientClosingTime.HasValue ? item.FinalClientClosingTime.Value.ToUniversalTime() : item.FinalClientClosingTime.GetValueOrDefault();
                    item.InternalClosingTime = item.InternalClosingTime.HasValue ? item.InternalClosingTime.Value.ToUniversalTime() : item.InternalClosingTime.GetValueOrDefault();
                    item.AssignmentTime = item.AssignmentTime.HasValue ? item.AssignmentTime.Value.ToUniversalTime() : item.AssignmentTime.GetValueOrDefault();
                    item.ActionDate = item.ActionDate.HasValue ? item.ActionDate.Value.ToUniversalTime() : item.ActionDate.GetValueOrDefault();
                    item.DateStopTimerSla = item.DateStopTimerSla.HasValue ? item.DateStopTimerSla.Value.ToUniversalTime() : item.DateStopTimerSla.GetValueOrDefault();
                    item.ResponseDateSla = item.ResponseDateSla.HasValue ? item.ResponseDateSla.Value.ToUniversalTime() : item.ResponseDateSla.GetValueOrDefault();
                    item.ResolutionDateSla = item.ResolutionDateSla.HasValue ? item.ResolutionDateSla.Value.ToUniversalTime() : item.ResolutionDateSla.GetValueOrDefault();
                    item.DateUnansweredPenaltySla = item.DateUnansweredPenaltySla.HasValue ? item.DateUnansweredPenaltySla.Value.ToUniversalTime() : item.DateUnansweredPenaltySla.GetValueOrDefault();
                    item.DatePenaltyWithoutResolutionSla = item.DatePenaltyWithoutResolutionSla.HasValue ? item.DatePenaltyWithoutResolutionSla.Value.ToUniversalTime() : item.DatePenaltyWithoutResolutionSla.GetValueOrDefault();
                    item.ActuationEndDate = item.ActuationEndDate.HasValue ? item.ActuationEndDate.Value.ToUniversalTime() : item.ActuationEndDate.GetValueOrDefault();
                    item.LastModified = item.LastModified.HasValue ? item.LastModified.Value.ToUniversalTime() : item.LastModified.GetValueOrDefault();
                    item.ClosingOtdate = item.ClosingOtdate.HasValue ? item.ClosingOtdate.Value.ToUniversalTime() : item.ClosingOtdate.GetValueOrDefault();
                    item.AccountingClosingDate = item.AccountingClosingDate.HasValue ? item.AccountingClosingDate.Value.ToUniversalTime() : item.AccountingClosingDate.GetValueOrDefault();
                    item.ClientClosingDate = item.ClientClosingDate.HasValue ? item.ClientClosingDate.Value.ToUniversalTime() : item.ClientClosingDate.GetValueOrDefault();
                    item.SystemDateWhenOtclosed = item.SystemDateWhenOtclosed.HasValue ? item.SystemDateWhenOtclosed.Value.ToUniversalTime() : item.SystemDateWhenOtclosed.GetValueOrDefault();
                    item.InternalCreationDate = item.InternalCreationDate.HasValue ? item.InternalCreationDate.Value.ToUniversalTime() : item.InternalCreationDate.GetValueOrDefault();
                    item.UpdateDate = DateTime.UtcNow;

                    localList.Add(item);

                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            if (listFilter.Any())
            {
                string[] copyParameters = new[]
                {
                  nameof(WorkOrders.Id),
                  nameof(WorkOrders.UpdateDate),
                  nameof(WorkOrders.InternalIdentifier),
                  nameof(WorkOrders.ExternalIdentifier),
                  nameof(WorkOrders.CreationDate),
                  nameof(WorkOrders.TextRepair),
                  nameof(WorkOrders.Observations),
                  nameof(WorkOrders.PickUpTime),
                  nameof(WorkOrders.FinalClientClosingTime),
                  nameof(WorkOrders.InternalClosingTime),
                  nameof(WorkOrders.AssignmentTime),
                  nameof(WorkOrders.ActionDate),
                  nameof(WorkOrders.PeopleResponsibleId),
                  nameof(WorkOrders.OriginId),
                  nameof(WorkOrders.PeopleIntroducedById),
                  nameof(WorkOrders.WorkOrderTypesId),
                  nameof(WorkOrders.LocationId),
                  nameof(WorkOrders.FinalClientId),
                  nameof(WorkOrders.WorkOrderStatusId),
                  nameof(WorkOrders.IcgId),
                  nameof(WorkOrders.AssetId),
                  nameof(WorkOrders.PeopleManipulatorId),
                  nameof(WorkOrders.QueueId),
                  nameof(WorkOrders.ExternalWorOrderStatusId),
                  nameof(WorkOrders.ProjectId),
                  nameof(WorkOrders.WorkOrdersFatherId),
                  nameof(WorkOrders.DateStopTimerSla),
                  nameof(WorkOrders.ResponseDateSla),
                  nameof(WorkOrders.ResolutionDateSla),
                  nameof(WorkOrders.DateUnansweredPenaltySla),
                  nameof(WorkOrders.DatePenaltyWithoutResolutionSla),
                  nameof(WorkOrders.GeneratorServiceId), 
                  nameof(WorkOrders.ReferenceGeneratorService),
                  nameof(WorkOrders.ReferenceOtherServices),
                  nameof(WorkOrders.WorkOrderCategoryId),
                  nameof(WorkOrders.Archived),
                  nameof(WorkOrders.ActuationEndDate),
                  nameof(WorkOrders.LastModified),
                  nameof(WorkOrders.IsResponsibleFixed),
                  nameof(WorkOrders.IsActuationDateFixed),
                  nameof(WorkOrders.SiteUserId),
                  nameof(WorkOrders.Billable),
                  nameof(WorkOrders.ExternalSystemId),
                  nameof(WorkOrders.ClosingOtdate),
                  nameof(WorkOrders.AccountingClosingDate),
                  nameof(WorkOrders.ClientClosingDate),
                  nameof(WorkOrders.SystemDateWhenOtclosed),
                  nameof(WorkOrders.InternalCreationDate),
                  nameof(WorkOrders.MeetSlaresponse),
                  nameof(WorkOrders.MeetSlaresolution),
                  nameof(WorkOrders.Overhead)
                };

                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters);
                dataList = null;
            }

            listFilter = null;
        }

        public void MapAudits(IEnumerable<object> dataList, KeyValuePair<string, string> val, int skip)
        {
            Console.WriteLine($"MapAudits - Registros a tratar: {dataList.Count()} apartir del {skip}");
            object localLockObject = new object();

            List<Audits> list = dataList.Cast<Audits>().ToList();
            List<Audits> listFilter = new List<Audits>();
            List<Users> listUsers = _contextInfrastructure.Users.ToList();

            Parallel.ForEach(list,
                () => { return new List<Audits>(); },
                (item, state, localList) =>
                {
                    int? userConfigurationId = listUsers.Where(user => user.OldUserId == item.UserConfigurationId).Select(s => s.UserConfigurationId).FirstOrDefault();

                    if (userConfigurationId.HasValue)
                    {
                        item.UserConfigurationId = userConfigurationId.Value;
                    }

                    int? UserConfigurationSupplanterId = listUsers.Where(user => user.OldUserId == item.UserConfigurationSupplanterId).Select(s => s.UserConfigurationId).FirstOrDefault();

                    if (UserConfigurationSupplanterId.HasValue)
                    {
                        item.UserConfigurationSupplanterId = UserConfigurationSupplanterId.Value;
                    }
                    else
                    {
                        item.UserConfigurationSupplanterId = null;
                    }
                    item.DataHora = item.DataHora.ToUniversalTime();
                    item.UpdateDate = DateTime.UtcNow;
                    localList.Add(item);

                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            if (listFilter.Any())
            {
                string[] copyParameters = new[]
                {
                    nameof(Audits.Id),
                    nameof(Audits.UpdateDate),
                    nameof(Audits.Name),
                    nameof(Audits.WorkOrderId),
                    nameof(Audits.DataHora),
                    nameof(Audits.UserConfigurationId),
                    nameof(Audits.Origin),
                    nameof(Audits.Latitude),
                    nameof(Audits.Longitude),
                    nameof(Audits.Height),
                    nameof(Audits.Observations),
                    nameof(Audits.UserConfigurationSupplanterId),
                    nameof(Audits.TaskId)
                };

                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters);
                listFilter.Clear();
            }
        }

        public void MapCalendarEvents(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapCalendarEvents - Registros a tratar: {dataList.Count()}");
            List<CalendarEvents> list = dataList.Cast<CalendarEvents>().ToList();
            List<CalendarEvents> listFilter = new List<CalendarEvents>();
            List<CalendarEvents> listContext = _contextTenant.CalendarEvents.ToList();

            List<CalendarEventCategories> oldListContextCalendarEventCategories = _sqlConnectionOldDatabase.Query<CalendarEventCategories>("SELECT CalendarEventCategoryId AS [Id], [Name] FROM CalendarEventCategories").ToList();
            List<RepetitionTypes> oldListRepetitionTypes = _sqlConnectionOldDatabase.Query<RepetitionTypes>("SELECT RepetitionTypeId AS [Id], [Name] FROM RepetitionTypes").ToList();
            List<CalendarEventCategories> listContextCalendarEventCategories = _contextInfrastructure.CalendarEventCategories.ToList();
            List<RepetitionTypes> listContextRepetitionTypes = _contextInfrastructure.RepetitionTypes.ToList();

            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<CalendarEvents>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        if (item.Category != null)
                        {
                            var categoryEvent = listContextCalendarEventCategories.FirstOrDefault(x => x.Id == item.Category);
                            var oldCategoryEvent = oldListContextCalendarEventCategories.FirstOrDefault(x => x.Name == categoryEvent.Name);
                            if (categoryEvent.Id != oldCategoryEvent.Id) item.Category = categoryEvent.Id;
                        }

                        if (item.RepetitionType != null)
                        {
                            var repetitionTypes = listContextRepetitionTypes.FirstOrDefault(x => x.Id == item.RepetitionType);

                            var oldRepetitionTypes = oldListRepetitionTypes.FirstOrDefault(x => x.Name == repetitionTypes.Name);
                            if (repetitionTypes.Id != oldRepetitionTypes.Id) item.Category = repetitionTypes.Id;
                        }
                        item.StartDate = item.StartDate.HasValue ? item.StartDate.Value.ToUniversalTime() : item.StartDate.GetValueOrDefault();
                        item.EndDate = item.EndDate.HasValue ? item.EndDate.Value.ToUniversalTime() : item.EndDate.GetValueOrDefault();
                        item.ModificationDate = item.ModificationDate.ToUniversalTime();
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");                    
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapCalendarPlanningViewConfiguration(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapCalendarPlanningViewConfiguration - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<CalendarPlanningViewConfiguration> list = dataList.Cast<CalendarPlanningViewConfiguration>().ToList();
            List<CalendarPlanningViewConfiguration> listFilter = new List<CalendarPlanningViewConfiguration>();
            List<CalendarPlanningViewConfiguration> listContext = _contextTenant.CalendarPlanningViewConfiguration.ToList();
            List<Users> listUsers = _contextInfrastructure.Users.ToList();

            Parallel.ForEach(list,
                () => { return new List<CalendarPlanningViewConfiguration>(); },
                (item, state, localList) =>
                {
                    int? userConfigurationId = listUsers.Where(user => user.OldUserId == item.UserConfigurationId).Select(s => s.UserConfigurationId).FirstOrDefault();

                    if (userConfigurationId.HasValue)
                    {
                        item.UserConfigurationId = userConfigurationId.Value;
                    }
                    else
                    {
                        Console.WriteLine($"{item.UserConfigurationId} no tiene valor");
                    }

                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
            }
        }

        public void MapFinalClients(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapFinalClients - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<FinalClients> list = dataList.Cast<FinalClients>().ToList();
            List<FinalClients> listFilter = new List<FinalClients>();
            List<FinalClients> listContext = _contextTenant.FinalClients.ToList();

            List<Origins> oldListContextOrigins = _sqlConnectionOldDatabase.Query<Origins>("SELECT [IdProcedencia] AS [Id],[Nom] AS [Name] FROM Procedencies").ToList();
            List<Origins> listContextOrigins = _contextInfrastructure.Origins.ToList();

            Parallel.ForEach(list,
                () => { return new List<FinalClients>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        var origin = listContextOrigins.Find(x => x.Id == item.OriginId);
                        var oldOrigin = oldListContextOrigins.Find(x => x.Name == origin.Name);
                        if (origin.Id != oldOrigin.Id) item.OriginId = origin.Id;
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
            }
        }

        public void MapServicesViewConfigurations(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapServicesViewConfigurations - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ServicesViewConfigurations> list = dataList.Cast<ServicesViewConfigurations>().ToList();
            List<ServicesViewConfigurations> listFilter = new List<ServicesViewConfigurations>();
            List<ServicesViewConfigurations> listContext = _contextTenant.ServicesViewConfigurations.ToList();
            List<Users> listUsers = _contextInfrastructure.Users.ToList();

            Parallel.ForEach(list,
                () => { return new List<ServicesViewConfigurations>(); },
                (item, state, localList) =>
                {
                    int? userConfigurationId = listUsers.Where(user => user.OldUserId == item.UserConfigurationId).Select(s => s.UserConfigurationId).FirstOrDefault();

                    if (userConfigurationId.HasValue)
                    {
                        item.UserConfigurationId = userConfigurationId.Value;
                    }
                    else
                    {
                        Console.WriteLine($"{item.UserConfigurationId} no tiene valor");
                    }

                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
            }
        }

        public void MapLocations(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapLocations - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Locations> list = dataList.Cast<Locations>().ToList();
            List<Locations> listFilter = new List<Locations>();
            //List<Locations> listContext = _contextTenant.Locations.ToList();

            List<States> oldListContextStates = _sqlConnectionOldDatabase.Query<States>("SELECT Id, [Name] FROM States").ToList();
            List<States> listContextStates = _contextInfrastructure.States.ToList();

            List<Cities> oldListContextCities = _sqlConnectionOldDatabase.Query<Cities>("SELECT Id, [Name] FROM Cities").ToList();
            List<Cities> listContextCities = _contextInfrastructure.Cities.ToList();

            List<Countries> oldListContextCountries = _sqlConnectionOldDatabase.Query<Countries>("SELECT Id, Name FROM Countries").ToList();
            List<Countries> listContextCountries = _contextInfrastructure.Countries.ToList();

            List<Municipalities> oldListContextMunicipalities = _sqlConnectionOldDatabase.Query<Municipalities>("SELECT Id, [Name] FROM Municipalities").ToList();
            List<Municipalities> listContextMunicipalities = _contextInfrastructure.Municipalities.ToList();

            List<Regions> oldListContextRegions = _sqlConnectionOldDatabase.Query<Regions>("SELECT Id, Name FROM Regions").ToList();
            List<Regions> listContextRegions = _contextInfrastructure.Regions.ToList();

            List<PostalCodes> oldListContextPostalCodes = _sqlConnectionOldDatabase.Query<PostalCodes>("SELECT Id, PostalCode FROM PostalCodes").ToList();
            List<PostalCodes> listContextPostalCodes = _contextInfrastructure.PostalCodes.ToList();

            Parallel.ForEach(list,
                () => { return new List<Locations>(); },
                (item, state, localList) =>
                {
                    //if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    //{
                    if (item.StateId != null)
                    {
                        var newState = listContextStates.Find(x => x.Id == item.StateId);
                        var oldState = oldListContextStates.Find(x => x.Name == newState.Name);
                        if (newState.Id != oldState.Id) item.StateId = newState.Id;
                    }
                    if (item.CityId != null)
                    {
                        var newCity = listContextCities.Find(x => x.Id == item.CityId);
                        var oldCity = oldListContextCities.Find(x => x.Name == newCity.Name);
                        if (newCity.Id != oldCity.Id) item.CityId = newCity.Id;
                    }
                    if (item.CountryId != null)
                    {
                        var newCountry = listContextCountries.Find(x => x.Id == item.CountryId);
                        var oldCountry = oldListContextCountries.Find(x => x.Name == newCountry.Name);
                        if (newCountry.Id != oldCountry.Id) item.CountryId = newCountry.Id;
                    }
                    if (item.MunicipalityId != null)
                    {
                        var newMunicipality = listContextMunicipalities.Find(x => x.Id == item.MunicipalityId);
                        var oldMunicipality = oldListContextMunicipalities.Find(x => x.Name == newMunicipality.Name);
                        if (newMunicipality.Id != oldMunicipality.Id) item.MunicipalityId = newMunicipality.Id;
                    }
                    if (item.RegionId != null)
                    {
                        var newRegion = listContextRegions.Find(x => x.Id == item.RegionId);
                        var oldRegion = oldListContextRegions.Find(x => x.Name == newRegion.Name);
                        if (newRegion.Id != oldRegion.Id) item.RegionId = newRegion.Id;
                    }
                    if (item.PostalCodeId != null)
                    {
                        var newPostalCode = listContextPostalCodes.Find(x => x.Id == item.PostalCodeId);
                        var oldPostalCode = oldListContextPostalCodes.Find(x => x.PostalCode == newPostalCode.PostalCode);
                        if (newPostalCode.Id != oldPostalCode.Id) item.PostalCodeId = newPostalCode.Id;
                    }
                    item.UpdateDate = DateTime.UtcNow;
                    localList.Add(item);
                    //}
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            //listContext.Clear();

            if (listFilter.Any())
            {
                string[] copyParameters = new[]
                {
                    nameof(Locations.Id),
                    nameof(Locations.UpdateDate),
                    nameof(Locations.Name),
                    nameof(Locations.StreetType),
                    nameof(Locations.Street),
                    nameof(Locations.Code),
                    nameof(Locations.Number),
                    nameof(Locations.Escala),
                    nameof(Locations.GateNumber),
                    nameof(Locations.City),
                    nameof(Locations.Province),
                    nameof(Locations.PostalCode),
                    nameof(Locations.Country),
                    nameof(Locations.Area),
                    nameof(Locations.Zone),
                    nameof(Locations.Subzone),
                    nameof(Locations.Latitude),
                    nameof(Locations.Longitude),
                    nameof(Locations.Observations),
                    nameof(Locations.PeopleResponsibleLocationId),
                    nameof(Locations.HashCity),
                    nameof(Locations.HashProvincie),
                    nameof(Locations.HashZone),
                    nameof(Locations.HashSubzone),
                    nameof(Locations.StateId),
                    nameof(Locations.CityId),
                    nameof(Locations.CountryId),
                    nameof(Locations.MunicipalityId),
                    nameof(Locations.RegionId),
                    nameof(Locations.PostalCodeId),
                    nameof(Locations.Phone1),
                    nameof(Locations.Phone2),
                    nameof(Locations.Phone3)
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters);
            }
        }

        public void MapUsersMainWoviewConfigurations(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapUsersMainWoviewConfigurations - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<UsersMainWoviewConfigurations> list = dataList.Cast<UsersMainWoviewConfigurations>().ToList();
            List<UsersMainWoviewConfigurations> listFilter = new List<UsersMainWoviewConfigurations>();
            List<UsersMainWoviewConfigurations> listContext = _contextTenant.UsersMainWoviewConfigurations.ToList();

            List<Users> listUsers = _contextInfrastructure.Users.ToList();

            Parallel.ForEach(list,
                () => { return new List<UsersMainWoviewConfigurations>(); },
                (item, state, localList) =>
                {
                    int? userConfigurationId = listUsers.Where(user => user.OldUserId == item.UserConfigurationId).Select(s => s.UserConfigurationId).FirstOrDefault();

                    if (userConfigurationId.HasValue)
                    {
                        item.UserConfigurationId = userConfigurationId.Value;
                    }
                    else
                    {
                        Console.WriteLine($"{item.UserConfigurationId} no tiene valor");
                    }

                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }

                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
            }
        }

        public void MapPeople(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPeople - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<People> list = dataList.Cast<People>().ToList();
            List<People> listFilter = new List<People>();
            List<People> listContext = _contextTenant.People.ToList();

            List<Users> listUsers = _contextInfrastructure.Users.ToList();

            Parallel.ForEach(list,
                () => { return new List<People>(); },
                (item, state, localList) =>
                {
                    int? userConfigurationId = listUsers.Where(user => user.OldUserId == item.UserConfigurationId).Select(s => s.UserConfigurationId).FirstOrDefault();

                    if (userConfigurationId.HasValue)
                    {
                        item.UserConfigurationId = userConfigurationId.Value;
                    }
                    else
                    {
                        Console.WriteLine($"{item.UserConfigurationId} no tiene valor");
                    }

                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }

                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
            }
        }

        public void MapContracts(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapContracts - Registros a tratar: {dataList.Count()}");
            List<Contracts> list = dataList.Cast<Contracts>().ToList();
            List<Contracts> listFilter = new List<Contracts>();
            List<Contracts> listContext = _contextTenant.Contracts.ToList();

            List<ContractType> oldListContextContractType = _sqlConnectionOldDatabase.Query<ContractType>("SELECT ContractTypeId AS Id,[Value] FROM ContractType").ToList();
            List<ContractType> listContextContractType = _contextInfrastructure.ContractType.ToList();

            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Contracts>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        var contractType = listContextContractType.Find(x => x.Id == item.ContractTypeId);
                        var oldContractType = oldListContextContractType.Find(x => x.Value == contractType.Value);
                        if (contractType.Id != oldContractType.Id) item.ContractTypeId = contractType.Id;
                        item.StartDate = item.StartDate.HasValue ? item.StartDate.Value.ToUniversalTime() : item.StartDate.GetValueOrDefault();
                        item.EndDate = item.EndDate.HasValue ? item.EndDate.Value.ToUniversalTime() : item.EndDate.GetValueOrDefault();
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");                    
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapJourneysStates(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapJourneysStates - Registros a tratar: {dataList.Count()}");
            List<JourneysStates> list = dataList.Cast<JourneysStates>().ToList();
            List<JourneysStates> listFilter = new List<JourneysStates>();

            List<PredefinedDayStates> oldListContextPredefinedDayStates = _sqlConnectionOldDatabase.Query<PredefinedDayStates>("SELECT ContractTypeId AS Id, [Value] FROM ContractType").ToList();
            List<PredefinedDayStates> listContextPredefinedDayStates = _contextInfrastructure.PredefinedDayStates.ToList();
            List<PredefinedReasons> oldListContextPredefinedReasons = _sqlConnectionOldDatabase.Query<PredefinedReasons>("SELECT ContractTypeId AS Id, [Value] FROM ContractType").ToList();
            List<PredefinedReasons> listContextPredefinedReasons = _contextInfrastructure.PredefinedReasons.ToList();

            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<JourneysStates>(); },
                (item, state, localList) =>
                {
                    var predefinedDayStates = listContextPredefinedDayStates.Find(x => x.Id == item.PredefinedDayStatesId);

                    if (predefinedDayStates is null)
                    {
                        var oldPredefinedDayStates = oldListContextPredefinedDayStates.Find(x => x.Name == predefinedDayStates.Name);

                        if (predefinedDayStates.Id != oldPredefinedDayStates.Id) item.PredefinedDayStatesId = predefinedDayStates.Id;
                    }

                    if (item.PredefinedReasonsId != null)
                    {
                        var predefinedReasons = listContextPredefinedReasons.Find(x => x.Id == item.PredefinedReasonsId);

                        if (predefinedReasons is null)
                        {
                            var oldPredefinedReasons = oldListContextPredefinedReasons.Find(x => x.Name == predefinedReasons.Name);

                            if (predefinedReasons.Id != oldPredefinedReasons.Id) item.PredefinedReasonsId = predefinedReasons.Id;
                        }
                    }
                    item.Data = item.Data.ToUniversalTime();
                    item.UpdateDate = DateTime.UtcNow;
                    localList.Add(item);
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            if (listFilter.Any())
            {
                string[] copyParameters = new[]
                {
                    nameof(JourneysStates.Id),
                    nameof(JourneysStates.UpdateDate),
                    nameof(JourneysStates.PredefinedDayStatesId),
                    nameof(JourneysStates.JourneyId),
                    nameof(JourneysStates.Data),
                    nameof(JourneysStates.Longitude),
                    nameof(JourneysStates.Latitude),
                    nameof(JourneysStates.PredefinedReasonsId),
                    nameof(JourneysStates.Observations)
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters);
            }
        }

        public void MapSessions(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapSessions - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Sessions> list = dataList.Cast<Sessions>().ToList();
            List<Sessions> listFilter = new List<Sessions>();
            List<Sessions> listContext = _contextTenant.Sessions.ToList();
            List<Users> listUsers = _contextInfrastructure.Users.ToList();

            Parallel.ForEach(list,
                () => { return new List<Sessions>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        int? userConfigurationId = listUsers.Where(user => user.OldUserId == item.UserConfigurationId).Select(s => s.UserConfigurationId).FirstOrDefault();

                        if (userConfigurationId.HasValue)
                        {
                            item.UserConfigurationId = userConfigurationId.Value;
                        }
                        item.DateLastActivity = item.DateLastActivity.ToUniversalTime();
                        item.UpdateDate = DateTime.UtcNow;
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
            }
        }

        public void MapExpensesTickets(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapExpensesTickets - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ExpensesTickets> list = dataList.Cast<ExpensesTickets>().ToList();
            List<ExpensesTickets> listFilter = new List<ExpensesTickets>();
            List<ExpensesTickets> listContext = _contextTenant.ExpensesTickets.ToList();
            List<ExpenseTicketStatus> listExpenseTicketStatuses = _contextInfrastructure.ExpenseTicketStatuses.ToList();

            Parallel.ForEach(list,
                () => { return new List<ExpensesTickets>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        var expenseTicketStatus = listExpenseTicketStatuses.Find(x => x.Description == item.Status);
                        if (expenseTicketStatus != null)
                        {
                            item.Date = item.Date.ToUniversalTime();
                            item.ValidationDate = item.ValidationDate.HasValue ? item.ValidationDate.Value.ToUniversalTime() : item.ValidationDate.GetValueOrDefault();
                            item.ExpenseTicketStatusId = expenseTicketStatus.Id;
                            item.UpdateDate = DateTime.UtcNow;
                            localList.Add(item);
                        }
                        else Console.WriteLine($"No existe el estado con Id {item.ExpenseTicketStatusId}.");
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");                    
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
            }
        }

        public void MapSla(KeyValuePair<string, string> val)
        {
            List<SlaMigration> listMigrationSla = _sqlConnectionOldDatabase.Query<SlaMigration>("SELECT IdSLA as Id,MinutsPenalitzacioSenseResolucio as MinutesPenaltyWithoutResolution,MinutsPenalitzacioSenseResolucioDefinitsEnOT as MinutesPenaltyWithoutResolutionOtDefined,MinutsPenalitzacioSenseResolucioNaturals as MinutesPenaltyWithoutNaturalResolution,MinutsPenalitzacioSenseResposta as MinutesPenaltyWithoutResponse,MinutsPenalitzacioSenseRespostaDefinitsEnOT as MinutesPenaltyWithoutResponseOtDefined,MinutsPenalitzacioSenseRespostaNaturals as MinutesPenaltyWithoutResponseNaturals,MinutsResolucio as MinutesResolutions,MinutsResolucioDefinitsEnOT as MinutesResolutionOtDefined,MinutsResolucioNaturals as MinutesResolutionNaturals,MinutsResposta as MinutesResponse,MinutsRespostaDefinitsEnOT as MinutesResponseOtDefined,MinutsRespostaNaturals as MinutesNaturalResponse,Nom as Name,ReferenciaMinutsPenalitzacioSenseResolucio as ReferenceMinutesPenaltyWithoutResolution,ReferenciaMinutsPenalitzacioSenseResposta as ReferenceMinutesPenaltyUnanswered,ReferenciaMinutsResolucio as ReferenceMinutesResolution,ReferenciaMinutsResposta as ReferenceMinutesResponse,TempsPenalitzacioSenseResolucioActiu as TimePenaltyWhithoutResolutionActive,TempsPenalitzacioSenseRespostaActiu as TimePenaltyWithoutResponseActive,TempsResolucioActiu as TimeResolutionActive,TempsRespostaActiu as TimeResponseActive FROM SLA").ToList();
            Console.WriteLine($"MapSla - Registros a tratar: {listMigrationSla.Count()}");
            List<Sla> listFilter = new List<Sla>();
            List<Sla> listContext = _contextTenant.Sla.ToList();
            List<ReferenceTimeSla> referenceTimeSlas = _contextInfrastructure.ReferenceTimeSla.ToList();
            object localLockObject = new object();
            Parallel.ForEach(listMigrationSla,
                () => { return new List<Sla>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        ReferenceTimeSla referenceMinutesResponseValue = referenceTimeSlas.Find(x => x.Name == item.ReferenceMinutesResponse);
                        ReferenceTimeSla referenceMinutesResolutionValue = referenceTimeSlas.Find(x => x.Name == item.ReferenceMinutesResolution);
                        ReferenceTimeSla referenceMinutesPenaltyUnansweredValue = referenceTimeSlas.Find(x => x.Name == item.ReferenceMinutesPenaltyUnanswered);
                        ReferenceTimeSla referenceMinutesPenaltyWithoutResolutionValue = referenceTimeSlas.Find(x => x.Name == item.ReferenceMinutesPenaltyWithoutResolution);

                        Sla itemSla = new Sla()
                        {
                            Id = item.Id,
                            UpdateDate = DateTime.UtcNow,
                            Name = item.Name,
                            MinutesResponse = item.MinutesResponse,
                            MinutesNaturalResponse = item.MinutesNaturalResponse,
                            MinutesResolutions = item.MinutesResolutions,
                            MinutesResolutionNaturals = item.MinutesResolutionNaturals,
                            MinutesUnansweredPenalty = item.MinutesUnansweredPenalty,
                            MinutesWithoutNaturalResponse = item.MinutesWithoutNaturalResponse,
                            MinutesPenaltyWithoutResponse = item.MinutesPenaltyWithoutResponse,
                            MinutesPenaltyWithoutResponseNaturals = item.MinutesPenaltyWithoutResponseNaturals,
                            MinutesPenaltyWithoutResolution = item.MinutesPenaltyWithoutResolution,
                            MinutesPenaltyWithoutNaturalResolution = item.MinutesPenaltyWithoutNaturalResolution,
                            TimeResponseActive = item.TimeResponseActive,
                            TimeResolutionActive = item.TimeResolutionActive,
                            TimePenaltyWithoutResponseActive = item.TimePenaltyWithoutResponseActive,
                            TimePenaltyWhithoutResolutionActive = item.TimePenaltyWhithoutResolutionActive,
                            MinutesResponseOtDefined = item.MinutesResponseOtDefined,
                            MinutesResolutionOtDefined = item.MinutesResolutionOtDefined,
                            MinutesPenaltyWithoutResponseOtDefined = item.MinutesPenaltyWithoutResponseOtDefined,
                            MinutesPenaltyWithoutResolutionOtDefined = item.MinutesPenaltyWithoutResolutionOtDefined
                        };
                        if (referenceMinutesResponseValue != null) itemSla.ReferenceMinutesResponse = referenceMinutesResponseValue.Id;
                        if (referenceMinutesResolutionValue != null) itemSla.ReferenceMinutesResolution = referenceMinutesResolutionValue.Id;
                        if (referenceMinutesPenaltyUnansweredValue != null) itemSla.ReferenceMinutesPenaltyUnanswered = referenceMinutesPenaltyUnansweredValue.Id;
                        if (referenceMinutesPenaltyWithoutResolutionValue != null) itemSla.ReferenceMinutesPenaltyWithoutResolution = referenceMinutesPenaltyWithoutResolutionValue.Id;

                        localList.Add(itemSla);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        #region scripts

        public void MapRepetitionParameters(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapRepetitionParameters");
            CalculationType calculationType = _contextInfrastructure.CalculationTypes.FirstOrDefault(x => x.Name == "MENSUA");
            DamagedEquipment damagedEquipment = _contextInfrastructure.DamagedEquipment.FirstOrDefault(x => x.Name == "NUM_SÈRIE");
            DaysType daysType = _contextInfrastructure.DaysTypes.FirstOrDefault(x => x.Name == "LABORALS");

            if (calculationType != null && damagedEquipment != null && daysType != null)
            {
                object localLockObject = new object();
                List<RepetitionParameters> listFilter = new List<RepetitionParameters>();
                List<RepetitionParameters> listContext = _contextTenant.RepetitionParameters.ToList();

                listFilter.Add(new RepetitionParameters() { Id = new Guid(), Days = 30, IdCalculationType = calculationType.Id, IdDamagedEquipment = damagedEquipment.Id, IdDaysType = daysType.Id, UpdateDate = DateTime.UtcNow });

                listContext.Clear();
                if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
                else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
            }
            else Console.WriteLine("Falta algún dato en las tablas relacionadas.");
        }

        #endregion
    }
}