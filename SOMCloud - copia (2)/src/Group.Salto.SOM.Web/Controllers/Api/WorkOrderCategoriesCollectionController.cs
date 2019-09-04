using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategoriesCollection;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class WorkOrderCategoriesCollectionController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private IWorkOrderCategoriesCollectionService _workOrderCategoriesCollectionService;

        public WorkOrderCategoriesCollectionController(ILoggingService loggingService,
                                IWorkOrderCategoriesCollectionService workOrderCategoriesCollectionService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _workOrderCategoriesCollectionService = workOrderCategoriesCollectionService ?? throw new ArgumentNullException(nameof(IWorkOrderCategoriesCollectionService));
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _workOrderCategoriesCollectionService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _workOrderCategoriesCollectionService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}