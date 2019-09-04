using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.SOM.Web.Models.Grid;
using Group.Salto.SOM.Web.Models.WorkOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Contracts.Billing;
using Group.Salto.ServiceLibrary.Common.Contracts.BillLines;
using Group.Salto.ServiceLibrary.Common.Dtos.Billing;
using Group.Salto.ServiceLibrary.Common.Dtos.BillLines;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class WorkOrderController: BaseAPIController
    {
        private readonly IWorkOrderService _workOrderService;
        private readonly IBillService _billService;
        private readonly IBillLineService _billLineService;
        
        public WorkOrderController(
            ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IWorkOrderService workOrderService,
            IBillService billService,
            IBillLineService billLineService) : base(loggingService, configuration, accessor)
        {
            _workOrderService = workOrderService ?? throw new ArgumentNullException($"{nameof(IWorkOrderService)} is null");
            _billService = billService ?? throw new ArgumentNullException($"{nameof(IWorkOrderService)} is null");
            _billLineService = billLineService ?? throw new ArgumentNullException($"{nameof(IWorkOrderService)} is null");
        }

        [HttpGet("GetSummaryInfo")]
        public IActionResult GetSummaryInfo(int Id)
        {
            var result = _workOrderService.GetDetailSummaryWO(Id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }

        [HttpGet("GetSubWO")]
        public IActionResult GetSubWO(int Id)
        {
            IList<WorkOrdersSubWODto> result = _workOrderService.GetDetailSubWO(Id);
            RootGrid rootObject = result.ToRootSubPetitionsGrid();
            return Ok(rootObject);
        }

        [HttpGet("GetBill")]
        public IActionResult GetBill(int Id)
        {
            ResultDto<IList<BillDto>> result = _billService.GetAllById(Id);
            RootGrid rootObject = result.Data.ToRootBillGrid();
            return Ok(rootObject);
        }

        [HttpGet("GetBillLine")]
        public IActionResult GetBillLine(int Id)
        {
            ResultDto<IList<BillLinesDto>> result = _billLineService.GetAllById(Id);
            RootGrid rootObject = result.Data.ToRootBillLinesGrid();
            return Ok(rootObject);
        }

        [HttpGet("GetAssetsWorkOrder")]
        public IActionResult GetAssetsWorkOrder(int Id)
        {
            ResultDto<IList<WorkOrderAssetsDto>> result = _workOrderService.GetAllAssetsByWorkOrderId(Id);
            RootGrid rootObject = result.Data.ToWorkOrderAsset();
            return Ok(rootObject);
        }
    }
}