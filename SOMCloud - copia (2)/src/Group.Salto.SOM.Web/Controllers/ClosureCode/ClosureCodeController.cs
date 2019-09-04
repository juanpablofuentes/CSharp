using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.ClosureCode;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ClosureCode;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.ClosureCode;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.ClosureCode
{
    public class ClosureCodeController : BaseController
    {
        private readonly IClosureCodeService _closureCodeService;

        public ClosureCodeController(ILoggingService loggingService,
                                     IConfiguration configuration,
                                     IHttpContextAccessor accessor,
                                     IClosureCodeService closureCodeService) : base(loggingService, configuration, accessor)
        {
            _closureCodeService = closureCodeService ?? throw new ArgumentNullException($"{nameof(IClosureCodeService)}");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ClosingCodes, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Closure Codes.Get get all Closure Codes");
            var result = DoFilterAndPaging(new ClosureCodeFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Filter(ClosureCodeFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.ClosingCodes, ActionEnum.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ClosingCodes, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"ClosureCode.Get get closure for id:{id}");
            var result = _closureCodeService.GetById(id);
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
        [CustomAuthorization(ActionGroupEnum.ClosingCodes, ActionEnum.Create)]
        public IActionResult Create(ClosureCodeDetailViewModel model)
        {
            LoggingService.LogInfo("ClosureCode create");
            if (ModelState.IsValid)
            {
                var result = _closureCodeService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        ClosureCodeConstants.ClosureCodeCreateSuccess,
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
        [CustomAuthorization(ActionGroupEnum.ClosingCodes, ActionEnum.Update)]
        public IActionResult Edit(ClosureCodeDetailViewModel model)
        {
            LoggingService.LogInfo($"ClosureCode update for id ={model.Id}");
            if (ModelState.IsValid && model.Id != default(int))
            {
                var result = _closureCodeService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        ClosureCodeConstants.ClosureCodeUpdateSuccessMessage,
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
        [CustomAuthorization(ActionGroupEnum.ClosingCodes, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            var deleteResult = _closureCodeService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    ClosureCodeConstants.ClosureCodeDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    ClosureCodeConstants.ClosureCodeDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        private ResultViewModel<ClosureCodeListViewModel> DoFilterAndPaging(ClosureCodeFilterViewModel filters)
        {
            var data = new ResultViewModel<ClosureCodeListViewModel>();
            var filteredData = _closureCodeService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ClosureCodeViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new ClosureCodeListViewModel()
                {
                    ClosureCodes = new MultiItemViewModel<ClosureCodeViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count;
            }
            else
            {
                data.Data = new ClosureCodeListViewModel()
                {
                    ClosureCodes = new MultiItemViewModel<ClosureCodeViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filters;
            return data;
        }
    }
}