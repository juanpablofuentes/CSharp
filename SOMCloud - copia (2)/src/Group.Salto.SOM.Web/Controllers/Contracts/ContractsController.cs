using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Contracts;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Clients;
using Group.Salto.ServiceLibrary.Common.Contracts.Contracts;
using Group.Salto.ServiceLibrary.Common.Contracts.ContractType;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Contracts;
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

namespace Group.Salto.SOM.Web.Controllers.Contracts
{
    public class ContractsController : BaseController
    {
        private readonly IContractsService _contractsService;
        private readonly IClientService _clientService;
        private readonly IPeopleService _peopleService;
        private readonly IContractTypeService _contractTypeService;

        public ContractsController(
            ILoggingService loggingService,
            IHttpContextAccessor accessor,
            IConfiguration configuration,
            IContractsService contractsService,
            IClientService clientService,
            IPeopleService peopleService,
            IContractTypeService contractTypeService) : base(loggingService, configuration, accessor)
        {
            _contractsService = contractsService ?? throw new ArgumentNullException($"{nameof(IContractsService)} is null");
            _clientService = clientService ?? throw new ArgumentNullException($"{nameof(IClientService)} is null");
            _peopleService = peopleService ?? throw new ArgumentNullException($"{nameof(IPeopleService)} is null");
            _contractTypeService = contractTypeService ?? throw new ArgumentNullException($"{nameof(IContractTypeService)} is null");
        }

        [CustomAuthorization(ActionGroupEnum.Contracts, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Contracts.Get get all contracts");
            var result = DoFilterAndPaging(new ContractsFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Contracts, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Contracts.Create Get");

            var response = new ResultViewModel<ContractEditViewModel>()
            {
                Data = new ContractEditViewModel()
            };
            FillData(response.Data);
            return View(response);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Contracts, ActionEnum.Create)]
        public IActionResult Create(ContractEditViewModel contractEdit)
        {
            LoggingService.LogInfo("Contracts create");
            if (ModelState.IsValid)
            {
                var result = _contractsService.Create(contractEdit.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        ContractsConstants.ContractCreateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                FillData(resultData.Data);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            return ModelInvalid("Create", contractEdit);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Contracts, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Contracts.Get Contract for id:{id}");
            var result = _contractsService.GetById(id);
            var response = result.ToViewModel();
            FillData(response.Data);
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
        [CustomAuthorization(ActionGroupEnum.Contracts, ActionEnum.Update)]
        public IActionResult Edit(ContractEditViewModel contractEdit)
        {
            LoggingService.LogInfo($"Contracts update for id = {contractEdit.Id}");
            if (ModelState.IsValid)
            {
                var result = _contractsService.Update(contractEdit.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        ContractsConstants.ContractEditSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                FillData(resultData.Data);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            return ModelInvalid("Edit", contractEdit);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Contracts, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            var deleteResult = _contractsService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    ContractsConstants.ContractsDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    ContractsConstants.ContractsDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }

        [HttpPost]
        public IActionResult Filter(ContractsFilterViewModel filter)
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

        private ResultViewModel<ContractsListViewModel> DoFilterAndPaging(ContractsFilterViewModel filters)
        {
            var data = new ResultViewModel<ContractsListViewModel>();
            var filteredData = _contractsService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ContractListViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new ContractsListViewModel()
                {
                    Contract = new MultiItemViewModel<ContractListViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new ContractsListViewModel()
                {
                    Contract = new MultiItemViewModel<ContractListViewModel, int>(filteredData)
                };
            }
            data.Data.ContractsFilters = filters;
            return data;
        }

        private IActionResult ModelInvalid(string view, ContractEditViewModel contractEdit, string KeyMessage = null)
        {
            FillData(contractEdit);
            return View(view, ProcessResult(contractEdit, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = KeyMessage ?? LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private ContractEditViewModel FillData(ContractEditViewModel source)
        {
            source.Clients = _clientService.GetAllKeyValues().ToSelectList();
            //TODO. Falta saber como filtrarlo
            source.People = _peopleService.GetAllCommercialKeyValues().ToSelectList();
            source.ContractTypes = _contractTypeService.GetAllKeyValues().ToSelectList();
            return source;
        }
    }
}