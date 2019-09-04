using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Sla;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ReferenceTimeSla;
using Group.Salto.ServiceLibrary.Common.Contracts.Sla;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Sla;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Sla
{
    public class SlaController : BaseController
    {
        private readonly ISlaService _slaService;
        private readonly IReferenceTimeSlaService _referenceService;

        public SlaController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            ISlaService slaService,
            IReferenceTimeSlaService referenceService) : base(loggingService, configuration, accessor)
        {
            _referenceService = referenceService ?? throw new ArgumentNullException(nameof(IReferenceTimeSlaService));
            _slaService = slaService ?? throw new ArgumentNullException(nameof(ISlaService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOSla, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"SlaController.Get get all sla");
            var result = DoFilterAndPaging(new SlaFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOSla, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Return Sla Create");
            var response = new ResultViewModel<SlaDetailsViewModel>()
            {
                Data = FillData(new SlaDetailsViewModel())
            };
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOSla, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($" Get get by Sla id: {id}");
            var result = _slaService.GetById(id);
            var resultData = result.ToViewModel();
            FillData(resultData.Data);
            if (result != null && (result.Errors?.Errors == null || !result.Errors.Errors.Any()))
            {
                return View("Edit", resultData);
            }

            return View();
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WOSla, ActionEnum.Create)]
        public IActionResult Create(SlaDetailsViewModel slaData)
        {
            LoggingService.LogInfo($"Actions Post create sla");
            if (ModelState.IsValid)
            {
                var result = _slaService.CreateSla(slaData.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        SlaConstants.SlaCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(slaData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            FillData(slaData);
            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WOSla, ActionEnum.Update)]
        public IActionResult Edit(SlaDetailsViewModel slaData)
        {
            LoggingService.LogInfo($"Post Update Sla by id {slaData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _slaService.UpdateSla(slaData.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        SlaConstants.SlaUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");

                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);

                return View("Edit", resultData);
            }
            var result = ProcessResult(slaData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);

            return View("Edit", result);
        }

        [HttpPost]
        public IActionResult Filter(SlaFilterViewModel filter)
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

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WOSla, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete Slae by id {id}");
            var deleteResult = _slaService.DeleteSla(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    SlaConstants.SlaDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    SlaConstants.SlaDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        private SlaDetailsViewModel FillData(SlaDetailsViewModel source)
        {
            source.ReferenceTime = _referenceService.GetAll()?.ToSelectList();

            return source;
        }

        private ResultViewModel<SlasViewModel> DoFilterAndPaging(SlaFilterViewModel filters)
        {
            var data = new ResultViewModel<SlasViewModel>();
            var filteredData = _slaService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<SlaViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new SlasViewModel()
                {
                    Sla = new MultiItemViewModel<SlaViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new SlasViewModel()
                {
                    Sla = new MultiItemViewModel<SlaViewModel, int>(filteredData)
                };
            }
            data.Data.SlaFilters = filters;
            return data;
        }
    }
}