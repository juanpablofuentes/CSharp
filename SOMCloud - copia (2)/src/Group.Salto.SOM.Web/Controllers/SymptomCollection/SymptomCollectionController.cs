using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.SymptomCollection;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Symptom;
using Group.Salto.ServiceLibrary.Common.Contracts.SymptomCollection;
using Group.Salto.ServiceLibrary.Common.Dtos.SymptomCollection;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Symptom;
using Group.Salto.SOM.Web.Models.SymptomCollection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.SymptomCollection
{
    public class SymptomCollectionController : BaseController
    {
        private readonly ISymptomCollectionService _symptomCollectionService;
        private readonly ISymptomService _symptomService;
        public SymptomCollectionController(ILoggingService loggingService,
                                            IConfiguration configuration,
                                            ISymptomService SymptomService,
                                            ISymptomCollectionService SymptomCollectionService,
                                            IHttpContextAccessor accessor) : base(loggingService, configuration, accessor)
        {
            _symptomCollectionService = SymptomCollectionService ?? throw new ArgumentNullException(nameof(ISymptomCollectionService));
            _symptomService = SymptomService ?? throw new ArgumentNullException(nameof(ISymptomService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.SymptomCollection, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            var result = DoFilterAndPaging(new SymptomCollectionFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.SymptomCollection, ActionEnum.Create)]
        public IActionResult Create()
        {
            var result = new SymptomCollectionDetailViewModel();
            FillCombosData(result);
            return View(ProcessResult(result));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.SymptomCollection, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            var result = _symptomCollectionService.GetById(id);
            var response = result.ToDetailViewModel();
            FillCombosData(response.Data, result.Data);
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
        public IActionResult Filter(SymptomCollectionFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.SymptomCollection, ActionEnum.Create)]
        public IActionResult Create(SymptomCollectionDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _symptomCollectionService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        SymptomCollectionConstants.SymptomCollectionCreateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToDetailViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            return View("Create", ProcessResult(model));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.SymptomCollection, ActionEnum.Update)]
        public IActionResult Edit(SymptomCollectionDetailViewModel model)
        {
            if (ModelState.IsValid && model.Id != default(int))
            {
                var result = _symptomCollectionService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        SymptomCollectionConstants.SymptomCollectionUpdateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToDetailViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            return View("Edit", ProcessResult(model));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.SymptomCollection, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            var deleteResult = _symptomCollectionService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    SymptomCollectionConstants.SymptomCollectionDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    SymptomCollectionConstants.SymptomCollectionCannotDeleteMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        private void FillCombosData(SymptomCollectionDetailViewModel model, SymptomCollectionDto data = null)
        {
            if (model != null)
            {
                var symptoms = _symptomService.GetOrphansKeyValue();
                model.SymptomsSelected = symptoms.SetSelectedValues(data?.SymptomSelected, SymptomCollectionConstants.SymptomCollectionsSymptomsMultiSelectorTitle);
            }
        }

        private ResultViewModel<SymptomCollectionsViewModel> DoFilterAndPaging(SymptomCollectionFilterViewModel filters)
        {
            var data = new ResultViewModel<SymptomCollectionsViewModel>();
            var filteredData = _symptomCollectionService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<SymptomCollectionViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new SymptomCollectionsViewModel()
                {
                    SymptomCollections = new MultiItemViewModel<SymptomCollectionViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new SymptomCollectionsViewModel()
                {
                    SymptomCollections = new MultiItemViewModel<SymptomCollectionViewModel, int>(filteredData)
                };
            }
            data.Data.SymptomCollectionFilter = filters;
            return data;
        }
    }
}