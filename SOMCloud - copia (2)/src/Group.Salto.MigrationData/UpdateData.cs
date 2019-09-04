using Dapper;
using Group.Salto.DataAccess.Tenant.Context;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.MigrationData
{
    public class UpdateData
    {
        private SOMTenantContext _contextTenant;
        private SqlConnection _sqlConnectionOldDatabase;
        private SqlConnection _sqlConnectionTenantDatabase;

        public UpdateData(SOMTenantContext contextTenant, SqlConnection sqlConnectionOldDatabase, SqlConnection sqlConnectionTenantDatabase)
        {
            _contextTenant = contextTenant;
            _sqlConnectionOldDatabase = sqlConnectionOldDatabase;
            _sqlConnectionTenantDatabase = sqlConnectionTenantDatabase;
        }

        public void UpdateDataMigration(string databaseType, Type[] asmTypes, string csvPath)
        {
            if (databaseType.ToLower() == "updateutcdate")
            {
                UpdateToUtcDate(asmTypes, csvPath);
            }
            else if (databaseType.ToLower() == "updateworkorders")
            {
                Type type = asmTypes.FirstOrDefault(t => t.Name.Equals("WorkOrders", StringComparison.InvariantCultureIgnoreCase));
                UpdateWorkOrders(type);
            }
        }

        private void UpdateWorkOrders(Type type)
        {
            Console.WriteLine("Inicio UpdateWorkOrders");
            var dataList = _sqlConnectionOldDatabase.Query(type, "SELECT IdOrdreTreball as Id, IdentificadorIntern as InternalIdentifier, IdentificadorExtern as ExternalIdentifier, DataCreacio as CreationDate, TextReparacio as TextRepair, Observacions as Observations, HoraRecollida as PickUpTime, HoraTancamentClient as FinalClientClosingTime, HoraTancamentSalto as InternalClosingTime, HoraAssignacio as AssignmentTime, DataActuacio as ActionDate, IdPersonaResponsable as PeopleResponsibleId, Procedencia as OriginId, IntroduidaPer as PeopleIntroducedById, IdTipusOrdreTreball WorkOrderTypesId, IdUbicacioClient as LocationId, IdClient as FinalClientId, IdEstatOT as WorkOrderStatusId, IdICG as IcgId, IdEquip as AssetId, IdManipulador as PeopleManipulatorId, IdCua as QueueId, IdEstatOTExtern as ExternalWorOrderStatusId, IdProject as ProjectId, IdOrdreTreballPare as WorkOrdersFatherId, DataAturadaCronometreSLA as DateStopTimerSla, DataRespostaSLA as ResponseDateSla, DataResolucioSLA as ResolutionDateSla, DataPenalitzacioSenseRespostaSLA as DateUnansweredPenaltySla, DataPenalitzacioSenseResolucioSLA as DatePenaltyWithoutResolutionSla, GeneratorServiceId as GeneratorServiceId, ReferenceGeneratorService as ReferenceGeneratorService, ReferenceOtherServices as ReferenceOtherServices, WorkOrderCategoryId as WorkOrderCategoryId, Archived as Archived, ActuationEndDate as ActuationEndDate, LastModified as LastModified, IsResponsibleFixed as IsResponsibleFixed, IsActuationDateFixed as IsActuationDateFixed, SiteUserId as SiteUserId, Billable as Billable,ExternalSystemId as ExternalSystemId, ClosingOTDate as ClosingOtdate, AccountingClosingDate as AccountingClosingDate, ClientClosingDate as ClientClosingDate, SystemDateWhenOTClosed as SystemDateWhenOtclosed, InternalCreationDate as InternalCreationDate, MeetSLAResponse as MeetSlaresponse, MeetSLAResolution as MeetSlaresolution, Overhead as Overhead FROM dbo.OrdresTreball WHERE IdEquip IS NOT NULL OR GeneratorServiceId IS NOT NULL ORDER BY IdOrdreTreball");
            List<WorkOrders> listOld = dataList.Cast<WorkOrders>().ToList();
            List<WorkOrders> listFilter = new List<WorkOrders>();
            List<WorkOrders> listContext = _contextTenant.WorkOrders.OrderBy(x => x.Id).ToList();
            object localLockObject = new object();
            var i = 1;

            Parallel.ForEach(listOld,
                () => { return new List<WorkOrders>(); },
                (itemOld, state, localList) =>
                {
                    if (listContext.Any(c => c.Id == itemOld.Id))
                    {
                        var itemContext = listContext.Find(x => x.Id == itemOld.Id);
                        if (itemContext != null)
                        {
                            Console.WriteLine($"{i} Workorder {itemOld.Id} - AssetId {itemOld.AssetId} - GeneratorServiceId {itemOld.GeneratorServiceId}");
                            itemContext.AssetId = itemOld.AssetId;
                            itemContext.GeneratorServiceId = itemOld.GeneratorServiceId;
                            listFilter.Add(itemContext);
                            i++;
                        }
                    }

                    return localList;
                },
                (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

            listContext.Clear();

            if (listFilter.Any())
            {
                UpdateDatabaseTenant(listFilter, "WorkOrders");
            }
        }

        private void UpdateToUtcDate(Type[] asmTypes, string csvPath)
        {
            var valuesList = ReadTablesFromCsv(csvPath);
            string valkey = string.Empty;

            foreach (KeyValuePair<string, string> val in valuesList)
            {
                valkey = val.Key;
                Type type = asmTypes.FirstOrDefault(t => t.Name.Equals(valkey, StringComparison.InvariantCultureIgnoreCase));

                if (type != null)
                {
                    if (valkey == "Projects")
                    {
                        Console.WriteLine("UpdateToUtcDate - Projects");
                        List<Projects> listFilter = new List<Projects>();
                        List<Projects> listContext = _contextTenant.Projects.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Projects>(); },
                            (item, state, localList) =>
                            {
                                item.StartDate = item.StartDate.ToUniversalTime();
                                item.EndDate = item.EndDate.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "CompaniesCostHistorical")
                    {
                        Console.WriteLine("UpdateToUtcDate - CompaniesCostHistorical");
                        List<CompaniesCostHistorical> listFilter = new List<CompaniesCostHistorical>();
                        List<CompaniesCostHistorical> listContext = _contextTenant.CompaniesCostHistorical.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<CompaniesCostHistorical>(); },
                            (item, state, localList) =>
                            {
                                item.Until = item.Until.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "MainWoViewConfigurationsColumns")
                    {
                        Console.WriteLine("UpdateToUtcDate - MainWoViewConfigurationsColumns");
                        List<MainWoViewConfigurationsColumns> listFilter = new List<MainWoViewConfigurationsColumns>();
                        List<MainWoViewConfigurationsColumns> listContext = _contextTenant.MainWoViewConfigurationsColumns.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<MainWoViewConfigurationsColumns>(); },
                            (item, state, localList) =>
                            {
                                
                                if (item.FilterStartDate.HasValue && item.FilterStartDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.FilterStartDate = item.FilterStartDate.Value.ToUniversalTime();
                                }
                                if (item.FilterEndDate.HasValue && item.FilterEndDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.FilterEndDate = item.FilterEndDate.Value.ToUniversalTime();
                                }
                                
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }                        
                    }
                    else if (valkey == "PlanificationProcesses")
                    {
                        Console.WriteLine("UpdateToUtcDate - PlanificationProcesses");
                        List<PlanificationProcesses> listFilter = new List<PlanificationProcesses>();
                        List<PlanificationProcesses> listContext = _contextTenant.PlanificationProcesses.Where(x => 
                            x.LastModification != null && x.LastModification.Value.ToString() != "0001-01-01 00:00:00.0000000").ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<PlanificationProcesses>(); },
                            (item, state, localList) =>
                            {
                                if (item.LastModification.HasValue && item.LastModification.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.LastModification = item.LastModification.Value.ToUniversalTime();
                                }
                                
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "ServicesViewConfigurationsColumns")
                    {
                        Console.WriteLine("UpdateToUtcDate - ServicesViewConfigurationsColumns");
                        List<ServicesViewConfigurationsColumns> listFilter = new List<ServicesViewConfigurationsColumns>();
                        List<ServicesViewConfigurationsColumns> listContext = _contextTenant.ServicesViewConfigurationsColumns.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<ServicesViewConfigurationsColumns>(); },
                            (item, state, localList) =>
                            {
                                if (item.FilterStartDate.HasValue && item.FilterStartDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.FilterStartDate = item.FilterStartDate.Value.ToUniversalTime();
                                }
                                if (item.FilterEndDate.HasValue && item.FilterEndDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.FilterEndDate = item.FilterEndDate.Value.ToUniversalTime();
                                }
                                
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "Tasks")
                    {
                        Console.WriteLine("UpdateToUtcDate - Tasks");
                        List<Tasks> listFilter = new List<Tasks>();
                        List<Tasks> listContext = _contextTenant.Tasks.Where(x => x.DateValue != null && x.DateValue.Value.ToString() != "0001-01-01 00:00:00.0000000").ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Tasks>(); },
                            (item, state, localList) =>
                            {
                                item.DateValue = item.DateValue.Value.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "AssetsAudit")
                    {
                        Console.WriteLine("UpdateToUtcDate - AssetsAudit");
                        List<AssetsAudit> listFilter = new List<AssetsAudit>();
                        List<AssetsAudit> listContext = _contextTenant.AssetsAudit.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<AssetsAudit>(); },
                            (item, state, localList) =>
                            {
                                item.RegistryDate = item.RegistryDate.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "Bill")
                    {
                        Console.WriteLine("UpdateToUtcDate - Bill");
                        List<Bill> listFilter = new List<Bill>();
                        List<Bill> listContext = _contextTenant.Bill.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Bill>(); },
                            (item, state, localList) =>
                            {
                                item.Date = item.Date.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "Postconditions")
                    {
                        Console.WriteLine("UpdateToUtcDate - Postconditions");
                        List<Postconditions> listFilter = new List<Postconditions>();
                        List<Postconditions> listContext = _contextTenant.Postconditions.Where(x => x.DateValue != null && x.DateValue.Value.ToString() != "0001-01-01 00:00:00.0000000").ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Postconditions>(); },
                            (item, state, localList) =>
                            {
                                item.DateValue = item.DateValue.Value.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "Vehicles")
                    {
                        Console.WriteLine("UpdateToUtcDate - Vehicles");
                        List<Vehicles> listFilter = new List<Vehicles>();
                        List<Vehicles> listContext = _contextTenant.Vehicles.Where(x => x.Date != null && x.Date.Value.ToString() != "0001-01-01 00:00:00.0000000").ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Vehicles>(); },
                            (item, state, localList) =>
                            {
                                item.Date = item.Date.Value.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "Expenses")
                    {
                        Console.WriteLine("UpdateToUtcDate - Expenses");
                        List<Expenses> listFilter = new List<Expenses>();
                        List<Expenses> listContext = _contextTenant.Expenses.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Expenses>(); },
                            (item, state, localList) =>
                            {
                                item.Date = item.Date.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "Journeys")
                    {
                        Console.WriteLine("UpdateToUtcDate - Journeys");
                        List<Journeys> listFilter = new List<Journeys>();
                        List<Journeys> listContext = _contextTenant.Journeys.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Journeys>(); },
                            (item, state, localList) =>
                            {
                                if (item.EndDate != null && item.EndDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.EndDate = item.EndDate.Value.ToUniversalTime();
                                }
                                item.StartDate = item.StartDate.ToUniversalTime();
                                
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "WorkOrdersDeritative")
                    {
                        Console.WriteLine("UpdateToUtcDate - WorkOrdersDeritative");
                        List<WorkOrdersDeritative> listFilter = new List<WorkOrdersDeritative>();
                        List<WorkOrdersDeritative> listContext = _contextTenant.WorkOrdersDeritative.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<WorkOrdersDeritative>(); },
                            (item, state, localList) =>
                            {
                                if (item.CreationDate != null && item.CreationDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.CreationDate = item.CreationDate.Value.ToUniversalTime();
                                }
                                if (item.PickUpTime != null && item.PickUpTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.PickUpTime = item.PickUpTime.Value.ToUniversalTime();
                                }
                                if (item.FinalClientClosingTime != null && item.FinalClientClosingTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.FinalClientClosingTime = item.FinalClientClosingTime.Value.ToUniversalTime();
                                }
                                if (item.InternalClosingTime != null && item.InternalClosingTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.InternalClosingTime = item.InternalClosingTime.Value.ToUniversalTime();
                                }
                                if (item.AssignmentTime != null && item.AssignmentTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.AssignmentTime = item.AssignmentTime.Value.ToUniversalTime();
                                }
                                if (item.ActionDate != null && item.ActionDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ActionDate = item.ActionDate.Value.ToUniversalTime();
                                }
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "ExtraFieldsValues")
                    {
                        Console.WriteLine("UpdateToUtcDate - ExtraFieldsValues");
                        List<ExtraFieldsValues> listFilter = new List<ExtraFieldsValues>();
                        List<ExtraFieldsValues> listContext = _contextTenant.ExtraFieldsValues.Where(x => x.DataValue != null && x.DataValue.Value.ToString() != "0001-01-01 00:00:00.0000000").ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<ExtraFieldsValues>(); },
                            (item, state, localList) =>
                            {
                                item.DataValue = item.DataValue.Value.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "PeopleCost")
                    {
                        Console.WriteLine("UpdateToUtcDate - PeopleCost");
                        List<PeopleCost> listFilter = new List<PeopleCost>();
                        List<PeopleCost> listContext = _contextTenant.PeopleCost.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<PeopleCost>(); },
                            (item, state, localList) =>
                            {
                                if (item.StartDate != null && item.StartDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.StartDate = item.StartDate.Value.ToUniversalTime();
                                }
                                if (item.EndDate != null && item.EndDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.EndDate = item.EndDate.Value.ToUniversalTime();
                                }
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "PeopleCostHistorical")
                    {
                        Console.WriteLine("UpdateToUtcDate - PeopleCostHistorical");
                        List<PeopleCostHistorical> listFilter = new List<PeopleCostHistorical>();
                        List<PeopleCostHistorical> listContext = _contextTenant.PeopleCostHistorical.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<PeopleCostHistorical>(); },
                            (item, state, localList) =>
                            {
                                item.Until = item.Until.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "SgsClosingInfo")
                    {
                        Console.WriteLine("UpdateToUtcDate - SgsClosingInfo");
                        List<SgsClosingInfo> listFilter = new List<SgsClosingInfo>();
                        List<SgsClosingInfo> listContext = _contextTenant.SgsClosingInfo.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<SgsClosingInfo>(); },
                            (item, state, localList) =>
                            {
                                item.SentDate = item.SentDate.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "PreconditionsLiteralValues")
                    {
                        Console.WriteLine("UpdateToUtcDate - PreconditionsLiteralValues");
                        List<PreconditionsLiteralValues> listFilter = new List<PreconditionsLiteralValues>();
                        List<PreconditionsLiteralValues> listContext = _contextTenant.PreconditionsLiteralValues.Where(x => x.DataValue != null && x.DataValue.Value.ToString() != "0001-01-01 00:00:00.0000000").ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<PreconditionsLiteralValues>(); },
                            (item, state, localList) =>
                            {
                                item.DataValue = item.DataValue.Value.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "PeoplePermissions")
                    {
                        Console.WriteLine("UpdateToUtcDate - PeoplePermissions");
                        List<PeoplePermissions> listFilter = new List<PeoplePermissions>();
                        List<PeoplePermissions> listContext = _contextTenant.PeoplePermissions.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<PeoplePermissions>(); },
                            (item, state, localList) =>
                            {
                                item.AssignmentDate = item.AssignmentDate.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "ServicesAnalysis")
                    {
                        Console.WriteLine("UpdateToUtcDate - ServicesAnalysis");
                        List<ServicesAnalysis> listFilter = new List<ServicesAnalysis>();
                        List<ServicesAnalysis> listContext = _contextTenant.ServicesAnalysis.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<ServicesAnalysis>(); },
                            (item, state, localList) =>
                            {
                                if (item.CreationDateTime != null && item.CreationDateTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.CreationDateTime = item.CreationDateTime.Value.ToUniversalTime();
                                }
                                if (item.StartTime != null && item.StartTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.StartTime = item.StartTime.Value.ToUniversalTime();
                                }
                                if (item.EndingTime != null && item.EndingTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.EndingTime = item.EndingTime.Value.ToUniversalTime();
                                }
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "WorkOrderAnalysis")
                    {
                        Console.WriteLine("UpdateToUtcDate - WorkOrderAnalysis");
                        List<WorkOrderAnalysis> listFilter = new List<WorkOrderAnalysis>();
                        List<WorkOrderAnalysis> listContext = _contextTenant.WorkOrderAnalysis.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<WorkOrderAnalysis>(); },
                            (item, state, localList) =>
                            {
                                if(item.ClientCreationDate != null && item.ClientCreationDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ClientCreationDate = item.ClientCreationDate.Value.ToUniversalTime();
                                }
                                if (item.ActuationDate != null && item.ActuationDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ActuationDate = item.ActuationDate.Value.ToUniversalTime();
                                }
                                if (item.InternalSystemTimeWhenOtclosed != null && item.InternalSystemTimeWhenOtclosed.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.InternalSystemTimeWhenOtclosed = item.InternalSystemTimeWhenOtclosed.Value.ToUniversalTime();
                                }
                                if (item.ClosingClientTime != null && item.ClosingClientTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ClosingClientTime = item.ClosingClientTime.Value.ToUniversalTime();
                                }
                                if (item.SlaresponseDate != null && item.SlaresponseDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.SlaresponseDate = item.SlaresponseDate.Value.ToUniversalTime();
                                }
                                if (item.SlaresolutionDate != null && item.SlaresolutionDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.SlaresolutionDate = item.SlaresolutionDate.Value.ToUniversalTime();
                                }
                                if (item.ClosingClientDate != null && item.ClosingClientDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ClosingClientDate = item.ClosingClientDate.Value.ToUniversalTime();
                                }
                                if (item.ClosingSystemDate != null && item.ClosingSystemDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ClosingSystemDate = item.ClosingSystemDate.Value.ToUniversalTime();
                                }
                                if (item.AccountingClosingDate != null && item.AccountingClosingDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.AccountingClosingDate = item.AccountingClosingDate.Value.ToUniversalTime();
                                }
                                if (item.SlaResponsePenaltyDate != null && item.SlaResponsePenaltyDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.SlaResponsePenaltyDate = item.SlaResponsePenaltyDate.Value.ToUniversalTime();
                                }
                                if (item.SlaResolutionPenaltyDate != null && item.SlaResolutionPenaltyDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.SlaResolutionPenaltyDate = item.SlaResolutionPenaltyDate.Value.ToUniversalTime();
                                }

                                item.InternalCreationDate = item.InternalCreationDate.ToUniversalTime();
                                item.ClosingWodate = item.ClosingWodate.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "Services")
                    {
                        Console.WriteLine("UpdateToUtcDate - Services");
                        List<Services> listFilter = new List<Services>();
                        List<Services> listContext = _contextTenant.Services.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Services>(); },
                            (item, state, localList) =>
                            {
                                if (item.DeliveryProcessInit != null && item.DeliveryProcessInit.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.DeliveryProcessInit = item.DeliveryProcessInit.Value.ToUniversalTime();
                                }
                                item.CreationDate = item.CreationDate.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "WorkOrders")
                    {
                        Console.WriteLine("UpdateToUtcDate - WorkOrders");
                        List<WorkOrders> listFilter = new List<WorkOrders>();
                        List<WorkOrders> listContext = _contextTenant.WorkOrders.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<WorkOrders>(); },
                            (item, state, localList) =>
                            {
                                if (item.PickUpTime != null && item.PickUpTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.PickUpTime = item.PickUpTime.Value.ToUniversalTime();
                                }
                                if (item.FinalClientClosingTime != null && item.FinalClientClosingTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.FinalClientClosingTime = item.FinalClientClosingTime.Value.ToUniversalTime();
                                }
                                if (item.InternalClosingTime != null && item.InternalClosingTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.InternalClosingTime = item.InternalClosingTime.Value.ToUniversalTime();
                                }
                                if (item.AssignmentTime != null && item.AssignmentTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.AssignmentTime = item.AssignmentTime.Value.ToUniversalTime();
                                }
                                if (item.ActionDate != null && item.ActionDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ActionDate = item.ActionDate.Value.ToUniversalTime();
                                }
                                if (item.DateStopTimerSla != null && item.DateStopTimerSla.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.DateStopTimerSla = item.DateStopTimerSla.Value.ToUniversalTime();
                                }
                                if (item.ResponseDateSla != null && item.ResponseDateSla.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ResponseDateSla = item.ResponseDateSla.Value.ToUniversalTime();
                                }
                                if (item.ResolutionDateSla != null && item.ResolutionDateSla.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ResolutionDateSla = item.ResolutionDateSla.Value.ToUniversalTime();
                                }
                                if (item.DateUnansweredPenaltySla != null && item.DateUnansweredPenaltySla.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.DateUnansweredPenaltySla = item.DateUnansweredPenaltySla.Value.ToUniversalTime();
                                }
                                if (item.DatePenaltyWithoutResolutionSla != null && item.DatePenaltyWithoutResolutionSla.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.DatePenaltyWithoutResolutionSla = item.DatePenaltyWithoutResolutionSla.Value.ToUniversalTime();
                                }
                                if (item.ActuationEndDate != null && item.ActuationEndDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ActuationEndDate = item.ActuationEndDate.Value.ToUniversalTime();
                                }
                                if (item.LastModified != null && item.LastModified.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.LastModified = item.LastModified.Value.ToUniversalTime();
                                }
                                if (item.ClosingOtdate != null && item.ClosingOtdate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ClosingOtdate = item.ClosingOtdate.Value.ToUniversalTime();
                                }
                                if (item.AccountingClosingDate != null && item.AccountingClosingDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.AccountingClosingDate = item.AccountingClosingDate.Value.ToUniversalTime();
                                }
                                if (item.ClientClosingDate != null && item.ClientClosingDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ClientClosingDate = item.ClientClosingDate.Value.ToUniversalTime();
                                }
                                if (item.SystemDateWhenOtclosed != null && item.SystemDateWhenOtclosed.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.SystemDateWhenOtclosed = item.SystemDateWhenOtclosed.Value.ToUniversalTime();
                                }
                                if (item.InternalCreationDate != null && item.InternalCreationDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.InternalCreationDate = item.InternalCreationDate.Value.ToUniversalTime();
                                }                                
                                item.CreationDate = item.CreationDate.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "Audits")
                    {
                        Console.WriteLine("UpdateToUtcDate - Audits");
                        List<Audits> listFilter = new List<Audits>();
                        List<Audits> listContext = _contextTenant.Audits.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Audits>(); },
                            (item, state, localList) =>
                            {
                                item.DataHora = item.DataHora.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "Contracts")
                    {
                        Console.WriteLine("UpdateToUtcDate - Contracts");
                        List<Contracts> listFilter = new List<Contracts>();
                        List<Contracts> listContext = _contextTenant.Contracts.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Contracts>(); },
                            (item, state, localList) =>
                            {
                                if (item.StartDate != null && item.StartDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.StartDate = item.StartDate.Value.ToUniversalTime();
                                }
                                if (item.EndDate != null && item.EndDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.EndDate = item.EndDate.Value.ToUniversalTime();
                                }
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "JourneysStates")
                    {
                        Console.WriteLine("UpdateToUtcDate - JourneysStates");
                        List<JourneysStates> listFilter = new List<JourneysStates>();
                        List<JourneysStates> listContext = _contextTenant.JourneysStates.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<JourneysStates>(); },
                            (item, state, localList) =>
                            {
                                item.Data = item.Data.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "ExpensesTickets")
                    {
                        Console.WriteLine("UpdateToUtcDate - ExpensesTickets");
                        List<ExpensesTickets> listFilter = new List<ExpensesTickets>();
                        List<ExpensesTickets> listContext = _contextTenant.ExpensesTickets.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<ExpensesTickets>(); },
                            (item, state, localList) =>
                            {
                                if (item.ValidationDate != null && item.ValidationDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ValidationDate = item.ValidationDate.Value.ToUniversalTime();
                                }
                                item.Date = item.Date.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "Guarantee")
                    {
                        Console.WriteLine("UpdateToUtcDate - Guarantee");
                        List<Guarantee> listFilter = new List<Guarantee>();
                        List<Guarantee> listContext = _contextTenant.Guarantee.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Guarantee>(); },
                            (item, state, localList) =>
                            {
                                if (item.StdStartDate != null && item.StdStartDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.StdStartDate = item.StdStartDate.Value.ToUniversalTime();
                                }
                                if (item.StdEndDate != null && item.StdEndDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.StdEndDate = item.StdEndDate.Value.ToUniversalTime();
                                }
                                if (item.BlnStartDate != null && item.BlnStartDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.BlnStartDate = item.BlnStartDate.Value.ToUniversalTime();
                                }
                                if (item.BlnEndDate != null && item.BlnEndDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.BlnEndDate = item.BlnEndDate.Value.ToUniversalTime();
                                }
                                if (item.ProStartDate != null && item.ProStartDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ProStartDate = item.ProStartDate.Value.ToUniversalTime();
                                }
                                if (item.ProEndDate != null && item.ProEndDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ProEndDate = item.ProEndDate.Value.ToUniversalTime();
                                }
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "MainWoregistry")
                    {
                        Console.WriteLine("UpdateToUtcDate - MainWoregistry");
                        List<MainWoregistry> listFilter = new List<MainWoregistry>();
                        List<MainWoregistry> listContext = _contextTenant.MainWoregistry.Where(x => x.ArrivalTime != null && x.ArrivalTime.Value.ToString() != "0001-01-01 00:00:00.0000000").ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<MainWoregistry>(); },
                            (item, state, localList) =>
                            {
                                item.ArrivalTime = item.ArrivalTime.Value.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "PlanificationProcessCalendarChangeTracker")
                    {
                        Console.WriteLine("UpdateToUtcDate - PlanificationProcessCalendarChangeTracker");
                        List<PlanificationProcessCalendarChangeTracker> listFilter = new List<PlanificationProcessCalendarChangeTracker>();
                        List<PlanificationProcessCalendarChangeTracker> listContext = _contextTenant.PlanificationProcessCalendarChangeTracker.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<PlanificationProcessCalendarChangeTracker>(); },
                            (item, state, localList) =>
                            {
                                if (item.LastCheckTime != null && item.LastCheckTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.LastCheckTime = item.LastCheckTime.Value.ToUniversalTime();
                                }
                                if (item.ModificationDate != null && item.ModificationDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.ModificationDate = item.ModificationDate.Value.ToUniversalTime();
                                }
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "PlanificationProcessWorkOrderChangeTracker")
                    {
                        Console.WriteLine("UpdateToUtcDate - PlanificationProcessWorkOrderChangeTracker");
                        List<PlanificationProcessWorkOrderChangeTracker> listFilter = new List<PlanificationProcessWorkOrderChangeTracker>();
                        List<PlanificationProcessWorkOrderChangeTracker> listContext = _contextTenant.PlanificationProcessWorkOrderChangeTracker.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<PlanificationProcessWorkOrderChangeTracker>(); },
                            (item, state, localList) =>
                            {
                                if (item.LastCheckTime != null && item.LastCheckTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.LastCheckTime = item.LastCheckTime.Value.ToUniversalTime();
                                }
                                if (item.LastModified != null && item.LastModified.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.LastModified = item.LastModified.Value.ToUniversalTime();
                                }
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "PushNotifications")
                    {
                        Console.WriteLine("UpdateToUtcDate - PushNotifications");
                        List<PushNotifications> listFilter = new List<PushNotifications>();
                        List<PushNotifications> listContext = _contextTenant.PushNotifications.Where(x => x.CreationDate != null && x.CreationDate.Value.ToString() != "0001-01-01 00:00:00.0000000").ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<PushNotifications>(); },
                            (item, state, localList) =>
                            {
                                item.CreationDate = item.CreationDate.Value.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "SomFiles")
                    {
                        Console.WriteLine("UpdateToUtcDate - SomFiles");
                        List<SomFiles> listFilter = new List<SomFiles>();
                        List<SomFiles> listContext = _contextTenant.SomFiles.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<SomFiles>(); },
                            (item, state, localList) =>
                            {
                                item.ModifiedDate = item.ModifiedDate.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "SystemNotifications")
                    {
                        Console.WriteLine("UpdateToUtcDate - SystemNotifications");
                        List<SystemNotifications> listFilter = new List<SystemNotifications>();
                        List<SystemNotifications> listContext = _contextTenant.SystemNotifications.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<SystemNotifications>(); },
                            (item, state, localList) =>
                            {
                                if (item.PublicationDateTime != null && item.PublicationDateTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.PublicationDateTime = item.PublicationDateTime.Value.ToUniversalTime();
                                }
                                if (item.VisibilityEndTime != null && item.VisibilityEndTime.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.VisibilityEndTime = item.VisibilityEndTime.Value.ToUniversalTime();
                                }
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "TaskTokens")
                    {
                        Console.WriteLine("UpdateToUtcDate - TaskTokens");
                        List<TaskTokens> listFilter = new List<TaskTokens>();
                        List<TaskTokens> listContext = _contextTenant.TaskTokens.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<TaskTokens>(); },
                            (item, state, localList) =>
                            {
                                item.CreationDate = item.CreationDate.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "ExternalSystemImportData")
                    {
                        Console.WriteLine("UpdateToUtcDate - ExternalSystemImportData");
                        List<ExternalSystemImportData> listFilter = new List<ExternalSystemImportData>();
                        List<ExternalSystemImportData> listContext = _contextTenant.ExternalSystemImportData.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<ExternalSystemImportData>(); },
                            (item, state, localList) =>
                            {
                                item.CreationDate = item.CreationDate.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "Sessions")
                    {
                        Console.WriteLine("UpdateToUtcDate - Sessions");
                        List<Sessions> listFilter = new List<Sessions>();
                        List<Sessions> listContext = _contextTenant.Sessions.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<Sessions>(); },
                            (item, state, localList) =>
                            {
                                item.DateLastActivity = item.DateLastActivity.ToUniversalTime();
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                    else if (valkey == "CalendarEvents")
                    {
                        Console.WriteLine("UpdateToUtcDate - CalendarEvents");
                        List<CalendarEvents> listFilter = new List<CalendarEvents>();
                        List<CalendarEvents> listContext = _contextTenant.CalendarEvents.ToList();
                        Console.WriteLine($"Registros a modificar: {listContext.Count()}");

                        object localLockObject = new object();
                        Parallel.ForEach(listContext,
                            () => { return new List<CalendarEvents>(); },
                            (item, state, localList) =>
                            {
                                if(item.EndDate != null && item.EndDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    item.EndDate = item.EndDate.Value.ToUniversalTime();
                                }

                                if (item.StartDate != null && item.StartDate.Value.ToString() != "0001-01-01 00:00:00.0000000")
                                {
                                    if (item.RepetitionType != 0)
                                    {
                                        DateTime toUtc = new DateTime(item.StartDate.Value.Year, item.StartDate.Value.Month, item.StartDate.Value.Day, item.StartTime.Value.Hours, item.StartTime.Value.Minutes, item.StartTime.Value.Seconds);
                                        item.StartDate = toUtc.ToUniversalTime();

                                        item.StartTime = item.StartDate.Value.TimeOfDay;
                                        item.StartDate = new DateTime(item.StartDate.Value.Year, item.StartDate.Value.Month, item.StartDate.Value.Day, 0, 0, 0);
                                    }
                                    else
                                    {
                                        item.StartDate = item.StartDate.Value.ToUniversalTime();
                                    }
                                }

                                item.ModificationDate = item.ModificationDate.ToUniversalTime();
                                
                                localList.Add(item);
                                return localList;
                            },
                            (finalResult) => { lock (localLockObject) listFilter.AddRange(finalResult); });

                        listContext.Clear();

                        if (listFilter.Any())
                        {
                            UpdateDatabaseTenant(listFilter, valkey);
                        }
                    }
                }
            }
        }

        private Dictionary<string, string> ReadTablesFromCsv(string csvPath)
        {
            Console.WriteLine("ReadQueriesFromCsv");
            StreamReader reader = new StreamReader(csvPath);
            var valuesList = new Dictionary<string, string>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

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

        public void UpdateDatabaseTenant<TEntity>(IEnumerable<TEntity> dataList, string table) where TEntity : class
        {
            Console.WriteLine($"Inicio update tabla {table} - Registros a modificar: {dataList.Count()}");
            using (var transaction = _contextTenant.Database.BeginTransaction())
            {
                try
                {
                    _contextTenant.UpdateRange(dataList);
                    _contextTenant.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la modificación de la tabla {table}. {ex}");
                }
            }
            dataList = null;
            Console.WriteLine($"Fin modificación tabla {table}");
        }
    }
}