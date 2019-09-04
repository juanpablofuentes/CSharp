using System;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CollectionTypeWorkOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class CollectionTypeWorkOrdersController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private readonly ICollectionTypeWorkOrdersService _collectionTypeWorkOrdersService;

        public CollectionTypeWorkOrdersController(ILoggingService loggingService, ICollectionTypeWorkOrdersService collectionTypeWorkOrdersService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException($"{nameof(ILoggingService)} is null"); ;
            _collectionTypeWorkOrdersService = collectionTypeWorkOrdersService ?? throw new ArgumentNullException($"{nameof(ICollectionTypeWorkOrdersService)} is null"); ;
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _collectionTypeWorkOrdersService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }

        [HttpGet("CanDeleteTreeLevel")]
        public IActionResult CanDeleteTreeLevel(int id)
        {
            var result = _collectionTypeWorkOrdersService.CanDeleteTreeLevel(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}