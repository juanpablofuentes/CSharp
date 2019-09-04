using Group.Salto.Common;
using Group.Salto.Common.Constants.Client;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Clients;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Clients;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Clients
{
    public class ClientService : BaseFilterService, IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMunicipalityRepository _municipalityRepository;

        public ClientService(ILoggingService logginingService,
                             IClientRepository clientRepository,
                             IMunicipalityRepository municipalityRepository,
                             IClientQueryFactory clientQueryFactory) : base(clientQueryFactory, logginingService)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException($"{nameof(IClientRepository)} is null ");
            _municipalityRepository = municipalityRepository ?? throw new ArgumentNullException($"{nameof(IMunicipalityRepository)} is null ");
        }

        public ResultDto<IList<ClientListDto>> GetAllFiltered(ClientFilterDto filter)
        {
            LogginingService.LogInfo($"Get clients filtered");
            var query = _clientRepository.GetAll();
            query = query.WhereIfNotDefault(filter.CorporateName, au => au.CorporateName.Contains(filter.CorporateName));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToDto());
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get Clients Key Value");
            var data = _clientRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public ResultDto<ClientDetailDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get Client by id {id}");
            
            var result = _clientRepository.GetById(id)?.ToDetailDto();
            var municipalitiesIds = _municipalityRepository.GetByIdWithStatesRegionsCountriesIncludes(result.MunicipalitySelected.Value).ToIdsDto();
            result.StateSelected = municipalitiesIds?.StateId;
            result.RegionSelected = municipalitiesIds?.RegionId;
            result.CountrySelected = municipalitiesIds?.CountryId;

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<ClientDetailDto> Create(ClientDetailDto client)
        {
            LogginingService.LogInfo($"Creating new client");
            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidateClient(client, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                Entities.Tenant.Clients newClient = client.ToEntity();
                var result = _clientRepository.CreateClients(newClient);
                return ProcessResult(result.Entity?.ToDetailDto(), result);
            }

            return ProcessResult(client, errors);
        }

        public ResultDto<ClientDetailDto> Update(ClientDetailDto client)
        {
            LogginingService.LogInfo($"Update client with id = {client.Id}");

            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidateClient(client, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                Entities.Tenant.Clients localClient = _clientRepository.GetById(client.Id);

                if (localClient != null)
                {
                    ResultDto<ClientDetailDto> result = null;

                    if (localClient != null)
                    {
                        localClient.UpdateClient(client.ToEntity());
                        var resultRepository = _clientRepository.UpdateClients(localClient);
                        result = ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
                    }
                }
                else
                {
                    errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = ClientsConstants.ClientsNotExist });
                }
            }

            return ProcessResult(client, errors);
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete Client by id {id}");
            ResultDto<bool> result = null;
            var localclient = _clientRepository.GetById(id);
            if (localclient != null)
            {
                var resultSave = _clientRepository.DeleteClients(localclient);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false,
            };
        }

        public ResultDto<List<MultiSelectItemDto>> GetClientMultiSelect(List<int> selectItems)
        {
            LogginingService.LogInfo($"GetWorkOrderStatusMultiSelect");
            IEnumerable<BaseNameIdDto<int>> client = GetAllKeyValues();
            return GetMultiSelect(client, selectItems);
        }

        public string GetNamesComaSeparated(List<int> ids)
        {
            List<string> names = _clientRepository.GetByIds(ids).Select(x => x.CorporateName).ToList();
            return names.Aggregate((i, j) => $"{i}, {j}");
        }

        private IQueryable<Entities.Tenant.Clients> OrderBy(IQueryable<Entities.Tenant.Clients> query, ClientFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.CorporateName);
            return query;
        }

        private bool ValidateClient(ClientDetailDto client, out List<ErrorDto> result)
        {
            result = new List<ErrorDto>();
            if (!client.IsValid())
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