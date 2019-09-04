using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.WorkOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Mobility.Controllers.WorkOrder
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkOrderController : BaseController
    {
        private readonly IWorkOrderService _workOrderService;

        public WorkOrderController(IConfiguration configuration, 
                                    IWorkOrderService workOrderService) : base(configuration)
        {
            _workOrderService = workOrderService;
        }

        [HttpPost]
        [ActionName("SearchBasic")]
        public IActionResult SearchBasic(WorkOrderSearchDto orderSearchDto)
        {
            var peopleConfigId = GetUserConfigId();
            IEnumerable<WorkOrderBasicInfoDto> result = _workOrderService.GetBasicByFilter(peopleConfigId, orderSearchDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ActionName("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _workOrderService.GetFullWorkOrderInfo(id);
            return Ok(result);
        }
    }
}
