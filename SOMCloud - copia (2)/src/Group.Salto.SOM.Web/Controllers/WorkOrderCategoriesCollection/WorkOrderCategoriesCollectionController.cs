using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.WorkOrderCategoriesCollection;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategoriesCollection;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoriesCollection;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkOrderCategoriesCollection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.WorkOrderCategoriesCollection
{
    public class WorkOrderCategoriesCollectionController : BaseController
    {
        private readonly IWorkOrderCategoriesCollectionService _workOrderCategoriesCollectionService;

        public WorkOrderCategoriesCollectionController(ILoggingService loggingService,
                                                            IConfiguration configuration,
                                                            IWorkOrderCategoriesCollectionService workOrderCategoriesCollectionService,
                                                            IHttpContextAccessor accessor) : base(loggingService, configuration, accessor)
        {
            _workOrderCategoriesCollectionService = workOrderCategoriesCollectionService ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesCollectionService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOCategoryCollection, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"WorkOrderCategoriesCollection.Get get all collection categories workOrders");
            var result = DoFilterAndPaging(new WorkOrderCategoriesCollectionFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOCategoryCollection, ActionEnum.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOCategoryCollection, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"WorkOrderCategoriesCollection.Get get WorkOrderCategoriesCollection for id:{id}");
            var result = _workOrderCategoriesCollectionService.GetById(id);
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
        [CustomAuthorization(ActionGroupEnum.WOCategoryCollection, ActionEnum.Create)]
        public IActionResult Create(WorkOrderCategoriesCollectionBaseViewModel model)
        {
            LoggingService.LogInfo("WorkOrderCategoriesCollection create");
            if (ModelState.IsValid)
            {
                var result = _workOrderCategoriesCollectionService.Create(model.ToDetailDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        WorkOrderCategoriesCollectionConstants.WorkOrderCategoriesCollectionCreateSuccess,
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
        [CustomAuthorization(ActionGroupEnum.WOCategoryCollection, ActionEnum.Update)]
        public IActionResult Edit(WorkOrderCategoriesCollectionBaseViewModel model)
        {
            LoggingService.LogInfo($"WorkOrderCategoriesCollection update for id ={model.Id}");
            if (ModelState.IsValid && model.Id != default(int))
            {
                var result = _workOrderCategoriesCollectionService.Update(model.ToDetailDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        WorkOrderCategoriesCollectionConstants.WorkOrderCategoriesCollectionUpdateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index));
                }
                var resultData = result.ToResultViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(nameof(Edit), resultData);
            }
            return View(nameof(Edit), ProcessResult(model));
        }

        [HttpPost]
        public IActionResult Filter(WorkOrderCategoriesCollectionFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.WOCategoryCollection, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            var deleteResult = _workOrderCategoriesCollectionService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    WorkOrderCategoriesCollectionConstants.WorkOrderCategoriesCollectionDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    WorkOrderCategoriesCollectionConstants.WorkOrderCategoriesCollectionDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }

        private ResultViewModel<WorkOrderCategoriesCollectionListViewModel> DoFilterAndPaging(WorkOrderCategoriesCollectionFilterViewModel filter)
        {
            var data = new ResultViewModel<WorkOrderCategoriesCollectionListViewModel>();
            var filteredData = _workOrderCategoriesCollectionService.GetAllFiltered(filter.ToDto()).Data.ToListViewModel();

            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<WorkOrderCategoriesCollectionBaseViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new WorkOrderCategoriesCollectionListViewModel()
                {
                    WorkOrderCategoriesCollectionList = new MultiItemViewModel<WorkOrderCategoriesCollectionBaseViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredData.Count;
            }
            else
            {
                data.Data = new WorkOrderCategoriesCollectionListViewModel()
                {
                    WorkOrderCategoriesCollectionList = new MultiItemViewModel<WorkOrderCategoriesCollectionBaseViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filter;
            return data;
        }
    }
}