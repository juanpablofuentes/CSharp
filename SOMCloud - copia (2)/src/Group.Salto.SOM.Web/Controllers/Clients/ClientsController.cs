using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Client;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Clients;
using Group.Salto.ServiceLibrary.Common.Contracts.Country;
using Group.Salto.ServiceLibrary.Common.Contracts.PostalCode;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Client;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Clients
{
    public class ClientsController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly ICountryService _countryService;

        public ClientsController(
            ILoggingService loggingService,
            IHttpContextAccessor accessor,
            IConfiguration configuration,
            IClientService clientService,
            ICountryService countryService) : base(loggingService, configuration, accessor)
        {
            _clientService = clientService ?? throw new ArgumentNullException($"{nameof(IClientService)} is null");
            _countryService = countryService ?? throw new ArgumentNullException($"{nameof(ICountryService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Clients, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Clients.Get get all Clients");
            var result = DoFilterAndPaging(new ClientsFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Filter(ClientsFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.Clients, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Clients.Create Get");

            var response = new ResultViewModel<ClientDetailViewModel>()
            {
                Data = FillData(new ClientDetailViewModel())
            };

            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Clients, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Clients.Get client for id:{id}");
            var result = _clientService.GetById(id);
            var response = result.ToViewModel();
            FillData(response.Data);
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Edit", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error, LocalizationsConstants.ErrorLoadingDataMessage, FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Clients, ActionEnum.Create)]
        public IActionResult Create(ClientDetailViewModel clientDetail)
        {
            LoggingService.LogInfo("client create");
            if (ModelState.IsValid)
            {
                var result = _clientService.Create(clientDetail.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, ClientsConstants.ClientCreateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            return ModelInvalid("Create", clientDetail);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Clients, ActionEnum.Update)]
        public IActionResult Edit(ClientDetailViewModel clientDetail)
        {
            LoggingService.LogInfo($"client update for id = {clientDetail.ClientGeneralDetail.Id}");
            if (ModelState.IsValid)
            {
                var result = _clientService.Update(clientDetail.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, ClientsConstants.ClientUpdateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            return ModelInvalid("Edit", clientDetail);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Clients, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            LoggingService.LogInfo($"client delete for id = {id}");
            var deleteResult = _clientService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle, ClientsConstants.ClientDeleteSuccessMessage, FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error, ClientsConstants.ClientDeleteErrorMessage, FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }

        private IActionResult ModelInvalid(string view, ClientDetailViewModel clientDetail)
        {
            return View(view, ProcessResult(FillData(clientDetail), new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private ClientDetailViewModel FillData(ClientDetailViewModel source)
        {
            source.ClientGeneralDetail.Countries = _countryService.GetAllKeyValues().ToKeyValuePair();
            return source;
        }

        private ResultViewModel<ClientsViewModel> DoFilterAndPaging(ClientsFilterViewModel filters)
        {
            var data = new ResultViewModel<ClientsViewModel>();
            var filteredData = _clientService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ClientViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new ClientsViewModel()
                {
                    Clients = new MultiItemViewModel<ClientViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new ClientsViewModel()
                {
                    Clients = new MultiItemViewModel<ClientViewModel, int>(filteredData)
                };
            }
            data.Data.ClientsFilters = filters;
            return data;
        }
    }
}