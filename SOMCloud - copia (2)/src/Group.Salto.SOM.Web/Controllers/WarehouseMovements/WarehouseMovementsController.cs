using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Items;
using Group.Salto.ServiceLibrary.Common.Contracts.WarehouseMovements;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WarehouseMovements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.WarehouseMovements
{
    public class WarehouseMovementsController : BaseController
    {
        private readonly IWarehouseMovementsService _warehouseMovementsService;
        private readonly IItemsService _itemsService;
        public WarehouseMovementsController(ILoggingService loggingService, 
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,                             
                                IItemsService itemsService,                             
                                IWarehouseMovementsService warehouseMovementsService) : base(loggingService, configuration, accessor)
        {
            _warehouseMovementsService = warehouseMovementsService ?? throw new ArgumentNullException($"{nameof(IWarehouseMovementsService)} is null");          
            _itemsService = itemsService ?? throw new ArgumentNullException($"{nameof(IItemsService)} is null");          
        }

        [HttpGet]
        public IActionResult Index(int Id)
        {
            LoggingService.LogInfo($"WarehouseMovements get all ");
            
            var result = new ResultViewModel<WarehouseMovementsViewModel>()
            {
                Data = new WarehouseMovementsViewModel 
                { 
                    Filters = new WarehouseMovementsFiltersViewModel()                   
                }
            };
            result.Data.Filters.WarehouseId = Id;
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            var response = new ResultViewModel<WarehouseMovementsLineViewModel>()
            {
                Data = new WarehouseMovementsLineViewModel()
            };
            return View(response);       
        }
                
        [HttpPost]
        public IActionResult Filter(WarehouseMovementsFiltersViewModel filter)
        {
            var resultData = DoFilterAndPaging(filter);
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                resultData.Feedbacks = resultData.Feedbacks ?? new FeedbacksViewModel();
                resultData.Feedbacks.AddFeedback(feedback);
            }
            return View(nameof(Index), resultData);
        }        
        
        private ResultViewModel<WarehouseMovementsViewModel> DoFilterAndPaging(WarehouseMovementsFiltersViewModel filters)
        {
            var data = new ResultViewModel<WarehouseMovementsViewModel>();
            var filteredData = _warehouseMovementsService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<WarehouseMovementsLineViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new WarehouseMovementsViewModel()
                {
                    Movements = new MultiItemViewModel<WarehouseMovementsLineViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new WarehouseMovementsViewModel()
                {
                    Movements = new MultiItemViewModel<WarehouseMovementsLineViewModel, int>(filteredData)
                };
            }

            data.Data.Filters = filters;
            return data;
        }
    }
}
