using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using Group.Salto.Infrastructure.Common.Dto;
using System.Data.SqlClient;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWorkOrdersRepository : IRepository<WorkOrders>
    {
        WorkOrders GetById(int id);
        WorkOrders GetEditById(int id);
        bool ExistOriginId(int originId);
        IEnumerable<WorkOrders> GetByPeopleAndDate(People peopleId, DateTime? date, bool getAll);
        int GetCountOpenOrdersSameLocation(int woLocationId);
        WorkOrders GetFullWorkOrderInfo(int id);
        WorkOrders GetByIdIncludeLocationAndCategory(int id);
        SaveResult<WorkOrders> CreateWorkOrders(WorkOrders entity);
        SaveResult<WorkOrders> UpdateWorkOrder(WorkOrders wo);
        bool ExistWorkOrderTypes(int id);
        WorkOrders GetByIdIncludingExecuteValues(int id);
        List<List<DataBaseResultDto>> GetConfiguredWorkOrder(string sql, List<SqlParameter> parameterList);
        int GetCountConfiguredWorkOrder(string sql, List<SqlParameter> parameterList);
        WorkOrders GetByIdIncludeBasicInfo(int workOrderId);
        WorkOrders GetDetailWorkOrderInfo(int Id);
        WorkOrders GetSummaryWorkOrderInfo(int Id);
        IQueryable<WorkOrders> GetSubWOInfo(int Id);
        IQueryable<WorkOrders> GetAllByAssetId(int assetId);
        WorkOrders GetAllServiceAndExtraFieldsById(int id);
        List<int> GetIdsForStatesInWorkOrders();
        bool GetPermissionToWorkOrder(WorkOrderPermissionsDto workOrderPermissionsDto);
    }
}