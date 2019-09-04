using Group.Salto.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Group.Salto.SOM.Web.Models.Assets;
using System;
using Group.Salto.SOM.Web.Models.Items;
using Group.Salto.ServiceLibrary.Common.Contracts.ItemsSerialNumberStatuses;
using System.Linq;
using Group.Salto.SOM.Web.Models.Extensions;

namespace Group.Salto.SOM.Web.Controllers.ItemsSerialNumbers
{
    [Authorize]
    public class ItemsSerialNumbersController : BaseController
    {
        private readonly IItemsSerialNumberStatusesService _itemsSerialNumberStatusesService;

        public ItemsSerialNumbersController(
            ILoggingService loggingService,
            IHttpContextAccessor accessor,
            IItemsSerialNumberStatusesService itemsSerialNumberStatusesService,
            IConfiguration configuration) : base(loggingService, configuration, accessor)
        {
            _itemsSerialNumberStatusesService = itemsSerialNumberStatusesService ?? throw new ArgumentNullException($"{nameof(IItemsSerialNumberStatusesService)} is null");    
        }

        [HttpGet]
        public IActionResult ItemsSerialNumbers()
        {
            var model = new SerialNumbersViewModel {};    
            //TODO get itemsserialnumber statuses from service
            model.SerialNumberStatuses = _itemsSerialNumberStatusesService.GetAllKeyValues().ToSelectList();
            return PartialView("_ItemsSerialNumbersModal", model);
        }

        [HttpPost]
        public IActionResult ItemsSerialNumbers(SerialNumbersViewModel model)
        {
            model.SerialNumberStatuses = _itemsSerialNumberStatusesService.GetAllKeyValues().ToSelectList();
            return PartialView("_ItemsSerialNumbersModal", model);
        }
    }
}