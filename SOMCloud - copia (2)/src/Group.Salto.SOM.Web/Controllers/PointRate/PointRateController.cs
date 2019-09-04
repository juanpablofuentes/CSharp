using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Knowledge;
using Group.Salto.Common.Constants.PointRate;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PointRate;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.PointRate;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Vehicles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.PoinRate
{
    public class PointRateController : BaseController
    {
        private readonly IPointRateService _pointrateService;

        public PointRateController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    IHttpContextAccessor accessor,
                                    IPointRateService pointrateService) : base(loggingService, configuration, accessor)
        {
            _pointrateService = pointrateService ?? throw new ArgumentNullException($"{nameof(IPointRateService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.PointRates, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions Get get all PointRates");
            var result = DoFilterAndPaging(new PointRateFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.PointRates, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Actions Get get by PointRate id: {id}");
            var result = _pointrateService.GetById(id);

            var resultData = result.ToViewModel();

            if (result != null && (result.Errors?.Errors == null || !result.Errors.Errors.Any()))
            {
                return View("Edit", resultData);
            }

            return View();
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.PointRates, ActionEnum.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.PointRates, ActionEnum.Create)]
        public IActionResult Create(ResultViewModel<PointRateViewModel> pointrateData)
        {
            LoggingService.LogInfo($"Actions Post create Point Rate");
            if (ModelState.IsValid)
            {
                var result = _pointrateService.CreatePointRate(pointrateData.Data.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        PointRateConstants.PointRateCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(pointrateData.Data,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);

            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.PointRates, ActionEnum.Update)]
        public IActionResult Update(ResultViewModel<PointRateViewModel> pointsrateData)
        {
            LoggingService.LogInfo($"Actions Post Update PointRate by id {pointsrateData.Data.Id}");

            if (ModelState.IsValid && pointsrateData.Data.Name != null)
            {
                var resultUpdate = _pointrateService.UpdatePointRate(pointsrateData.Data.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        KnowledgeConstants.KnowledgeUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");

                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);

                return View("Edit", resultData);
            }
            var result = ProcessResult(pointsrateData.Data, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);

            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.PointRates, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete PointRate by id {id}");
            var deleteResult = _pointrateService.DeletePointRate(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    PointRateConstants.PointRateDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    PointRateConstants.PointRateDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        [HttpPost]
        public IActionResult Filter(PointRateFilterViewModel filter)
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
        public IActionResult GetPointRate()
        {
            var pointrate = _pointrateService.GetAllKeyValues().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });
            return Ok(pointrate);
        }

        private ResultViewModel<PointsRateViewModel> DoFilterAndPaging(PointRateFilterViewModel filters)
        {
            var data = new ResultViewModel<PointsRateViewModel>();

            var filteredData = _pointrateService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<PointRateViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new PointsRateViewModel()
                {
                    PointRate = new MultiItemViewModel<PointRateViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new PointsRateViewModel()
                {
                    PointRate = new MultiItemViewModel<PointRateViewModel, int>(filteredData)
                };
            }

            data.Data.PointRateFilters = filters;
            return data;
        }        
    }
}