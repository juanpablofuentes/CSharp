using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Warehouses;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WarehouseMovements;
using Group.Salto.ServiceLibrary.Common.Contracts.Warehouses;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Warehouses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Warehouses
{
    public class WarehousesController : BaseController
    {
        private readonly IWarehousesService _warehousesService;
        private readonly IWarehouseMovementsService _warehouseMovementsService;

        public WarehousesController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                IWarehouseMovementsService warehouseMovementsService,
                                IWarehousesService warehousesService) : base(loggingService, configuration, accessor)
        {
            _warehousesService = warehousesService ?? throw new ArgumentNullException($"{nameof(IWarehousesService)} is null");
            _warehouseMovementsService = warehouseMovementsService ?? throw new ArgumentNullException($"{nameof(IWarehouseMovementsService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Wharehouses, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Warehouses get all ");

            var result = DoFilterAndPaging(new WarehousesFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Wharehouses, ActionEnum.Create)]
        public IActionResult Create()
        {
            var response = new ResultViewModel<WarehousesBaseViewModel>()
            {
                Data = new WarehousesBaseViewModel()
                {
                    GeneralViewModel = new WarehouseGeneralViewModel()
                }
            };
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Wharehouses, ActionEnum.GetById)]
        public IActionResult Edit(int Id)
        {
            var result = _warehousesService.GetById(Id);
            var response = result.ToResultViewModel();
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                response.Feedbacks = response.Feedbacks ?? new FeedbacksViewModel();
                response.Feedbacks.AddFeedback(feedback);
            }
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Edit", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Wharehouses, ActionEnum.Update)]
        public IActionResult Edit(WarehousesBaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Warehouse = model.GeneralViewModel?.ToDto();
                var resultUpdate = _warehousesService.UpdateWarehouse(Warehouse);
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        WarehousesConstants.WarehousesUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    SetFeedbackTempData(LocalizationsConstants.Error,
                                       WarehousesConstants.WarehousesUpdateErrorMessage,
                                       FeedbackTypeEnum.Error);
                    return RedirectToAction("Edit");
                }
            }
            var result = ProcessResult(model, LocalizationsConstants.Error,
                LocalizationsConstants.FormErrorsMessage,
                FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Wharehouses, ActionEnum.Create)]
        public IActionResult Create(WarehousesBaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var warehouse = model.GeneralViewModel?.ToDto();
                var result = _warehousesService.CreateWarehouse(warehouse);
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        WarehousesConstants.WarehousesCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = new ResultViewModel<WarehousesBaseViewModel>()
                {
                    Data = new WarehousesBaseViewModel()
                    {
                        GeneralViewModel = result.Data.ToViewModel(),
                    },
                    Feedbacks = result.Errors.ToViewModel()
                };
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(model,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            return View("Create", results);
        }

        [HttpPost]
        public IActionResult Filter(WarehousesFilterViewModel filter)
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

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.Wharehouses, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            var deleteResult = _warehousesService.DeleteWarehouse(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    WarehousesConstants.WarehousesDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    WarehousesConstants.WarehousesDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        private ResultViewModel<WarehousesViewModel> DoFilterAndPaging(WarehousesFilterViewModel filters)
        {
            var data = new ResultViewModel<WarehousesViewModel>();
            var filteredData = _warehousesService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<WarehouseGeneralViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new WarehousesViewModel()
                {
                    Warehouses = new MultiItemViewModel<WarehouseGeneralViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new WarehousesViewModel()
                {
                    Warehouses = new MultiItemViewModel<WarehouseGeneralViewModel, int>(filteredData)
                };
            }

            data.Data.Filters = filters;
            return data;
        }
    }
}