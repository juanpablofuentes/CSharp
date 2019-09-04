using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Families;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Families;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Families;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Families
{
    public class FamiliesController : BaseController
    {
        private readonly IFamiliesService _familiesService;

        public FamiliesController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IFamiliesService familiesService) : base(loggingService, configuration, accessor)
        {
            _familiesService = familiesService ?? throw new ArgumentNullException(nameof(IFamiliesService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.AssetFamilies, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"FamiliesController.Get get all families");
            var result = DoFilterAndPaging(new FamiliesFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.AssetFamilies, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Return Families Create");
            var response = new ResultViewModel<FamiliesDetailsViewModel>();
            return View(response);
        }

        [HttpGet]
        public IActionResult SubFamilies()
        {
            var model = new SubFamiliesViewModel { SubFamiliesId = 1 };
            return PartialView("_FamiliesModal", model);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.AssetFamilies, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Families .get for id:{id}");
            var result = _familiesService.GetById(id);
            var response = result.ToViewModel();
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                response.Feedbacks = response.Feedbacks ?? new FeedbacksViewModel();
                response.Feedbacks.AddFeedback(feedback);
            }
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
        public IActionResult SubFamilies(SubFamiliesViewModel model)
        {
            return PartialView("_FamiliesModal", model);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.AssetFamilies, ActionEnum.Create)]
        public IActionResult Create(FamiliesDetailsViewModel familiesData)
        {
            LoggingService.LogInfo($"Actions Post create Families");
            if (ModelState.IsValid)
            {
                var result = _familiesService.CreateFamilies(familiesData.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        FamiliesConstants.FamiliesCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(familiesData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.AssetFamilies, ActionEnum.Update)]
        public IActionResult Edit(FamiliesDetailsViewModel familiesData)
        {
            LoggingService.LogInfo($"Actions Post Update families by id {familiesData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _familiesService.UpdateFamilies(familiesData.ToEditDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        FamiliesConstants.FamiliesUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    SetFeedbackTempData(LocalizationsConstants.Error,
                                       FamiliesConstants.FamiliesUpdateErrorMessage,
                                       FeedbackTypeEnum.Error);
                    return RedirectToAction("Edit");
                }
            }
            var result = ProcessResult(familiesData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.AssetFamilies, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete Families by id {id}");
            var deleteResult = _familiesService.DeleteFamilies(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    FamiliesConstants.FamiliesDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    FamiliesConstants.FamiliesDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        [HttpPost]
        public IActionResult Filter(FamiliesFilterViewModel filter)
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

        private ResultViewModel<FamiliesViewModel> DoFilterAndPaging(FamiliesFilterViewModel filters)
        {
            var data = new ResultViewModel<FamiliesViewModel>();
            var filteredData = _familiesService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<FamilieViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new FamiliesViewModel()
                {
                    Families = new MultiItemViewModel<FamilieViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new FamiliesViewModel()
                {
                    Families = new MultiItemViewModel<FamilieViewModel, int>(filteredData)
                };
            }
            data.Data.FamiliesFilters = filters;
            return data;
        }
    }
}