using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.WorkOrder;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder
{
    public interface IWorkOrderService
    {
        IEnumerable<WorkOrderBasicInfoDto> GetBasicByFilter(int peopleConfigId, WorkOrderSearchDto orderSearchDto);
        WorkOrderFullInfoDto GetFullWorkOrderInfo(int id);
        WorkOrderResultDto GetConfiguredWorkOrdersList(GridDto gridConfig);
        FileContentDto ExportToExcel(IList<GridDataDto> listGridData, IList<WorkOrderColumnsDto> columns);
        ResultDto<WorkOrderEditDto> GetById(int Id);
        ResultDto<WorkOrderEditDto> Create(WorkOrderEditDto workOrderEditDto);
        ResultDto<WorkOrderEditDto> Update(WorkOrderEditDto workOrderEditDto);
        ResultDto<WorkOrderDetailDto> GetDetailWO(int id);
        ResultDto<WorkOrdersSummaryDto> GetDetailSummaryWO(int Id);
        IList<WorkOrdersSubWODto> GetDetailSubWO(int Id);
        ResultDto<IList<WorkOrderAssetsDto>> GetAllAssetsByWorkOrderId(int Id);
        ResultDto<AssetsDetailWorkOrderServicesDto> GetAllServiceAndExtraFieldsById(int Id);
        WorkOrderOperationsDto GetWOAnalysisAndServiceAnalysis(int Id);
        ResultDto<bool> GetPermissionToWorkOrder(int Id, int userId);
    }
}