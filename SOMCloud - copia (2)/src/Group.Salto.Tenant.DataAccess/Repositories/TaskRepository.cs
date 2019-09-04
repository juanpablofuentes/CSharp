using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Entities.Tenant.ContentTranslations;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class TaskRepository : BaseRepository<Tasks>, ITaskRepository
    {
        public TaskRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Tasks> GetAll()
        {
            return All();
        }

        public IQueryable<Tasks> GetAllById(IList<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id));
        }

        public Tasks GetByIdWithIncludeTranslations(int id)
        {
            return Filter(x => x.Id == id)
                .Include(x => x.TasksTranslations)
                .Include(pt => pt.PermissionsTasks)
                .Include(p => p.Preconditions)
                    .ThenInclude(lp => lp.LiteralsPreconditions)
                        .ThenInclude(plv => plv.PreconditionsLiteralValues)
                .FirstOrDefault();
        }

        public SaveResult<Tasks> UpdateTask(Tasks task)
        {
            task.UpdateDate = DateTime.UtcNow;
            Update(task);
            var result = SaveChange(task);
            return result;
        }

        public SaveResult<Tasks> CreateTask(Tasks entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public Dictionary<int, string> GetTasksIdNameByFlowId(int id)
        {
            return Filter(x => x.FlowId == id).ToDictionary(c => c.Id, c => c.Name);
        }

        public Tasks GetById(int taskId)
        {
            return Filter(t => t.Id == taskId)
                .Include(x => x.ExternalWorOrderStatus)
                .Include(x => x.MailTemplate)
                .Include(x => x.PeopleManipulator)
                .Include(x => x.PeopleResponsibleTechnicians)
                .Include(x => x.PeopleTechnician)
                .Include(x => x.PredefinedService)
                .Include(x => x.Queue)
                .Include(x => x.WorkOrderStatus)
                .Include(x => x.TasksTranslations)
                .Include(pt => pt.PermissionsTasks)
                .Include(p => p.Preconditions)
                    .ThenInclude(lp => lp.LiteralsPreconditions)
                        .ThenInclude(plv => plv.PreconditionsLiteralValues)
                .FirstOrDefault();
        }

        public IQueryable<Tasks> GetAvailableTasksFromWoId(People people, WorkOrders workOrder, IEnumerable<int> woTypes)
        {
            int countWoTypes = 0;
            if (woTypes != null)
            {
                countWoTypes = woTypes.Count();
            }

            var postalCodes = workOrder.Project.ZoneProject.SelectMany(zp => zp.ZoneProjectPostalCode).Select(pc => pc.PostalCode);
            var tasksPermited = people.PeoplePermissions.SelectMany(pp => pp.Permission.PermissionTask).Select(pt => pt.TaskId).Distinct().ToList();

            //TODO if merge filters task precond and task postondition precondition, increase execution time
            var tasks = Filter(t =>
                tasksPermited.Contains(t.Id) &&
                (t.Preconditions.Count == 0 || t.Preconditions.Any(pc => pc.LiteralsPreconditions.All(p =>
                      p.NomCampModel == PreconditionFieldNameEnum.Cua.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.QueueId == workOrder.QueueId)
                   || p.NomCampModel == PreconditionFieldNameEnum.EstatOT.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.State.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && workOrder.Location != null && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.Location.StateId)
                   || p.NomCampModel == PreconditionFieldNameEnum.EstatOTExtern.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.ExternalWorOrderStatusId == workOrder.ExternalWorOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Tecnic.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleTechnicianId == workOrder.PeopleResponsibleId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Tecnic.ToString() && p.ComparisonOperator == TaskOperatorEnum.NoNull.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleTechnicianId != null)
                   || p.NomCampModel == PreconditionFieldNameEnum.Tecnic.ToString() && p.ComparisonOperator == TaskOperatorEnum.Null.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleTechnicianId == null)
                   || p.NomCampModel == PreconditionFieldNameEnum.Manipulador.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleManipulatorId == workOrder.PeopleManipulatorId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Procedencia.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.OriginId == workOrder.OriginId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Billable.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.BooleanValue == workOrder.Billable)
                   || p.NomCampModel == PreconditionFieldNameEnum.Reparacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.StringValue == workOrder.TextRepair)
                   || p.NomCampModel == PreconditionFieldNameEnum.Zone.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.Zone.ZoneProject.Any(zp => zp.ZoneProjectPostalCode.Any(zpc => postalCodes.Contains(zpc.PostalCode))))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN1.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 1 && lv.WorkOrderTypesN1id == woTypes.ElementAt(0)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN2.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 2 && lv.WorkOrderTypesN2id == woTypes.ElementAt(1)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN3.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 3 && lv.WorkOrderTypesN3id == woTypes.ElementAt(2)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN4.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 4 && lv.WorkOrderTypesN4id == woTypes.ElementAt(3)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN5.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 5 && lv.WorkOrderTypesN5id == woTypes.ElementAt(4)))
                   || p.NomCampModel == PreconditionFieldNameEnum.ClientFinal.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.FinalClientId == workOrder.FinalClientId)
                   || p.NomCampModel == PreconditionFieldNameEnum.ParentWOInternalStatus.ToString() && workOrder.WorkOrdersFather != null && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrdersFather.WorkOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.ParentWOExternalStatus.ToString() && workOrder.WorkOrdersFather != null && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrdersFather.ExternalWorOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.ParentWOQueue.ToString() && workOrder.WorkOrdersFather != null && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrdersFather.QueueId)
                   || p.NomCampModel == PreconditionFieldNameEnum.UbicacioClientFinal.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.LocationId == workOrder.LocationId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Equip.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.AssetId == workOrder.AssetId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Project.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.ProjectId == workOrder.ProjectId)
                   || p.NomCampModel == PreconditionFieldNameEnum.WOCategory.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderCategoryId == workOrder.WorkOrderCategoryId)
                   || p.NomCampModel == PreconditionFieldNameEnum.MinutsPerFiSLA.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ResolutionDateSla.HasValue && DateTime.Now < workOrder.ResolutionDateSla.Value.AddMinutes(lv.EnterValue.Value * -1))
                   || p.NomCampModel == PreconditionFieldNameEnum.MinutsPerFiSLA.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ResolutionDateSla.HasValue && DateTime.Now > workOrder.ResolutionDateSla.Value.AddMinutes(lv.EnterValue.Value * -1))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataCreacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.CreationDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataAssignacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.AssignmentTime > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataRecollida.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.PickUpTime > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataActuacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ActuationEndDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentSalto.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.InternalClosingTime > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentClient.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ClientClosingDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataCreacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.CreationDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataAssignacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.AssignmentTime < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataRecollida.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.PickUpTime < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataActuacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ActuationEndDate < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentSalto.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.InternalClosingTime < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentClient.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ClientClosingDate < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   ))));

            return tasks;
        }

        public Tasks GetByIdIncludeBasicInfo(int taskId)
        {
            return Filter(x => x.Id == taskId)
                .Include(t => t.WorkOrderStatus)
                .Include(t => t.ExternalWorOrderStatus)
                .Include(t => t.Queue)
                .Include(t => t.DerivedServices)
                .Include(t => t.WorkOrdersDeritative)
                .Include(t => t.BillingRule).ThenInclude(b => b.BillingRuleItem)
                .Include(t => t.PredefinedService.CollectionExtraField.CollectionsExtraFieldExtraField).ThenInclude(cef => cef.ExtraField).ThenInclude(ef => ef.ExtraFieldsTranslations)
                .Include(t => t.PredefinedService.Project.CollectionsClosureCodes.ClosingCodes).ThenInclude(cc => cc.InverseClosingCodesFather)
                .Include(t => t.MailTemplate)
                .FirstOrDefault();
        }

        public IQueryable<PostconditionCollections> GetAvailablePostConditionCollection(int taskId, People people, WorkOrders workOrder, IEnumerable<int> woTypes)
        {
            int countWoTypes = 0;
            if (woTypes != null)
            {
                countWoTypes = woTypes.Count();
            }

            var postalCodes = workOrder.Project.ZoneProject.SelectMany(zp => zp.ZoneProjectPostalCode).Select(pc => pc.PostalCode);

            //TODO if merge filters task precond and task postondition precondition, increase execution time
            return Filter(t => t.Id == taskId).SelectMany(t => t.PostconditionCollections).Where(pc => pc.Preconditions.Count == 0 || pc.Preconditions.Any(prec => prec.LiteralsPreconditions.All(p =>
                      p.NomCampModel == PreconditionFieldNameEnum.Cua.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.QueueId == workOrder.QueueId)
                   || p.NomCampModel == PreconditionFieldNameEnum.EstatOT.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.State.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && workOrder.Location != null && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.Location.StateId)
                   || p.NomCampModel == PreconditionFieldNameEnum.EstatOTExtern.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.ExternalWorOrderStatusId == workOrder.ExternalWorOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Tecnic.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleTechnicianId == workOrder.PeopleResponsibleId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Tecnic.ToString() && p.ComparisonOperator == TaskOperatorEnum.NoNull.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleTechnicianId != null)
                   || p.NomCampModel == PreconditionFieldNameEnum.Tecnic.ToString() && p.ComparisonOperator == TaskOperatorEnum.Null.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleTechnicianId == null)
                   || p.NomCampModel == PreconditionFieldNameEnum.Manipulador.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleManipulatorId == workOrder.PeopleManipulatorId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Procedencia.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.OriginId == workOrder.OriginId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Billable.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.BooleanValue == workOrder.Billable)
                   || p.NomCampModel == PreconditionFieldNameEnum.Reparacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.StringValue == workOrder.TextRepair)
                   || p.NomCampModel == PreconditionFieldNameEnum.Zone.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.Zone.ZoneProject.Any(zp => zp.ZoneProjectPostalCode.Any(zpc => postalCodes.Contains(zpc.PostalCode))))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN1.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 1 && lv.WorkOrderTypesN1id == woTypes.ElementAt(0)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN2.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 2 && lv.WorkOrderTypesN2id == woTypes.ElementAt(1)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN3.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 3 && lv.WorkOrderTypesN3id == woTypes.ElementAt(2)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN4.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 4 && lv.WorkOrderTypesN4id == woTypes.ElementAt(3)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN5.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 5 && lv.WorkOrderTypesN5id == woTypes.ElementAt(4)))
                   || p.NomCampModel == PreconditionFieldNameEnum.ClientFinal.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.FinalClientId == workOrder.FinalClientId)
                   || p.NomCampModel == PreconditionFieldNameEnum.ParentWOInternalStatus.ToString() && workOrder.WorkOrdersFather != null && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrdersFather.WorkOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.ParentWOExternalStatus.ToString() && workOrder.WorkOrdersFather != null && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrdersFather.ExternalWorOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.ParentWOQueue.ToString() && workOrder.WorkOrdersFather != null && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrdersFather.QueueId)
                   || p.NomCampModel == PreconditionFieldNameEnum.UbicacioClientFinal.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.LocationId == workOrder.LocationId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Equip.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.AssetId == workOrder.AssetId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Project.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.ProjectId == workOrder.ProjectId)
                   || p.NomCampModel == PreconditionFieldNameEnum.WOCategory.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderCategoryId == workOrder.WorkOrderCategoryId)
                   || p.NomCampModel == PreconditionFieldNameEnum.MinutsPerFiSLA.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ResolutionDateSla.HasValue && DateTime.Now < workOrder.ResolutionDateSla.Value.AddMinutes(lv.EnterValue.Value * -1))
                   || p.NomCampModel == PreconditionFieldNameEnum.MinutsPerFiSLA.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ResolutionDateSla.HasValue && DateTime.Now > workOrder.ResolutionDateSla.Value.AddMinutes(lv.EnterValue.Value * -1))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataCreacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.CreationDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataAssignacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.AssignmentTime > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataRecollida.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.PickUpTime > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataActuacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ActuationEndDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentSalto.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.InternalClosingTime > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentClient.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ClientClosingDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataCreacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.CreationDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataAssignacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.AssignmentTime < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataRecollida.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.PickUpTime < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataActuacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ActuationEndDate < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentSalto.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.InternalClosingTime < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentClient.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ClientClosingDate < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   ))).Include(pc => pc.Postconditions);
        }

        public IQueryable<Tasks> GetWorkOrderCreationTask(People people, WorkOrders workOrder, IEnumerable<int> woTypes)
        {
            int countWoTypes = 0;
            if (woTypes != null)
            {
                countWoTypes = woTypes.Count();
            }

            var postalCodes = workOrder.Project.ZoneProject.SelectMany(zp => zp.ZoneProjectPostalCode).Select(pc => pc.PostalCode);
            var tasksPermited = people.PeoplePermissions.SelectMany(pp => pp.Permission.PermissionTask).Select(pt => pt.TaskId).Distinct().ToList();

            //TODO if merge filters task precond and task postondition precondition, increase execution time
            var tasks = Filter(t =>
                tasksPermited.Contains(t.Id) && t.NameFieldModel == TaskTypeEnum.Creacio.ToString() &&
                (t.Preconditions.Count == 0 || t.Preconditions.Any(pc => pc.LiteralsPreconditions.All(p =>
                      p.NomCampModel == PreconditionFieldNameEnum.Cua.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.QueueId == workOrder.QueueId)
                   || p.NomCampModel == PreconditionFieldNameEnum.EstatOT.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.State.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && workOrder.Location != null && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.Location.StateId)
                   || p.NomCampModel == PreconditionFieldNameEnum.EstatOTExtern.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.ExternalWorOrderStatusId == workOrder.ExternalWorOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Tecnic.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleTechnicianId == workOrder.PeopleResponsibleId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Tecnic.ToString() && p.ComparisonOperator == TaskOperatorEnum.NoNull.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleTechnicianId != null)
                   || p.NomCampModel == PreconditionFieldNameEnum.Tecnic.ToString() && p.ComparisonOperator == TaskOperatorEnum.Null.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleTechnicianId == null)
                   || p.NomCampModel == PreconditionFieldNameEnum.Manipulador.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.PeopleManipulatorId == workOrder.PeopleManipulatorId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Procedencia.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.OriginId == workOrder.OriginId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Billable.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.BooleanValue == workOrder.Billable)
                   || p.NomCampModel == PreconditionFieldNameEnum.Reparacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.StringValue == workOrder.TextRepair)
                   || p.NomCampModel == PreconditionFieldNameEnum.Zone.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.Zone.ZoneProject.Any(zp => zp.ZoneProjectPostalCode.Any(zpc => postalCodes.Contains(zpc.PostalCode))))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN1.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 1 && lv.WorkOrderTypesN1id == woTypes.ElementAt(0)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN2.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 2 && lv.WorkOrderTypesN2id == woTypes.ElementAt(1)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN3.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 3 && lv.WorkOrderTypesN3id == woTypes.ElementAt(2)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN4.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 4 && lv.WorkOrderTypesN4id == woTypes.ElementAt(3)))
                   || p.NomCampModel == PreconditionFieldNameEnum.TipusOTN5.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && (p.PreconditionsLiteralValues == null || p.PreconditionsLiteralValues.Count == 0 || p.PreconditionsLiteralValues.Any(lv => countWoTypes >= 5 && lv.WorkOrderTypesN5id == woTypes.ElementAt(4)))
                   || p.NomCampModel == PreconditionFieldNameEnum.ClientFinal.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.FinalClientId == workOrder.FinalClientId)
                   || p.NomCampModel == PreconditionFieldNameEnum.ParentWOInternalStatus.ToString() && workOrder.WorkOrdersFather != null && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrdersFather.WorkOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.ParentWOExternalStatus.ToString() && workOrder.WorkOrdersFather != null && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrdersFather.ExternalWorOrderStatusId)
                   || p.NomCampModel == PreconditionFieldNameEnum.ParentWOQueue.ToString() && workOrder.WorkOrdersFather != null && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderStatusId == workOrder.WorkOrdersFather.QueueId)
                   || p.NomCampModel == PreconditionFieldNameEnum.UbicacioClientFinal.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.LocationId == workOrder.LocationId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Equip.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.AssetId == workOrder.AssetId)
                   || p.NomCampModel == PreconditionFieldNameEnum.Project.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.ProjectId == workOrder.ProjectId)
                   || p.NomCampModel == PreconditionFieldNameEnum.WOCategory.ToString() && p.ComparisonOperator == TaskOperatorEnum.Igual.ToString() && p.PreconditionsLiteralValues.Any(lv => lv.WorkOrderCategoryId == workOrder.WorkOrderCategoryId)
                   || p.NomCampModel == PreconditionFieldNameEnum.MinutsPerFiSLA.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ResolutionDateSla.HasValue && DateTime.Now < workOrder.ResolutionDateSla.Value.AddMinutes(lv.EnterValue.Value * -1))
                   || p.NomCampModel == PreconditionFieldNameEnum.MinutsPerFiSLA.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ResolutionDateSla.HasValue && DateTime.Now > workOrder.ResolutionDateSla.Value.AddMinutes(lv.EnterValue.Value * -1))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataCreacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.CreationDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataAssignacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.AssignmentTime > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataRecollida.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.PickUpTime > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataActuacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ActuationEndDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentSalto.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.InternalClosingTime > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentClient.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ClientClosingDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataCreacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MajorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.CreationDate > DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataAssignacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.AssignmentTime < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataRecollida.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.PickUpTime < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataActuacio.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ActuationEndDate < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentSalto.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.InternalClosingTime < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   || p.NomCampModel == PreconditionFieldNameEnum.DataTancamentClient.ToString() && p.ComparisonOperator == TaskOperatorEnum.MenorQue.ToString() && p.PreconditionsLiteralValues.Any(lv => workOrder.ClientClosingDate < DateTime.Now.AddMinutes(lv.EnterValue.Value))
                   ))));

            return tasks;
        }

        public IQueryable<Tasks> GetTasksByFlowIdIncludeTranslations(int id)
        {
            return this.Filter(x => x.FlowId == id, GetIncludeTasksTranslations());
        }

        private List<Expression<Func<Tasks, object>>> GetIncludeTasksTranslations()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(TasksTranslations) });
        }

        private List<Expression<Func<Tasks, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Tasks, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(TasksTranslations))
                {
                    includesPredicate.Add(p => p.TasksTranslations);
                }
            }
            return includesPredicate;
        }
    }
}