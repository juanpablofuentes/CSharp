using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.PeopleCollection;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PeopleCollection;
using Group.Salto.ServiceLibrary.Implementations.Language;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.PeopleCollection;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.SubContract;
using Group.Salto.SOM.Web.Models.WorkOrderStatus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.PeopleCollection
{
    public class PeopleCollectionController : BaseController
    {
        private readonly IPeopleCollectionService _peopleCollectionService;

        public PeopleCollectionController(ILoggingService loggingService,
                                            IConfiguration configuration,
                                            IPeopleCollectionService peopleCollectionService,
                                            IHttpContextAccessor accessor) : base(loggingService, configuration, accessor)
        {
            _peopleCollectionService = peopleCollectionService ?? throw new ArgumentNullException($"{nameof(IPeopleCollectionService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.PeopleGroup, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"PeopleCollection.Get get all PeopleCollections");
            var result = DoFilterAndPaging(new PeopleCollectionFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Filter(PeopleCollectionFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.PeopleGroup, ActionEnum.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.PeopleGroup, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"PeopleCollection.Get PeopleCollection for id:{id}");
            var result = _peopleCollectionService.GetById(id);
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
        [CustomAuthorization(ActionGroupEnum.PeopleGroup, ActionEnum.Update)]
        public IActionResult Edit(PeopleCollectionDetailViewModel model)
        {
            LoggingService.LogInfo($"PeopleCollection update for id ={model.Id}");
            if (ModelState.IsValid)
            {
                var result = _peopleCollectionService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        PeopleCollectionConstants.PeopleCollectionUpdateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToResultViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            return ModelInvalid("Edit", model);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.PeopleGroup, ActionEnum.Create)]
        public IActionResult Create(PeopleCollectionDetailViewModel model)
        {
            LoggingService.LogInfo("PeopleCollection create");
            if (ModelState.IsValid)
            {
                var result = _peopleCollectionService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        PeopleCollectionConstants.PeopleCollectionCreateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToResultViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            return ModelInvalid("Create", model);
        }

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.PeopleGroup, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            var deleteResult = _peopleCollectionService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    PeopleCollectionConstants.PeopleCollectionDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    PeopleCollectionConstants.PeopleCollectionDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        private IActionResult ModelInvalid(string view, PeopleCollectionDetailViewModel peopleCollection)
        {
            return View(view, ProcessResult(peopleCollection, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private ResultViewModel<PeopleCollectionsViewModel> DoFilterAndPaging(PeopleCollectionFilterViewModel filters)
        {
            var data = new ResultViewModel<PeopleCollectionsViewModel>();
            var filteredData = _peopleCollectionService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<PeopleCollectionViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new PeopleCollectionsViewModel()
                {
                    PeopleCollections = new MultiItemViewModel<PeopleCollectionViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new PeopleCollectionsViewModel()
                {
                    PeopleCollections = new MultiItemViewModel<PeopleCollectionViewModel, int>(filteredData)
                };
            }
            data.Data.PeopleCollectionFilter = filters;
            return data;
        }
    }
}