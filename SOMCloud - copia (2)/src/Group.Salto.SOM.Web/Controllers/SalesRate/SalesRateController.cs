using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.SalesRate;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.SalesRate;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.SalesRate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.SalesRate
{
    public class SalesRateController : BaseController
    {
        private readonly ISalesRateService _salesRateService;

        public SalesRateController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            ISalesRateService salesRateService)
            : base(loggingService, configuration, accessor)
        {
            _salesRateService = salesRateService ?? throw new ArgumentNullException(nameof(ISalesRateService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.SalesRate, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"SalesRateController.Get get all sale rates");
            var result = DoFilterAndPaging(new SalesRateFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.SalesRate, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Return Sales Rate Create");
            var response = new ResultViewModel<SalesRateViewModel>();
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.SalesRate, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Sales rate .get for id:{id}");
            var result = _salesRateService.GetById(id);
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
        [CustomAuthorization(ActionGroupEnum.SalesRate, ActionEnum.Create)]
        public IActionResult Create(SalesRateViewModel salesrateData)
        {
            LoggingService.LogInfo($"Actions Post create sales rate");
            if (ModelState.IsValid)
            {
                var result = _salesRateService.Create(salesrateData.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        SalesRateConstants.SalesRateCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(salesrateData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.SalesRate, ActionEnum.Update)]
        public IActionResult Edit(SalesRateViewModel salesrateData)
        {
            LoggingService.LogInfo($"Actions Post Update Sales rate by id {salesrateData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _salesRateService.Update(salesrateData.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        SalesRateConstants.SalesRateUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");

                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);

                return View("Edit", resultData);
            }
            var result = ProcessResult(salesrateData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);

            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.SalesRate, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete Sales rate by id {id}");
            var deleteResult = _salesRateService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    SalesRateConstants.SalesRateDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    SalesRateConstants.SalesRateDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        [HttpPost]
        public IActionResult Filter(SalesRateFilterViewModel filter)
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

        private ResultViewModel<SalesRatesViewModel> DoFilterAndPaging(SalesRateFilterViewModel filters)
        {
            var data = new ResultViewModel<SalesRatesViewModel>();
            var filteredData = _salesRateService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<SalesRateViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new SalesRatesViewModel()
                {
                    SalesRates = new MultiItemViewModel<SalesRateViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new SalesRatesViewModel()
                {
                    SalesRates = new MultiItemViewModel<SalesRateViewModel, int>(filteredData)
                };
            }
            data.Data.SalesRateFilters = filters;
            return data;
        }
    }
}