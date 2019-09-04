using Group.Salto.Common;
using Group.Salto.Common.Constants.Sites;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Sites;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.Sites;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Sites
{
    public class SitesService : BaseFilterService, ISitesService
    {
        private readonly ISitesRepository _sitesRepository;
        private readonly IFinalClientsRepository _finalClientsRepository;
        private readonly ISitesFinalClientsRepository _sitesFinalClientsRepository;
        private readonly ICalendarRepository _calendarRepo;
        private readonly ISiteCalendarRepository _sitesCalendarRepo;

        public SitesService(
            ILoggingService logginingService,
            ISitesRepository sitesRepository,
            IFinalClientsRepository finalClientsRepository,
            ISitesFinalClientsRepository sitesFinalClientsRepository,
            ISitesQueryFactory queryFactory,
            ICalendarRepository calendarRepo,
            ISiteCalendarRepository sitesCalendarRepo) : base(queryFactory, logginingService)
        {
            _sitesRepository = sitesRepository ?? throw new ArgumentNullException($"{nameof(ISitesRepository)} is null ");
            _finalClientsRepository = finalClientsRepository ?? throw new ArgumentNullException($"{nameof(IFinalClientsRepository)} is null ");
            _sitesFinalClientsRepository = sitesFinalClientsRepository ?? throw new ArgumentNullException($"{nameof(ISitesFinalClientsRepository)} is null ");
            _calendarRepo = calendarRepo ?? throw new ArgumentNullException($"{nameof(ICalendarRepository)} is null ");
            _sitesCalendarRepo = sitesCalendarRepo ?? throw new ArgumentNullException($"{nameof(ISiteCalendarRepository)} is null ");
        }

        public ResultDto<IList<SitesListDto>> GetAllFiltered(SitesFilterDto filter)
        {
            var query = _sitesRepository.GetAllByClientSite(filter.finalClientId);
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToListDto());
        }

        public ResultDto<SitesDetailDto> GetById(int id)
        {
            var entity = _sitesRepository.GetByIdWithContacts(id);
            var clientSites = _sitesFinalClientsRepository.GetBySiteId(id);
            return ProcessResult(entity.ToDetailDtoWithClient(clientSites.FinalClientId), entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<SitesDetailDto> Create(SitesDetailDto model)
        {
            SaveResult<Locations> result = null;
            SaveResult<LocationsFinalClients> resultSitesFinalClient = null;

            var entity = model.ToEntity();
            var codeExists = _sitesRepository.ValidateCodeSite(entity, model.FinalClientId);

            if (!codeExists) {
                entity = entity.AssignSitesContacts(model.ContactsSelected);
                result = _sitesRepository.CreateSite(entity);

                var finalClient = _finalClientsRepository.GetById(model.FinalClientId);
                var newCode = finalClient.Code + '-' + model.Code;

                LocationsFinalClients sitesFinalClient = new LocationsFinalClients()
                {
                    LocationId = result.Entity.Id,
                    FinalClientId = model.FinalClientId,
                    CompositeCode = newCode,
                };

                resultSitesFinalClient = _sitesFinalClientsRepository.CreateSitesFinalClient(sitesFinalClient);
                return ProcessResult(result.Entity?.ToDetailDtoWithClient(model.FinalClientId), result);
            }
            else
            {
                return ProcessResult(model, new ErrorDto() {
                    ErrorType = ErrorType.EntityAlredyExists,
                    ErrorMessageKey = SitesConstants.SitesCodeAlreadyExist
                });
            }
        }

        public ResultDto<SitesDetailDto> Update(SitesDetailDto model)
        {
            ResultDto<SitesDetailDto> result = null;
            var entity = _sitesRepository.GetByIdWithContacts(model.Id);
            if (entity != null)
            {
                entity.Update(model);
                entity = entity.AssignSitesContacts(model.ContactsSelected);
                var resultRepository = _sitesRepository.UpdateSite(entity);
                result = ProcessResult(resultRepository.Entity.ToDetailDto(), resultRepository);
            }
            else
            {
                result = ProcessResult(model, new ErrorDto()
                {
                    ErrorType = ErrorType.EntityNotExists
                });
            }
            return result;
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete FinalClientsLocation");
            ResultDto<bool> result = null;
            var location = _sitesRepository.GetByIdCanDelete(id);
            var siteCalendars = location.LocationCalendar.ToList();
            bool TeamsExist = false;
            if (location != null)
            {
                TeamsExist = location.AssetsLocationClient.Count() > 0;
                if (!TeamsExist && location.SiteUser.Count() == 0)
                {
                    if (siteCalendars.Count > 0)
                    {
                        foreach (LocationCalendar locationCalendar in siteCalendars)
                        {
                            if (locationCalendar.Calendar.IsPrivate != null && locationCalendar.Calendar.IsPrivate == true)
                            {
                                _calendarRepo.DeleteOnContext(locationCalendar.Calendar);
                            }
                            _sitesCalendarRepo.DeleteOnContext(locationCalendar);
                        }
                    }
                    location.ContactsLocationsFinalClients.Clear();
                    location.LocationsFinalClients.Clear();
                    var resultSave = _sitesRepository.DeleteLocation(location);
                    result = ProcessResult(resultSave);
                }
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false
            };
        }

        public ResultDto<ErrorDto> CanDelete(int id)
        {
            var location = _sitesRepository.GetByIdCanDelete(id);

            ErrorDto result = new ErrorDto() { ErrorMessageKey = string.Empty };

            if (location.AssetsLocationClient.Any() == true)
            {
                result.ErrorMessageKey = "SiteLocationHaveTeams";
            }
            else if (location.SiteUser.Any() == true)
            {
                result.ErrorMessageKey = "SiteLocationHaveSiteUsers";
            }

            return ProcessResult(result);
        }

        public IList<BaseNameIdDto<int>> FilterByClientSite(QueryCascadeDto queryCascadeDto) 
        {
            var query = _sitesRepository.FilterByClientSite(queryCascadeDto.Text, queryCascadeDto.Selected);
            var result = query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return result;
        }

        public ResultDto<IList<Common.Dtos.AdvancedSearch.AdvancedSearchDto>> GetAdvancedSearch(AdvancedSearchQueryTypeDto queryTypeParameters)
        {
            LogginingService.LogInfo($"Get FinalClient GetAdvancedSearch");

            IList<Locations> sites = _sitesRepository.GetSitesForAdvancedSearch(new Infrastructure.Common.Dto.AdvancedSearchDto() { SelectType = queryTypeParameters.SearchType, Name = queryTypeParameters.Value, Text = queryTypeParameters.Text });
            IList<Common.Dtos.AdvancedSearch.AdvancedSearchDto> result = sites.ToAdvancedSearchListDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        private IQueryable<Locations> OrderBy(IQueryable<Locations> data, SitesFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }
    }
}