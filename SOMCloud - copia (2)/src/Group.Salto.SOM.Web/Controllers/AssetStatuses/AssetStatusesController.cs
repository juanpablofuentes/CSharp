using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.AssetStatuses;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.AssetStatuses;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.AssetStatuses;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.AssetStatuses
{
    public class AssetStatusesController : BaseController
    {
        private readonly IAssetStatusesService _assetStatusesService;

        public AssetStatusesController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IAssetStatusesService slaService)
            : base(loggingService, configuration, accessor)
        {
            _assetStatusesService = slaService ?? throw new ArgumentNullException(nameof(IAssetStatusesService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.AssetStatus, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions Get get all Asset Statuses");
            var result = DoFilterAndPaging(new AssetStatusesFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.AssetStatus, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Return AssetStatuses Create");
            var response = new ResultViewModel<AssetStatusesViewModel>() {
                Data = new AssetStatusesViewModel()
                {
                    ModeActionType =  ModeActionTypeEnum.Create
                }
            };
            
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.AssetStatus, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"AssetStatuses .get for id:{id}");
            var result = _assetStatusesService.GetById(id);
            var response = result.ToViewModel();
            response.Data.ModeActionType = ModeActionTypeEnum.Edit;
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
        [CustomAuthorization(ActionGroupEnum.AssetStatus, ActionEnum.Create)]
        public IActionResult Create(AssetStatusesViewModel assetStatusesData)
        {
            LoggingService.LogInfo($"Actions Post create AssetStatuses");
            if (ModelState.IsValid)
            {
                var result = _assetStatusesService.CreateAssetStatuses(assetStatusesData.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        AssetStatusesConstants.AssetStatusesCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(assetStatusesData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.AssetStatus, ActionEnum.Update)]
        public IActionResult Edit(AssetStatusesViewModel assetStatusesData)
        {
            LoggingService.LogInfo($"Actions Post Update assetStatuses by id {assetStatusesData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _assetStatusesService.UpdateAssetStatuses(assetStatusesData.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        AssetStatusesConstants.AssetStatusesUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");

                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);

                return View("Edit", resultData);
            }
            var result = ProcessResult(assetStatusesData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.AssetStatus, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Delete Post AssetStatuses  by id {id}");
            var deleteResult = _assetStatusesService.DeleteAssetStatuses(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    AssetStatusesConstants.AssetStatusesDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    AssetStatusesConstants.AssetStatusesDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        [HttpPost]
        public IActionResult Filter(AssetStatusesFilterViewModel filter)
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

        private ResultViewModel<AssetsStatusesViewModel> DoFilterAndPaging(AssetStatusesFilterViewModel filters)
        {
            var data = new ResultViewModel<AssetsStatusesViewModel>();
            var filteredData = _assetStatusesService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<AssetStatusesViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new AssetsStatusesViewModel()
                {
                    AssetStatuses = new MultiItemViewModel<AssetStatusesViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new AssetsStatusesViewModel()
                {
                    AssetStatuses = new MultiItemViewModel<AssetStatusesViewModel, int>(filteredData)
                };
            }

            data.Data.AssetStatusesFilters = filters;
            return data;
        }
    }
}