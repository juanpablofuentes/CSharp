using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Company;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Company;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Company;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.Company
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    IHttpContextAccessor accessor,
                                    ICompanyService companyService) : base(loggingService, configuration, accessor)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(ICompanyService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Companies, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions.Get get all Companies");
            var result = DoFilterAndPaging(new CompanyFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }       

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Companies, ActionEnum.Create)]
        public IActionResult Create(int id)
        {
            LoggingService.LogInfo("Return Customer Create");
            return View();
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Companies, ActionEnum.Create)]
        public IActionResult Create(CompanyDetailViewModel company)
        {
            LoggingService.LogInfo("Company create");
            if (ModelState.IsValid)
            {
                if (!company.HasValidDepartmentsSelected())
                {
                    return ModelInvalid("Create", company, CompanyConstants.DepartmentsAreInvalidMessage);
                }
                var result = _companyService.Create(company.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        CompanyConstants.CompanyCreateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToCompanyDetailViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            return ModelInvalid("Create", company);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Companies, ActionEnum.GetById)]
        public IActionResult Detail(int id)
        {
            LoggingService.LogInfo($"Company.Get get compnay for id:{id}");
            var result = _companyService.GetById(id);
            var response = result.ToCompanyDetailViewModel();
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Detail", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Companies, ActionEnum.Update)]
        public IActionResult Detail(CompanyDetailViewModel company)
        {
            LoggingService.LogInfo($"Company update for id ={company.Id}");
            if (ModelState.IsValid && company.Id != default(int))
            {
                if (!company.HasValidDepartmentsSelected())
                {
                    return ModelInvalid("Detail", company, CompanyConstants.DepartmentsAreInvalidMessage);
                }
                var result = _companyService.Update(company.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        CompanyConstants.CompanyUpdateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToCompanyDetailViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Detail", resultData);
            }
            company.Departments = company.DepartmentsSelected.ToList();
            return ModelInvalid("Detail", company);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Companies, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            var deleteResult = _companyService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    CompanyConstants.CompanyDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    CompanyConstants.CompanyDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
            }

            return deleteResult.Data;
        }

        [HttpPost]
        public IActionResult Filter(CompanyFilterViewModel filter)
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

        private IActionResult ModelInvalid(string view, CompanyDetailViewModel company, string KeyMessage = null)
        {
            company.Departments = company.DepartmentsSelected?.ToList();
            return View(view, ProcessResult(company, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = KeyMessage ?? LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }        

        private ResultViewModel<CompaniesViewModel> DoFilterAndPaging(CompanyFilterViewModel filters)
        {
            var data = new ResultViewModel<CompaniesViewModel>();
            var filteredData = _companyService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<CompanyViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new CompaniesViewModel()
                {
                    Companies = new MultiItemViewModel<CompanyViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new CompaniesViewModel()
                {
                    Companies = new MultiItemViewModel<CompanyViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filters;
            return data;
        }
    }
}