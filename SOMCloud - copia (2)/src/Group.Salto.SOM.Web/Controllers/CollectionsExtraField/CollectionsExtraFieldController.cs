using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.CollectionsExtraField;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CollectionsExtraField;
using Group.Salto.ServiceLibrary.Common.Contracts.ExtraFieldTypes;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraField;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.CollectionsExtraField;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.CollectionExtraFields
{
    public class CollectionsExtraFieldController : BaseController
    {
        private readonly ICollectionsExtraFieldService _collectionsExtraFieldService;
        private readonly IExtraFieldTypesService _extraFieldTypesService;

        public CollectionsExtraFieldController(ILoggingService loggingService,
                                                    IConfiguration configuration,
                                                    IHttpContextAccessor accessor,
                                                    ICollectionsExtraFieldService collectionsExtraFieldService,
                                                    IExtraFieldTypesService extraFieldTypesService) : base(loggingService, configuration, accessor)
        {
            _collectionsExtraFieldService = collectionsExtraFieldService ?? throw new ArgumentNullException($"{nameof(ICollectionsExtraFieldService)} is null");
            _extraFieldTypesService = extraFieldTypesService ?? throw new ArgumentNullException($"{nameof(ICollectionsExtraFieldService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExtraFieldsCollection, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"CollectionsExtraField.Get get all collection Extra Fields");
            var result = DoFilterAndPaging(new CollectionsExtraFieldFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExtraFieldsCollection, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("CollectionsExtraField.Create Get");
            var types = _extraFieldTypesService.GetAll().Data;
            var response = new ResultViewModel<CollectionsExtraFieldDetailViewModel>()
            {
                Data = FillData(new CollectionsExtraFieldDetailViewModel() { ExtraFieldsTypes = types.ToExtraFieldsTypes() }, ModeActionTypeEnum.Create)
            };

            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExtraFieldsCollection, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"CollectionsExtraField.Get get CollectionsExtraField for id:{id}");
            var result = _collectionsExtraFieldService.GetByIdWithExtraFields(id);
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
        [CustomAuthorization(ActionGroupEnum.ExtraFieldsCollection, ActionEnum.Create)]
        public IActionResult Create(CollectionsExtraFieldDetailViewModel model)
        {
            LoggingService.LogInfo("CollectionsExtraField.Post Create");
            if (ModelState.IsValid)
            {
                var result = _collectionsExtraFieldService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, CollectionsExtraFieldConstants.CollectionsExtraFieldCreateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                FillData(resultData.Data, ModeActionTypeEnum.Create);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(resultData);
            }
            return ModelInvalid(ModeActionTypeEnum.Create, model);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.ExtraFieldsCollection, ActionEnum.Update)]
        public IActionResult Edit(CollectionsExtraFieldDetailViewModel model)
        {
            LoggingService.LogInfo($"CollectionsExtraField.Post update for id = {model.Id}");
            if (ModelState.IsValid && model.Id != default(int))
            {
                var result = _collectionsExtraFieldService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        CollectionsExtraFieldConstants.CollectionsExtraFieldUpdateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index));
                }
                var resultData = result.ToViewModel();
                FillData(resultData.Data, ModeActionTypeEnum.Edit);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(nameof(Edit), resultData);
            }
            return ModelInvalid(ModeActionTypeEnum.Edit, model);
        }

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.ExtraFieldsCollection, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            var deleteResult = _collectionsExtraFieldService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    CollectionsExtraFieldConstants.CollectionsExtraFielDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    CollectionsExtraFieldConstants.CollectionsExtraFielDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        [HttpPost]
        public IActionResult Filter(CollectionsExtraFieldFilterViewModel filter)
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

        private IActionResult ModelInvalid(ModeActionTypeEnum modeAction, CollectionsExtraFieldDetailViewModel model)
        {
            return View(modeAction.ToString(), ProcessResult(FillData(model, modeAction), new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private CollectionsExtraFieldDetailViewModel FillData(CollectionsExtraFieldDetailViewModel source, ModeActionTypeEnum modeAction)
        {
            return source;
        }

        private ResultViewModel<CollectionsExtraFieldListViewModel> DoFilterAndPaging(CollectionsExtraFieldFilterViewModel filter)
        {
            var data = new ResultViewModel<CollectionsExtraFieldListViewModel>();
            var filteredData = _collectionsExtraFieldService.GetAllFiltered(filter.ToDto()).Data.ToListViewModel();

            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<CollectionsExtraFieldViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new CollectionsExtraFieldListViewModel()
                {
                    CollectionsExtraFieldList = new MultiItemViewModel<CollectionsExtraFieldViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredData.Count;
            }
            else
            {
                data.Data = new CollectionsExtraFieldListViewModel()
                {
                    CollectionsExtraFieldList = new MultiItemViewModel<CollectionsExtraFieldViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filter;
            return data;
        }

    }
}