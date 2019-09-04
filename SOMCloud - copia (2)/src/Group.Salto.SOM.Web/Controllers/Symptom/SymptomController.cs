using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Symptom;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Symptom;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Symptom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Symptom
{
    public class SymptomController : BaseController
    {
        private readonly ISymptomService _symptomService;

        public SymptomController(ILoggingService loggingService,
                                        IConfiguration configuration,
                                        ISymptomService SymptomService,
                                        IHttpContextAccessor accessor) : base(loggingService, configuration, accessor)
        {
            _symptomService = SymptomService ?? throw new ArgumentNullException(nameof(ISymptomService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Symptom, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            var result = DoFilterAndPaging(new SymptomFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Symptom, ActionEnum.Create)]
        public IActionResult Create()
        {
            var response = new ResultViewModel<SymptomDetailViewModel>();
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Symptom, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            var result = _symptomService.GetById(id);
            var response = new ResultViewModel<SymptomDetailViewModel>
            {
                Data = result.Data.ToDetailViewModel()
            };

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
        public IActionResult Filter(SymptomFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.Symptom, ActionEnum.Create)]
        public IActionResult Create(SymptomDetailViewModel SymptomData)
        {
            if (ModelState.IsValid)
            {
                var result = _symptomService.Create(SymptomData.Symptom.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        SymptomConstants.SymptomCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(SymptomData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Symptom, ActionEnum.Update)]
        public IActionResult Edit(SymptomDetailViewModel SymptomData)
        {
            if (ModelState.IsValid)
            {
                var resultUpdate = _symptomService.Update(SymptomData.Symptom.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        SymptomConstants.SymptomUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");

                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);

                return View("Edit", resultData);
            }
            var result = ProcessResult(SymptomData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);

            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Symptom, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            var deleteResult = _symptomService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    SymptomConstants.SymptomDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    SymptomConstants.SymptomCannotDeleteMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        private ResultViewModel<SymptomsViewModel> DoFilterAndPaging(SymptomFilterViewModel filters)
        {
            var data = new ResultViewModel<SymptomsViewModel>();
            var filteredData = _symptomService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<SymptomViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new SymptomsViewModel()
                {
                    Symptoms = new MultiItemViewModel<SymptomViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new SymptomsViewModel()
                {
                    Symptoms = new MultiItemViewModel<SymptomViewModel, int>(filteredData)
                };
            }
            data.Data.SymptomFilter = filters;
            return data;
        }
    }
}