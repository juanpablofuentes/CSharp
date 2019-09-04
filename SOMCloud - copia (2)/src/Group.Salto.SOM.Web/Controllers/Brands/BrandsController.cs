using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Brands;
using Group.Salto.Common.Constants.Model;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Brand;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Brands;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Brand
{
    public class BrandsController : BaseController
    {
        private readonly IBrandsService _brandsService;

        public BrandsController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IBrandsService brandsService)
            : base(loggingService, configuration, accessor)
        {
            _brandsService = brandsService ?? throw new ArgumentNullException(nameof(IBrandsService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Brands, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions Get get all Brands");
            var result = DoFilterAndPaging(new BrandsFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Brands, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Return Brands Create");
            var response = new ResultViewModel<BrandsDetailsViewModel>();
            return View(response);
        }

        [HttpGet]
        public IActionResult Models()
        {
            var model = new ModelViewModel { ModelId = 1 };
            return PartialView("_BrandsModal", model);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Brands, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Brands .get for id:{id}");
            var result = _brandsService.GetById(id);
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
        public IActionResult Models(ModelViewModel model)
        {
            LoggingService.LogInfo($"Brands --> create a new Model");

            return PartialView("_BrandsModal", model);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Brands, ActionEnum.Create)]
        public IActionResult Create(BrandsDetailsViewModel brandsData)
        {
            LoggingService.LogInfo($"Actions Post create Brands");
            if (ModelState.IsValid)
            {
                var result = _brandsService.CreateBrands(brandsData.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        BrandsConstants.BrandsCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(brandsData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Brands, ActionEnum.Update)]
        public IActionResult Edit(BrandsDetailsViewModel brandsData)
        {
            LoggingService.LogInfo($"Actions Post Update assetStatuses by id {brandsData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _brandsService.UpdateBrands(brandsData.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        BrandsConstants.BrandsUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            var result = ProcessResult(brandsData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpPost]
        public IActionResult Filter(BrandsFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.Brands, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete Brands by id {id}");
            var deleteResult = _brandsService.DeleteBrands(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    BrandsConstants.BrandsDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    BrandsConstants.BrandsDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        private ResultViewModel<BrandsViewModel> DoFilterAndPaging(BrandsFilterViewModel filters)
        {
            var data = new ResultViewModel<BrandsViewModel>();
            var filteredData = _brandsService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<BrandViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new BrandsViewModel()
                {
                    Brands = new MultiItemViewModel<BrandViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new BrandsViewModel()
                {
                    Brands = new MultiItemViewModel<BrandViewModel, int>(filteredData)
                };
            }

            data.Data.BrandsFilters = filters;
            return data;
        }
    }
}