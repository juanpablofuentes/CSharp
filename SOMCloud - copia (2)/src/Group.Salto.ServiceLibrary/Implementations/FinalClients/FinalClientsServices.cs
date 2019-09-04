using Group.Salto.Common;
using Group.Salto.Common.Constants.FinalClients;
using Group.Salto.DataAccess.Tenant.Repositories;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.FinalClients;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClients;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.FinalClients
{
    public class FinalClientsServices : BaseFilterService, IFinalClientsServices
    {
        private readonly IFinalClientsRepository _finalClientRepository;
        private readonly IFinalClientSiteCalendarRepository _finalClientSiteCalendarRepo;
        private readonly ICalendarRepository _calendarRepo;
        private readonly ILocationsFinalClientsRepository _locationFinalClientsRepo;
        private readonly ISitesRepository _sitesRepo;

        public FinalClientsServices(ILoggingService logginingService,
                             IFinalClientsQueryFactory queryFactory,
                             IFinalClientsRepository finalClientRepository,
                             IFinalClientSiteCalendarRepository finalClientSiteCalendarRepo,
                             ICalendarRepository calendarRepo,
                             ILocationsFinalClientsRepository locationFinalClientsRepo,
                             ISitesRepository sitesRepo) : base(queryFactory, logginingService)
        {
            _finalClientRepository = finalClientRepository ?? throw new ArgumentNullException($"{nameof(IFinalClientsRepository)} is null ");
            _finalClientSiteCalendarRepo = finalClientSiteCalendarRepo ?? throw new ArgumentNullException($"{nameof(IFinalClientSiteCalendarRepository)} is null ");
            _calendarRepo = calendarRepo ?? throw new ArgumentNullException($"{nameof(ICalendarRepository)} is null ");
            _locationFinalClientsRepo = locationFinalClientsRepo ?? throw new ArgumentNullException($"{nameof(ILocationsFinalClientsRepository)} is null ");
            _sitesRepo = sitesRepo ?? throw new ArgumentNullException($"{nameof(ISitesRepository)} is null ");
        }

        public ResultDto<FinalClientsDetailDto> Create(FinalClientsDetailDto finalClients)
        {
            ErrorsDto errors = new ErrorsDto();
            ResultDto<FinalClientsDetailDto> result = null;
            FinalClientsDetailDto resultSave = null;
            if (_finalClientRepository.CheckUniqueCode(finalClients.Code))
            {
                errors.AddError(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = FinalClientsConstants.FinalClientsCodeExists });
            }
            else
            {
                LogginingService.LogInfo($"Create FinalClients");
                var finalClientsToCreate = finalClients.ToEntity();
                resultSave = _finalClientRepository.CreateFinalClients(finalClientsToCreate).Entity.ToDetailDto();
            }
            result = new ResultDto<FinalClientsDetailDto>()
            {
                Errors = (errors != null && errors.Errors != null && errors.Errors.Count > 0) ? errors : null,
                Data = resultSave
            };
            return result;
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete FinalClients");
            ResultDto<bool> result = null;
            bool TeamsExist = false;
            var finalClients = _finalClientRepository.GetByIdIncludeReferencesToDelete(id);

            if (finalClients != null)
            {
                var finalClientsCalendars = finalClients.FinalClientSiteCalendar.ToList();
                var finalClientsContacts = finalClients.ContactsFinalClients.ToList();
                var finalClientsLocations = finalClients.LocationsFinalClients.ToList();

                if (finalClientsLocations.Count > 0)
                {
                    foreach (LocationsFinalClients locationFinalClient in finalClientsLocations)
                    {
                        var location = locationFinalClient.Location;
                        location.ContactsLocationsFinalClients.Clear();
                        _sitesRepo.DeleteOnContext(locationFinalClient.Location);
                        _locationFinalClientsRepo.DeleteOnContext(locationFinalClient);
                    }                    
                }

                if (finalClientsCalendars.Count > 0)
                {
                    foreach (FinalClientSiteCalendar finalClientsCalendar in finalClientsCalendars)
                    {
                        if (finalClientsCalendar.Calendar.IsPrivate != null && finalClientsCalendar.Calendar.IsPrivate == true)
                        {
                            _calendarRepo.DeleteOnContext(finalClientsCalendar.Calendar);
                        }
                        _finalClientSiteCalendarRepo.DeleteOnContext(finalClientsCalendar);
                    }
                }

                if (finalClientsContacts.Count > 0)
                {
                    finalClients.ContactsFinalClients.Clear();
                }                
                
                var resultSave = TeamsExist ? false : _finalClientRepository.DeleteFinalClients(finalClients);
                result = ProcessResult(resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists } }
                },
                Data = false,
            };
        }

        public ResultDto<IList<FinalClientsListDto>> GetAllFiltered(FinalClientsFilterDto filter)
        {
            LogginingService.LogInfo($"Get Final clients filtered");
            var query = _finalClientRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToDto());
        }

        public ResultDto<FinalClientsDetailDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get FinalClients by Id");
            var query = _finalClientRepository.GetById(id);
            return ProcessResult(query.ToDetailDto());
        }

        public ResultDto<FinalClientsDetailDto> Update(FinalClientsDetailDto finalClient)
        {
            ErrorsDto errors = new ErrorsDto();
            FinalClientsDetailDto finalClientToUpdateDTO = null;

            var fClient = _finalClientRepository.GetById(finalClient.Id);
            bool finalClientCodeExist = false;
            if (fClient != null)
            {
                if (fClient.Code != finalClient.Code)
                {
                    finalClientCodeExist = CheckUniqueCode(finalClient.Code);
                }
            }
            else
            {
                errors.AddError(new ErrorDto() { ErrorType = ErrorType.EntityNotExists, ErrorMessageKey = FinalClientsConstants.FinalClientsNotExists });
            }

            if (!finalClientCodeExist)
            {
                LogginingService.LogInfo($"Create FinalClients");
                fClient.ContactsFinalClients.Clear();
                fClient.AssignFinalClientContacts(finalClient.Contacts);
                fClient.UpdateFinalClients(finalClient);
                finalClientToUpdateDTO = _finalClientRepository.UpdateFinalClients(fClient).Entity.ToDetailDto();
            }
            else
            {
                errors.AddError(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = FinalClientsConstants.FinalClientsCodeExists });
            }

            ResultDto<FinalClientsDetailDto> result = new ResultDto<FinalClientsDetailDto>()
            {
                Errors = (errors != null && errors.Errors != null && errors.Errors.Count > 0) ? errors : null,
                Data = finalClientToUpdateDTO
            };
            return result;
        }

        public bool CheckUniqueCode(string code)
        {
            return _finalClientRepository.CheckUniqueCode(code);
        }

        public ResultDto<ErrorDto> CanDelete(int id)
        {
            var finalClient = _finalClientRepository.GetByIdCanDelete(id);
            
            ErrorDto result = new ErrorDto() { ErrorMessageKey = string.Empty };

            if (finalClient.LocationsFinalClients?.Select(x => x.Location?.AssetsLocationClient).Any() == true)
            {
                result.ErrorMessageKey = "FinalClientHaveTeams";
            }
            else if (finalClient.LocationsFinalClients?.Select(X => X.Location.SiteUser).Any() == true)
            {
                result.ErrorMessageKey = "FinalClientLocationHavSiteUsers";
            }
            else if (finalClient.WorkOrders?.Any() == true)
            {
                result.ErrorMessageKey = "FinalClientHaveWorkOrders";
            }
            else if (finalClient.WorkOrdersDeritative?.Any() == true)
            {
                result.ErrorMessageKey = "FinalClientHaveWODerivative";
            }
            else if (finalClient.PreconditionsLiteralValues?.Any() == true)
            {
                result.ErrorMessageKey = "FinalClientHavePreconditionsLiteralValues";
            }
            else if (finalClient?.WorkOrders?.Any() == true
                    && finalClient?.WorkOrdersDeritative?.Any() == true
                    && finalClient?.PreconditionsLiteralValues?.Any() == true)
            {
                result.ErrorMessageKey = "FinalClientCanDeleteHaveRelations";
            }

            return ProcessResult(result);
        }

        public ResultDto<IList<Common.Dtos.AdvancedSearch.AdvancedSearchDto>> GetAdvancedSearch(AdvancedSearchQueryTypeDto queryTypeParameters)
        {
            LogginingService.LogInfo($"Get FinalClient GetAdvancedSearch");

            IList<Entities.Tenant.FinalClients> finalClients = _finalClientRepository.GetFinalClientsForAdvancedSearch(new Infrastructure.Common.Dto.AdvancedSearchDto() { SelectType = queryTypeParameters.SearchType, Name = queryTypeParameters.Value, Text = queryTypeParameters.Text });
            IList<Common.Dtos.AdvancedSearch.AdvancedSearchDto> result = finalClients.ToAdvancedSearchListDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get FinalClients Key Value");
            var data = _finalClientRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public ResultDto<List<MultiSelectItemDto>> GetFinalClientMultiSelect(List<int> selectItems)
        {
            LogginingService.LogInfo($"GetFinalClientMultiSelect");
            IEnumerable<BaseNameIdDto<int>> finalClient = GetAllKeyValues();
            return GetMultiSelect(finalClient, selectItems);
        }

        public string GetNamesComaSeparated(List<int> ids)
        {
            List<string> names = _finalClientRepository.GetByIds(ids).Select(x => x.Name).ToList();
            return names.Aggregate((i, j) => $"{i}, {j}");
        }

        private IQueryable<Entities.Tenant.FinalClients> OrderBy(IQueryable<Entities.Tenant.FinalClients> query, FinalClientsFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }
    }
}