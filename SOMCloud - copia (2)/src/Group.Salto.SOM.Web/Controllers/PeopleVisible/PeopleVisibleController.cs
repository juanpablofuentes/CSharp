using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Company;
using Group.Salto.ServiceLibrary.Common.Contracts.Knowledge;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkCenter;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleVisible;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.PeopleVisible;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.PeopleVisible
{
    public class PeopleVisibleController : BaseController
    {
        private readonly IPeopleService _peopleService;
        private readonly ICompanyService _companyService;
        private readonly IKnowledgeService _knowledgeService;
        private readonly IWorkCenterService _workCenterService;

        public PeopleVisibleController(
            ILoggingService loggingService,
            IHttpContextAccessor accessor,
            IConfiguration configuration,
            IPeopleService peopleService,
            ICompanyService companyService,
            IKnowledgeService knowledgeService,
            IWorkCenterService workCenterService) : base(loggingService, configuration, accessor)
        {
            _peopleService = peopleService ?? throw new ArgumentNullException($"{nameof(IPeopleService)} is null");
            _companyService = companyService ?? throw new ArgumentNullException($"{nameof(ICompanyService)} is null");
            _knowledgeService = knowledgeService ?? throw new ArgumentNullException($"{nameof(IKnowledgeService)} is null");
            _workCenterService = workCenterService ?? throw new ArgumentNullException($"{nameof(IWorkCenterService)} is null");
        }

        [CustomAuthorization(ActionGroupEnum.PeopleSearch, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            var filter = new PeopleVisibleFilterViewModel();
            var result = DoFilterAndPaging(filter);
            var feedback = GetFeedbackTempData();

            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }

            return View(result);
        }

        [HttpPost]
        public IActionResult Filter(PeopleVisibleFilterViewModel filter)
        {
            var result = DoFilterAndPaging(filter);
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(nameof(Index), result);
        }        

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _companyService.GetAllNotDeleteKeyValues().ToKeyValuePair();
            return Ok(companies);
        }

        [HttpGet]
        public IActionResult GetDepartments(int id)
        {
            var departments = _companyService.GetDepartmentsByCompanyIdKeyValues(id).ToKeyValuePair();
            return Ok(departments);
        }

        [HttpGet]
        public IActionResult GetKnowledge()
        {
            var knowledge = _knowledgeService.GetAllKeyValues().ToKeyValuePair();
            return Ok(knowledge);
        }

        [HttpGet]
        public IActionResult GetWorkCenter(int id)
        {
            var knowledge = _workCenterService.GetAllKeyValues(id).ToKeyValuePair();
            return Ok(knowledge);
        }

        private ResultViewModel<PeoplesVisibleViewModel> DoFilterAndPaging(PeopleVisibleFilterViewModel filters)
        {
            var data = new ResultViewModel<PeoplesVisibleViewModel>();
            var filteredData = _peopleService.GetVisibleListFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<PeopleVisibleViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new PeoplesVisibleViewModel()
                {
                    PeoplesVisibles = new MultiItemViewModel<PeopleVisibleViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new PeoplesVisibleViewModel()
                {
                    PeoplesVisibles = new MultiItemViewModel<PeopleVisibleViewModel, int>(filteredData)
                };
            }

            data.Data.PeopleVisibleFilters = filters;

            return data;
        }
    }
}