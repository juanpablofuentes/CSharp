using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.CollectionTypeWorkOrders;
using Group.Salto.Common.Enums;
using Group.Salto.Common.Helpers;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CollectionTypeWorkOrders;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.CollectionTypeWorkOrders;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.CollectionTypeWorkOrders
{
    public class CollectionTypeWorkOrdersController : BaseController
    {
        private readonly ICollectionTypeWorkOrdersService _collectionTypeWorkOrdersService;

        public CollectionTypeWorkOrdersController(ILoggingService loggingService,
                                                    IConfiguration configuration,
                                                    IHttpContextAccessor accessor,
                                                    ICollectionTypeWorkOrdersService collectionTypeWorkOrdersService) : base(loggingService, configuration, accessor)
        {
            _collectionTypeWorkOrdersService = collectionTypeWorkOrdersService ?? throw new ArgumentNullException($"{nameof(ICollectionTypeWorkOrdersService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOTypesCollection, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"CollectionTypeWorkOrders.Get get all collection types workOrders");
            var result = DoFilterAndPaging(new CollectionTypeWorkOrdersFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Filter(CollectionTypeWorkOrdersFilterViewModel filter)
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

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOTypesCollection, ActionEnum.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOTypesCollection, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"CollectionTypeWorkOrders.Get get CollectionTypeWorkOrders for id:{id}");
            var result = _collectionTypeWorkOrdersService.GetById(id);
            var response = result.ToResultViewModel();
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
        [CustomAuthorization(ActionGroupEnum.WOTypesCollection, ActionEnum.Create)]
        public IActionResult Create(CollectionTypeWorkOrdersDetailViewModel model)
        {
            LoggingService.LogInfo("CollectionTypeWorkOrders create");
            if (ModelState.IsValid)
            {
                var result = _collectionTypeWorkOrdersService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        CollectionTypeWorkOrdersConstants.CollectionTypeWorkOrdersCreateSuccess,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index));
                }
                var resultData = result.ToResultViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(nameof(Create), resultData);
            }
            return View(nameof(Create), ProcessResult(model));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WOTypesCollection, ActionEnum.Update)]
        public IActionResult Edit(CollectionTypeWorkOrdersDetailViewModel model)
        {
            LoggingService.LogInfo($"CollectionTypeWorkOrders update for id ={model.Id}");
            if (ModelState.IsValid && model.Id != default(int))
            {
                var result = _collectionTypeWorkOrdersService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        CollectionTypeWorkOrdersConstants.CollectionTypeWorkOrdersUpdateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index));
                }
                var resultData = result.ToResultViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(nameof(Edit), resultData);
            }
            return View(nameof(Edit), ProcessResult(model));
        }

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.WOTypesCollection, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            var deleteResult = _collectionTypeWorkOrdersService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    CollectionTypeWorkOrdersConstants.CollectionTypeWorkOrderDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    CollectionTypeWorkOrdersConstants.CollectionTypeWorkOrderDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        private ResultViewModel<CollectionTypeWorkOrdersListViewModel> DoFilterAndPaging(CollectionTypeWorkOrdersFilterViewModel filter)
        {
            var data = new ResultViewModel<CollectionTypeWorkOrdersListViewModel>();
            var filteredData = _collectionTypeWorkOrdersService.GetAllFiltered(filter.ToDto()).Data.ToListViewModel();

            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<CollectionTypeWorkOrdersViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new CollectionTypeWorkOrdersListViewModel()
                {
                    CollectionTypesWorkOrders = new MultiItemViewModel<CollectionTypeWorkOrdersViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredData.Count;
            }
            else
            {
                data.Data = new CollectionTypeWorkOrdersListViewModel()
                {
                    CollectionTypesWorkOrders = new MultiItemViewModel<CollectionTypeWorkOrdersViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filter;
            return data;
        }
    }
}