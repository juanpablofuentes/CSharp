using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class WorkOrdersRepository : BaseRepository<WorkOrders>, IWorkOrdersRepository
    {
        public WorkOrdersRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public WorkOrders GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public WorkOrders GetEditById(int id)
        {
            return Filter(x => x.Id == id)
                .Include(x => x.Project)
                .Include(x => x.PeopleResponsible)
                .Include(x => x.FinalClient)
                .Include(x => x.Location)
                .Include(x => x.Asset)
                .Include(x => x.SiteUser)
                .FirstOrDefault();
        }

        public WorkOrders GetByIdIncludeLocationAndCategory(int id)
        {
            return Filter(x => x.Id == id)
                .Include(wo => wo.Location)
                .Include(wo => wo.WorkOrderTypes.WorkOrderTypesFather)
                    .ThenInclude(wof => wof.WorkOrderTypesFather)
                    .ThenInclude(wof => wof.WorkOrderTypesFather)
                    .ThenInclude(wof => wof.WorkOrderTypesFather)
                    .ThenInclude(wof => wof.WorkOrderTypesFather)
                .Include(wo => wo.Project.ZoneProject).ThenInclude(zp => zp.ZoneProjectPostalCode).FirstOrDefault();
        }

        public bool ExistOriginId(int originId)
        {
            return base.Any(x => x.OriginId == originId);
        }

        public IEnumerable<WorkOrders> GetByPeopleAndDate(People people, DateTime? date, bool getAll)
        {
            var projectPerm = people.PeoplePermissions.SelectMany(pp => pp.Permission.ProjectPermission.Select(prp => prp.ProjectId)).Distinct();
            var queuePerm = people.PeoplePermissions.SelectMany(pp => pp.Permission.PermissionQueue.Select(prp => prp.QueueId)).Distinct();
            var woCategoyPerm = people.PeoplePermissions.SelectMany(pp => pp.Permission.WorkOrderCategoryPermission.Select(prp => prp.WorkOrderCategoryId)).Distinct();

            var query = Filter(w => w.PeopleResponsibleId == people.Id).Include(w => w.WorkOrderStatus)
                .Include(w => w.WorkOrderCategory.Sla.StatesSla)
                .Include(w => w.WorkOrderTypes)
                .Include(w => w.Location)
                .Include(w => w.Asset);

            var queryInclude = query.Where(w => queuePerm.Contains(w.QueueId) || projectPerm.Contains(w.ProjectId) || woCategoyPerm.Contains(w.WorkOrderCategoryId));

            if (!getAll)
            {
                if (date != null)
                {
                    queryInclude = queryInclude.Where(w =>
                        w.ActionDate.HasValue &&
                        w.ActionDate.Value.Year == date.Value.Year &&
                        w.ActionDate.Value.Month == date.Value.Month &&
                        w.ActionDate.Value.Day == date.Value.Day);
                }
                else
                {
                    queryInclude = queryInclude.Where(w => w.ActionDate == null);
                }
            }

            return queryInclude;
        }

        public int GetCountOpenOrdersSameLocation(int woLocationId)
        {
            return DbSet.Count(wo => wo.LocationId == woLocationId && wo.WorkOrderStatus.IsWoclosed.HasValue && !wo.WorkOrderStatus.IsWoclosed.Value);
        }

        public WorkOrders GetFullWorkOrderInfo(int id)
        {
            //TODO ClosingCodesFather recursive load
            return Filter(wo => wo.Id == id)
                .Include(wo => wo.Project).ThenInclude(p => p.Contract).ThenInclude(c => c.Client)
                .Include(wo => wo.PeopleResponsible)
                .Include(wo => wo.Location).ThenInclude(l => l.ContactsLocationsFinalClients).ThenInclude(c => c.Contact)
                .Include(wo => wo.Location.PeopleResponsibleLocation)
                .Include(wo => wo.Queue)
                .Include(wo => wo.FinalClient)
                .Include(wo => wo.WorkOrderTypes).ThenInclude(x => x.WorkOrderTypesFather)
                .Include(wo => wo.WorkOrderTypes).ThenInclude(x => x.CollectionsTypesWorkOrders)
                .Include(wo => wo.WorkOrderStatus)
                .Include(wo => wo.ExternalWorOrderStatus)
                .Include(wo => wo.WorkOrderCategory)
                .Include(wo => wo.SiteUser).ThenInclude(su => su.Location).ThenInclude(l => l.PeopleResponsibleLocation)
                .Include(wo => wo.SiteUser).ThenInclude(su => su.Assets).ThenInclude(l => l.Guarantee)
                .Include(wo => wo.SiteUser).ThenInclude(su => su.Assets).ThenInclude(l => l.Location)
                .Include(wo => wo.SiteUser).ThenInclude(su => su.Assets).ThenInclude(l => l.Model.Brand)
                .Include(wo => wo.SiteUser).ThenInclude(su => su.Assets).ThenInclude(l => l.WorkOrders)
                .Include(wo => wo.Asset).ThenInclude(l => l.Guarantee)
                .Include(wo => wo.Asset).ThenInclude(l => l.Location)
                .Include(wo => wo.Asset).ThenInclude(l => l.AssetStatus)
                .Include(wo => wo.Asset).ThenInclude(l => l.SubFamily.Family)
                .Include(wo => wo.Asset).ThenInclude(l => l.Model.Brand)
                .Include(wo => wo.Asset).ThenInclude(l => l.WorkOrders).ThenInclude(wo => wo.Services).ThenInclude(s => s.PeopleResponsible)
                .Include(su => su.Asset).ThenInclude(l => l.WorkOrders).ThenInclude(wo => wo.Services).ThenInclude(s => s.PredefinedService)
                .Include(su => su.Asset).ThenInclude(l => l.WorkOrders).ThenInclude(wo => wo.Services).ThenInclude(s => s.ExtraFieldsValues).ThenInclude(e => e.ExtraField)
                .Include(su => su.Asset).ThenInclude(l => l.WorkOrders).ThenInclude(wo => wo.Services).ThenInclude(s => s.ExtraFieldsValues).ThenInclude(e => e.MaterialForm)
                .Include(su => su.Asset).ThenInclude(l => l.WorkOrders).ThenInclude(wo => wo.Services).ThenInclude(s => s.ClosingCode.ClosingCodesFather.ClosingCodesFather.ClosingCodesFather.ClosingCodesFather.ClosingCodesFather.ClosingCodesFather.ClosingCodesFather)
                .Include(wo => wo.Services).ThenInclude(s => s.ExtraFieldsValues).ThenInclude(e => e.ExtraField)
                .Include(wo => wo.Services).ThenInclude(s => s.ExtraFieldsValues).ThenInclude(e => e.MaterialForm)
                .Include(wo => wo.Services).ThenInclude(s => s.PredefinedService)
                .Include(wo => wo.Services).ThenInclude(s => s.ClosingCode.ClosingCodesFather.ClosingCodesFather.ClosingCodesFather.ClosingCodesFather.ClosingCodesFather.ClosingCodesFather.ClosingCodesFather)
                .FirstOrDefault();
        }

        public SaveResult<WorkOrders> CreateWorkOrders(WorkOrders entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<WorkOrders> UpdateWorkOrder(WorkOrders wo)
        {
            wo.UpdateDate = DateTime.UtcNow;
            Update(wo);
            var result = SaveChange(wo);
            return result;
        }

        public bool ExistWorkOrderTypes(int id)
        {
            return Any(x => x.WorkOrderTypesId == id);
        }

        public WorkOrders GetByIdIncludingExecuteValues(int id)
        {
            return Filter(wo => wo.Id == id)
                .Include(w => w.Services)
                .Include(w => w.Audits)
                .Include(w => w.WorkOrderCategory.Sla)
                .Include(wo => wo.Location)
                .Include(wo => wo.FinalClient)
                .Include(wo => wo.Location.LocationsFinalClients)
                .Include(wo => wo.PeopleResponsible)
                .Include(wo => wo.PeopleResponsible).ThenInclude(p => p.Subcontract)
                .Include(wo => wo.PeopleResponsible).ThenInclude(p => p.PeopleCost)
                .Include(wo => wo.PeopleResponsible).ThenInclude(p => p.Company)
                .Include(wo => wo.PeopleManipulator)
                .Include(wo => wo.WorkOrdersFather)
                .Include(wo => wo.ServicesAnalysis)
                .Include(wo => wo.Services).ThenInclude(s => s.ExtraFieldsValues)
                .Include(wo => wo.WorkOrderTypes.WorkOrderTypesFather)
                    .ThenInclude(wof => wof.WorkOrderTypesFather)
                    .ThenInclude(wof => wof.WorkOrderTypesFather)
                    .ThenInclude(wof => wof.WorkOrderTypesFather)
                    .ThenInclude(wof => wof.WorkOrderTypesFather)
                .Include(wo => wo.Project.ZoneProject).ThenInclude(zp => zp.ZoneProjectPostalCode)
                .Include(wo => wo.Project.Contract).ThenInclude(c => c.Client)
                .Include(w => w.Bill).ThenInclude(b => b.BillLine).ThenInclude(bl => bl.Item).ThenInclude(i => i.ItemsPurchaseRate)
                .Include(w => w.Bill).ThenInclude(b => b.BillLine).ThenInclude(bl => bl.DnAndMaterialsAnalysis)
                .Include(wo => wo.Queue)
                .Include(wo => wo.WorkOrderStatus)
                .Include(wo => wo.ExternalWorOrderStatus)
                .Include(wo => wo.Asset).ThenInclude(a => a.Location)
                .Include(wo => wo.ExpensesTickets)
                .Include(wo => wo.WorkOrderAnalysis)
                .FirstOrDefault();
        }

        public List<List<DataBaseResultDto>> GetConfiguredWorkOrder(string sql, List<SqlParameter> parameterList)
        {
            List<List<DataBaseResultDto>> result = this.ExecuteRawSQL(sql, parameterList);
            return result;
        }

        public int GetCountConfiguredWorkOrder(string sql, List<SqlParameter> parameterList)
        {
            int result = Convert.ToInt32(this.ExecuteScalarSQL(sql, parameterList));
            return result;
        }

        public WorkOrders GetByIdIncludeBasicInfo(int workOrderId)
        {
            return Filter(wo => wo.Id == workOrderId)
                .Include(w => w.Queue)
                .Include(w => w.WorkOrderStatus)
                .Include(w => w.ExternalWorOrderStatus)
                .FirstOrDefault();
        }

        public WorkOrders GetDetailWorkOrderInfo(int Id)
        {
            var query = Filter(x => x.Id == Id)
                .Include(w => w.Queue)
                .Include(w => w.WorkOrderStatus)
                .Include(w => w.ExternalWorOrderStatus)
                .Include(x => x.WorkOrderCategory)
                .Include(x => x.WorkOrderTypes)
                .Include(x => x.Location)
                .Include(x => x.Project)
                .Include(wo => wo.PeopleResponsible)
                .Include(wo => wo.Asset)
                    .ThenInclude(m => m.Model)
                        .ThenInclude(b => b.Brand)
                .Include(x => x.FinalClient)
                .Include(x => x.WorkOrderAnalysis);
            return query.FirstOrDefault();
        }

        public WorkOrders GetSummaryWorkOrderInfo(int Id)
        {
            var query = Filter(x => x.Id == Id)
                .Include(wo => wo.Queue)
                .Include(wo => wo.WorkOrderStatus)
                .Include(wo => wo.ExternalWorOrderStatus)
                .Include(wo => wo.WorkOrderCategory)
                .Include(wo => wo.WorkOrderTypes)
                .Include(wo => wo.Location)
                .Include(wo => wo.Project)
                    .ThenInclude(p => p.Contract)
                .Include(wo => wo.PeopleResponsible)
                .Include(wo => wo.Asset)
                .Include(wo => wo.FinalClient);
            return query.FirstOrDefault();
        }

        public IQueryable<WorkOrders> GetSubWOInfo(int Id)
        {
            return Filter(wo => wo.WorkOrdersFatherId == Id)
                .Include(wo => wo.WorkOrderStatus)
                .Include(wo => wo.PeopleResponsible)
                .Include(wo => wo.WorkOrderCategory)
                .Include(wo => wo.Project)
                .Include(wo => wo.Queue);
        }

        public IQueryable<WorkOrders> GetAllByAssetId(int assetId)
        {
            return Filter(wo => wo.AssetId == assetId);
        }

        public WorkOrders GetAllServiceAndExtraFieldsById(int id)
        {
            return Filter(wo => wo.Id == id)
                .Include(x => x.Services)
                    .ThenInclude(x => x.PredefinedService)
                .Include(x => x.Services)
                    .ThenInclude(x => x.PeopleResponsible)
                .Include(x => x.Services)
                    .ThenInclude(x => x.ExtraFieldsValues)
                        .ThenInclude(x => x.ExtraField)
            .FirstOrDefault();
        }

        public List<int> GetIdsForStatesInWorkOrders()
        {
            return All()
                .Include(x => x.Location)
                .Where(x => x.Location.StateId.HasValue)
                .Select(x => x.Location.StateId.Value)
                .Distinct()
                .ToList();
        }

        public bool GetPermissionToWorkOrder(WorkOrderPermissionsDto workOrderPermissionsDto)
        {
            var result = Any(wo => wo.Id == workOrderPermissionsDto.Id &&
                (wo.Queue.PermissionQueue.Any(p => workOrderPermissionsDto.Permissions.Contains(p.PermissionId)) &&
                    (wo.PeopleResponsibleId == workOrderPermissionsDto.PeopleId ||
                        (workOrderPermissionsDto.SubContracts.Contains(wo.PeopleResponsibleId.Value)) ||
                        (wo.Project.ProjectPermission.Any(r => workOrderPermissionsDto.Permissions.Contains(r.PermissionId)) && wo.WorkOrderCategory.WorkOrderCategoryPermission.Any(r => workOrderPermissionsDto.Permissions.Contains(r.PermissionId)))
                    )
                )
            );

            return result;
        }
    }
}