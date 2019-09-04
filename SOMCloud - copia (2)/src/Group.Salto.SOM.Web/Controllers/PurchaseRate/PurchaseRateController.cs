using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.PurchaseRate;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PurchaseRates;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.PurchaseRate;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.PurchaseRate
{
    public class PurchaseRateController : BaseController
    {
        private readonly IPurchaseRateService _purchaserateService;
        public PurchaseRateController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    IHttpContextAccessor accessor,
                                    IPurchaseRateService purchaserateService) : base(loggingService, configuration, accessor)
        {
            _purchaserateService = purchaserateService ?? throw new ArgumentNullException($"{nameof(purchaserateService)} is null");

        }

        [CustomAuthorization(ActionGroupEnum.CostRate, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions Get get all PurchaseRates");
            var result = DoFilterAndPaging(new PurchaseRateFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.CostRate, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Return Purchase Rate Create");
            var response = new ResultViewModel<PurchaseRateDetailsViewModel>();
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.CostRate, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Purchase rate .get for id:{id}");
            var result = _purchaserateService.GetById(id);
            var response = result.ToViewModel();
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
        [CustomAuthorization(ActionGroupEnum.CostRate, ActionEnum.Create)]
        public IActionResult Create(PurchaseRateDetailsViewModel purchaserateData)
        {
            LoggingService.LogInfo($"Actions Post create purchase rate");
            if (ModelState.IsValid)
            {
                var result = _purchaserateService.CreatePurchaseRate(purchaserateData.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        PurchaseRateConstants.PurchaseRateCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(purchaserateData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);

            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.CostRate, ActionEnum.Update)]
        public IActionResult Edit(PurchaseRateDetailsViewModel purchaserateData)
        {
            LoggingService.LogInfo($"Actions Post Update purchaserate by id {purchaserateData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _purchaserateService.UpdatePurchaseRate(purchaserateData.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        PurchaseRateConstants.PurchaseRateUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");

                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);

                return View("Edit", resultData);
            }
            var result = ProcessResult(purchaserateData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.CostRate, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete PurchaseRate by id {id}");
            var deleteResult = _purchaserateService.DeletePurchaseRate(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    PurchaseRateConstants.PurchaseRateDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    PurchaseRateConstants.PurchaseRateDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        [HttpPost]
        public IActionResult Filter(PurchaseRateFilterViewModel filter)
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

        private ResultViewModel<PurchaseRatesViewModel> DoFilterAndPaging(PurchaseRateFilterViewModel filters)
        {
            var data = new ResultViewModel<PurchaseRatesViewModel>();
            var filteredData = _purchaserateService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<PurchaseRateViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new PurchaseRatesViewModel()
                {
                    PurchaseRate = new MultiItemViewModel<PurchaseRateViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new PurchaseRatesViewModel()
                {
                    PurchaseRate = new MultiItemViewModel<PurchaseRateViewModel, int>(filteredData)
                };
            }

            data.Data.PurchaseRateFilters = filters;
            return data;
        }
    }
}