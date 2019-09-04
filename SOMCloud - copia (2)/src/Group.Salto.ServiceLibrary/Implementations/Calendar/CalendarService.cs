using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Constants.Calendar;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClientSiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClientsSiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.ProjectCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteCalendar;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoryCalendar;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.Calendar
{
    public class CalendarService : BaseService, ICalendarService
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly IPeopleCalendarsRepository _peopleCalendarsRepository;
        private readonly IWorkOrderCategoryCalendarRepository _workOrderCategoryCalendarRepository;
        private readonly IProjectCalendarRepository _projectCalendarRepository;
        private readonly IFinalClientSiteCalendarRepository _finalClientSiteCalendarRepository;
        private readonly ISiteCalendarRepository _siteCalendarRepository;

        public CalendarService(ILoggingService logginingService,
                                ICalendarRepository calendarRepository,
                                ICalendarEventRepository calendarEventRepository,
                                IPeopleCalendarsRepository peopleCalendarsRepository,
                                IWorkOrderCategoryCalendarRepository workOrderCategoryCalendarRepository,
                                IProjectCalendarRepository projectCalendarRepository,
                                IFinalClientSiteCalendarRepository finalClientSiteCalendarRepository,
                                ISiteCalendarRepository siteCalendarRepository) : base(logginingService)
        {
            _calendarRepository = calendarRepository ?? throw new ArgumentNullException($"{nameof(ICalendarRepository)} is null");
            _calendarEventRepository = calendarEventRepository ?? throw new ArgumentNullException($"{nameof(ICalendarEventRepository)} is null");
            _peopleCalendarsRepository = peopleCalendarsRepository ?? throw new ArgumentNullException($"{nameof(IPeopleCalendarsRepository)} is null");
            _workOrderCategoryCalendarRepository = workOrderCategoryCalendarRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoryCalendarRepository)} is null");
            _projectCalendarRepository = projectCalendarRepository ?? throw new ArgumentNullException($"{nameof(IProjectCalendarRepository)} is null");
            _finalClientSiteCalendarRepository = finalClientSiteCalendarRepository ?? throw new ArgumentNullException($"{nameof(IFinalClientSiteCalendarRepository)} is null");
            _siteCalendarRepository = siteCalendarRepository ?? throw new ArgumentNullException($"{nameof(ISiteCalendarRepository)} is null");
        }

        public ResultDto<IList<CalendarBaseDto>> GetAllFiltered(CalendarFilterDto filter)
        {
            LogginingService.LogInfo($"Get Calendars filtered");
            var query = _calendarRepository.GetAllGlobals();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            IList<CalendarBaseDto> result = query.ToDto();
            result = OrderBy(result, filter);
            return ProcessResult(result);
        }

        private List<CalendarBaseDto> OrderBy(IList<CalendarBaseDto> data, CalendarFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query.ToList();
        }

        public ResultDto<CalendarBaseDto> GetById(int id)
        {
            var localCalendar = _calendarRepository.GetById(id);
            return ProcessResult(localCalendar.ToDto(), localCalendar != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<CalendarBaseDto> Create(CalendarBaseDto model)
        {
            var newEntity = model.ToEntity();
            var resultRepository = _calendarRepository.CreateCalendar(newEntity);
            ResultDto<CalendarBaseDto> result = ProcessResult(resultRepository.Entity.ToDto(), resultRepository);
            return result;
        }

        public ResultDto<CalendarBaseDto> Update(CalendarBaseDto model)
        {
            ResultDto<CalendarBaseDto> result = null;
            var entity = _calendarRepository.GetById(model.Id);
            if (entity != null)
            {
                entity.Update(model);
                var resultRepository = _calendarRepository.UpdateCalendar(entity);
                result = ProcessResult(resultRepository.Entity.ToDto(), resultRepository);
            }
            else
            {
                result = ProcessResult(model, new ErrorDto()
                {
                    ErrorType = ErrorType.EntityNotExists,
                    ErrorMessageKey = CalendarsConstants.CalendarNotExistsErrorMessage,
                });
            }

            return result;
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete Calendar by id {id}");
            ResultDto<bool> result = null;
            var calendar = _calendarRepository.GetByIdWithEventsAndPeople(id);
            if (calendar != null)
            {
                calendar = RemovePeople(calendar, calendar.PeopleCalendars);
                calendar = RemovePeopleCollection(calendar, calendar.PeopleCollectionCalendars);
                calendar = RemoveEvents(calendar, calendar.CalendarEvents);
                var resultSave = _calendarRepository.DeleteCalendar(calendar);
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

        public ResultDto<bool> Delete(PeopleCalendarDto model)
        {
            LogginingService.LogInfo($"Delete Calendar by id {model.CalendarId} and people {model.PeopleId}");
            ResultDto<bool> result = null;
            Calendars calendar = _calendarRepository.GetByIdWithEventsAndPeople(model.CalendarId);

            if (calendar != null)
            {
                bool isPrivate = calendar.IsPrivate.HasValue ? calendar.IsPrivate.Value : false;
                if (isPrivate)
                {
                    calendar = RemovePeople(calendar, model.PeopleId);
                    calendar = RemoveEvents(calendar, calendar.CalendarEvents);
                    var resultSave = _calendarRepository.DeleteCalendar(calendar);
                    result = ProcessResult(resultSave.IsOk, resultSave);
                }
                else
                {
                    PeopleCalendars peopleToDelete = calendar.PeopleCalendars.Where(x => x.PeopleId == model.PeopleId).FirstOrDefault();
                    if (peopleToDelete != null)
                    {
                        var resultSave = _peopleCalendarsRepository.DeleteCalendarPeople(peopleToDelete);
                        result = ProcessResult(resultSave.IsOk, resultSave);
                    }
                }
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

        public ResultDto<bool> Delete(WorkOrderCategoryCalendarDto model)
        {
            LogginingService.LogInfo($"Delete Calendar by id {model.CalendarId} and workOrderCategory {model.WorkOrderCategoryId}");
            ResultDto<bool> result = null;
            Calendars calendar = _calendarRepository.GetByIdWithEventsAndWorkOrderCategoryCalendar(model.CalendarId);

            if (calendar != null)
            {
                bool isPrivate = calendar.IsPrivate.HasValue ? calendar.IsPrivate.Value : false;
                if (isPrivate)
                {
                    calendar = RemoveWorkOrderCategoryCalendar(calendar, model.WorkOrderCategoryId);
                    calendar = RemoveEvents(calendar, calendar.CalendarEvents);
                    var resultSave = _calendarRepository.DeleteCalendar(calendar);
                    result = ProcessResult(resultSave.IsOk, resultSave);
                }
                else
                {
                    Entities.Tenant.WorkOrderCategoryCalendar workOrderCategoryToDelete = calendar.WorkOrderCategoryCalendar.Where(x => x.WorkOrderCategoryId == model.WorkOrderCategoryId).FirstOrDefault();
                    if (workOrderCategoryToDelete != null)
                    {
                        var resultSave = _workOrderCategoryCalendarRepository.DeleteWorkOrderCategoryCalendar(workOrderCategoryToDelete);
                        result = ProcessResult(resultSave.IsOk, resultSave);
                    }
                }
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

        public ResultDto<bool> Delete(ProjectCalendarDto model)
        {
            LogginingService.LogInfo($"Delete Calendar by id {model.CalendarId} and ProjectId {model.ProjectId}");
            ResultDto<bool> result = null;
            Calendars calendar = _calendarRepository.GetByIdWithEventsAndProjectCalendar(model.CalendarId);

            if (calendar != null)
            {
                bool isPrivate = calendar.IsPrivate.HasValue ? calendar.IsPrivate.Value : false;
                if (isPrivate)
                {
                    calendar = RemoveWorkOrderCategoryCalendar(calendar, model.ProjectId);
                    calendar = RemoveEvents(calendar, calendar.CalendarEvents);
                    var resultSave = _calendarRepository.DeleteCalendar(calendar);
                    result = ProcessResult(resultSave.IsOk, resultSave);
                }
                else
                {
                    ProjectsCalendars projectCalendarToDelete = calendar.ProjectsCalendars.Where(x => x.ProjectId == model.ProjectId).FirstOrDefault();
                    if (projectCalendarToDelete != null)
                    {
                        var resultSave = _projectCalendarRepository.DeleteProjectCalendar(projectCalendarToDelete);
                        result = ProcessResult(resultSave.IsOk, resultSave);
                    }
                }
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

        public ResultDto<bool> Delete(FinalClientSiteCalendarDto model)
        {
            LogginingService.LogInfo($"Delete Calendar by id {model.CalendarId} and FinalClientSiteId {model.FinalClientSiteId}");
            ResultDto<bool> result = null;
            Calendars calendar = _calendarRepository.GetById(model.CalendarId);

            if (calendar != null)
            {
                bool isPrivate = calendar.IsPrivate.HasValue ? calendar.IsPrivate.Value : false;
                if (isPrivate)
                {                    
                    calendar = RemoveFinalClientSiteCalendar(calendar, model.FinalClientSiteId);                    
                    var resultSave = _calendarRepository.DeleteCalendar(calendar);
                    result = ProcessResult(resultSave.IsOk, resultSave);
                }
                else
                {
                    FinalClientSiteCalendar finalClientSiteCalendarToDelete = calendar.FinalClientSiteCalendar.Where(x => x.FinalClientSiteId == model.FinalClientSiteId).FirstOrDefault();
                    if (finalClientSiteCalendarToDelete != null)
                    {
                        var resultSave = _finalClientSiteCalendarRepository.DeleteFinalClientSiteCalendar(finalClientSiteCalendarToDelete);
                        result = ProcessResult(resultSave.IsOk, resultSave);
                    }
                }
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

        private Calendars RemoveEvents(Calendars calendar, ICollection<CalendarEvents> calendarEvents)
        {
            if (calendarEvents != null && calendarEvents.Any())
            {
                foreach (var eventCalendar in calendarEvents.ToList())
                {
                    _calendarEventRepository.DeleteOnContext(eventCalendar);
                    calendar.CalendarEvents.Remove(eventCalendar);
                }
            }

            return calendar;
        }

        private Calendars RemovePeopleCollection(Calendars calendar, ICollection<PeopleCollectionCalendars> peopleCollectionCalendars)
        {
            if (peopleCollectionCalendars != null && peopleCollectionCalendars.Any())
            {
                foreach (var element in peopleCollectionCalendars.ToList())
                {
                    calendar.PeopleCollectionCalendars.Remove(element);
                }
            }
            return calendar;
        }

        private Calendars RemovePeople(Calendars calendar, ICollection<PeopleCalendars> peopleCalendars)
        {
            if (peopleCalendars != null && peopleCalendars.Any())
            {
                foreach (var element in peopleCalendars.ToList())
                {
                    calendar.PeopleCalendars.Remove(element);
                }
            }
            return calendar;
        }

        private Calendars RemovePeople(Calendars calendar, int peopleId)
        {
            ICollection<PeopleCalendars> peopleCalendars = calendar.PeopleCalendars;
            if (peopleCalendars != null && peopleCalendars.Any())
            {
                List<PeopleCalendars> peopleToDelete = peopleCalendars.Where(x => x.PeopleId == peopleId).ToList();
                foreach (PeopleCalendars element in peopleToDelete)
                {
                    calendar.PeopleCalendars.Remove(element);
                }
            }
            return calendar;
        }

        private Calendars RemoveWorkOrderCategoryCalendar(Calendars calendar, int workOrderCategoryId)
        {
            ICollection<Entities.Tenant.WorkOrderCategoryCalendar> workOrderCategoryCalendars = calendar.WorkOrderCategoryCalendar;
            if (workOrderCategoryCalendars != null && workOrderCategoryCalendars.Any())
            {
                List<Entities.Tenant.WorkOrderCategoryCalendar> workOrderCategoryToDelete = workOrderCategoryCalendars.Where(x => x.WorkOrderCategoryId == workOrderCategoryId).ToList();
                foreach (Entities.Tenant.WorkOrderCategoryCalendar element in workOrderCategoryToDelete)
                {
                    calendar.WorkOrderCategoryCalendar.Remove(element);
                }
            }
            return calendar;
        }

        private Calendars RemoveFinalClientSiteCalendar(Calendars calendar, int finalClientSiteId)
        {
            ICollection<Entities.Tenant.FinalClientSiteCalendar> finalClientSiteCalendar = calendar.FinalClientSiteCalendar;
            if (finalClientSiteCalendar != null && finalClientSiteCalendar.Any())
            {
                List<Entities.Tenant.FinalClientSiteCalendar> finalClientSiteCalendarToDelete = finalClientSiteCalendar.Where(x => x.FinalClientSiteId == finalClientSiteId).ToList();
                foreach (Entities.Tenant.FinalClientSiteCalendar element in finalClientSiteCalendarToDelete)
                {
                    calendar.FinalClientSiteCalendar.Remove(element);
                }
            }
            return calendar;
        }

        private Calendars RemoveSiteCalendar(Calendars calendar, int siteId)
        {
            ICollection<Entities.Tenant.LocationCalendar> locationCalendars = calendar.LocationCalendar;
            if (locationCalendars != null && locationCalendars.Any())
            {
                List<Entities.Tenant.LocationCalendar> locationCalendarToDelete = locationCalendars.Where(x => x.LocationId == siteId).ToList();
                foreach (Entities.Tenant.LocationCalendar element in locationCalendarToDelete)
                {
                    calendar.LocationCalendar.Remove(element);
                }
            }
            return calendar;
        }

        public BaseNameIdDto<int> GetKeyValuesById(int Id)
        {
            LogginingService.LogInfo($"Get calendars Key Value by Id");
            var data = _calendarRepository.GetById(Id);
            return new BaseNameIdDto<int>() { Id = data.Id, Name = data.Name };
        }

        public IList<BaseNameIdDto<int>> GetKeyValuesAvailablePeopleCalendarsToAssign(int peopleId)
        {
            LogginingService.LogInfo($"Get calendars to assing Key Value by peopleid {peopleId}");
            var data = _calendarRepository.GetKeyValuesAvailablePeopleCalendarsToAssign(peopleId);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetKeyValuesAvailableWorkOrderCategoryCalendarsToAssign(int workOrderCategoryId)
        {
            LogginingService.LogInfo($"Get calendars to assing Key Value by peopleid {workOrderCategoryId}");
            var data = _calendarRepository.GetKeyValuesAvailableWorkOrderCategoryCalendarsToAssign(workOrderCategoryId);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetKeyValuesAvailableProjectCalendarsToAssign(int projectId)
        {
            LogginingService.LogInfo($"Get calendars to assing Key Value by projectId {projectId}");
            var data = _calendarRepository.GetKeyValuesAvailableProjectCalendarsToAssign(projectId);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetKeyValuesAvailableFinalClientsSiteCalendarsToAssign(int finalClientSiteId)
        {
            LogginingService.LogInfo($"Get calendars to assing Key Value by finalClientSiteId {finalClientSiteId}");
            var data = _calendarRepository.GetKeyValuesAvailableFinalClientSiteCalendarsToAssign(finalClientSiteId);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetKeyValuesAvailableSiteCalendarsToAssign(int siteId)
        {
            LogginingService.LogInfo($"Get calendars to assing Key Value by SiteId {siteId}");
            var data = _calendarRepository.GetKeyValuesAvailableFinalClientSiteCalendarsToAssign(siteId);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public ResultDto<AddPeopleCalendarDto> AddPrivateCalendar(AddPeopleCalendarDto model)
        {
            var newEntity = model.ToPrivateCalendarEntity();
            var resultRepository = _calendarRepository.CreateCalendar(newEntity);
            ResultDto<AddPeopleCalendarDto> result = ProcessResult(resultRepository.Entity.ToPrivatePeopleCalendarDto(), resultRepository);
            return result;
        }

        public ResultDto<AddWorkOrderCategoryCalendarDto> AddPrivateCalendar(AddWorkOrderCategoryCalendarDto model)
        {
            var newEntity = model.ToPrivateCalendarEntity();
            var resultRepository = _calendarRepository.CreateCalendar(newEntity);
            ResultDto<AddWorkOrderCategoryCalendarDto> result = ProcessResult(resultRepository.Entity.ToPrivateWorkOrderCategoryCalendarDto(), resultRepository);
            return result;
        }

        public ResultDto<AddProjectCalendarDto> AddPrivateCalendar(AddProjectCalendarDto model)
        {
            var newEntity = model.ToPrivateCalendarEntity();
            var resultRepository = _calendarRepository.CreateCalendar(newEntity);
            ResultDto<AddProjectCalendarDto> result = ProcessResult(resultRepository.Entity.ToPrivateProjectCalendarDto(), resultRepository);
            return result;
        }

        public ResultDto<AddFinalClientSiteCalendarDto> AddPrivateCalendar(AddFinalClientSiteCalendarDto model)
        {
            var newEntity = model.ToPrivateCalendarEntity();
            var resultRepository = _calendarRepository.CreateCalendar(newEntity);
            ResultDto<AddFinalClientSiteCalendarDto> result = ProcessResult(resultRepository.Entity.ToPrivateFinalClientSiteCalendarDto(), resultRepository);
            return result;
        }

        public ResultDto<AddSiteCalendarDto> AddPrivateCalendar(AddSiteCalendarDto model)
        {
            var newEntity = model.ToPrivateCalendarEntity();
            var resultRepository = _calendarRepository.CreateCalendar(newEntity);
            ResultDto<AddSiteCalendarDto> result = ProcessResult(resultRepository.Entity.ToPrivateSiteCalendarDto(), resultRepository);
            return result;
        }

        public ResultDto<bool> Delete(SiteCalendarDto model)
        {
            //throw new NotImplementedException();
            LogginingService.LogInfo($"Delete Calendar by id {model.CalendarId} and SiteId {model.SiteId}");
            ResultDto<bool> result = null;
            Calendars calendar = _calendarRepository.GetById(model.CalendarId);

            if (calendar != null)
            {
                bool isPrivate = calendar.IsPrivate.HasValue ? calendar.IsPrivate.Value : false;
                if (isPrivate)
                {
                    calendar = RemoveSiteCalendar(calendar, model.SiteId);
                    var resultSave = _calendarRepository.DeleteCalendar(calendar);
                    result = ProcessResult(resultSave.IsOk, resultSave);
                }
                else
                {
                    LocationCalendar locationCalendarToDelete = calendar.LocationCalendar.Where(x => x.LocationId == model.SiteId).FirstOrDefault();
                    if (locationCalendarToDelete != null)
                    {
                        var resultSave = _siteCalendarRepository.DeleteSiteCalendar(locationCalendarToDelete);
                        result = ProcessResult(resultSave.IsOk, resultSave);
                    }
                }
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
    }
}