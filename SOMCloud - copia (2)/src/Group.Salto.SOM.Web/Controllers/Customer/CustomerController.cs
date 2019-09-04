using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Customer;
using Group.Salto.Common.Enums;
using Group.Salto.Common.Helpers;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Country;
using Group.Salto.ServiceLibrary.Common.Contracts.Customer;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.Module;
using Group.Salto.ServiceLibrary.Common.Dtos.User;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models;
using Group.Salto.SOM.Web.Models.Customer;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Modules;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Customer
{
    public class CustomerController : IdentityController
    {
        private readonly IModuleService _moduleService;
        private readonly ICustomerService _customerService;
        private readonly IIdentityService _identityService;
        private readonly ILanguageService _languageService;
        private readonly ICountryService _countryService;

        public CustomerController(ILoggingService loggingService,
                                    IIdentityService identityService,
                                    IConfiguration configuration,
                                    IModuleService moduleService,
                                    ILanguageService languageService,
                                    IHttpContextAccessor accessor,
                                    ICountryService countryService,
                                    ICustomerService customerService) : base(identityService, loggingService, accessor, configuration)
        {
            _moduleService = moduleService ?? throw new ArgumentNullException(nameof(IModuleService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(ICustomerService));
            _identityService = identityService ?? throw new ArgumentNullException($"{nameof(IIdentityService)} is null");
            _languageService = languageService ?? throw new ArgumentNullException(nameof(ILanguageService));
            _countryService = countryService ?? throw new ArgumentNullException(nameof(ICountryService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Customers, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"CustomerController.Get get all customers");
            var result = DoFilterAndPaging(new CustomerFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Customers, ActionEnum.GetById)]
        public IActionResult Detail(Guid id)
        {
            LoggingService.LogInfo($"Customer.Get get customer for id:{id}");

            var result = _customerService.GetById(id);
            var modules = _moduleService.GetAll();
            var countries = _countryService.GetAllKeyValues().ToKeyValuePair();
            var response = result.ToCustomerDetailViewModel(modules, countries);
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

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Customers, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Return Customer Create");
            var response = new ResultViewModel<CustomerDetailViewModel>()
            {
                Data = new CustomerDetailViewModel()
                {
                    Modules = _moduleService.GetAll().ToViewModel(),
                    Countries = _countryService.GetAllKeyValues().ToKeyValuePair(),
                },
            };
            return View(response);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Customers, ActionEnum.Update)]
        public IActionResult Update(CustomerDetailViewModel customerData)
        {
            LoggingService.LogInfo($"CustomerController.Post Update new Customer");
            if (ModelState.IsValid && customerData.Customer.Id != null && customerData.Customer.Id != default(Guid))
            {
                var result = _customerService.UpdateCustomer(customerData.ToCustomerDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        CustomerConstants.CustomerUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToCustomerDetailViewModel(customerData.Modules.ToDto());
                resultData.Data = RefreshCustomer(customerData);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Detail", resultData);
            }
            return View("Detail", ProcessResult(RefreshCustomer(customerData), new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Customers, ActionEnum.Create)]
        public async Task<IActionResult> Create(CustomerDetailViewModel customerData)
        {
            LoggingService.LogInfo($"CustomerController.Post Create new Customer");
            if (ModelState.IsValid)
            {
                if (!CanCreateCustomer(customerData.Customer, out var validationResult))
                {
                    validationResult.Data = RefreshCustomer(customerData);
                    return View(validationResult);
                }

                if (await CanCreateUserForCustomer(customerData.Customer.TechnicalAdministratorEmail))
                {
                    var canCreateUser = new ResultViewModel<CustomerDetailViewModel>
                    {
                        Data = new CustomerDetailViewModel() { Customer = customerData.Customer, Modules = customerData.Modules },
                        Feedbacks = new FeedbacksViewModel(),
                    };
                    canCreateUser.Data = RefreshCustomer(customerData);
                    canCreateUser.Feedbacks.AddFeedback(LocalizationsConstants.Error,
                        CustomerConstants.CustomerAdministrativeUserExists,
                        FeedbackTypeEnum.Error);
                    return View(canCreateUser);
                }
                var result = await _customerService.Create(customerData.ToCustomerDto(), GetLanguageId());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        CustomerConstants.CustomerCreateSuccessMessage,
                        FeedbackTypeEnum.Success); return RedirectToAction("Index");
                }
                var resultData = result.ToCustomerDetailViewModel();
                resultData.Data = RefreshCustomer(customerData);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            return View("Create", ProcessResult(RefreshCustomer(customerData), new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        public int GetLanguageId()
        {
            var culture = GetCookies(AppConstants.CookieLanguageConstant);
            int languageId;

            if (!string.IsNullOrEmpty(culture))
            {
                languageId = _languageService.GetByCulture(culture).Data.Id;
            }
            else
            {
                languageId = _languageService.GetByCulture(AppConstants.CultureTwoLettersSpanish).Data.Id;
            }

            return languageId;
        }

        [HttpPost]
        public IActionResult Filter(CustomerFilterViewModel filter)
        {
            var resultData = DoFilterAndPaging(filter);
            return View(nameof(Index), resultData);
        }

        private ResultViewModel<CustomersViewModel> DoFilterAndPaging(CustomerFilterViewModel filter)
        {
            var data = new ResultViewModel<CustomersViewModel>();
            var filteredCustomers = _customerService.GetAllFiltered(filter.ToDto()).Data.ToViewModel();
            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<CustomerViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new CustomersViewModel()
                {
                    Customers = new MultiItemViewModel<CustomerViewModel, Guid>
                        (pager.GetPageIEnumerable(filteredCustomers))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredCustomers.Count();
            }
            else
            {
                data.Data = new CustomersViewModel()
                {
                    Customers = new MultiItemViewModel<CustomerViewModel, Guid>(filteredCustomers)
                };
            }
            data.Data.CustomerFilters = filter;
            return data;
        }

        private async Task<bool> CanCreateUserForCustomer(string email)
        {
            var res = await _identityService.FindByEmail(email);
            return res == null;
        }

        private bool CanCreateCustomer(CustomerViewModel customer, out ResultViewModel<CustomerDetailViewModel> result)
        {
            result = null;
            var canCreateCustomer = _customerService.CanCreate(customer.ToDto());
            if (canCreateCustomer.Errors?.Errors != null && canCreateCustomer.Errors.Errors.Any())
            {
                result = canCreateCustomer.ToCustomerDetailViewModel();
            }
            else if (_identityService.FindByEmail(customer.InvoicingContactEmail).Result.Data != null)
            {
                result = new ResultViewModel<CustomerDetailViewModel>
                {
                    Data = new CustomerDetailViewModel() { Customer = customer },
                    Feedbacks = new FeedbacksViewModel(),
                };
                result.Feedbacks.AddFeedback(LocalizationsConstants.Error,
                                            CustomerConstants.CustomerAdministrativeUserExists,
                                            FeedbackTypeEnum.Error);
            }

            return result == null;
        }

        private CustomerDetailViewModel RefreshCustomer(CustomerDetailViewModel customer)
        {
            var countries = _countryService.GetAllKeyValues();
            customer.Customer.ModulesAssigned = customer.ModuleIdsSelected?.ToGuids(CustomerConstants.ModulesSplitCharacter);
            customer.Modules = _moduleService.GetAll().ToViewModel();
            customer.Countries = countries.ToKeyValuePair();
            customer.ModuleIdsSelected = string.Empty;
            return customer;
        }
    }
}