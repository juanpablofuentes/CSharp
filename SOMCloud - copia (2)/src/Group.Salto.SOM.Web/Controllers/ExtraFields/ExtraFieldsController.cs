using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.ExtraFields;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ErpSystemInstanceQuery;
using Group.Salto.ServiceLibrary.Common.Contracts.ExtraFields;
using Group.Salto.ServiceLibrary.Common.Contracts.ExtraFieldTypes;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFields;
using Group.Salto.ServiceLibrary.Implementations.ExtraFields;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.ExtraFields;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.ExtraFields
{
    public class ExtraFieldsController : BaseLanguageController
    {
        private readonly IExtraFieldsService _extraFieldsService;
        private readonly IExtraFieldTypesService _extraFieldTypesService;
        private readonly IErpSystemInstanceQueryService _erpSystemInstanceQueryService;

        public ExtraFieldsController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    IHttpContextAccessor accessor,
                                    ILanguageService languageService,
                                    IExtraFieldsService extraFieldsService,
                                    IExtraFieldTypesService extraFieldTypesService,
                                    IErpSystemInstanceQueryService erpSystemInstanceQueryService) : base(loggingService, configuration, accessor, languageService)
        {
            _extraFieldsService = extraFieldsService ?? throw new ArgumentNullException($"{nameof(IExtraFieldsService)} is null");
            _extraFieldTypesService = extraFieldTypesService ?? throw new ArgumentNullException($"{nameof(IExtraFieldTypesService)} is null");
            _erpSystemInstanceQueryService = erpSystemInstanceQueryService ?? throw new ArgumentNullException($"{nameof(IErpSystemInstanceQueryService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExtraFields, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"ExtraFields.Get get all Extra Fields");
            var result = DoFilterAndPaging(new ExtraFieldsFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExtraFields, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Extra Fields.Create Get");
            var languages = LanguageService.GetAll()?.Data;
            var result = new ExtraFieldsDetailDto();
            var response = result.ToViewModelWithLanguages(languages);
            FillData(response);
            return View(ProcessResult(response));
        }

        [HttpGet]
        public IActionResult ExtraFieldsModal()
        {
            var model = new ExtraFieldsDetailViewModel() { ExtraFieldsId = 1 };
            model.ExtraFieldsSystemListItems = _extraFieldsService.GetAllByDelSystemKeyValues(GetLanguageId(), true).ToSelectList();
            model.ExtraFieldsRegularListItems = _extraFieldsService.GetAllByDelSystemKeyValues(GetLanguageId(), false).ToSelectList();
            model.ErpSystemInstanceQueryListItems = _erpSystemInstanceQueryService.GetAllKeyValues().ToSelectList();
            model.TypeListItems = _extraFieldTypesService.GetAllKeyValues().ToSelectList();
            return PartialView("_ExtraFieldsModal", model);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExtraFields, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"ExtraFields.Get get ExtraFields for id:{id}");
            var result = _extraFieldsService.GetById(id);
            var languages = LanguageService.GetAll()?.Data;
            var response = result.ToViewModelWithLanguages(languages);
            FillData(response.Data);
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
        [CustomAuthorization(ActionGroupEnum.ExtraFields, ActionEnum.Create)]
        public IActionResult Create(ExtraFieldsDetailViewModel extrafields)
        {
            LoggingService.LogInfo("ExtraFields.Post extrafields");
            if (ModelState.IsValid)
            {
                var result = _extraFieldsService.Create(extrafields.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, ExtraFieldsConstants.ExtraFieldsCreateSuccess, FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index));
                }
                var resultData = result.ToResultDetailViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(nameof(Create), resultData);
            }
            return View(nameof(Create), ProcessResult(extrafields));
        }

        [HttpPost]
        public IActionResult Filter(ExtraFieldsFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.ExtraFields, ActionEnum.Update)]
        public IActionResult Edit(ExtraFieldsDetailViewModel model)
        {
            LoggingService.LogInfo($"ExtraFields update for id = {model.ExtraFieldsId}");
            if (ModelState.IsValid)
            {
                var result = _extraFieldsService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        ExtraFieldsConstants.ExtraFieldsSuccessUpdateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModelWithLanguages(LanguageService.GetAll()?.Data);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(nameof(Edit), resultData);
            }
            return ModelInvalid(nameof(Edit), model);
        }

        [HttpPost]
        public IActionResult ExtraFieldsModal(ExtraFieldsDetailViewModel model)
        {
            model.ExtraFieldsSystemListItems = _extraFieldsService.GetAllByDelSystemKeyValues(GetLanguageId(), true).ToSelectList();
            model.ExtraFieldsRegularListItems = _extraFieldsService.GetAllByDelSystemKeyValues(GetLanguageId(), false).ToSelectList();
            model.ErpSystemInstanceQueryListItems = _erpSystemInstanceQueryService.GetAllKeyValues().ToSelectList();
            model.TypeListItems = _extraFieldTypesService.GetAllKeyValues().ToSelectList();
            return PartialView("_ExtraFieldsModal", model);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var deleteResult = _extraFieldsService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    ExtraFieldsConstants.ExtraFieldsDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    ExtraFieldsConstants.ExtraFieldsDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }

        private ResultViewModel<ExtraFieldsListViewModel> DoFilterAndPaging(ExtraFieldsFilterViewModel filter)
        {
            var data = new ResultViewModel<ExtraFieldsListViewModel>();
            filter.LanguageId = GetLanguageId();
            var filteredData = _extraFieldsService.GetAllFilteredByLanguage(filter.ToDto()).Data.ToListViewModel();

            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ExtraFieldsDetailViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new ExtraFieldsListViewModel()
                {
                    ExtraFieldsList = new MultiItemViewModel<ExtraFieldsDetailViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredData.Count;
            }
            else
            {
                data.Data = new ExtraFieldsListViewModel()
                {
                    ExtraFieldsList = new MultiItemViewModel<ExtraFieldsDetailViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filter;
            return data;
        }

        private IActionResult ModelInvalid(string view, ExtraFieldsDetailViewModel extrafields)
        {
            return View(view, ProcessResult(extrafields, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private ExtraFieldsDetailViewModel FillData(ExtraFieldsDetailViewModel source)
        {
            source.ExtraFieldsTypes = _extraFieldTypesService.GetAllKeyValues().ToSelectList();
            source.ErpSystemInstanceQueryListItems = _erpSystemInstanceQueryService.GetAllKeyValues().ToSelectList();
            return source;
        }
    }
}