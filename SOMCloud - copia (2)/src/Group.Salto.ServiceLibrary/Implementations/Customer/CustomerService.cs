using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Common;
using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.Entities.Tenant;
using Group.Salto.Entities.Tenant.ResultEntities;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Infrastructure.Common.Service;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Customer;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Customer;
using Group.Salto.ServiceLibrary.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Group.Salto.ServiceLibrary.Implementations.Customer
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IMunicipalityRepository _cityRepository;
        private readonly UserManager<Users> _userManager;
        private readonly IPeopleRepository _peopleRepository;
        private readonly ITenantCreatorService _tenantCreatorService;
        private readonly IRolesActionGroupsActionsTenantRepository _rolesActionGroupsActionsTenantRepository;

        public CustomerService( ICustomerRepository customerRepository,
                                ILoggingService logginingService,
                                IModuleRepository moduleRepository,
                                IMunicipalityRepository cityRepository,
                                UserManager<Users> userManager,
                                IPeopleRepository peopleRepository,
                                ITenantCreatorService tenantCreatorService,
                                IRolesActionGroupsActionsTenantRepository rolesActionGroupsActionsTenantRepository) : base(logginingService)

        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException($"{nameof(customerRepository)} is null ");
            _moduleRepository = moduleRepository ?? throw new ArgumentNullException(nameof(IModuleRepository));
            _cityRepository = cityRepository ?? throw new ArgumentNullException($"{nameof(IMunicipalityRepository)} is null");
            _userManager = userManager ?? throw new ArgumentNullException($"{nameof(UserManager<Users>)} is null");
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null"); ;
            _tenantCreatorService = tenantCreatorService ?? throw new ArgumentNullException($"{nameof(_tenantCreatorService)} is null");
            _rolesActionGroupsActionsTenantRepository = rolesActionGroupsActionsTenantRepository ?? throw new ArgumentNullException($"{nameof(IRolesActionGroupsActionsTenantRepository)} is null");
        }

        public async Task<ResultDto<CustomerDto>> Create(CustomerDto source, int languageId)
        {
            LogginingService.LogInfo($"Creating new Customer");
            var errors = new List<ErrorDto>();
            if (!ValidateCustomer(source, out var validations))
            {
                errors.AddRange(validations);
            }
            else if (_customerRepository.Any(x => x.DatabaseName
                                                  == _tenantCreatorService.GetDatabaseName(source.Name.RemoveBlankSpaces())))
            {
                errors.Add(new ErrorDto() { ErrorType = ErrorType.DatabaseAlredyExists });
            }
            else
            {
                var dbCreationResult = _tenantCreatorService.Create(source.Name.RemoveBlankSpaces());
                if (dbCreationResult != null)
                {
                    var newCustomer = source.ToEntity();
                    SetNewCustomer(newCustomer, dbCreationResult);
                    if (source.MunicipalitySelected != null)
                    {
                        var municipality = _cityRepository.GetById(source.MunicipalitySelected.MunicipalityId);
                        newCustomer.Municipality = municipality;
                    }
                    AssingModules(newCustomer, source.ModulesAssociated);
                    var createLocalPeople = CreatePersonForCustomer(source, dbCreationResult.ConnectionString);
                    if (createLocalPeople.IsOk)
                    {
                        var localUser = await CreateUserForCustomer(source,
                                            languageId,
                                            newCustomer,
                                            createLocalPeople.Entity.UserConfiguration.Id);
                        return ProcessResult(localUser.Customer?.ToDto());
                    }
                    return ProcessResult(source, createLocalPeople.Error.ToErrorsDto()?.Errors);
                }
                errors.Add(new ErrorDto() { ErrorType = ErrorType.CannotAccessDatabase });
            }
            return ProcessResult(source, errors);
        }

        private SaveResult<Entities.Tenant.People> CreatePersonForCustomer(CustomerDto source, string connectionString)
        {
            LogginingService.LogInfo($"Create people on tenant for customer {source.Name}");
            var person = new Entities.Tenant.People()
            {
                Name = source.TechnicalAdministratorName,
                FisrtSurname = source.TechnicalAdministratorFirstSurname,
                SecondSurname = source.TechnicalAdministratorSecondSurname,
                Email = source.TechnicalAdministratorEmail,
                IsActive = true,
                IsVisible = true,
                UserConfiguration = new UserConfiguration()
                {
                    GuidId = Guid.NewGuid(),
                }
            };
            var result = _peopleRepository.CreatePeople(person, connectionString);
            return result;
        }

        private async Task<Users> CreateUserForCustomer(CustomerDto customer, int languageId, Customers customerEntity, int userConfigurationId)
        {
            var user = new Users()
            {
                UserName = customer.TechnicalAdministratorEmail,
                Email = customer.TechnicalAdministratorEmail,
                Name = customer.TechnicalAdministratorName,
                FirstSurname = customer.TechnicalAdministratorFirstSurname,
                SecondSurname = customer.TechnicalAdministratorSecondSurname,
                Observations = "Created on create Customer",
                LanguageId = languageId,
                Customer = customerEntity,
                UserConfigurationId = userConfigurationId
            };

            var res = await _userManager.CreateAsync(user, PasswordGenerator.GeneratePassword(8, 20));
            return res.Succeeded ? user : null;
        }

        private static void SetNewCustomer(Customers newCustomer, DatabaseCreation dbCreationResult)
        {
            newCustomer.UpdateStatusDate = DateTime.UtcNow;
            newCustomer.IsActive = true;
            newCustomer.ConnString = dbCreationResult.ConnectionString;
            newCustomer.DatabaseName = dbCreationResult.DatabaseName;
            newCustomer.UpdateDate = DateTime.UtcNow;
            newCustomer.DateCreated = DateTime.UtcNow;
        }

        public ResultDto<CustomerDto> GetById(Guid id)
        {
            LogginingService.LogInfo($"Get customer by id {id}");
            var result = _customerRepository.FindById(id).ToDto();
            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<IList<CustomerDto>> GetAll()
        {
            LogginingService.LogInfo($"Get all customers avaible");
            var result = _customerRepository.GetAll().ToList();
            return ProcessResult(result.MapList(c => c.ToDto()));
        }

        public ResultDto<CustomerDto> UpdateCustomer(CustomerDto source)
        {
            LogginingService.LogInfo($"Update Customer");
            var localCustomer = _customerRepository.FindById(source.Id);
            bool removeModules = localCustomer.ModulesAssigned != source.ModulesAssociated;
            ResultDto<CustomerDto> result = null;
            if (localCustomer != null)
            {
                localCustomer.UpdateCustomer(source.ToEntity());
                Municipalities localMunicipality = null;
                if (source.MunicipalitySelected != null)
                {
                    localMunicipality = _cityRepository.GetById(source.MunicipalitySelected.MunicipalityId);
                }
                localCustomer.Municipality = localMunicipality;
                AssingModules(localCustomer, source.ModulesAssociated);
                var resultRepository = _customerRepository.UpdateCustomer(localCustomer);
                if (removeModules && resultRepository.IsOk)
                {
                    var remove = RemoveActionGroups(source.ModulesAssociated);
                } 

                result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
            }
            return result ?? new ResultDto<CustomerDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<CustomerDto> CanCreate(CustomerDto customer)
        {
            var errors = new List<ErrorDto>();
            if (!ValidateCustomer(customer, out var validations))
            {
                errors.AddRange(validations);
            }
            else if (_customerRepository.Any(x => x.DatabaseName
                                                  == _tenantCreatorService.GetDatabaseName(customer.Name.RemoveBlankSpaces())))
            {
                errors.Add(new ErrorDto() { ErrorType = ErrorType.DatabaseAlredyExists });
            }
            return ProcessResult(customer, errors);
        }

        public ResultDto<IList<CustomerDto>> GetAllFiltered(CustomerFilterDto filter)
        {
            var query = _customerRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().MapList(c => c.ToDto()));
        }

        private bool ValidateCustomer(CustomerDto customer, out List<ErrorDto> result)
        {
            result = new List<ErrorDto>();
            if (!customer.IsValid())
            {
                result.Add(new ErrorDto()
                {
                    ErrorType = ErrorType.ValidationError,
                });
            }
            else if (_customerRepository.Any(x => x.Name == customer.Name.Trim()))
            {
                result.Add(new ErrorDto()
                {
                    ErrorType = ErrorType.EntityAlredyExists,
                });
            }
            return !result.Any();
        }

        private void AssingModules(Customers localCustomer, IList<Guid> modulesIds)
        {
            var modules = GetModulesById(modulesIds)?.Select(mod => new CustomerModule()
            {
                Customer = localCustomer,
                Module = mod,
            })?.ToList();
            localCustomer.ModulesAssigned = modules;
        }

        private bool RemoveActionGroups(IList<Guid> modulesIds)
        {
            var moduleActionGroupsIds = _moduleRepository.GetListByIdIncludeActionGroups(modulesIds)
                .SelectMany(x => x.ModuleActionGroups)
                .GroupBy(x => x.ActionGroupsId)
                .Where(ag => ag.Count() == 1)
                .SelectMany(ag => ag)
                .Select(ag => ag.ActionGroupsId)
                .ToList();
                
            var rolesActionGroupsActionsTenantNotContain = _rolesActionGroupsActionsTenantRepository.GetAllRolesActionGroupsActionsTenant()
                .Where(x => !moduleActionGroupsIds.Contains(x.ActionGroupId));

            if (rolesActionGroupsActionsTenantNotContain.Any())
            {
                return _rolesActionGroupsActionsTenantRepository.DeleteListRolesActionGroupsActionsTenant(rolesActionGroupsActionsTenantNotContain);
            }
            return true;
        }

        private ICollection<Entities.Module> GetModulesById(IList<Guid> modulesIds)
        {
            return modulesIds != null && modulesIds.Any() ?
                    _moduleRepository.GetAllById(modulesIds).ToList() : null;
        }

        private IQueryable<Customers> OrderBy(IQueryable<Customers> query, CustomerFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.InvoicingContactEmail);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.DateCreated);
            return query;
        }
    }
}