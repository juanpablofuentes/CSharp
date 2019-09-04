using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrder
{
    public class WorkOrderQueryAdapter : IWorkOrderQueryAdapter
    {
        private readonly ILoggingService _loggingService;
        private readonly IWorkOrderQuerySelect _workOrderQuerySelect;
        private readonly IWorkOrderQueryForm _workOrderQueryForm;
        private readonly IWorkOrderQueryWhere _workOrderQueryWhere;
        private readonly IWorkOrderQueryOrderBy _workOrderQueryOrderBy;
        private readonly IWorkOrderQueryPagination _workOrderQueryPagination;

        public List<SqlParameter> ParameterList { get; } = new List<SqlParameter>();

        public WorkOrderQueryAdapter(ILoggingService loggingService, IWorkOrderQuerySelect workOrderQuerySelect, IWorkOrderQueryForm workOrderQueryForm, IWorkOrderQueryWhere workOrderQueryWhere, IWorkOrderQueryOrderBy workOrderQueryOrderBy, IWorkOrderQueryPagination workOrderQueryPagination)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null");
            _workOrderQuerySelect = workOrderQuerySelect ?? throw new ArgumentNullException($"{nameof(IWorkOrderQuerySelect)} is null");
            _workOrderQueryForm = workOrderQueryForm ?? throw new ArgumentNullException($"{nameof(IWorkOrderQueryForm)} is null");
            _workOrderQueryWhere = workOrderQueryWhere ?? throw new ArgumentNullException($"{nameof(IWorkOrderQueryWhere)} is null");
            _workOrderQueryOrderBy = workOrderQueryOrderBy ?? throw new ArgumentNullException($"{nameof(IWorkOrderQueryOrderBy)} is null");
            _workOrderQueryPagination = workOrderQueryPagination ?? throw new ArgumentNullException($"{nameof(IWorkOrderQueryPagination)} is null");
        }

        public string GenerateCountQuery(IList<WorkOrderColumnsDto> columns, GridDto gridConfig)
        {
            ParameterList.Clear();
            string sql = _workOrderQuerySelect.CreateCountSelect(columns, gridConfig);
            sql += _workOrderQueryForm.CreateCountForm(columns, gridConfig);
            sql += _workOrderQueryWhere.CreateWhere(gridConfig, ParameterList);
            _loggingService.LogInfo(sql);
            return sql;
        }

        public string GenerateQuery(IList<WorkOrderColumnsDto> columns, GridDto gridConfig)
        {
            ParameterList.Clear();
            string sql = _workOrderQuerySelect.CreateSelect(columns, gridConfig);
            sql += _workOrderQueryForm.CreateForm(columns, gridConfig);
            sql += _workOrderQueryWhere.CreateWhere(gridConfig, ParameterList);
            sql += _workOrderQueryOrderBy.CreateOrderBy(columns, gridConfig);
            sql += _workOrderQueryPagination.CreatePagination(gridConfig);
            _loggingService.LogInfo(sql);
            return sql;
        }
    }
}