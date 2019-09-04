using System;
using System.Linq;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.SubContracts;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Priority;
using Group.Salto.ServiceLibrary.Common.Contracts.PurchaseRates;
using Group.Salto.ServiceLibrary.Common.Contracts.Subcontracts;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.SubContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Web.Controllers.SubContract
{
    public class SubContractController : BaseController
    {
        private readonly ISubContractService _subContractService;
        private readonly IPurchaseRateService _purchaseRateService;
        private readonly IPriorityService _priorityService;

        public SubContractController(ILoggingService loggingService,
                                        IConfiguration configuration,
                                        ISubContractService subContractService,
                                        IPurchaseRateService purchaseRateService,
                                        IPriorityService priorityService,
                                        IHttpContextAccessor accessor) : base(loggingService, configuration, accessor)
        {
            _subContractService = subContractService ?? throw new ArgumentNullException(nameof(ISubContractService));
            _purchaseRateService = purchaseRateService ?? throw new ArgumentNullException(nameof(IPurchaseRateService));
            _priorityService = priorityService ?? throw new ArgumentNullException(nameof(IPriorityService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ThirdParty, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions.Get get all actions");
            var result = DoFilterAndPaging(new SubContractFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Filter(SubContractFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.ThirdParty, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"SubContract.Get get subcontract for id:{id}");
            var result = _subContractService.GetById(id);
            var response = result.ToDetailViewModel();
            FillCombosData(response.Data);
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


        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ThirdParty, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("SubContract.Create subcontract");
            var initData = new ResultViewModel<SubContractDetailViewModel>()
            {
                Data = new SubContractDetailViewModel()
            };
            FillCombosData(initData.Data);
            return View(initData);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.ThirdParty, ActionEnum.Create)]
        public IActionResult Create(SubContractDetailViewModel model)
        {
            LoggingService.LogInfo("SubContract create");
            if (ModelState.IsValid)
            {
                var result = _subContractService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        SubContractsConstants.SubContractCreateSuccess,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToDetailViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                FillCombosData(resultData.Data);
                return View("Create", resultData);
            }
            FillCombosData(model);
            return View("Create", ProcessResult(model));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.ThirdParty, ActionEnum.Update)]
        public IActionResult Edit(SubContractDetailViewModel model)
        {
            LoggingService.LogInfo($"SubContract update for id ={model.Id}");
            if (ModelState.IsValid && model.Id != default(int))
            {
                var result = _subContractService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        SubContractsConstants.SubContractUpdateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToDetailViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                FillCombosData(resultData.Data);
                return View("Edit", resultData);
            }
            FillCombosData(model);
            return View("Edit", ProcessResult(model));
        }

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.ThirdParty, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            var deleteResult = _subContractService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    SubContractsConstants.SubContractsDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    SubContractsConstants.SubContractsDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        private ResultViewModel<SubContractsViewModel> DoFilterAndPaging(SubContractFilterViewModel filters)
        {
            var data = new ResultViewModel<SubContractsViewModel>();
            var filteredData = _subContractService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<SubContractViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new SubContractsViewModel()
                {
                    SubContracts = new MultiItemViewModel<SubContractViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new SubContractsViewModel()
                {
                    SubContracts = new MultiItemViewModel<SubContractViewModel, int>(filteredData)
                };
            }
            data.Data.SubContractFilter = filters;
            return data;
        }        

        private void FillCombosData(SubContractDetailViewModel model)
        {
            model.PurchaseRate = _purchaseRateService.GetBasePurchaseRate().ToSelectList();
            model.Priorities = _priorityService.GetBasePriorities().ToSelectList();
        }
    }
}