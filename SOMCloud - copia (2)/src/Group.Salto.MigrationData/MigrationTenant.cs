using Dapper;
using Group.Salto.DataAccess.Context;
using Group.Salto.DataAccess.Tenant.Context;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.MigrationData
{
    public class MigrationTenant
    {
        private SqlConnection _sqlConnectionOldDatabase;
        private SqlConnection _sqlConnectionTenantDatabase;
        private SOMTenantContext _contextTenant;
        private MigrationTenantCommon _migrationTenantCommon;

        public MigrationTenant(SqlConnection sqlConnectionOldDatabase, SqlConnection sqlConnectionTenantDatabase, SOMContext contextInfrastructure, SOMTenantContext contextTenant)
        {
            _sqlConnectionOldDatabase = sqlConnectionOldDatabase;
            _sqlConnectionTenantDatabase = sqlConnectionTenantDatabase;
            _contextTenant = contextTenant;
            _migrationTenantCommon = new MigrationTenantCommon(contextTenant, _sqlConnectionTenantDatabase.ConnectionString);
        }

        public void MapProjects(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapProjects - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Projects> list = dataList.Cast<Projects>().ToList();
            List<Projects> listFilter = new List<Projects>();
            List<Projects> listContext = _contextTenant.Projects.ToList();

            Parallel.ForEach(list,
                () => { return new List<Projects>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.IsActive = true;
                        item.StartDate = item.StartDate.ToUniversalTime();
                        item.EndDate = item.EndDate.ToUniversalTime();
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

        public void MapExtraFields(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapExtraFields - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ExtraFields> list = dataList.Cast<ExtraFields>().ToList();
            List<ExtraFields> listFilter = new List<ExtraFields>();
            List<ExtraFields> listContext = _contextTenant.ExtraFields.ToList();

            Parallel.ForEach(list,
                () => { return new List<ExtraFields>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        if (item.Type == 0) item.Type = 12;
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

        public void MapCollectionsExtraFieldExtraField(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapCollectionsExtraFieldExtraField - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<CollectionsExtraFieldExtraField> list = dataList.Cast<CollectionsExtraFieldExtraField>().ToList();
            List<CollectionsExtraFieldExtraField> listFilter = new List<CollectionsExtraFieldExtraField>();
            List<CollectionsExtraFieldExtraField> listContext = _contextTenant.CollectionsExtraFieldExtraField.ToList();

            Parallel.ForEach(list,
                () => { return new List<CollectionsExtraFieldExtraField>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.CollectionsExtraFieldId.Equals(item.CollectionsExtraFieldId) && c.ExtraFieldId.Equals(item.ExtraFieldId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        //TODO:no esta probado con datos
        public void MapCompaniesCostHistorical(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapCompaniesCostHistorical - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<CompaniesCostHistorical> list = dataList.Cast<CompaniesCostHistorical>().ToList();
            List<CompaniesCostHistorical> listFilter = new List<CompaniesCostHistorical>();
            List<CompaniesCostHistorical> listContext = _contextTenant.CompaniesCostHistorical.ToList();

            Parallel.ForEach(list,
                () => { return new List<CompaniesCostHistorical>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.Until = item.Until.ToUniversalTime();
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

        public void MapClosingCodes(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapClosingCodes - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ClosingCodes> list = dataList.Cast<ClosingCodes>().ToList();
            List<ClosingCodes> listFilter = new List<ClosingCodes>();
            List<ClosingCodes> listContext = _contextTenant.ClosingCodes.ToList();

            Parallel.ForEach(list,
                () => { return new List<ClosingCodes>(); },
                (item, state, localList) =>
                {
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

        public void MapSubContracts(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapSubContracts - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<SubContracts> list = dataList.Cast<SubContracts>().ToList();
            List<SubContracts> listFilter = new List<SubContracts>();
            List<SubContracts> listContext = _contextTenant.SubContracts.ToList();

            Parallel.ForEach(list,
                () => { return new List<SubContracts>(); },
                (item, state, localList) =>
                {
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

        public void MapDepartments(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapDepartments - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Departments> list = dataList.Cast<Departments>().ToList();
            List<Departments> listFilter = new List<Departments>();
            List<Departments> listContext = _contextTenant.Departments.ToList();

            Parallel.ForEach(list,
                () => { return new List<Departments>(); },
                (item, state, localList) =>
                {
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

        public void MapErpSystemInstanceQuery(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapErpSystemInstanceQuery - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ErpSystemInstanceQuery> list = dataList.Cast<ErpSystemInstanceQuery>().ToList();
            List<ErpSystemInstanceQuery> listFilter = new List<ErpSystemInstanceQuery>();
            List<ErpSystemInstanceQuery> listContext = _contextTenant.ErpSystemInstanceQuery.ToList();

            Parallel.ForEach(list,
                () => { return new List<ErpSystemInstanceQuery>(); },
                (item, state, localList) =>
                {
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

        public void MapStatesSla(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapStatesSla - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<StatesSla> list = dataList.Cast<StatesSla>().ToList();
            List<StatesSla> listFilter = new List<StatesSla>();
            List<StatesSla> listContext = _contextTenant.StatesSla.ToList();

            Parallel.ForEach(list,
                () => { return new List<StatesSla>(); },
                (item, state, localList) =>
                {
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

        public void MapFormElements(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapFormElements - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<FormElements> list = dataList.Cast<FormElements>().ToList();
            List<FormElements> listFilter = new List<FormElements>();
            List<FormElements> listContext = _contextTenant.FormElements.ToList();

            Parallel.ForEach(list,
                () => { return new List<FormElements>(); },
                (item, state, localList) =>
                {
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

        public void MapItemsPointsRate(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapItemsPointsRate - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ItemsPointsRate> list = dataList.Cast<ItemsPointsRate>().ToList();
            List<ItemsPointsRate> listFilter = new List<ItemsPointsRate>();
            List<ItemsPointsRate> listContext = _contextTenant.ItemsPointsRate.ToList();

            Parallel.ForEach(list,
                () => { return new List<ItemsPointsRate>(); },
                (item, state, localList) =>
                {
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

        public void MapItemsPurchaseRate(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapItemsPurchaseRate - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ItemsPurchaseRate> list = dataList.Cast<ItemsPurchaseRate>().ToList();
            List<ItemsPurchaseRate> listFilter = new List<ItemsPurchaseRate>();
            List<ItemsPurchaseRate> listContext = _contextTenant.ItemsPurchaseRate.ToList();

            Parallel.ForEach(list,
                () => { return new List<ItemsPurchaseRate>(); },
                (item, state, localList) =>
                {
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

        public void MapItemsSalesRate(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapItemsSalesRate - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ItemsSalesRate> list = dataList.Cast<ItemsSalesRate>().ToList();
            List<ItemsSalesRate> listFilter = new List<ItemsSalesRate>();
            List<ItemsSalesRate> listContext = _contextTenant.ItemsSalesRate.ToList();

            Parallel.ForEach(list,
                () => { return new List<ItemsSalesRate>(); },
                (item, state, localList) =>
                {
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

        public void MapItemsSerialNumber(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapItemsSerialNumber - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ItemsSerialNumber> list = dataList.Cast<ItemsSerialNumber>().ToList();
            List<ItemsSerialNumber> listFilter = new List<ItemsSerialNumber>();
            List<ItemsSerialNumber> listContext = _contextTenant.ItemsSerialNumber.ToList();
            
            Parallel.ForEach(list,
                () => { return new List<ItemsSerialNumber>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ItemId == item.ItemId && c.SerialNumber == item.SerialNumber))
                    {
                        if (localList.Any(c => c.ItemId == item.ItemId && c.SerialNumber == item.SerialNumber)) Console.WriteLine($"Ya existe uno en la lista {item.ItemId} - {item.SerialNumber}");
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();
            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapMainWoViewConfigurationsColumns(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapMainWoViewConfigurationsColumns - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<MainWoViewConfigurationsColumns> list = dataList.Cast<MainWoViewConfigurationsColumns>().ToList();
            List<MainWoViewConfigurationsColumns> listFilter = new List<MainWoViewConfigurationsColumns>();
            List<MainWoViewConfigurationsColumns> listContext = _contextTenant.MainWoViewConfigurationsColumns.ToList();

            Parallel.ForEach(list,
                () => { return new List<MainWoViewConfigurationsColumns>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.UserMainWoviewConfigurationId.Equals(item.UserMainWoviewConfigurationId) && c.ColumnId.Equals(item.ColumnId)))
                    {
                        item.FilterStartDate = item.FilterStartDate.HasValue ? item.FilterStartDate.Value.ToUniversalTime() : item.FilterStartDate.GetValueOrDefault();
                        item.FilterEndDate = item.FilterEndDate.HasValue ? item.FilterEndDate.Value.ToUniversalTime() : item.FilterEndDate.GetValueOrDefault();
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapModels(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapModels - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Models> list = dataList.Cast<Models>().ToList();
            List<Models> listFilter = new List<Models>();
            List<Models> listContext = _contextTenant.Models.ToList();

            Parallel.ForEach(list,
                () => { return new List<Models>(); },
                (item, state, localList) =>
                {
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

        public void MapPeopleCollectionCalendars(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPeopleCollectionCalendars - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PeopleCollectionCalendars> list = dataList.Cast<PeopleCollectionCalendars>().ToList();
            List<PeopleCollectionCalendars> listFilter = new List<PeopleCollectionCalendars>();
            List<PeopleCollectionCalendars> listContext = _contextTenant.PeopleCollectionCalendars.ToList();

            Parallel.ForEach(list,
                () => { return new List<PeopleCollectionCalendars>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.PeopleCollectionId.Equals(item.PeopleCollectionId) && c.CalendarId.Equals(item.CalendarId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPlanificationProcesses(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPlanificationProcesses - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PlanificationProcesses> list = dataList.Cast<PlanificationProcesses>().ToList();
            List<PlanificationProcesses> listFilter = new List<PlanificationProcesses>();
            List<PlanificationProcesses> listContext = _contextTenant.PlanificationProcesses.ToList();

            Parallel.ForEach(list,
                () => { return new List<PlanificationProcesses>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.LastModification = item.LastModification.HasValue ? item.LastModification.Value.ToUniversalTime() : item.LastModification.GetValueOrDefault();
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

        public void MapPushNotificationsPeopleCollections(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPushNotificationsPeopleCollections - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PushNotificationsPeopleCollections> list = dataList.Cast<PushNotificationsPeopleCollections>().ToList();
            List<PushNotificationsPeopleCollections> listFilter = new List<PushNotificationsPeopleCollections>();
            List<PushNotificationsPeopleCollections> listContext = _contextTenant.PushNotificationsPeopleCollections.ToList();

            Parallel.ForEach(list,
                () => { return new List<PushNotificationsPeopleCollections>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.NotificationId.Equals(item.Notification) && c.PeopleCollectionsId.Equals(item.PeopleCollectionsId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapServicesViewConfigurationsColumns(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapServicesViewConfigurationsColumns - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ServicesViewConfigurationsColumns> list = dataList.Cast<ServicesViewConfigurationsColumns>().ToList();
            List<ServicesViewConfigurationsColumns> listFilter = new List<ServicesViewConfigurationsColumns>();
            List<ServicesViewConfigurationsColumns> listContext = _contextTenant.ServicesViewConfigurationsColumns.ToList();

            Parallel.ForEach(list,
                () => { return new List<ServicesViewConfigurationsColumns>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id) && c.ColumnId.Equals(item.ColumnId)))
                    {
                        item.FilterStartDate = item.FilterStartDate.HasValue ? item.FilterStartDate.Value.ToUniversalTime() : item.FilterStartDate.GetValueOrDefault();
                        item.FilterEndDate = item.FilterStartDate.HasValue ? item.FilterStartDate.Value.ToUniversalTime() : item.FilterStartDate.GetValueOrDefault();
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
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapSubFamilies(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapSubFamilies - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<SubFamilies> list = dataList.Cast<SubFamilies>().ToList();
            List<SubFamilies> listFilter = new List<SubFamilies>();
            List<SubFamilies> listContext = _contextTenant.SubFamilies.ToList();

            Parallel.ForEach(list,
                () => { return new List<SubFamilies>(); },
                (item, state, localList) =>
                {
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

        public void MapWorkOrderTypes(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrderTypes - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<WorkOrderTypes> list = dataList.Cast<WorkOrderTypes>().ToList();
            List<WorkOrderTypes> listFilter = new List<WorkOrderTypes>();
            List<WorkOrderTypes> listContext = _contextTenant.WorkOrderTypes.ToList();

            Parallel.ForEach(list,
                () => { return new List<WorkOrderTypes>(); },
                (item, state, localList) =>
                {
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

        public void MapWorkOrderCategories(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrderCategories - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<WorkOrderCategories> list = dataList.Cast<WorkOrderCategories>().ToList();
            List<WorkOrderCategories> listFilter = new List<WorkOrderCategories>();
            List<WorkOrderCategories> listContext = _contextTenant.WorkOrderCategories.ToList();

            Parallel.ForEach(list,
                () => { return new List<WorkOrderCategories>(); },
                (item, state, localList) =>
                {
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

        public void MapWorkOrderCategoriesCollectionCalendar(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrderCategoriesCollectionCalendar - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<WorkOrderCategoriesCollectionCalendar> list = dataList.Cast<WorkOrderCategoriesCollectionCalendar>().ToList();
            List<WorkOrderCategoriesCollectionCalendar> listFilter = new List<WorkOrderCategoriesCollectionCalendar>();
            List<WorkOrderCategoriesCollectionCalendar> listContext = _contextTenant.WorkOrderCategoriesCollectionCalendar.ToList();

            Parallel.ForEach(list,
                () => { return new List<WorkOrderCategoriesCollectionCalendar>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.WorkOrderCategoriesCollectionId.Equals(item.WorkOrderCategoriesCollectionId) && c.CalendarId.Equals(item.CalendarId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapWorkOrderCategoryCalendar(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrderCategoryCalendar - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<WorkOrderCategoryCalendar> list = dataList.Cast<WorkOrderCategoryCalendar>().ToList();
            List<WorkOrderCategoryCalendar> listFilter = new List<WorkOrderCategoryCalendar>();
            List<WorkOrderCategoryCalendar> listContext = _contextTenant.WorkOrderCategoryCalendar.ToList();

            Parallel.ForEach(list,
                () => { return new List<WorkOrderCategoryCalendar>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.WorkOrderCategoryId.Equals(item.WorkOrderCategoryId) && c.CalendarId.Equals(item.CalendarId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapWorkOrderCategoryKnowledge(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrderCategoryKnowledge - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<WorkOrderCategoryKnowledge> list = dataList.Cast<WorkOrderCategoryKnowledge>().ToList();
            List<WorkOrderCategoryKnowledge> listFilter = new List<WorkOrderCategoryKnowledge>();
            List<WorkOrderCategoryKnowledge> listContext = _contextTenant.WorkOrderCategoryKnowledge.ToList();

            Parallel.ForEach(list,
                () => { return new List<WorkOrderCategoryKnowledge>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.WorkOrderCategoryId.Equals(item.WorkOrderCategoryId) && c.KnowledgeId.Equals(item.KnowledgeId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapZoneProject(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapZoneProject - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ZoneProject> list = dataList.Cast<ZoneProject>().ToList();
            List<ZoneProject> listFilter = new List<ZoneProject>();
            List<ZoneProject> listContext = _contextTenant.ZoneProject.ToList();

            Parallel.ForEach(list,
                () => { return new List<ZoneProject>(); },
                (item, state, localList) =>
                {
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

        public void MapZoneProjectPostalCode(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapZoneProjectPostalCode - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ZoneProjectPostalCode> list = dataList.Cast<ZoneProjectPostalCode>().ToList();
            List<ZoneProjectPostalCode> listFilter = new List<ZoneProjectPostalCode>();
            List<ZoneProjectPostalCode> listContext = _contextTenant.ZoneProjectPostalCode.ToList();

            Parallel.ForEach(list,
                () => { return new List<ZoneProjectPostalCode>(); },
                (item, state, localList) =>
                {
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

        public void MapWsErpSystemInstance(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWsErpSystemInstance - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<WsErpSystemInstance> list = dataList.Cast<WsErpSystemInstance>().ToList();
            List<WsErpSystemInstance> listFilter = new List<WsErpSystemInstance>();
            List<WsErpSystemInstance> listContext = _contextTenant.WsErpSystemInstance.ToList();

            Parallel.ForEach(list,
                () => { return new List<WsErpSystemInstance>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ErpSystemInstanceId.Equals(item.ErpSystemInstanceId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPredefinedServices(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPredefinedServices - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PredefinedServices> list = dataList.Cast<PredefinedServices>().ToList();
            List<PredefinedServices> listFilter = new List<PredefinedServices>();
            List<PredefinedServices> listContext = _contextTenant.PredefinedServices.ToList();

            Parallel.ForEach(list,
                () => { return new List<PredefinedServices>(); },
                (item, state, localList) =>
                {
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

        public void MapSiteUser(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapSiteUser - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<SiteUser> list = dataList.Cast<SiteUser>().ToList();
            List<SiteUser> listFilter = new List<SiteUser>();
            List<SiteUser> listContext = _contextTenant.SiteUser.ToList();

            Parallel.ForEach(list,
                () => { return new List<SiteUser>(); },
                (item, state, localList) =>
                {
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

        public void MapAssets(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapTeams - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Assets> list = dataList.Cast<Assets>().ToList();
            List<Assets> listFilter = new List<Assets>();
            List<Assets> listContext = _contextTenant.Assets.ToList();

            Parallel.ForEach(list,
                () => { return new List<Assets>(); },
                (item, state, localList) =>
                {
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

        public void MapTasks(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapTasks - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Tasks> list = dataList.Cast<Tasks>().ToList();
            List<Tasks> listFilter = new List<Tasks>();
            List<Tasks> listContext = _contextTenant.Tasks.ToList();

            Parallel.ForEach(list,
                () => { return new List<Tasks>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.DateValue = item.DateValue.HasValue ? item.DateValue.Value.ToUniversalTime() : item.DateValue.GetValueOrDefault();
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

        public void MapAssetsAudit(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapAssetsAudit - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<AssetsAudit> list = dataList.Cast<AssetsAudit>().ToList();
            List<AssetsAudit> listFilter = new List<AssetsAudit>();
            List<AssetsAudit> listContext = _contextTenant.AssetsAudit.ToList();

            Parallel.ForEach(list,
                () => { return new List<AssetsAudit>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.RegistryDate = item.RegistryDate.ToUniversalTime();
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

        public void MapAssetsAuditChanges(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapAssetsAuditChanges - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<AssetsAuditChanges> list = dataList.Cast<AssetsAuditChanges>().ToList();
            List<AssetsAuditChanges> listFilter = new List<AssetsAuditChanges>();
            List<AssetsAuditChanges> listContext = _contextTenant.AssetsAuditChanges.ToList();

            Parallel.ForEach(list,
                () => { return new List<AssetsAuditChanges>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.AssetAuditId.Equals(item.AssetAuditId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapBill(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapBill - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Bill> list = dataList.Cast<Bill>().ToList();
            List<Bill> listFilter = new List<Bill>();
            List<Bill> listContext = _contextTenant.Bill.ToList();

            Parallel.ForEach(list,
                () => { return new List<Bill>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.Date = item.Date.ToUniversalTime();
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

        public void MapBillingItems(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapBillingItems - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<BillingItems> list = dataList.Cast<BillingItems>().ToList();
            List<BillingItems> listFilter = new List<BillingItems>();
            List<BillingItems> listContext = _contextTenant.BillingItems.ToList();

            Parallel.ForEach(list,
                () => { return new List<BillingItems>(); },
                (item, state, localList) =>
                {
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

        public void MapBillingLineItems(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapBillingLineItems - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<BillingLineItems> list = dataList.Cast<BillingLineItems>().ToList();
            List<BillingLineItems> listFilter = new List<BillingLineItems>();
            List<BillingLineItems> listContext = _contextTenant.BillingLineItems.ToList();

            Parallel.ForEach(list,
                () => { return new List<BillingLineItems>(); },
                (item, state, localList) =>
                {
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

        public void MapBillLine(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapBillLine - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<BillLine> list = dataList.Cast<BillLine>().ToList();
            List<BillLine> listFilter = new List<BillLine>();
            List<BillLine> listContext = _contextTenant.BillLine.ToList();

            Parallel.ForEach(list,
                () => { return new List<BillLine>(); },
                (item, state, localList) =>
                {
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

        public void MapPostconditionCollections(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPostconditionCollections - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PostconditionCollections> list = dataList.Cast<PostconditionCollections>().ToList();
            List<PostconditionCollections> listFilter = new List<PostconditionCollections>();
            List<PostconditionCollections> listContext = _contextTenant.PostconditionCollections.ToList();

            Parallel.ForEach(list,
                () => { return new List<PostconditionCollections>(); },
                (item, state, localList) =>
                {
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

        public void MapPostconditions(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPostconditions - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Postconditions> list = dataList.Cast<Postconditions>().ToList();
            List<Postconditions> listFilter = new List<Postconditions>();
            List<Postconditions> listContext = _contextTenant.Postconditions.ToList();

            Parallel.ForEach(list,
                () => { return new List<Postconditions>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.DateValue = item.DateValue.HasValue ? item.DateValue.Value.ToUniversalTime() : item.DateValue.GetValueOrDefault();
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

        public void MapVehicles(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapVehicles - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Vehicles> list = dataList.Cast<Vehicles>().ToList();
            List<Vehicles> listFilter = new List<Vehicles>();
            List<Vehicles> listContext = _contextTenant.Vehicles.ToList();

            Parallel.ForEach(list,
                () => { return new List<Vehicles>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.Date = item.Date.HasValue ? item.Date.Value.ToUniversalTime() : item.Date.GetValueOrDefault();
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

        public void MapTools(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapTools - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Tools> list = dataList.Cast<Tools>().ToList();
            List<Tools> listFilter = new List<Tools>();
            List<Tools> listContext = _contextTenant.Tools.ToList();

            Parallel.ForEach(list,
                () => { return new List<Tools>(); },
                (item, state, localList) =>
                {
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

        public void MapExpenses(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapExpenses - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Expenses> list = dataList.Cast<Expenses>().ToList();
            List<Expenses> listFilter = new List<Expenses>();
            List<Expenses> listContext = _contextTenant.Expenses.ToList();

            Parallel.ForEach(list,
                () => { return new List<Expenses>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.Date = item.Date.ToUniversalTime();
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
                string[] copyParameters = new[]
                {
                    nameof(Expenses.Id),
                    nameof(Expenses.UpdateDate),
                    nameof(Expenses.ExpenseTypeId),
                    nameof(Expenses.PaymentMethodId),
                    nameof(Expenses.Description),
                    nameof(Expenses.Amount),
                    nameof(Expenses.Date),
                    nameof(Expenses.ExpenseTicketId),
                    nameof(Expenses.Factor)
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters);
            }
        }

        public void MapExternalServicesConfiguration(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapExternalServicesConfiguration - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ExternalServicesConfiguration> list = dataList.Cast<ExternalServicesConfiguration>().ToList();
            List<ExternalServicesConfiguration> listFilter = new List<ExternalServicesConfiguration>();
            List<ExternalServicesConfiguration> listContext = _contextTenant.ExternalServicesConfiguration.ToList();

            Parallel.ForEach(list,
                () => { return new List<ExternalServicesConfiguration>(); },
                (item, state, localList) =>
                {
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

        public void MapExternalServicesConfigurationSites(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapExternalServicesConfigurationSites - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ExternalServicesConfigurationSites> list = dataList.Cast<ExternalServicesConfigurationSites>().ToList();
            List<ExternalServicesConfigurationSites> listFilter = new List<ExternalServicesConfigurationSites>();
            List<ExternalServicesConfigurationSites> listContext = _contextTenant.ExternalServicesConfigurationSites.ToList();

            Parallel.ForEach(list,
                () => { return new List<ExternalServicesConfigurationSites>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ExternalServicesConfigurationId.Equals(item.ExternalServicesConfigurationId) && c.FinalClientId.Equals(item.FinalClientId) && c.ExtClientId.Equals(item.ExtClientId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapExternalServicesConfigurationProjectCategories(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapExternalServicesConfigurationProjectCategories - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ExternalServicesConfigurationProjectCategories> list = dataList.Cast<ExternalServicesConfigurationProjectCategories>().ToList();
            List<ExternalServicesConfigurationProjectCategories> listFilter = new List<ExternalServicesConfigurationProjectCategories>();
            List<ExternalServicesConfigurationProjectCategories> listContext = _contextTenant.ExternalServicesConfigurationProjectCategories.ToList();

            Parallel.ForEach(list,
                () => { return new List<ExternalServicesConfigurationProjectCategories>(); },
                (item, state, localList) =>
                {
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

        public void MapExternalServicesConfigurationProjectCategoriesProperties(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapExternalServicesConfigurationProjectCategoriesProperties - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ExternalServicesConfigurationProjectCategoriesProperties> list = dataList.Cast<ExternalServicesConfigurationProjectCategoriesProperties>().ToList();
            List<ExternalServicesConfigurationProjectCategoriesProperties> listFilter = new List<ExternalServicesConfigurationProjectCategoriesProperties>();
            List<ExternalServicesConfigurationProjectCategoriesProperties> listContext = _contextTenant.ExternalServicesConfigurationProjectCategoriesProperties.ToList();

            Parallel.ForEach(list,
                () => { return new List<ExternalServicesConfigurationProjectCategoriesProperties>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ExternalServicesConfigurationProjectCategoriesId.Equals(item.ExternalServicesConfigurationProjectCategoriesId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapBillingRule(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapBillingRule - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<BillingRule> list = dataList.Cast<BillingRule>().ToList();
            List<BillingRule> listFilter = new List<BillingRule>();
            List<BillingRule> listContext = _contextTenant.BillingRule.ToList();

            Parallel.ForEach(list,
                () => { return new List<BillingRule>(); },
                (item, state, localList) =>
                {
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

        public void MapBillingRuleItem(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapBillingRuleItem - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<BillingRuleItem> list = dataList.Cast<BillingRuleItem>().ToList();
            List<BillingRuleItem> listFilter = new List<BillingRuleItem>();
            List<BillingRuleItem> listContext = _contextTenant.BillingRuleItem.ToList();

            Parallel.ForEach(list,
                () => { return new List<BillingRuleItem>(); },
                (item, state, localList) =>
                {
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

        public void MapJourneys(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapJourneys - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Journeys> list = dataList.Cast<Journeys>().ToList();
            List<Journeys> listFilter = new List<Journeys>();
            List<Journeys> listContext = _contextTenant.Journeys.ToList();

            Parallel.ForEach(list,
                () => { return new List<Journeys>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.StartDate = item.StartDate.ToUniversalTime();
                        item.EndDate = item.EndDate.HasValue ? item.EndDate.Value.ToUniversalTime() : item.EndDate.GetValueOrDefault();
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
                string[] copyParameters = new[]
                {
                    nameof(Journeys.Id),
                    nameof(Journeys.UpdateDate),
                    nameof(Journeys.PeopleId),
                    nameof(Journeys.StartDate),
                    nameof(Journeys.EndDate),
                    nameof(Journeys.IsCompanyVehicle),
                    nameof(Journeys.CompanyVehicleId),
                    nameof(Journeys.Finished),
                    nameof(Journeys.StartKm),
                    nameof(Journeys.EndKm),
                    nameof(Journeys.Observations)
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters);
            }
        }

        public void MapPreconditions(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPreconditions - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Preconditions> list = dataList.Cast<Preconditions>().ToList();
            List<Preconditions> listFilter = new List<Preconditions>();
            List<Preconditions> listContext = _contextTenant.Preconditions.ToList();

            Parallel.ForEach(list,
                () => { return new List<Preconditions>(); },
                (item, state, localList) =>
                {
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
                string[] copyParameters = new[]
                {
                    nameof(Preconditions.Id),
                    nameof(Preconditions.UpdateDate),
                    nameof(Preconditions.TaskId),
                    nameof(Preconditions.PostconditionCollectionId),
                    nameof(Preconditions.PeopleResponsibleTechniciansCollectionId)
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters);
            }
        }

        public void MapLiteralsPreconditions(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapLiteralsPreconditions - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<LiteralsPreconditions> list = dataList.Cast<LiteralsPreconditions>().ToList();
            List<LiteralsPreconditions> listFilter = new List<LiteralsPreconditions>();
            List<LiteralsPreconditions> listContext = _contextTenant.LiteralsPreconditions.ToList();

            Parallel.ForEach(list,
                () => { return new List<LiteralsPreconditions>(); },
                (item, state, localList) =>
                {
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

        public void MapWorkOrdersDeritative(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrdersDeritative - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<WorkOrdersDeritative> list = dataList.Cast<WorkOrdersDeritative>().ToList();
            List<WorkOrdersDeritative> listFilter = new List<WorkOrdersDeritative>();
            List<WorkOrdersDeritative> listContext = _contextTenant.WorkOrdersDeritative.ToList();

            Parallel.ForEach(list,
                () => { return new List<WorkOrdersDeritative>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.CreationDate = item.CreationDate.HasValue ? item.CreationDate.Value.ToUniversalTime() : item.CreationDate.GetValueOrDefault();
                        item.PickUpTime = item.PickUpTime.HasValue ? item.PickUpTime.Value.ToUniversalTime() : item.PickUpTime.GetValueOrDefault();
                        item.FinalClientClosingTime = item.FinalClientClosingTime.HasValue ? item.FinalClientClosingTime.Value.ToUniversalTime() : item.FinalClientClosingTime.GetValueOrDefault();
                        item.InternalClosingTime = item.InternalClosingTime.HasValue ? item.InternalClosingTime.Value.ToUniversalTime() : item.InternalClosingTime.GetValueOrDefault();
                        item.AssignmentTime = item.AssignmentTime.HasValue ? item.AssignmentTime.Value.ToUniversalTime() : item.AssignmentTime.GetValueOrDefault();
                        item.ActionDate = item.ActionDate.HasValue ? item.ActionDate.Value.ToUniversalTime() : item.ActionDate.GetValueOrDefault();
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

        public void MapDerivedServices(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapDerivedServices - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<DerivedServices> list = dataList.Cast<DerivedServices>().ToList();
            List<DerivedServices> listFilter = new List<DerivedServices>();
            List<DerivedServices> listContext = _contextTenant.DerivedServices.ToList();

            Parallel.ForEach(list,
                () => { return new List<DerivedServices>(); },
                (item, state, localList) =>
                {
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

        public void MapExtraFieldsValues(IEnumerable<object> dataList, KeyValuePair<string, string> val, int skip)
        {
            Console.WriteLine($"MapExtraFieldsValues - Registros a tratar: {dataList.Count()} apartir del {skip}");
            object localLockObject = new object();

            List<ExtraFieldsValues> list = dataList.Cast<ExtraFieldsValues>().ToList();
            List<ExtraFieldsValues> listFilter = new List<ExtraFieldsValues>();

            Parallel.ForEach(list,
                () => { return new List<ExtraFieldsValues>(); },
                (item, state, localList) =>
                {
                    item.DataValue = item.DataValue.HasValue ? item.DataValue.Value.ToUniversalTime() : item.DataValue.GetValueOrDefault();
                    item.UpdateDate = DateTime.UtcNow;
                    localList.Add(item);
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            if (listFilter.Any())
            {
                string[] copyParameters = new[]
                {
                    nameof(ExtraFieldsValues.Id),
                    nameof(ExtraFieldsValues.UpdateDate),
                    nameof(ExtraFieldsValues.ServiceId),
                    nameof(ExtraFieldsValues.WorkOrderDeritativeId),
                    nameof(ExtraFieldsValues.DerivedServiceId),
                    nameof(ExtraFieldsValues.WorkOrderId),
                    nameof(ExtraFieldsValues.ExtraFieldId),
                    nameof(ExtraFieldsValues.EnterValue),
                    nameof(ExtraFieldsValues.DataValue),
                    nameof(ExtraFieldsValues.DecimalValue),
                    nameof(ExtraFieldsValues.BooleaValue),
                    nameof(ExtraFieldsValues.StringValue),
                    nameof(ExtraFieldsValues.Signature)
                };

                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters);
            }
        }

        public void MapMaterialForm(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapMaterialForm - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<MaterialForm> list = dataList.Cast<MaterialForm>().ToList();
            List<MaterialForm> listFilter = new List<MaterialForm>();
            List<MaterialForm> listContext = _contextTenant.MaterialForm.ToList();

            Parallel.ForEach(list,
                () => { return new List<MaterialForm>(); },
                (item, state, localList) =>
                {
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
                string[] copyParameters = new[]
                {
                    nameof(MaterialForm.Id),
                    nameof(MaterialForm.UpdateDate),
                    nameof(MaterialForm.ExtraFieldValueId),
                    nameof(MaterialForm.SerialNumber),
                    nameof(MaterialForm.Reference),
                    nameof(MaterialForm.Description),
                    nameof(MaterialForm.Units),
                    nameof(MaterialForm.AssetId)
                };

                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters);
            }
        }

        public void MapPeopleCost(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPeopleCost - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PeopleCost> list = dataList.Cast<PeopleCost>().ToList();
            List<PeopleCost> listFilter = new List<PeopleCost>();
            List<PeopleCost> listContext = _contextTenant.PeopleCost.ToList();

            Parallel.ForEach(list,
                () => { return new List<PeopleCost>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
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

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
            }
        }

        public void MapPeopleCostHistorical(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPeopleCostHistorical - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PeopleCostHistorical> list = dataList.Cast<PeopleCostHistorical>().ToList();
            List<PeopleCostHistorical> listFilter = new List<PeopleCostHistorical>();
            List<PeopleCostHistorical> listContext = _contextTenant.PeopleCostHistorical.ToList();

            Parallel.ForEach(list,
                () => { return new List<PeopleCostHistorical>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.Until = item.Until.ToUniversalTime();
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

        public void MapPlanningPanelViewConfiguration(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPlanningPanelViewConfiguration - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PlanningPanelViewConfiguration> list = dataList.Cast<PlanningPanelViewConfiguration>().ToList();
            List<PlanningPanelViewConfiguration> listFilter = new List<PlanningPanelViewConfiguration>();
            List<PlanningPanelViewConfiguration> listContext = _contextTenant.PlanningPanelViewConfiguration.ToList();

            Parallel.ForEach(list,
                () => { return new List<PlanningPanelViewConfiguration>(); },
                (item, state, localList) =>
                {
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

        public void MapSaltoCsversion(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapSaltoCsversion - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<SaltoCsversion> list = dataList.Cast<SaltoCsversion>().ToList();
            List<SaltoCsversion> listFilter = new List<SaltoCsversion>();
            List<SaltoCsversion> listContext = _contextTenant.SaltoCsversion.ToList();

            Parallel.ForEach(list,
                () => { return new List<SaltoCsversion>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Version.Equals(item.Version)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapSynchronizationSessions(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapSynchronizationSessions - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<SynchronizationSessions> list = dataList.Cast<SynchronizationSessions>().ToList();
            List<SynchronizationSessions> listFilter = new List<SynchronizationSessions>();
            List<SynchronizationSessions> listContext = _contextTenant.SynchronizationSessions.ToList();

            Parallel.ForEach(list,
                () => { return new List<SynchronizationSessions>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.StartDate = item.StartDate.ToUniversalTime();
                        item.EndDate = item.EndDate.HasValue ? item.EndDate.Value.ToUniversalTime() : item.EndDate.GetValueOrDefault();
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

        public void MapSgsClosingInfo(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapSgsClosingInfo - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<SgsClosingInfo> list = dataList.Cast<SgsClosingInfo>().ToList();
            List<SgsClosingInfo> listFilter = new List<SgsClosingInfo>();
            List<SgsClosingInfo> listContext = _contextTenant.SgsClosingInfo.ToList();

            Parallel.ForEach(list,
                () => { return new List<SgsClosingInfo>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.SentDate = item.SentDate.ToUniversalTime();
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

        public void MapTechnicalCodes(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapTechnicalCodes - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<TechnicalCodes> list = dataList.Cast<TechnicalCodes>().ToList();
            List<TechnicalCodes> listFilter = new List<TechnicalCodes>();
            List<TechnicalCodes> listContext = _contextTenant.TechnicalCodes.ToList();

            Parallel.ForEach(list,
                () => { return new List<TechnicalCodes>(); },
                (item, state, localList) =>
                {
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

        public void MapPreconditionsLiteralValues(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPreconditionsLiteralValues - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PreconditionsLiteralValues> list = dataList.Cast<PreconditionsLiteralValues>().ToList();
            List<PreconditionsLiteralValues> listFilter = new List<PreconditionsLiteralValues>();
            List<PreconditionsLiteralValues> listContext = _contextTenant.PreconditionsLiteralValues.ToList();

            Parallel.ForEach(list,
                () => { return new List<PreconditionsLiteralValues>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.DataValue = item.DataValue.HasValue ? item.DataValue.Value.ToUniversalTime() : item.DataValue.GetValueOrDefault();
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

        #region Relations MN

        public void MapCalendarPlanningViewConfigurationPeople(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapCalendarPlanningViewConfigurationPeople - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<CalendarPlanningViewConfigurationPeople> list = dataList.Cast<CalendarPlanningViewConfigurationPeople>().ToList();
            List<CalendarPlanningViewConfigurationPeople> listFilter = new List<CalendarPlanningViewConfigurationPeople>();
            //List<CalendarPlanningViewConfigurationPeople> listContext = _contextTenant.CalendarPlanningViewConfigurationPeople.ToList();

            Parallel.ForEach(list,
                () => { return new List<CalendarPlanningViewConfigurationPeople>(); },
                (item, state, localList) =>
                {
                    //if (!listContext.Any(c => c.PeopleId.Equals(item.PeopleId) && c.ViewId.Equals(item.ViewId)))
                    //{
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
                    nameof(CalendarPlanningViewConfigurationPeople.ViewId),
                    nameof(CalendarPlanningViewConfigurationPeople.PeopleId)
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters, false);
            }
        }

        public void MapCalendarPlanningViewConfigurationPeopleCollection(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapCalendarPlanningViewConfigurationPeopleCollection - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<CalendarPlanningViewConfigurationPeopleCollection> list = dataList.Cast<CalendarPlanningViewConfigurationPeopleCollection>().ToList();
            List<CalendarPlanningViewConfigurationPeopleCollection> listFilter = new List<CalendarPlanningViewConfigurationPeopleCollection>();
            //List<CalendarPlanningViewConfigurationPeopleCollection> listContext = _contextTenant.CalendarPlanningViewConfigurationPeopleCollection.ToList();

            Parallel.ForEach(list,
                () => { return new List<CalendarPlanningViewConfigurationPeopleCollection>(); },
                (item, state, localList) =>
                {
                    //if (!listContext.Any(c => c.PeopleCollectionId.Equals(item.PeopleCollectionId) && c.ViewId.Equals(item.ViewId)))
                    //{
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
                    nameof(CalendarPlanningViewConfigurationPeopleCollection.ViewId),
                    nameof(CalendarPlanningViewConfigurationPeopleCollection.PeopleCollectionId)
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters, false);
            }
        }

        public void MapFinalClientSiteCalendar(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapFinalClientSiteCalendar - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<FinalClientSiteCalendar> list = dataList.Cast<FinalClientSiteCalendar>().ToList();
            List<FinalClientSiteCalendar> listFilter = new List<FinalClientSiteCalendar>();
            List<FinalClientSiteCalendar> listContext = _contextTenant.FinalClientSiteCalendar.ToList();

            Parallel.ForEach(list,
                () => { return new List<FinalClientSiteCalendar>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.FinalClientSiteId.Equals(item.FinalClientSiteId) && c.CalendarId.Equals(item.CalendarId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        //TODO:no esta probado con datos
        public void MapKnowledgeSubContracts(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapKnowledgeSubContracts - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<KnowledgeSubContracts> list = dataList.Cast<KnowledgeSubContracts>().ToList();
            List<KnowledgeSubContracts> listFilter = new List<KnowledgeSubContracts>();
            List<KnowledgeSubContracts> listContext = _contextTenant.KnowledgeSubContracts.ToList();

            Parallel.ForEach(list,
                () => { return new List<KnowledgeSubContracts>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.KnowledgeId.Equals(item.KnowledgeId) && c.SubContractId.Equals(item.SubContractId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        //TODO:no esta probado con datos
        public void MapKnowledgeToolsType(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapKnowledgeToolsType - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<KnowledgeToolsType> list = dataList.Cast<KnowledgeToolsType>().ToList();
            List<KnowledgeToolsType> listFilter = new List<KnowledgeToolsType>();
            List<KnowledgeToolsType> listContext = _contextTenant.KnowledgeToolsType.ToList();

            Parallel.ForEach(list,
                () => { return new List<KnowledgeToolsType>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.KnowledgeId.Equals(item.KnowledgeId) && c.ToolsTypeId.Equals(item.ToolsTypeId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapKnowledgePeople(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapKnowledgePeople - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<KnowledgePeople> list = dataList.Cast<KnowledgePeople>().ToList();
            List<KnowledgePeople> listFilter = new List<KnowledgePeople>();
            List<KnowledgePeople> listContext = _contextTenant.KnowledgePeople.ToList();

            Parallel.ForEach(list,
                () => { return new List<KnowledgePeople>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.KnowledgeId.Equals(item.KnowledgeId) && c.PeopleId.Equals(item.PeopleId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapKnowledgeWorkOrderTypes(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapKnowledgeWorkOrderTypes - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<KnowledgeWorkOrderTypes> list = dataList.Cast<KnowledgeWorkOrderTypes>().ToList();
            List<KnowledgeWorkOrderTypes> listFilter = new List<KnowledgeWorkOrderTypes>();
            List<KnowledgeWorkOrderTypes> listContext = _contextTenant.KnowledgeWorkOrderTypes.ToList();

            Parallel.ForEach(list,
                () => { return new List<KnowledgeWorkOrderTypes>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.KnowledgeId.Equals(item.KnowledgeId) && c.WorkOrderTypeId.Equals(item.WorkOrderTypeId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapContactsFinalClients(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapContactsFinalClients - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ContactsFinalClients> list = dataList.Cast<ContactsFinalClients>().ToList();
            List<ContactsFinalClients> listFilter = new List<ContactsFinalClients>();
            List<ContactsFinalClients> listContext = _contextTenant.ContactsFinalClients.ToList();

            Parallel.ForEach(list,
                () => { return new List<ContactsFinalClients>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ContactId == item.ContactId && c.FinalClientId == item.FinalClientId))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapContactsLocationsFinalClients(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapContactsLocationsFinalClients - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ContactsLocationsFinalClients> list = dataList.Cast<ContactsLocationsFinalClients>().ToList();
            List<ContactsLocationsFinalClients> listFilter = new List<ContactsLocationsFinalClients>();
            List<ContactsLocationsFinalClients> listContext = _contextTenant.ContactsLocationsFinalClients.ToList();

            Parallel.ForEach(list,
                () => { return new List<ContactsLocationsFinalClients>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ContactId.Equals(item.ContactId) && c.LocationId.Equals(item.LocationId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                string[] copyParameters = new[]
                {
                    nameof(ContactsLocationsFinalClients.LocationId),
                    nameof(ContactsLocationsFinalClients.ContactId)
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters, false);
            }
        }

        public void MapContractContacts(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapContractContacts - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ContractContacts> list = dataList.Cast<ContractContacts>().ToList();
            List<ContractContacts> listFilter = new List<ContractContacts>();
            List<ContractContacts> listContext = _contextTenant.ContractContacts.ToList();

            Parallel.ForEach(list,
                () => { return new List<ContractContacts>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ContactId.Equals(item.ContactId) && c.ContractId.Equals(item.ContractId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapToolsToolTypes(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapToolsToolTypes - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ToolsToolTypes> list = dataList.Cast<ToolsToolTypes>().ToList();
            List<ToolsToolTypes> listFilter = new List<ToolsToolTypes>();
            List<ToolsToolTypes> listContext = _contextTenant.ToolsToolTypes.ToList();

            Parallel.ForEach(list,
                () => { return new List<ToolsToolTypes>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ToolId.Equals(item.ToolId) && c.ToolTypeId.Equals(item.ToolTypeId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapAssetsWorkOrders(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapTeamsWorkOrders - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<AssetsWorkOrders> list = dataList.Cast<AssetsWorkOrders>().ToList();
            List<AssetsWorkOrders> listFilter = new List<AssetsWorkOrders>();
            List<AssetsWorkOrders> listContext = _contextTenant.AssetsWorkOrders.ToList();

            Parallel.ForEach(list,
                () => { return new List<AssetsWorkOrders>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.AssetId.Equals(item.AssetId) && c.WorkOrderId.Equals(item.WorkOrderId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapAssetsHiredServices(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapTeamsHiredServices - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<AssetsHiredServices> list = dataList.Cast<AssetsHiredServices>().ToList();
            List<AssetsHiredServices> listFilter = new List<AssetsHiredServices>();
            List<AssetsHiredServices> listContext = _contextTenant.AssetsHiredServices.ToList();

            Parallel.ForEach(list,
                () => { return new List<AssetsHiredServices>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.AssetId.Equals(item.AssetId) && c.HiredServiceId.Equals(item.HiredServiceId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapExpensesTicketFile(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapExpensesTicketFile - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ExpensesTicketFile> list = dataList.Cast<ExpensesTicketFile>().ToList();
            List<ExpensesTicketFile> listFilter = new List<ExpensesTicketFile>();
            List<ExpensesTicketFile> listContext = _contextTenant.ExpensesTicketFile.ToList();

            Parallel.ForEach(list,
                () => { return new List<ExpensesTicketFile>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ExpenseTicketId.Equals(item.ExpenseTicketId) && c.SomFileId.Equals(item.SomFileId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapMainWoViewConfigurationsPeople(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapMainWoViewConfigurationsPeople - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<MainWoViewConfigurationsPeople> list = dataList.Cast<MainWoViewConfigurationsPeople>().ToList();
            List<MainWoViewConfigurationsPeople> listFilter = new List<MainWoViewConfigurationsPeople>();
            List<MainWoViewConfigurationsPeople> listContext = _contextTenant.MainWoViewConfigurationsPeople.ToList();

            Parallel.ForEach(list,
                () => { return new List<MainWoViewConfigurationsPeople>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.UserMainWoViewConfigurationId.Equals(item.UserMainWoViewConfigurationId) && c.PeopleId.Equals(item.PeopleId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPeopleCalendars(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPeopleCalendars - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PeopleCalendars> list = dataList.Cast<PeopleCalendars>().ToList();
            List<PeopleCalendars> listFilter = new List<PeopleCalendars>();
            List<PeopleCalendars> listContext = _contextTenant.PeopleCalendars.ToList();

            Parallel.ForEach(list,
                () => { return new List<PeopleCalendars>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.CalendarId.Equals(item.CalendarId) && c.PeopleId.Equals(item.PeopleId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPeoplePermissions(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPeoplePermissions - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PeoplePermissions> list = dataList.Cast<PeoplePermissions>().ToList();
            List<PeoplePermissions> listFilter = new List<PeoplePermissions>();
            List<PeoplePermissions> listContext = _contextTenant.PeoplePermissions.ToList();

            Parallel.ForEach(list,
                () => { return new List<PeoplePermissions>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.PermissionId.Equals(item.PermissionId) && c.PeopleId.Equals(item.PeopleId)))
                    {
                        item.AssignmentDate = item.AssignmentDate.ToUniversalTime();
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPeopleCollectionsAdmins(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPeopleCollectionsAdmins - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PeopleCollectionsAdmins> list = dataList.Cast<PeopleCollectionsAdmins>().ToList();
            List<PeopleCollectionsAdmins> listFilter = new List<PeopleCollectionsAdmins>();
            List<PeopleCollectionsAdmins> listContext = _contextTenant.PeopleCollectionsAdmins.ToList();

            Parallel.ForEach(list,
                () => { return new List<PeopleCollectionsAdmins>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.PeopleCollectionId.Equals(item.PeopleCollectionId) && c.PeopleId.Equals(item.PeopleId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPeopleCollectionsPeople(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPeopleCollectionsPeople - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PeopleCollectionsPeople> list = dataList.Cast<PeopleCollectionsPeople>().ToList();
            List<PeopleCollectionsPeople> listFilter = new List<PeopleCollectionsPeople>();
            List<PeopleCollectionsPeople> listContext = _contextTenant.PeopleCollectionsPeople.ToList();

            Parallel.ForEach(list,
                () => { return new List<PeopleCollectionsPeople>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.PeopleCollectionId.Equals(item.PeopleCollectionId) && c.PeopleId.Equals(item.PeopleId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPeopleCollectionsPermissions(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPeopleCollectionsPermissions - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PeopleCollectionsPermissions> list = dataList.Cast<PeopleCollectionsPermissions>().ToList();
            List<PeopleCollectionsPermissions> listFilter = new List<PeopleCollectionsPermissions>();
            List<PeopleCollectionsPermissions> listContext = _contextTenant.PeopleCollectionsPermissions.ToList();

            Parallel.ForEach(list,
                () => { return new List<PeopleCollectionsPermissions>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.PeopleCollectionId.Equals(item.PeopleCollectionId) && c.PermissionId.Equals(item.PermissionId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPlanningPanelViewConfigurationPeople(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPlanningPanelViewConfigurationPeople - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PlanningPanelViewConfigurationPeople> list = dataList.Cast<PlanningPanelViewConfigurationPeople>().ToList();
            List<PlanningPanelViewConfigurationPeople> listFilter = new List<PlanningPanelViewConfigurationPeople>();
            List<PlanningPanelViewConfigurationPeople> listContext = _contextTenant.PlanningPanelViewConfigurationPeople.ToList();

            Parallel.ForEach(list,
                () => { return new List<PlanningPanelViewConfigurationPeople>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.PlanningPanelViewConfigurationId.Equals(item.PlanningPanelViewConfigurationId) && c.PeopleId.Equals(item.PeopleId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPlanningPanelViewConfigurationPeopleCollection(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPlanningPanelViewConfigurationPeopleCollection - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PlanningPanelViewConfigurationPeopleCollection> list = dataList.Cast<PlanningPanelViewConfigurationPeopleCollection>().ToList();
            List<PlanningPanelViewConfigurationPeopleCollection> listFilter = new List<PlanningPanelViewConfigurationPeopleCollection>();
            List<PlanningPanelViewConfigurationPeopleCollection> listContext = _contextTenant.PlanningPanelViewConfigurationPeopleCollection.ToList();

            Parallel.ForEach(list,
                () => { return new List<PlanningPanelViewConfigurationPeopleCollection>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.PlanningPanelViewConfigurationId.Equals(item.PlanningPanelViewConfigurationId) && c.PeopleCollectionId.Equals(item.PeopleCollectionId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPredefinedServicesPermission(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPredefinedServicesPermission - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PredefinedServicesPermission> list = dataList.Cast<PredefinedServicesPermission>().ToList();
            List<PredefinedServicesPermission> listFilter = new List<PredefinedServicesPermission>();
            List<PredefinedServicesPermission> listContext = _contextTenant.PredefinedServicesPermission.ToList();

            Parallel.ForEach(list,
                () => { return new List<PredefinedServicesPermission>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.PredefinedServiceId.Equals(item.PredefinedServiceId) && c.PermissionId.Equals(item.PermissionId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                string[] copyParameters = new[]
                {
                    nameof(PredefinedServicesPermission.PredefinedServiceId),
                    nameof(PredefinedServicesPermission.PermissionId)
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters, false);
            }
        }

        public void MapProjectsCalendars(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapProjectsCalendars - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ProjectsCalendars> list = dataList.Cast<ProjectsCalendars>().ToList();
            List<ProjectsCalendars> listFilter = new List<ProjectsCalendars>();
            List<ProjectsCalendars> listContext = _contextTenant.ProjectsCalendars.ToList();

            Parallel.ForEach(list,
                () => { return new List<ProjectsCalendars>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ProjectId.Equals(item.ProjectId) && c.CalendarId.Equals(item.CalendarId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapProjectsContacts(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapProjectsContacts - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ProjectsContacts> list = dataList.Cast<ProjectsContacts>().ToList();
            List<ProjectsContacts> listFilter = new List<ProjectsContacts>();
            List<ProjectsContacts> listContext = _contextTenant.ProjectsContacts.ToList();

            Parallel.ForEach(list,
                () => { return new List<ProjectsContacts>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ProjectId.Equals(item.ProjectId) && c.ContactId.Equals(item.ContactId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapProjectsPermissions(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapProjectsPermissions - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ProjectsPermissions> list = dataList.Cast<ProjectsPermissions>().ToList();
            List<ProjectsPermissions> listFilter = new List<ProjectsPermissions>();
            List<ProjectsPermissions> listContext = _contextTenant.ProjectsPermissions.ToList();

            Parallel.ForEach(list,
                () => { return new List<ProjectsPermissions>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ProjectId.Equals(item.ProjectId) && c.PermissionId.Equals(item.PermissionId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPermissionsQueues(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPermissionsQueues - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PermissionsQueues> list = dataList.Cast<PermissionsQueues>().ToList();
            List<PermissionsQueues> listFilter = new List<PermissionsQueues>();
            List<PermissionsQueues> listContext = _contextTenant.PermissionsQueues.ToList();

            Parallel.ForEach(list,
                () => { return new List<PermissionsQueues>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.QueueId.Equals(item.QueueId) && c.PermissionId.Equals(item.PermissionId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPermissionsTasks(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPermissionsTasks - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PermissionsTasks> list = dataList.Cast<PermissionsTasks>().ToList();
            List<PermissionsTasks> listFilter = new List<PermissionsTasks>();
            List<PermissionsTasks> listContext = _contextTenant.PermissionsTasks.ToList();

            Parallel.ForEach(list,
                () => { return new List<PermissionsTasks>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.TaskId.Equals(item.TaskId) && c.PermissionId.Equals(item.PermissionId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapLocationCalendar(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapLocationCalendar - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<LocationCalendar> list = dataList.Cast<LocationCalendar>().ToList();
            List<LocationCalendar> listFilter = new List<LocationCalendar>();
            List<LocationCalendar> listContext = _contextTenant.LocationCalendar.ToList();

            Parallel.ForEach(list,
                () => { return new List<LocationCalendar>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.LocationId.Equals(item.LocationId) && c.CalendarId.Equals(item.CalendarId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapToolsTypeWorkOrderTypes(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapToolsTypeWorkOrderTypes - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ToolsTypeWorkOrderTypes> list = dataList.Cast<ToolsTypeWorkOrderTypes>().ToList();
            List<ToolsTypeWorkOrderTypes> listFilter = new List<ToolsTypeWorkOrderTypes>();
            List<ToolsTypeWorkOrderTypes> listContext = _contextTenant.ToolsTypeWorkOrderTypes.ToList();

            Parallel.ForEach(list,
                () => { return new List<ToolsTypeWorkOrderTypes>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ToolsTypeId.Equals(item.ToolsTypeId) && c.WorkOrderTypesId.Equals(item.WorkOrderTypesId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapWorkOrderCategoryPermissions(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrderCategoryPermissions - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<WorkOrderCategoryPermissions> list = dataList.Cast<WorkOrderCategoryPermissions>().ToList();
            List<WorkOrderCategoryPermissions> listFilter = new List<WorkOrderCategoryPermissions>();
            List<WorkOrderCategoryPermissions> listContext = _contextTenant.WorkOrderCategoryPermissions.ToList();

            Parallel.ForEach(list,
                () => { return new List<WorkOrderCategoryPermissions>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.WorkOrderCategoryId.Equals(item.WorkOrderCategoryId) && c.PermissionId.Equals(item.PermissionId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapWorkOrderCategoryRoles(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrderCategoryRoles - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<WorkOrderCategoryRoles> list = dataList.Cast<WorkOrderCategoryRoles>().ToList();
            List<WorkOrderCategoryRoles> listFilter = new List<WorkOrderCategoryRoles>();
            List<WorkOrderCategoryRoles> listContext = _contextTenant.WorkOrderCategoryRoles.ToList();

            Parallel.ForEach(list,
                () => { return new List<WorkOrderCategoryRoles>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.WorkOrderCategoryId.Equals(item.WorkOrderCategoryId) && c.RoleId.Equals(item.RoleId)))
                    {
                        localList.Add(item);
                    }
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapMainWoviewConfigurationsGroups(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapMainWoviewConfigurationsGroups - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<MainWoviewConfigurationsGroups> list = dataList.Cast<MainWoviewConfigurationsGroups>().ToList();
            List<MainWoviewConfigurationsGroups> listFilter = new List<MainWoviewConfigurationsGroups>();
            List<MainWoviewConfigurationsGroups> listContext = _contextTenant.MainWoviewConfigurationsGroups.ToList();

            Parallel.ForEach(list,
                () => { return new List<MainWoviewConfigurationsGroups>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.UserMainWoviewConfigurationId.Equals(item.UserMainWoviewConfigurationId) && c.PeopleCollectionId.Equals(item.PeopleCollectionId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapExternalSystemImportData(IEnumerable<object> dataList, KeyValuePair<string, string> val, int skip)
        {
            Console.WriteLine($"MapExternalSystemImportData - Registros a tratar: {dataList.Count()} apartir del {skip}");
            object localLockObject = new object();

            List<ExternalSystemImportData> list = dataList.Cast<ExternalSystemImportData>().ToList();
            List<ExternalSystemImportData> listFilter = new List<ExternalSystemImportData>();
            List<ExternalSystemImportData> listContext = _contextTenant.ExternalSystemImportData.ToList();

            Parallel.ForEach(list,
                () => { return new List<ExternalSystemImportData>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ImportCode == item.ImportCode && c.Property == item.Property))
                    {
                        item.CreationDate = item.CreationDate.ToUniversalTime();
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");                    
                    return localList;                    
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();
            if (listFilter.Any())
            {
                string[] copyParameters = new[]
                {
                    nameof(ExternalSystemImportData.ImportCode),
                    nameof(ExternalSystemImportData.ExternalSystem),
                    nameof(ExternalSystemImportData.Property),
                    nameof(ExternalSystemImportData.Value),
                    nameof(ExternalSystemImportData.WorkOrderId),
                    nameof(ExternalSystemImportData.CreationDate)
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters, false);
            }            
        }

        public void MapLocationsFinalClients(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapLocationsFinalClients - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<LocationsFinalClients> list = dataList.Cast<LocationsFinalClients>().ToList();
            List<LocationsFinalClients> listFilter = new List<LocationsFinalClients>();
            List<LocationsFinalClients> listContext = _contextTenant.LocationsFinalClients.ToList();

            Parallel.ForEach(list,
                () => { return new List<LocationsFinalClients>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.FinalClientId.Equals(item.FinalClientId) && c.LocationId.Equals(item.LocationId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                string[] copyParameters = new[]
                {
                    nameof(LocationsFinalClients.FinalClientId),
                    nameof(LocationsFinalClients.LocationId),
                    nameof(LocationsFinalClients.PeopleCommercialId),
                    nameof(LocationsFinalClients.OriginId),
                    nameof(LocationsFinalClients.CompositeCode)
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters, false);
            }
        }

        public void MapPeopleRegisteredPda(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPeopleRegisteredPda - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PeopleRegisteredPda> list = dataList.Cast<PeopleRegisteredPda>().ToList();
            List<PeopleRegisteredPda> listFilter = new List<PeopleRegisteredPda>();
            List<PeopleRegisteredPda> listContext = _contextTenant.PeopleRegisteredPda.ToList();

            Parallel.ForEach(list,
                () => { return new List<PeopleRegisteredPda>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.PeopleId.Equals(item.PeopleId) && c.DeviceId.Equals(item.DeviceId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapTaskWebServiceCallItems(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapTaskWebServiceCallItems - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<TaskWebServiceCallItems> list = dataList.Cast<TaskWebServiceCallItems>().ToList();
            List<TaskWebServiceCallItems> listFilter = new List<TaskWebServiceCallItems>();
            List<TaskWebServiceCallItems> listContext = _contextTenant.TaskWebServiceCallItems.ToList();

            Parallel.ForEach(list,
                () => { return new List<TaskWebServiceCallItems>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.TaskId.Equals(item.TaskId) && c.ItemId.Equals(item.ItemId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPeopleProjects(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapPeopleProjects");
            object localLockObject = new object();
            List<Model.Projects> dataProjects = _sqlConnectionOldDatabase.Query<Model.Projects>("SELECT IdProject, Gestor FROM dbo.Projects").ToList();
            List<Model.Persones> dataPersones = _sqlConnectionOldDatabase.Query<Model.Persones>("SELECT IdPersona, ProjectId FROM dbo.Persones where ProjectId is not null").ToList();
            List<PeopleProjects> listFilter = new List<PeopleProjects>();

            Parallel.ForEach(dataProjects,
                () => { return new List<PeopleProjects>(); },
                (item, state, localList) =>
                {
                    localList.Add(new PeopleProjects() { PeopleId = item.Gestor, ProjectId = item.IdProject, IsManager = true });
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            Parallel.ForEach(dataPersones,
                () => { return new List<PeopleProjects>(); },
                (item, state, localList) =>
                {
                    if (item.ProjectId.HasValue && !listFilter.Any(x => x.PeopleId == item.IdPersona && x.ProjectId == item.ProjectId))
                    {
                        localList.Add(new PeopleProjects() { PeopleId = item.IdPersona, ProjectId = item.ProjectId.Value, IsManager = false });
                    }
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            if (listFilter.Any())
            {
                string[] copyParameters = new[]
                {
                    nameof(PeopleProjects.PeopleId),
                    nameof(PeopleProjects.ProjectId),
                    nameof(PeopleProjects.IsManager),
                };
                _migrationTenantCommon.SQLBulkCopyToNewDatabaseTenant(listFilter, val.Key, copyParameters, false);
            }
        }

        public void MapAdvancedTechnicianListFilterPersons(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapAdvancedTechnicianListFilterPersons - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<AdvancedTechnicianListFilterPersons> list = dataList.Cast<AdvancedTechnicianListFilterPersons>().ToList();
            List<AdvancedTechnicianListFilterPersons> listFilter = new List<AdvancedTechnicianListFilterPersons>();
            List<AdvancedTechnicianListFilterPersons> listContext = _contextTenant.AdvancedTechnicianListFilterPersons.ToList();

            Parallel.ForEach(list,
                () => { return new List<AdvancedTechnicianListFilterPersons>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.TechnicianListFilterId.Equals(item.TechnicianListFilterId) && c.PeopleId.Equals(item.PeopleId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapAdvancedTechnicianListFilters(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapAdvancedTechnicianListFilters - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<AdvancedTechnicianListFilters> list = dataList.Cast<AdvancedTechnicianListFilters>().ToList();
            List<AdvancedTechnicianListFilters> listFilter = new List<AdvancedTechnicianListFilters>();
            List<AdvancedTechnicianListFilters> listContext = _contextTenant.AdvancedTechnicianListFilters.ToList();

            Parallel.ForEach(list,
                () => { return new List<AdvancedTechnicianListFilters>(); },
                (item, state, localList) =>
                {
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
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapBasicTechnicianListFilters(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapBasicTechnicianListFilters - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<BasicTechnicianListFilters> list = dataList.Cast<BasicTechnicianListFilters>().ToList();
            List<BasicTechnicianListFilters> listFilter = new List<BasicTechnicianListFilters>();
            List<BasicTechnicianListFilters> listContext = _contextTenant.BasicTechnicianListFilters.ToList();

            Parallel.ForEach(list,
                () => { return new List<BasicTechnicianListFilters>(); },
                (item, state, localList) =>
                {
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
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapBasicTechnicianListFilterSkills(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapBasicTechnicianListFilterSkills - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<BasicTechnicianListFilterSkills> list = dataList.Cast<BasicTechnicianListFilterSkills>().ToList();
            List<BasicTechnicianListFilterSkills> listFilter = new List<BasicTechnicianListFilterSkills>();
            List<BasicTechnicianListFilterSkills> listContext = _contextTenant.BasicTechnicianListFilterSkills.ToList();

            Parallel.ForEach(list,
                () => { return new List<BasicTechnicianListFilterSkills>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.TechnicianListFilterId.Equals(item.TechnicianListFilterId) && c.SkillId.Equals(item.SkillId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapDnAndMaterialsAnalysis(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapDnAndMaterialsAnalysis - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<DnAndMaterialsAnalysis> list = dataList.Cast<DnAndMaterialsAnalysis>().ToList();
            List<DnAndMaterialsAnalysis> listFilter = new List<DnAndMaterialsAnalysis>();
            List<DnAndMaterialsAnalysis> listContext = _contextTenant.DnAndMaterialsAnalysis.ToList();

            Parallel.ForEach(list,
                () => { return new List<DnAndMaterialsAnalysis>(); },
                (item, state, localList) =>
                {
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
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPlanificationCriterias(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPlanificationCriterias - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PlanificationCriterias> list = dataList.Cast<PlanificationCriterias>().ToList();
            List<PlanificationCriterias> listFilter = new List<PlanificationCriterias>();
            List<PlanificationCriterias> listContext = _contextTenant.PlanificationCriterias.ToList();

            Parallel.ForEach(list,
                () => { return new List<PlanificationCriterias>(); },
                (item, state, localList) =>
                {
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

        public void MapServicesAnalysis(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapServicesAnalysis - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ServicesAnalysis> list = dataList.Cast<ServicesAnalysis>().ToList();
            List<ServicesAnalysis> listFilter = new List<ServicesAnalysis>();
            List<ServicesAnalysis> listContext = _contextTenant.ServicesAnalysis.ToList();

            Parallel.ForEach(list,
                () => { return new List<ServicesAnalysis>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.ServiceCode.Equals(item.ServiceCode)))
                    {
                        item.CreationDateTime = item.CreationDateTime.HasValue ? item.CreationDateTime.Value.ToUniversalTime() : item.CreationDateTime.GetValueOrDefault();
                        item.StartTime = item.StartTime.HasValue ? item.StartTime.Value.ToUniversalTime() : item.StartTime.GetValueOrDefault();
                        item.EndingTime = item.EndingTime.HasValue ? item.EndingTime.Value.ToUniversalTime() : item.EndingTime.GetValueOrDefault();
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapErpItemsSyncConfig(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapErpItemsSyncConfig - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ErpItemsSyncConfig> list = dataList.Cast<ErpItemsSyncConfig>().ToList();
            List<ErpItemsSyncConfig> listFilter = new List<ErpItemsSyncConfig>();
            List<ErpItemsSyncConfig> listContext = _contextTenant.ErpItemsSyncConfig.ToList();

            Parallel.ForEach(list,
                () => { return new List<ErpItemsSyncConfig>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.TenantId.Equals(item.TenantId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapExpensesTicketsFiles(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapExpensesTicketsFiles - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<ExpensesTicketsFiles> list = dataList.Cast<ExpensesTicketsFiles>().ToList();
            List<ExpensesTicketsFiles> listFilter = new List<ExpensesTicketsFiles>();
            List<ExpensesTicketsFiles> listContext = _contextTenant.ExpensesTicketsFiles.ToList();

            Parallel.ForEach(list,
                () => { return new List<ExpensesTicketsFiles>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id) && c.ExpenseTicketId.Equals(item.ExpenseTicketId)))
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
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapPushNotificationsPeople(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPushNotificationsPeople - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<PushNotificationsPeople> list = dataList.Cast<PushNotificationsPeople>().ToList();
            List<PushNotificationsPeople> listFilter = new List<PushNotificationsPeople>();
            List<PushNotificationsPeople> listContext = _contextTenant.PushNotificationsPeople.ToList();

            Parallel.ForEach(list,
                () => { return new List<PushNotificationsPeople>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.NotificationId.Equals(item.NotificationId) && c.PeopleId.Equals(item.PeopleId)))
                    {
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapWorkOrderAnalysis(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrderAnalysis - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<WorkOrderAnalysis> list = dataList.Cast<WorkOrderAnalysis>().ToList();
            List<WorkOrderAnalysis> listFilter = new List<WorkOrderAnalysis>();
            List<WorkOrderAnalysis> listContext = _contextTenant.WorkOrderAnalysis.ToList();

            Parallel.ForEach(list,
                () => { return new List<WorkOrderAnalysis>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.WorkOrderCode.Equals(item.WorkOrderCode)))
                    {
                        item.ClientCreationDate = item.ClientCreationDate.HasValue ? item.ClientCreationDate.Value.ToUniversalTime() : item.ClientCreationDate.GetValueOrDefault();
                        item.InternalCreationDate = item.InternalCreationDate.ToUniversalTime();
                        item.ActuationDate = item.ActuationDate.HasValue ? item.ActuationDate.Value.ToUniversalTime() : item.ActuationDate.GetValueOrDefault();
                        item.ClosingClientTime = item.ClosingClientTime.HasValue ? item.ClosingClientTime.Value.ToUniversalTime() : item.ClosingClientTime.GetValueOrDefault();
                        item.InternalSystemTimeWhenOtclosed = item.InternalSystemTimeWhenOtclosed.HasValue ? item.InternalSystemTimeWhenOtclosed.Value.ToUniversalTime() : item.InternalSystemTimeWhenOtclosed.GetValueOrDefault();
                        item.SlaresponseDate = item.SlaresponseDate.HasValue ? item.SlaresponseDate.Value.ToUniversalTime() : item.SlaresponseDate.GetValueOrDefault();
                        item.SlaresolutionDate = item.SlaresolutionDate.HasValue ? item.SlaresolutionDate.Value.ToUniversalTime() : item.SlaresolutionDate.GetValueOrDefault();
                        item.ClosingClientDate = item.ClosingClientDate.HasValue ? item.ClosingClientDate.Value.ToUniversalTime() : item.ClosingClientDate.GetValueOrDefault();
                        item.ClosingSystemDate = item.ClosingSystemDate.HasValue ? item.ClosingSystemDate.Value.ToUniversalTime() : item.ClosingSystemDate.GetValueOrDefault();
                        item.ClosingWodate = item.ClosingWodate.ToUniversalTime();
                        item.AccountingClosingDate = item.AccountingClosingDate.HasValue ? item.AccountingClosingDate.Value.ToUniversalTime() : item.AccountingClosingDate.GetValueOrDefault();
                        item.SlaResponsePenaltyDate = item.SlaResponsePenaltyDate.HasValue ? item.SlaResponsePenaltyDate.Value.ToUniversalTime() : item.SlaResponsePenaltyDate.GetValueOrDefault();
                        item.SlaResolutionPenaltyDate = item.SlaResolutionPenaltyDate.HasValue ? item.SlaResolutionPenaltyDate.Value.ToUniversalTime() : item.SlaResolutionPenaltyDate.GetValueOrDefault();
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
            }
        }

        public void MapTechnicianListFilters(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapTechnicianListFilters - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<TechnicianListFilters> list = dataList.Cast<TechnicianListFilters>().ToList();
            List<TechnicianListFilters> listFilter = new List<TechnicianListFilters>();
            List<TechnicianListFilters> listContext = _contextTenant.TechnicianListFilters.ToList();

            Parallel.ForEach(list,
                () => { return new List<TechnicianListFilters>(); },
                (item, state, localList) =>
                {
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

        #endregion       
    }
}