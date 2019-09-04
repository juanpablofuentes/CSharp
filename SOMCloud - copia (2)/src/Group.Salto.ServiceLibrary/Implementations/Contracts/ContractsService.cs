using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Contracts;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.Common;
using Group.Salto.Common.Constants.Contracts;
using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Implementations.Contracts
{
    public class ContractsService : BaseService, IContractsService
    {
        private readonly IContractsRepository _contractsRepository;

        public ContractsService(ILoggingService logginingService,
                                IContractsRepository contractsRepository) : base(logginingService)
        {
            _contractsRepository = contractsRepository ?? throw new ArgumentNullException($"{nameof(IContractsRepository)} is null ");
        }

        public ResultDto<ContractDto> GetById(int Id)
        {
            LogginingService.LogInfo($"Get People by id {Id}");
            ContractDto result = _contractsRepository.GetById(Id)?.ToDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<IList<ContractsListDto>> GetAllFiltered(ContractsFilterDto filter)
        {
            LogginingService.LogInfo($"Get Contracts filtered");
            var query = _contractsRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Object, au => au.Object.Contains(filter.Object));
            IList<ContractsListDto> result = query.ToList().ToListDto();
            result = OrderBy(result, filter);
            return ProcessResult(result);
        }

        private List<ContractsListDto> OrderBy(IList<ContractsListDto> data, ContractsFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Object);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Active);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Client);
            return query.ToList();
        }

        public ResultDto<ContractDto> Create(ContractDto contract)
        {
            LogginingService.LogInfo($"Creating new Contract");
            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidateContract(contract, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                Entities.Tenant.Contracts newContract = contract.ToEntity();
                newContract = AssignContractContacts(newContract, contract.ContactsSelected);
                var result = _contractsRepository.CreateContracts(newContract);
                return ProcessResult(result.Entity?.ToDto(), result);
            }

            return ProcessResult(contract, errors);
        }

        public ResultDto<ContractDto> Update(ContractDto contract)
        {
            LogginingService.LogInfo($"Update contract");
            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidateContract(contract, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                Entities.Tenant.Contracts localContract = _contractsRepository.GetById(contract.Id);

                if (localContract != null)
                {
                    ResultDto<ContractDto> result = null;

                    if (localContract != null)
                    {
                        localContract.UpdatePeople(contract.ToEntity());
                        localContract = AssignContractContacts(localContract, contract.ContactsSelected);
                        var resultRepository = _contractsRepository.UpdateContracts(localContract);
                        result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
                    }
                }
                else
                {
                    errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = ContractsConstants.ContractNotExist });
                }
            }

            return ProcessResult(contract, errors);
        }

        public ResultDto<bool> Delete(int Id)
        {
            LogginingService.LogInfo($"Delete contract {Id}");
            List<ErrorDto> errors = new List<ErrorDto>();
            bool deleteResult = false;

            var contractToDelete = _contractsRepository.GetById(Id);
            if (contractToDelete != null)
            {
                if (contractToDelete != null)
                {
                    deleteResult = _contractsRepository.DeleteContracts(contractToDelete);
                }
            }
            else
            {
                errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = ContractsConstants.ContractNotExist });
            }

            return ProcessResult(deleteResult, errors);
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get Contract Key Value");
            var data = _contractsRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        private Entities.Tenant.Contracts AssignContractContacts(Entities.Tenant.Contracts entity, IList<ContactsDto> contacts)
        {
            entity.ContractContacts?.Clear();
            if (contacts != null && contacts.Any())
            {
                entity.ContractContacts = entity.ContractContacts ?? new List<Entities.Tenant.ContractContacts>();
                IList<Entities.Tenant.Contacts> localContacts = contacts.ToEntity();
                foreach (Entities.Tenant.Contacts localContact in localContacts)
                {
                    entity.ContractContacts.Add(new Entities.Tenant.ContractContacts()
                    {
                        Contact = localContact
                    });
                }
            }
            return entity;
        }

        private bool ValidateContract(ContractDto contract, out List<ErrorDto> result)
        {
            result = new List<ErrorDto>();
            if (!contract.IsValid())
            {
                result.Add(new ErrorDto()
                {
                    ErrorType = ErrorType.ValidationError,
                });
            }

            return !result.Any();
        }
    }
}