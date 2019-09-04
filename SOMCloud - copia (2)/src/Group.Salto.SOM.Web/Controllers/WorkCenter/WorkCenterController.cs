using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.WorkCenter;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Company;
using Group.Salto.ServiceLibrary.Common.Contracts.Country;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkCenter;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkCenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.WorkCenter
{
    public class WorkCenterController : BaseController
    {
        private IWorkCenterService _workCenterService;
        private ICountryService _countryService;
        private ICompanyService _companyService;
        private IWorkCenterAdapter _workCenterAdapter;

        public WorkCenterController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IWorkCenterService workCenterService,
            ICountryService countryService,
            ICompanyService companyService,
            IWorkCenterAdapter workCenterAdapter)
            : base(loggingService, configuration, accessor)
        {
            _workCenterService = workCenterService ?? throw new ArgumentNullException(nameof(IWorkCenterService));
            _countryService = countryService ?? throw new ArgumentNullException(nameof(ICountryService));
            _companyService = companyService ?? throw new ArgumentNullException(nameof(ICompanyService));
            _workCenterAdapter = workCenterAdapter ?? throw new ArgumentNullException(nameof(IWorkCenterAdapter));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WorkCenters, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"WorkCenterController.Get get all work centers");
            var result = DoFilterAndPaging(new WorkCenterFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WorkCenters, ActionEnum.Create)]
        public IActionResult Create(int? id)
        {
            LoggingService.LogInfo("Return Work Center Create");

            var response = new ResultViewModel<WorkCenterDetailViewModel>()
            {
                Data = FillData(new WorkCenterDetailViewModel(), id)
            };
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WorkCenters, ActionEnum.GetById)]
        public IActionResult Edit(int id, int? companyId)
        {
            LoggingService.LogInfo($"WorkCenter.Get workcenter for id:{id}");
            var result = _workCenterAdapter.GetById(id);
            var response = result.ToViewModel();
            FillData(response.Data, companyId);
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
        [CustomAuthorization(ActionGroupEnum.WorkCenters, ActionEnum.Create)]
        public IActionResult Create(WorkCenterDetailViewModel workCenterDetail)
        {
            LoggingService.LogInfo("WorkCenter create");
            int? companyId = workCenterDetail.WorkCenterFromCompanyId;

            workCenterDetail.CompanySelected = companyId ?? null;

            if (ModelState.IsValid)
            {
                var result = _workCenterService.Create(workCenterDetail.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        WorkCenterConstants.WorkCenterCreateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    if (companyId != null && companyId != -1)
                    {
                        return RedirectToAction("Detail", "Company", new { id = companyId });
                    }
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();

                resultData.Data.WorkCenterFromCompanyId = companyId ?? -1;

                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            return ModelInvalid("Create", workCenterDetail);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WorkCenters, ActionEnum.Update)]
        public IActionResult Edit(WorkCenterDetailViewModel workCenterDetail)
        {
            LoggingService.LogInfo($"Workcenter update for id ={workCenterDetail.WorkCenter.Id}");
            if (ModelState.IsValid)
            {
                var result = _workCenterService.Update(workCenterDetail.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        WorkCenterConstants.WorkCenterUpdateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    if (workCenterDetail.WorkCenterFromCompanyId != null && workCenterDetail.WorkCenterFromCompanyId != -1)
                    {
                        return RedirectToAction("Detail", "Company", new { @id = workCenterDetail.WorkCenterFromCompanyId });
                    }
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                resultData.Data.WorkCenterFromCompanyId = workCenterDetail.WorkCenterFromCompanyId ?? null;
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            return ModelInvalid("Edit", workCenterDetail);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WorkCenters, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            var deleteResult = _workCenterService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    WorkCenterConstants.WorkCenterDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    WorkCenterConstants.WorkCenterDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }

        [HttpPost]
        public IActionResult Filter(WorkCenterFilterViewModel filter)
        {
            var resultData = DoFilterAndPaging(filter);
            return View(nameof(Index), resultData);
        }

        private ResultViewModel<WorkCentersListViewModel> DoFilterAndPaging(WorkCenterFilterViewModel filter)
        {
            var data = new ResultViewModel<WorkCentersListViewModel>();
            var filteredWorkCenters = _workCenterAdapter.GetList(filter.ToDto()).Data.ToViewModel();
            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<WorkCenterListViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new WorkCentersListViewModel()
                {
                    WorkCenters = new MultiItemViewModel<WorkCenterListViewModel, int>(pager.GetPageIEnumerable(filteredWorkCenters))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredWorkCenters.Count();
            }
            else
            {
                data.Data = new WorkCentersListViewModel()
                {
                    WorkCenters = new MultiItemViewModel<WorkCenterListViewModel, int>(filteredWorkCenters)
                };
            }
            data.Data.WorkCenterFilters = filter;
            return data;
        }

        private IActionResult ModelInvalid(string view, WorkCenterDetailViewModel workCenterDetail)
        {
            return View(view, ProcessResult(FillData(workCenterDetail, null), new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private WorkCenterDetailViewModel FillData(WorkCenterDetailViewModel source, int? companyId)
        {
            source.Countries = _countryService.GetAllKeyValues().ToKeyValuePair();
            source.Companies = _companyService.GetAllNotDeleteKeyValues().ToSelectList();
            if (companyId != null)
            {
                source.CompanySelected = companyId;
                source.WorkCenterFromCompanyId = companyId ?? null;
            }
            else
            {
                source.WorkCenterFromCompanyId = -1;
            }
            return source;
        }
    }
}