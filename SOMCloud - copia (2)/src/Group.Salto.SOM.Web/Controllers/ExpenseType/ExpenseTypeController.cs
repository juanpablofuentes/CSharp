using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.ExpenseType;
using Group.Salto.Common.Constants.Model;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Brand;
using Group.Salto.ServiceLibrary.Common.Contracts.ExpenseType;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Brands;
using Group.Salto.SOM.Web.Models.ExpenseType;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.ExpenseType
{
    public class ExpenseTypeController : BaseController
    {
        private readonly IExpenseTypeService _expensetypeService;

        public ExpenseTypeController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IExpenseTypeService expensetypeService)
            : base(loggingService, configuration, accessor)
        {
            _expensetypeService = expensetypeService ?? throw new ArgumentNullException(nameof(IExpenseTypeService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExpenseTypes, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions Get get all Expense type");
            var result = DoFilterAndPaging(new ExpenseTypeFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExpenseTypes, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Return Expense type Create");
            var response = new ResultViewModel<ExpenseTypeViewModel>();
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ExpenseTypes, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Expense type .get for id:{id}");
            var result = _expensetypeService.GetById(id);
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
        [CustomAuthorization(ActionGroupEnum.ExpenseTypes, ActionEnum.Create)]
        public IActionResult Create(ExpenseTypeViewModel brandsData)
        {
            LoggingService.LogInfo($"Actions Post create expense type");
            if (ModelState.IsValid)
            {
                var result = _expensetypeService.CreateExpenseType(brandsData.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ExpenseTypeConstants.ExpenseTypeCreateSuccessMessage,
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
        [CustomAuthorization(ActionGroupEnum.ExpenseTypes, ActionEnum.Update)]
        public IActionResult Edit(ExpenseTypeViewModel expensetypeData)
        {
            LoggingService.LogInfo($"Actions Post Update expense type by id {expensetypeData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _expensetypeService.UpdateExpenseType(expensetypeData.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ExpenseTypeConstants.ExpenseTypeUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            var result = ProcessResult(expensetypeData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.ExpenseTypes, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete expense type by id {id}");
            var deleteResult = _expensetypeService.DeleteExpenseType(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    ExpenseTypeConstants.ExpenseTypeDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    ExpenseTypeConstants.ExpenseTypeDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        [HttpPost]
        public IActionResult Filter(ExpenseTypeFilterViewModel filter)
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

        private ResultViewModel<ExpenseTypesViewModel> DoFilterAndPaging(ExpenseTypeFilterViewModel filters)
        {
            var data = new ResultViewModel<ExpenseTypesViewModel>();
            var filteredData = _expensetypeService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ExpenseTypeViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new ExpenseTypesViewModel()
                {
                    ExpenseType = new MultiItemViewModel<ExpenseTypeViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new ExpenseTypesViewModel()
                {
                    ExpenseType = new MultiItemViewModel<ExpenseTypeViewModel, int>(filteredData)
                };
            }

            data.Data.ExpenseTypeFilters = filters;
            return data;
        }
    }
}