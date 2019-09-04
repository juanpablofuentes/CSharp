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
    public class MigrationTenantWithoutRelations
    {
        private SqlConnection _sqlConnectionOldDatabase;
        private SqlConnection _sqlConnectionTenantDatabase;
        private SOMContext _contextInfrastructure;
        private SOMTenantContext _contextTenant;
        private MigrationTenantCommon _migrationTenantCommon;

        public MigrationTenantWithoutRelations(SqlConnection sqlConnectionOldDatabase, SqlConnection sqlConnectionTenantDatabase, SOMContext contextInfrastructure, SOMTenantContext contextTenant)
        {
            _sqlConnectionOldDatabase = sqlConnectionOldDatabase;
            _sqlConnectionTenantDatabase = sqlConnectionTenantDatabase;
            _contextInfrastructure = contextInfrastructure;
            _contextTenant = contextTenant;
            _migrationTenantCommon = new MigrationTenantCommon(contextTenant, sqlConnectionTenantDatabase.ConnectionString);
        }

        public void MapCompanies(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapCompanies - Registros a tratar: {dataList.Count()}");
            List<Companies> list = dataList.Cast<Companies>().ToList();
            List<Companies> listFilter = new List<Companies>();
            List<Companies> listContext = _contextTenant.Companies.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Companies>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapSalesRate(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapSalesRate - Registros a tratar: {dataList.Count()}");
            List<SalesRate> list = dataList.Cast<SalesRate>().ToList();
            List<SalesRate> listFilter = new List<SalesRate>();
            List<SalesRate> listContext = _contextTenant.SalesRate.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<SalesRate>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapCollectionsExtraField(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapCollectionsExtraField - Registros a tratar: {dataList.Count()}");
            List<CollectionsExtraField> list = dataList.Cast<CollectionsExtraField>().ToList();
            List<CollectionsExtraField> listFilter = new List<CollectionsExtraField>();
            List<CollectionsExtraField> listContext = _contextTenant.CollectionsExtraField.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<CollectionsExtraField>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapAssetStatuses(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapAssetStatuses - Registros a tratar: {dataList.Count()}");
            List<AssetStatuses> list = dataList.Cast<AssetStatuses>().ToList();
            List<AssetStatuses> listFilter = new List<AssetStatuses>();
            List<AssetStatuses> listContext = _contextTenant.AssetStatuses.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<AssetStatuses>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapCalendars(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapCalendars - Registros a tratar: {dataList.Count()}");
            List<Calendars> list = dataList.Cast<Calendars>().ToList();
            List<Calendars> listFilter = new List<Calendars>();
            List<Calendars> listContext = _contextTenant.Calendars.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Calendars>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapCollectionsClosureCodes(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapCollectionsClosureCodes - Registros a tratar: {dataList.Count()}");
            List<CollectionsClosureCodes> list = dataList.Cast<CollectionsClosureCodes>().ToList();
            List<CollectionsClosureCodes> listFilter = new List<CollectionsClosureCodes>();
            List<CollectionsClosureCodes> listContext = _contextTenant.CollectionsClosureCodes.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<CollectionsClosureCodes>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapCollectionsTypesWorkOrders(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapCollectionsTypesWorkOrders - Registros a tratar: {dataList.Count()}");
            List<CollectionsTypesWorkOrders> list = dataList.Cast<CollectionsTypesWorkOrders>().ToList();
            List<CollectionsTypesWorkOrders> listFilter = new List<CollectionsTypesWorkOrders>();
            List<CollectionsTypesWorkOrders> listContext = _contextTenant.CollectionsTypesWorkOrders.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<CollectionsTypesWorkOrders>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapKnowledge(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapKnowledge - Registros a tratar: {dataList.Count()}");
            List<Knowledge> list = dataList.Cast<Knowledge>().ToList();
            List<Knowledge> listFilter = new List<Knowledge>();
            List<Knowledge> listContext = _contextTenant.Knowledge.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Knowledge>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapContacts(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapContacts - Registros a tratar: {dataList.Count()}");
            List<Contacts> list = dataList.Cast<Contacts>().ToList();
            List<Contacts> listFilter = new List<Contacts>();
            List<Contacts> listContext = _contextTenant.Contacts.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Contacts>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapQueues(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapQueues - Registros a tratar: {dataList.Count()}");
            List<Queues> list = dataList.Cast<Queues>().ToList();
            List<Queues> listFilter = new List<Queues>();
            List<Queues> listContext = _contextTenant.Queues.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Queues>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapErpSystemInstance(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapErpSystemInstance - Registros a tratar: {dataList.Count()}");
            List<ErpSystemInstance> list = dataList.Cast<ErpSystemInstance>().ToList();
            List<ErpSystemInstance> listFilter = new List<ErpSystemInstance>();
            List<ErpSystemInstance> listContext = _contextTenant.ErpSystemInstance.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<ErpSystemInstance>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapWorkOrderStatuses(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrderStatuses - Registros a tratar: {dataList.Count()}");
            List<WorkOrderStatuses> list = dataList.Cast<WorkOrderStatuses>().ToList();
            List<WorkOrderStatuses> listFilter = new List<WorkOrderStatuses>();
            List<WorkOrderStatuses> listContext = _contextTenant.WorkOrderStatuses.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<WorkOrderStatuses>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapExternalWorOrderStatuses(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapExternalWorOrderStatuses - Registros a tratar: {dataList.Count()}");
            List<ExternalWorOrderStatuses> list = dataList.Cast<ExternalWorOrderStatuses>().ToList();
            List<ExternalWorOrderStatuses> listFilter = new List<ExternalWorOrderStatuses>();
            List<ExternalWorOrderStatuses> listContext = _contextTenant.ExternalWorOrderStatuses.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<ExternalWorOrderStatuses>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapExpenseTypes(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapExpenseTypes - Registros a tratar: {dataList.Count()}");
            List<ExpenseTypes> list = dataList.Cast<ExpenseTypes>().ToList();
            List<ExpenseTypes> listFilter = new List<ExpenseTypes>();
            List<ExpenseTypes> listContext = _contextTenant.ExpenseTypes.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<ExpenseTypes>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapFamilies(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapFamilies - Registros a tratar: {dataList.Count()}");
            List<Families> list = dataList.Cast<Families>().ToList();
            List<Families> listFilter = new List<Families>();
            List<Families> listContext = _contextTenant.Families.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Families>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapFlows(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapFlows - Registros a tratar: {dataList.Count()}");
            List<Flows> list = dataList.Cast<Flows>().ToList();
            List<Flows> listFilter = new List<Flows>();
            List<Flows> listContext = _contextTenant.Flows.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Flows>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapFormConfigs(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapFormConfigs - Registros a tratar: {dataList.Count()}");
            List<FormConfigs> list = dataList.Cast<FormConfigs>().ToList();
            List<FormConfigs> listFilter = new List<FormConfigs>();
            List<FormConfigs> listContext = _contextTenant.FormConfigs.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<FormConfigs>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapGuarantee(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapGuarantee - Registros a tratar: {dataList.Count()}");
            List<Guarantee> list = dataList.Cast<Guarantee>().ToList();
            List<Guarantee> listFilter = new List<Guarantee>();
            List<Guarantee> listContext = _contextTenant.Guarantee.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Guarantee>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.StdStartDate = item.StdStartDate.HasValue ? item.StdStartDate.Value.ToUniversalTime() : item.StdStartDate.GetValueOrDefault();
                        item.StdEndDate = item.StdEndDate.HasValue ? item.StdEndDate.Value.ToUniversalTime() : item.StdEndDate.GetValueOrDefault();
                        item.BlnStartDate = item.BlnStartDate.HasValue ? item.BlnStartDate.Value.ToUniversalTime() : item.BlnStartDate.GetValueOrDefault();
                        item.BlnEndDate = item.BlnEndDate.HasValue ? item.BlnEndDate.Value.ToUniversalTime() : item.BlnEndDate.GetValueOrDefault();
                        item.ProStartDate = item.ProStartDate.HasValue ? item.ProStartDate.Value.ToUniversalTime() : item.ProStartDate.GetValueOrDefault();
                        item.ProEndDate = item.ProEndDate.HasValue ? item.ProEndDate.Value.ToUniversalTime() : item.ProEndDate.GetValueOrDefault();
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

        public void MapItems(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapItems - Registros a tratar: {dataList.Count()}");
            List<Items> list = dataList.Cast<Items>().ToList();
            List<Items> listFilter = new List<Items>();
            List<Items> listContext = _contextTenant.Items.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Items>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapMailTemplate(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapMailTemplate - Registros a tratar: {dataList.Count()}");
            List<MailTemplate> list = dataList.Cast<MailTemplate>().ToList();
            List<MailTemplate> listFilter = new List<MailTemplate>();
            List<MailTemplate> listContext = _contextTenant.MailTemplate.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<MailTemplate>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapMainOtstatics(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapMainOtstatics - Registros a tratar: {dataList.Count()}");
            List<MainOtstatics> list = dataList.Cast<MainOtstatics>().ToList();
            List<MainOtstatics> listFilter = new List<MainOtstatics>();
            List<MainOtstatics> listContext = _contextTenant.MainOtstatics.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<MainOtstatics>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapMainWoregistry(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapMainWoregistry- Registros a tratar: {dataList.Count()}");
            List<MainWoregistry> list = dataList.Cast<MainWoregistry>().ToList();
            List<MainWoregistry> listFilter = new List<MainWoregistry>();
            List<MainWoregistry> listContext = _contextTenant.MainWoregistry.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<MainWoregistry>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.ArrivalTime = item.ArrivalTime.HasValue ? item.ArrivalTime.Value.ToUniversalTime() : item.ArrivalTime.GetValueOrDefault();
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

        public void MapBrands(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapBrands- Registros a tratar: {dataList.Count()}");
            List<Brands> list = dataList.Cast<Brands>().ToList();
            List<Brands> listFilter = new List<Brands>();
            List<Brands> listContext = _contextTenant.Brands.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Brands>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapOptimizationFunctionWeights(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapOptimizationFunctionWeights- Registros a tratar: {dataList.Count()}");
            List<OptimizationFunctionWeights> list = dataList.Cast<OptimizationFunctionWeights>().ToList();
            List<OptimizationFunctionWeights> listFilter = new List<OptimizationFunctionWeights>();
            List<OptimizationFunctionWeights> listContext = _contextTenant.OptimizationFunctionWeights.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<OptimizationFunctionWeights>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapPaymentMethods(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPaymentMethods- Registros a tratar: {dataList.Count()}");
            List<PaymentMethods> list = dataList.Cast<PaymentMethods>().ToList();
            List<PaymentMethods> listFilter = new List<PaymentMethods>();
            List<PaymentMethods> listContext = _contextTenant.PaymentMethods.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<PaymentMethods>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapPeopleCollections(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPeopleCollections- Registros a tratar: {dataList.Count()}");
            List<PeopleCollections> list = dataList.Cast<PeopleCollections>().ToList();
            List<PeopleCollections> listFilter = new List<PeopleCollections>();
            List<PeopleCollections> listContext = _contextTenant.PeopleCollections.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<PeopleCollections>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapPlanificationProcessCalendarChangeTracker(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPlanificationProcessCalendarChangeTracker - Registros a tratar: {dataList.Count()}");
            List<PlanificationProcessCalendarChangeTracker> list = dataList.Cast<PlanificationProcessCalendarChangeTracker>().ToList();
            List<PlanificationProcessCalendarChangeTracker> listFilter = new List<PlanificationProcessCalendarChangeTracker>();
            List<PlanificationProcessCalendarChangeTracker> listContext = _contextTenant.PlanificationProcessCalendarChangeTracker.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<PlanificationProcessCalendarChangeTracker>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.LastCheckTime = item.LastCheckTime.HasValue ? item.LastCheckTime.Value.ToUniversalTime() : item.LastCheckTime.GetValueOrDefault();
                        item.ModificationDate = item.ModificationDate.HasValue ? item.ModificationDate.Value.ToUniversalTime() : item.ModificationDate.GetValueOrDefault();
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

        public void MapPlanificationProcessWorkOrderChangeTracker(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPlanificationProcessWorkOrderChangeTracker - Registros a tratar: {dataList.Count()}");
            List<PlanificationProcessWorkOrderChangeTracker> list = dataList.Cast<PlanificationProcessWorkOrderChangeTracker>().ToList();
            List<PlanificationProcessWorkOrderChangeTracker> listFilter = new List<PlanificationProcessWorkOrderChangeTracker>();
            List<PlanificationProcessWorkOrderChangeTracker> listContext = _contextTenant.PlanificationProcessWorkOrderChangeTracker.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<PlanificationProcessWorkOrderChangeTracker>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.LastCheckTime = item.LastCheckTime.HasValue ? item.LastCheckTime.Value.ToUniversalTime() : item.LastCheckTime.GetValueOrDefault();
                        item.LastModified = item.LastModified.HasValue ? item.LastModified.Value.ToUniversalTime() : item.LastModified.GetValueOrDefault();
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

        public void MapPointsRate(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPointsRate - Registros a tratar: {dataList.Count()}");
            List<PointsRate> list = dataList.Cast<PointsRate>().ToList();
            List<PointsRate> listFilter = new List<PointsRate>();
            List<PointsRate> listContext = _contextTenant.PointsRate.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<PointsRate>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapPurchaseRate(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPurchaseRate - Registros a tratar: {dataList.Count()}");
            List<PurchaseRate> list = dataList.Cast<PurchaseRate>().ToList();
            List<PurchaseRate> listFilter = new List<PurchaseRate>();
            List<PurchaseRate> listContext = _contextTenant.PurchaseRate.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<PurchaseRate>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapPushNotifications(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPushNotifications - Registros a tratar: {dataList.Count()}");
            List<PushNotifications> list = dataList.Cast<PushNotifications>().ToList();
            List<PushNotifications> listFilter = new List<PushNotifications>();
            List<PushNotifications> listContext = _contextTenant.PushNotifications.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<PushNotifications>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.CreationDate = item.CreationDate.HasValue ? item.CreationDate.Value.ToUniversalTime() : item.CreationDate.GetValueOrDefault();
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

        public void MapHiredServices(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapHiredServices - Registros a tratar: {dataList.Count()}");
            List<HiredServices> list = dataList.Cast<HiredServices>().ToList();
            List<HiredServices> listFilter = new List<HiredServices>();
            List<HiredServices> listContext = _contextTenant.HiredServices.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<HiredServices>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapSomFiles(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapSomFiles - Registros a tratar: {dataList.Count()}");
            List<SomFiles> list = dataList.Cast<SomFiles>().ToList();
            List<SomFiles> listFilter = new List<SomFiles>();
            List<SomFiles> listContext = _contextTenant.SomFiles.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<SomFiles>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.ModifiedDate = item.ModifiedDate.ToUniversalTime();
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

        public void MapStopSlaReason(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapStopSlaReason - Registros a tratar: {dataList.Count()}");
            List<StopSlaReason> list = dataList.Cast<StopSlaReason>().ToList();
            List<StopSlaReason> listFilter = new List<StopSlaReason>();
            List<StopSlaReason> listContext = _contextTenant.StopSlaReason.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<StopSlaReason>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapSystemNotifications(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapSystemNotifications - Registros a tratar: {dataList.Count()}");
            List<SystemNotifications> list = dataList.Cast<SystemNotifications>().ToList();
            List<SystemNotifications> listFilter = new List<SystemNotifications>();
            List<SystemNotifications> listContext = _contextTenant.SystemNotifications.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<SystemNotifications>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Id.Equals(item.Id)))
                    {
                        item.PublicationDateTime = item.PublicationDateTime.HasValue ? item.PublicationDateTime.Value.ToUniversalTime() : item.PublicationDateTime.GetValueOrDefault();
                        item.VisibilityEndTime = item.VisibilityEndTime.HasValue ? item.VisibilityEndTime.Value.ToUniversalTime() : item.VisibilityEndTime.GetValueOrDefault();
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

        public void MapTaskTokens(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapTaskTokens - Registros a tratar: {dataList.Count()}");
            List<TaskTokens> list = dataList.Cast<TaskTokens>().ToList();
            List<TaskTokens> listFilter = new List<TaskTokens>();
            List<TaskTokens> listContext = _contextTenant.TaskTokens.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<TaskTokens>(); },
                (item, state, localList) =>
                {
                    if (!listContext.Any(c => c.Token.Equals(item.Token)))
                    {
                        item.CreationDate = item.CreationDate.ToUniversalTime();
                        localList.Add(item);
                    }
                    //else Console.WriteLine($"Ya existe un registro en la tabla {value.Key} con el valor {item.Name}.");
                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });
            listContext.Clear();
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key, false);
        }

        public void MapToolsType(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapToolsType - Registros a tratar: {dataList.Count()}");
            List<ToolsType> list = dataList.Cast<ToolsType>().ToList();
            List<ToolsType> listFilter = new List<ToolsType>();
            List<ToolsType> listContext = _contextTenant.ToolsType.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<ToolsType>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapUsages(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapUsages - Registros a tratar: {dataList.Count()}");
            List<Usages> list = dataList.Cast<Usages>().ToList();
            List<Usages> listFilter = new List<Usages>();
            List<Usages> listContext = _contextTenant.Usages.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Usages>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapWorkOrderCategoriesCollections(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrderCategoriesCollections - Registros a tratar: {dataList.Count()}");
            List<WorkOrderCategoriesCollections> list = dataList.Cast<WorkOrderCategoriesCollections>().ToList();
            List<WorkOrderCategoriesCollections> listFilter = new List<WorkOrderCategoriesCollections>();
            List<WorkOrderCategoriesCollections> listContext = _contextTenant.WorkOrderCategoriesCollections.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<WorkOrderCategoriesCollections>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapZones(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapWorkOrderCategoriesCollections - Registros a tratar: {dataList.Count()}");
            List<Zones> list = dataList.Cast<Zones>().ToList();
            List<Zones> listFilter = new List<Zones>();
            List<Zones> listContext = _contextTenant.Zones.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Zones>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapPermissions(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapPermissions - Registros a tratar: {dataList.Count()}");
            List<Permissions> list = dataList.Cast<Permissions>().ToList();
            List<Permissions> listFilter = new List<Permissions>();
            List<Permissions> listContext = _contextTenant.Permissions.ToList();
            object localLockObject = new object();
            Parallel.ForEach(list,
                () => { return new List<Permissions>(); },
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
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
        }

        public void MapClients(IEnumerable<object> dataList, KeyValuePair<string, string> val)
        {
            Console.WriteLine($"MapClients - Registros a tratar: {dataList.Count()}");
            object localLockObject = new object();

            List<Clients> list = dataList.Cast<Clients>().ToList();
            List<Clients> listFilter = new List<Clients>();
            List<Clients> listContext = _contextTenant.Clients.ToList();

            Parallel.ForEach(list,
                () => { return new List<Clients>(); },
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

        #region scripts

        public void MapTenantConfiguration(KeyValuePair<string, string> val)
        {
            Console.WriteLine("MapLanguage");
            List<TenantConfiguration> listFilter = new List<TenantConfiguration>();
            List<TenantConfiguration> listContext = _contextTenant.TenantConfiguration.ToList();

            if (listContext.Find(x => x.Key == "NotificationType") == null) listFilter.Add(new TenantConfiguration() { Key = "NotificationType", Value = "SendGrid", Group = null });
            if (listContext.Find(x => x.Key == "SendGridFrom") == null) listFilter.Add(new TenantConfiguration() { Key = "SendGridFrom", Value = "notificacioncierre@esalto.es", Group = null });
            if (listContext.Find(x => x.Key == "SmtpServer") == null) listFilter.Add(new TenantConfiguration() { Key = "SmtpServer", Value = "smtp.office365.com", Group = "" });
            if (listContext.Find(x => x.Key == "SmtpPort") == null) listFilter.Add(new TenantConfiguration() { Key = "SmtpPort", Value = "587", Group = "SmtpConfiguration" });
            if (listContext.Find(x => x.Key == "SmtpUserName") == null) listFilter.Add(new TenantConfiguration() { Key = "SmtpUserName", Value = "notificacioncierre@esalto.es", Group = "SmtpConfiguration" });
            if (listContext.Find(x => x.Key == "SmtpPassword") == null) listFilter.Add(new TenantConfiguration() { Key = "SmtpPassword", Value = "Cierre2016", Group = "SmtpConfiguration" });
            if (listContext.Find(x => x.Key == "SmtpEnableSSL") == null) listFilter.Add(new TenantConfiguration() { Key = "SmtpEnableSSL", Value = "TRUE", Group = "SmtpConfiguration" });
            if (listContext.Find(x => x.Key == "SmtpFrom") == null) listFilter.Add(new TenantConfiguration() { Key = "SmtpFrom", Value = "notificacioncierre@esalto.es", Group = "SmtpConfiguration" });
            if (listContext.Find(x => x.Key == "Ten") == null) listFilter.Add(new TenantConfiguration() { Key = "Ten", Value = "10", Group = "NumberEntriesPerPage" });
            if (listContext.Find(x => x.Key == "Fifteen") == null) listFilter.Add(new TenantConfiguration() { Key = "Fifteen", Value = "15", Group = "NumberEntriesPerPage" });
            if (listContext.Find(x => x.Key == "Twenty") == null) listFilter.Add(new TenantConfiguration() { Key = "Twenty", Value = "20", Group = "NumberEntriesPerPage" });
            if (listContext.Find(x => x.Key == "Forty") == null) listFilter.Add(new TenantConfiguration() { Key = "Forty", Value = "40", Group = "NumberEntriesPerPage" });
            if (listContext.Find(x => x.Key == "CurrentErpSystemInstanceId") == null) listFilter.Add(new TenantConfiguration() { Key = "CurrentErpSystemInstanceId", Value = "6", Group = null });

            listContext.Clear();
           
            if (listFilter.Any()) _migrationTenantCommon.InsertToNewDatabaseTenant(listFilter, val.Key);
            else Console.WriteLine($"No hay registros para insertar en la tabla {val.Key}.");
        }

        #endregion
    }
}
