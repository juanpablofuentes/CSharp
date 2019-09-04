using Group.Salto.Common.Constants;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Billing;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Bill;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Bill
{
    public class BillController : BaseController
    {
        private readonly IBillService _billService;

        public BillController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IBillService billService)
            : base(loggingService, configuration, accessor)
        {
            _billService = billService ?? throw new ArgumentNullException(nameof(IBillService));
        }
        
        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.DeliveryNotes, ActionEnum.GetAll)]
        public IActionResult Index(int? Page)
        {

            var filter = new BillFilterViewModel();
            var enddate = DateTime.Now;
            var startdate = enddate.AddMonths(-1);
            filter.EndDate = enddate;
            filter.StartDate = startdate;
            filter.Page = Page is null? 1 : Page.Value;
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
        public IActionResult Filter(BillFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.DeliveryNotes, ActionEnum.GetById)]
        public IActionResult Detail(int Id)
        {

            var DetailData = _billService.GetDetailById(Id).Data.ToListViewModelDetail();
            var BillData = _billService.GetBillById(Id);

            var response = new ResultViewModel<BillsViewModel>();
            response.Data = new BillsViewModel
            {
                Bill = _billService.GetBillById(Id).Data.ToViewModel(),
                BillItems = new MultiItemViewModel<BillDetailViewModel, int>(DetailData),
            };

            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                response.Feedbacks = response.Feedbacks ?? new FeedbacksViewModel();
                response.Feedbacks.AddFeedback(feedback);
            }
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (BillData.Errors?.Errors == null || !BillData.Errors.Errors.Any())
            {
                return View("Detail", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        private ResultViewModel<BillsViewModel> DoFilterAndPaging(BillFilterViewModel filters)
        {
            var data = new ResultViewModel<BillsViewModel>();
            filters.Size = GetNumberEntriesPerPage();
            var filteredData = _billService.GetAllFiltered(filters.ToDto()).Data.ToListViewModel();

            var count = _billService.CountId(filters.ToDto());

            if (filteredData.Count < filters.Size -1)
            {
                 count = filteredData.Count;
            }
            
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<BillViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new BillsViewModel()
                {
                    BillViewModels = new MultiItemViewModel<BillViewModel, int>(filteredData)
                };
                filters.PagesCount = (int) Math.Ceiling((double)count / (double)pager.PageSize);
                filters.Page = pager.Page;
                filters.TotalValues = count; 
            }
            else
            {
                data.Data = new BillsViewModel()
                {
                    BillViewModels = new MultiItemViewModel<BillViewModel, int>(filteredData)
                };
            }
            data.Data.BillFilter = filters;
            return data;
        }
    }
}