using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Group.Salto.Common;
using Group.Salto.Common.Cache;
using Group.Salto.Common.Enums;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Extensions;
using Itenso.TimePeriod;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.Common.Constants.WorkOrder;
using Group.Salto.Common.Constants;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrder
{
    public class WorkOrderCalculateSLADate : BaseComputeRepetitions, IWorkOrderCalculateSLADate
    {
        private readonly ICache _cacheService;
        private readonly IWorkOrderCategoriesService _workOrderCategoriesService;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IPeopleCalendarsRepository _peopleCalendarsRepository;
        private readonly IWorkOrderCategoryCalendarRepository _workOrderCategoryCalendarRepository;
        private readonly IProjectCalendarRepository _projectCalendarRepository;
        private readonly IFinalClientSiteCalendarRepository _finalClientSiteCalendarRepository;
        private readonly ISiteCalendarRepository _siteCalendarRepository;
        private readonly IOrderedCalendars _orderedCalendars;

        public WorkOrderCalculateSLADate(ILoggingService logginingService,
                                        ICache cacheService,
                                        IPeoplePermissionsRepository peoplePermissionsRepository,
                                        IWorkOrderCategoriesService workOrderCategoriesService,
                                        ICalendarRepository calendarRepository,
                                        IPeopleCalendarsRepository peopleCalendarsRepository,
                                        IWorkOrderCategoryCalendarRepository workOrderCategoryCalendarRepository,
                                        IProjectCalendarRepository projectCalendarRepository,
                                        IFinalClientSiteCalendarRepository finalClientSiteCalendarRepository,
                                        ISiteCalendarRepository siteCalendarRepository,
                                        IOrderedCalendars orderedCalendars) : base(logginingService)
        {
            _workOrderCategoriesService = workOrderCategoriesService ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesService)} is null");
            _calendarRepository = calendarRepository ?? throw new ArgumentNullException($"{nameof(ICalendarRepository)} is null");
            _peopleCalendarsRepository = peopleCalendarsRepository ?? throw new ArgumentNullException($"{nameof(IPeopleCalendarsRepository)} is null");
            _workOrderCategoryCalendarRepository = workOrderCategoryCalendarRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoryCalendarRepository)} is null");
            _projectCalendarRepository = projectCalendarRepository ?? throw new ArgumentNullException($"{nameof(IProjectCalendarRepository)} is null");
            _finalClientSiteCalendarRepository = finalClientSiteCalendarRepository ?? throw new ArgumentNullException($"{nameof(IFinalClientSiteCalendarRepository)} is null");
            _siteCalendarRepository = siteCalendarRepository ?? throw new ArgumentNullException($"{nameof(ISiteCalendarRepository)} is null");
            _cacheService = cacheService ?? throw new ArgumentNullException($"{nameof(ICache)} is null");
            _orderedCalendars = orderedCalendars ?? throw new ArgumentNullException($"{nameof(IOrderedCalendars)} is null");
        }

        public void CalculateSLADates(WorkOrderEditDto workorder)
        {
            Entities.Tenant.WorkOrderCategories category = _workOrderCategoriesService.GetByIdWithSLA(workorder.WorkOrderCategoryId);

            string slaRespostaRef = GetSLAReferenceDate(category.Sla.ReferenceMinutesResponse.ToString(), workorder);
            string slaResolucioRef = GetSLAReferenceDate(category.Sla.ReferenceMinutesResolution.ToString(), workorder);
            string slaPenalitzacioRespostaRef = GetSLAReferenceDate(category.Sla.ReferenceMinutesPenaltyUnanswered.ToString(), workorder);
            string slaPenalitzacioResolucioRef = GetSLAReferenceDate(category.Sla.ReferenceMinutesPenaltyWithoutResolution.ToString(), workorder);

            if (category.Sla.TimeResolutionActive.HasValue && category.Sla.TimeResolutionActive.Value && !category.Sla.MinutesResolutionOtDefined.Value && string.IsNullOrEmpty(workorder.ResolutionDateSla))
            {
                workorder.ResolutionDateSla = GetWorkOrderSLADate(new WorkOrderSLAParameters() { ReferenceDate = slaResolucioRef, Category = category, Minutes = category.Sla.MinutesResolutions ?? 0, NaturalMinutes = category.Sla.MinutesResolutionNaturals ?? false, WorkOrder = workorder }).ToString();
            }
            if (category.Sla.TimeResponseActive.Value && !category.Sla.MinutesResponseOtDefined.Value && string.IsNullOrEmpty(workorder.ResponseDateSla))
            {
                workorder.ResponseDateSla = GetWorkOrderSLADate(new WorkOrderSLAParameters() { ReferenceDate = slaRespostaRef, Category = category, Minutes = category.Sla.MinutesResolutions ?? 0, NaturalMinutes = category.Sla.MinutesResolutionNaturals ?? false, WorkOrder = workorder }).ToString();
            }
            if (category.Sla.TimePenaltyWhithoutResolutionActive.Value && !category.Sla.MinutesPenaltyWithoutResolutionOtDefined.Value && string.IsNullOrEmpty(workorder.DatePenaltyWithoutResolutionSla))
            {
                workorder.DatePenaltyWithoutResolutionSla = GetWorkOrderSLADate(new WorkOrderSLAParameters() { ReferenceDate = slaPenalitzacioResolucioRef, Category = category, Minutes = category.Sla.MinutesResolutions ?? 0, NaturalMinutes = category.Sla.MinutesResolutionNaturals ?? false, WorkOrder = workorder }).ToString();
            }
            if (category.Sla.TimePenaltyWithoutResponseActive.Value && !category.Sla.MinutesPenaltyWithoutResponseOtDefined.Value && string.IsNullOrEmpty(workorder.DateUnansweredPenaltySla))
            {
                workorder.DateUnansweredPenaltySla = GetWorkOrderSLADate(new WorkOrderSLAParameters() { ReferenceDate = slaPenalitzacioRespostaRef, Category = category, Minutes = category.Sla.MinutesResolutions ?? 0, NaturalMinutes = category.Sla.MinutesResolutionNaturals ?? false, WorkOrder = workorder }).ToString();
            }
        }

        private string GetSLAReferenceDate(string referencia, WorkOrderEditDto workOrder)
        {
            switch (referencia)
            {
                default:
                    throw new Exception("Unknown SLA ReferenceDate code: " + referencia);
                case WorkOrderCalculatedSLaDateConstants.ActuationDate:
                    return workOrder.ActuationDate ?? DateTime.MaxValue.ToString();
                case WorkOrderCalculatedSLaDateConstants.CreationDate:
                    return workOrder.CreationDate;
                case WorkOrderCalculatedSLaDateConstants.AssignmentTime:
                    return workOrder.AssignmentTime ?? DateTime.MaxValue.ToString();
                case WorkOrderCalculatedSLaDateConstants.PickUpTime:
                    return workOrder.PickUpTime ?? DateTime.MaxValue.ToString();
            }
        }

        private DateTime GetWorkOrderSLADate(WorkOrderSLAParameters parameters)
        {
            string SLAWorkOrderCategoryId = "SLA: " + (parameters.Category.Sla?.Name ?? "??") + ">" + (parameters.Category?.Name ?? "??");

            CancellationTokenSource CancellationSource = new CancellationTokenSource();
            try
            {
                Task<DateTime> calculateTask = Task.Factory.StartNew(() => GetWorkOrderSLADateCancellable(parameters));
                calculateTask.Wait();
                //calcTask.Wait(300);

                if (calculateTask.IsCompleted) return calculateTask.Result;
                else
                {
                    if (parameters.DoLog)
                    {
                        LogginingService.LogWarning($"GetOtSLADate: WorkOrder: {(parameters.WorkOrder.InternalIdentifier ?? "??")} CalculateSLADate timed out after 300 ms. Cancellation Issued.\n {SLAWorkOrderCategoryId}");
                    }
                    CancellationSource.Cancel();
                    return GetEpoch();
                }
            }
            catch (Exception e)
            {
                if (parameters.DoLog)
                {
                    LogginingService.LogError($"GetOtSLADate WorkOrder: {(parameters.WorkOrder.InternalIdentifier ?? "??")} MAJOR SLA Calculation ERROR {SLAWorkOrderCategoryId}");
                }
                return GetEpoch();
            }
        }

        private DateTime GetWorkOrderSLADateCancellable(WorkOrderSLAParameters parameters)
        {
            DateTime result;
            ResultDto<List<OrderedCalendarsDto>> orderedCalendars = new ResultDto<List<OrderedCalendarsDto>>();
            List<OrderedCalendarsDto> calendarsApplied;

            if (Convert.ToDateTime(parameters.ReferenceDate) == DateTime.MaxValue) result = GetEpoch();
            else
            {
                if (!parameters.NaturalMinutes)
                {
                    orderedCalendars = GetCalendarsByPreference(parameters.WorkOrder, parameters.Category);
                    if (orderedCalendars.Errors != null)
                    {
                        LogginingService.LogInfo($"GetOtSLADateCancellable: WorkOrder: {parameters.WorkOrder.InternalIdentifier} SLACalculation Failed!NO CALENDARS FOR CATEGORY: {parameters.Category.Name}");
                        return GetEpoch();
                    }
                    if (parameters.LogTime)
                    {
                        LogginingService.LogInfo($"GetOtSLADateCancellable: Got Calendars");
                    }
                }
                calendarsApplied = orderedCalendars.Data ?? new List<OrderedCalendarsDto>();

                result = CalculateSLADate(calendarsApplied, parameters);
            }

            if (result > GetEpoch())
            {
                if (parameters.LogTime)
                {
                    LogginingService.LogInfo($"GetOtSLADateCancellable: WorkOrder: {parameters.WorkOrder.InternalIdentifier} SLACalculation Done!\n SLA: {parameters.Category.Sla.Name} > {parameters.Category.Name}");
                }
            }
            else
            {
                LogginingService.LogInfo($"GetOtSLADateCancellable: WorkOrder: {parameters.WorkOrder.InternalIdentifier} SLACalculation Failed!");
            }
            return result;
        }

        private ResultDto<List<OrderedCalendarsDto>> GetCalendarsByPreference(WorkOrderEditDto workOrder, Entities.Tenant.WorkOrderCategories category)
        {
            var result = new ResultDto<List<OrderedCalendarsDto>>();
            List<OrderedCalendarsDto> typedCalendars = new List<OrderedCalendarsDto>();

            OrderedCalendarsDto projectCalendars = GetCachedOrderedCalendars(new OrderCalendarParameters(EntitiesWithCalendarsEnum.Project, workOrder.ProjectId, category.ProjectCalendarPreference));
            OrderedCalendarsDto categoryCalendars = GetCachedOrderedCalendars(new OrderCalendarParameters(EntitiesWithCalendarsEnum.WOCategory, workOrder.WorkOrderCategoryId, category.CategoryCalendarPreference));
            OrderedCalendarsDto clientSitecalendars = GetCachedOrderedCalendars(new OrderCalendarParameters(EntitiesWithCalendarsEnum.ClientSite, workOrder.ClientSiteId, category.ClientSiteCalendarPreference));
            OrderedCalendarsDto siteCalendars = GetOrderedCalendars(new OrderCalendarParameters(EntitiesWithCalendarsEnum.Site, workOrder.SiteId, category.SiteCalendarPreference));

            if (projectCalendars.Preference > 0) typedCalendars.Add(projectCalendars);
            if (categoryCalendars.Preference > 0) typedCalendars.Add(categoryCalendars);
            if (clientSitecalendars.Preference > 0) typedCalendars.Add(clientSitecalendars);
            if (siteCalendars.Preference > 0) typedCalendars.Add(siteCalendars);
            typedCalendars = typedCalendars.OrderBy(e => e.Preference).ToList();

            result.Data = typedCalendars;
            if (typedCalendars.Count == 0)
            {
                result.Errors = new ErrorsDto();
                result.Errors.AddError(new ErrorDto() { ErrorType = ErrorType.EntityNotExists });
            }

            return result;
        }

        private OrderedCalendarsDto GetOrderedCalendars(OrderCalendarParameters parameters)
        {
            return _orderedCalendars.NewOrderedCalendars(new OrderedCalendarsDto() { Calendars = GetCalendarsAppliedToEntityCombinedByPreference(parameters.EntityType, parameters.EntityId).Data, Preference = parameters.Preference, Type = parameters.EntityType });
        }

        private OrderedCalendarsDto GetCachedOrderedCalendars(OrderCalendarParameters parameters)
        {
            string key = (int)parameters.EntityType + "-" + parameters.EntityId + "-" + parameters.Preference;
            OrderedCalendarsDto value = (OrderedCalendarsDto)_cacheService.GetData(AppCache.WorkOrderCalendar, key);
            if (value == null)
            {
                value = GetOrderedCalendars(new OrderCalendarParameters(parameters.EntityType, parameters.EntityId, parameters.Preference));
            }
            _cacheService.SetData(AppCache.WorkOrderCalendar, key, value);
            return value;
        }

        private DateTime CalculateSLADate(List<OrderedCalendarsDto> calendarsApplied, WorkOrderSLAParameters parameters)
        {
            DateTime date = Convert.ToDateTime(parameters.ReferenceDate);
            if (date == DateTime.MaxValue) return date;
            if (parameters.NaturalMinutes) return date.AddMinutes(parameters.Minutes);
            else
            {
                return AddActiveMinutes(new TimePeriodParameters()
                {
                    OrderedCalendars = calendarsApplied,
                    Start = date,
                    Minutes = parameters.Minutes,
                    Token = parameters.Token
                });
            }
        }

        private ResultDto<IEnumerable<PreferenceCalendarDto>> GetPreferenceCalendarsAppliedToEntity(EntitiesWithCalendarsEnum entityType, int entityId)
        {
            var res = new ResultDto<IEnumerable<PreferenceCalendarDto>>();
            try
            {
                IEnumerable<PreferenceCalendarDto> PreferenceCalendars;

                switch (entityType)
                {
                    default:
                    case EntitiesWithCalendarsEnum.Global:
                        PreferenceCalendars = _calendarRepository.GetGlobalPreferenceCalendar(entityId).Select(e => new PreferenceCalendarDto { Calendar = e, Preference = 0 }).ToList();
                        break;

                    case EntitiesWithCalendarsEnum.People:
                        PreferenceCalendars = _peopleCalendarsRepository.GetPeoplePreferenceCalendar(entityId).Select(e => new PreferenceCalendarDto { Calendar = e.Calendar, Preference = e.CalendarPriority }).ToList();
                        break;

                    case EntitiesWithCalendarsEnum.WOCategory:
                        PreferenceCalendars = _workOrderCategoryCalendarRepository.GetCategoryPreferenceCalendar(entityId).Select(e => new PreferenceCalendarDto { Calendar = e.Calendar, Preference = e.CalendarPriority }).ToList();
                        break;

                    case EntitiesWithCalendarsEnum.Project:
                        PreferenceCalendars = _projectCalendarRepository.GetProjectPreferenceCalendar(entityId).Select(e => new PreferenceCalendarDto { Calendar = e.Calendar, Preference = e.CalendarPriority }).ToList();
                        break;

                    case EntitiesWithCalendarsEnum.ClientSite:
                        PreferenceCalendars = _finalClientSiteCalendarRepository.GetFinalClientSitePreferenceCalendar(entityId).Select(e => new PreferenceCalendarDto { Calendar = e.Calendar, Preference = e.CalendarPriority }).ToList();
                        break;

                    case EntitiesWithCalendarsEnum.Site:
                        PreferenceCalendars = _siteCalendarRepository.GetSitePreferenceCalendar(entityId).Select(e => new PreferenceCalendarDto { Calendar = e.Calendar, Preference = e.CalendarPriority }).ToList();
                        break;
                }

                res.Data = PreferenceCalendars;
            }
            catch (Exception ex)
            {
                LogginingService.LogError($"WorkOrderCalcluateSLADate, GetPreferenceCalendarsAppliedToEntity Error: {ex}");
                res.Data = Enumerable.Empty<PreferenceCalendarDto>().AsQueryable();
                res.Errors.AddError(new ErrorDto());
            }
            return res;
        }

        private ResultDto<List<Calendars>> GetCalendarsAppliedToEntityCombinedByPreference(EntitiesWithCalendarsEnum entityType, int entityId)
        {
            var result = new ResultDto<List<Calendars>>();
            var appliedPreferencedCalendars = GetPreferenceCalendarsAppliedToEntity(entityType, entityId);
            if (appliedPreferencedCalendars.Errors == null)
            {
                result.Data = CombineByPreferenceLevels(appliedPreferencedCalendars.Data);
            }
            else
            {
                result.Data = new List<Calendars>();
                result.Errors = appliedPreferencedCalendars.Errors;
            }
            return result;
        }

        private DateTime AddActiveMinutes(TimePeriodParameters parameters)
        {
            if (parameters.OrderedCalendars.Count == 0 || !parameters.OrderedCalendars.Any(e => e.HasFutureEvents(parameters.Start))) return GetEpoch();

            TimePeriodSubtractor<TimeRange> substractor = new TimePeriodSubtractor<TimeRange>();
            TimePeriodCombiner<TimeRange> combiner = new TimePeriodCombiner<TimeRange>();

            DateTime minActiveDate = DateTime.MaxValue;
            minActiveDate = parameters.OrderedCalendars.Select(x => x.ActiveStart).Min();
            DateTime maxActiveDate = DateTime.MinValue;
            maxActiveDate = parameters.OrderedCalendars.Select(x => x.ActiveEnd).Max();

            ITimePeriodCollection periods = new TimePeriodCollection();
            DateTime current = parameters.Start.AddSeconds(-parameters.Start.Second).AddMilliseconds(-parameters.Start.Millisecond);
            if (current >= maxActiveDate) return GetEpoch();
            if (current < minActiveDate) current = minActiveDate;
            try
            {
                while (periods.TotalDuration.TotalMinutes <= parameters.Minutes)
                {
                    if (current.Date.Year == 9999) break;
                    DateTime end = current.Date.AddDays(7);
                    OrderedCalendarsDto selectedCalendars = parameters.OrderedCalendars.FirstOrDefault(e => e.HasFutureEvents(current));
                    if (selectedCalendars == null)
                    {
                        return GetEpoch();//No more future events
                    }
                    ITimePeriodCollection nPeriods = GetAvailabilityPeriods(new TimePeriodParameters() { Calendars = selectedCalendars.Calendars, Start = current, End = end, Combiner = combiner, Substractor = substractor });
                    if (nPeriods.Count > 0)
                    {
                        periods = AddPeriodCollections(new TimePeriodParameters() { Collection1 = periods, Collection2 = nPeriods, Combiner = combiner });
                        current = nPeriods.End; //Force check if lower preference calendarLists have events for the rest of the day
                    }
                    else
                    {
                        current = end; //next Day
                    }
                    parameters.Token.ThrowIfCancellationRequested();
                }
                //Trim Excess minutes
                while (((int)periods.TotalDuration.TotalMinutes) > parameters.Minutes)
                {
                    double ExcessMinutes = parameters.Minutes - periods.TotalDuration.TotalMinutes;
                    ITimePeriodCollection Trim = new TimePeriodCollection
                    {
                        new TimeRange(periods.End.AddMinutes(ExcessMinutes), periods.End)
                    };
                    periods = substractor.SubtractPeriods(periods, Trim);
                    if (parameters.Token != null) parameters.Token.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {
                return GetEpoch();
            }

            return periods.End;
        }

        private ITimePeriodCollection AddPeriodCollections(TimePeriodParameters parameters)
        {
            ITimePeriodCollection result = new TimePeriodCollection();
            result.AddAll(parameters.Collection1);
            result.AddAll(parameters.Collection2);
            if (parameters.Combiner == null) parameters.Combiner = new TimePeriodCombiner<TimeRange>();
            return parameters.Combiner.CombinePeriods(result);
        }

        public ITimePeriodCollection GetAvailabilityPeriods(TimePeriodParameters parameters)
        {
            if (parameters.Calendars == null || parameters.Calendars.Count == 0) return new TimePeriodCollection();

            if (parameters.Combiner == null) parameters.Combiner = new TimePeriodCombiner<TimeRange>();
            if (parameters.Substractor == null) parameters.Substractor = new TimePeriodSubtractor<TimeRange>();
            ITimePeriodCollection accActive = new TimePeriodCollection();
            ITimePeriodCollection accInactive = new TimePeriodCollection();

            for (int i = 0; i < parameters.Calendars.Count; i++)
            {
                Calendars calendar = parameters.Calendars.ElementAt(i);
                ITimePeriodCollection[] nPeriods = GetCachedAvailabilityPeriodsPerCalendar(new TimePeriodParameters() { Calendar = calendar, Start = parameters.Start, End = parameters.End, Combiner = parameters.Combiner, Substractor = parameters.Substractor });
                ITimePeriodCollection currentActive = SubstractPeriodCollections(new TimePeriodParameters() { Collection1 = nPeriods[0], Collection2 = nPeriods[1], Substractor = parameters.Substractor });
                ITimePeriodCollection currentInactive = SubstractPeriodCollections(new TimePeriodParameters() { Collection1 = nPeriods[1], Collection2 = accActive, Substractor = parameters.Substractor });
                accActive = AddPeriodCollections(new TimePeriodParameters() { Collection1 = accActive, Collection2 = currentActive, Combiner = parameters.Combiner });
                accInactive = AddPeriodCollections(new TimePeriodParameters() { Collection1 = accInactive, Collection2 = currentInactive, Combiner = parameters.Combiner });
            }
            return SubstractPeriodCollections(new TimePeriodParameters() { Collection1 = accActive, Collection2 = accInactive, Substractor = parameters.Substractor });
        }

        private ITimePeriodCollection[] GetCachedAvailabilityPeriodsPerCalendar(TimePeriodParameters parameters)
        {
            string key = parameters.Calendar.Id + "-" + parameters.Start.Ticks + "-" + parameters.End.Ticks;
            ITimePeriodCollection[] value = (ITimePeriodCollection[])_cacheService.GetData(AppCache.GetCachedAvailabilityPeriodsPerCalendar, key);
            if (value == null)
            {
                List<CalendarEventDto> calEvents = GetEventsFromSingleCalendarBetweenAsSingleEvent(new TimePeriodParameters() { Calendar = parameters.Calendar, Start = parameters.Start, End = parameters.End });
                value = GetAvailabilityPeriodsSeparated(new TimePeriodParameters() { CalendarEvents = calEvents, Start = parameters.Start, End = parameters.End, Combiner = parameters.Combiner, Substractor = parameters.Substractor });
            }
            _cacheService.SetData(AppCache.GetCachedAvailabilityPeriodsPerCalendar, key, value);
            return value;
        }

        private ITimePeriodCollection SubstractPeriodCollections(TimePeriodParameters parameters)
        {
            if (parameters.Substractor == null) parameters.Substractor = new TimePeriodSubtractor<TimeRange>();

            return parameters.Substractor.SubtractPeriods(parameters.Collection1, parameters.Collection2);
        }

        private List<CalendarEventDto> GetEventsFromSingleCalendarBetweenAsSingleEvent(TimePeriodParameters parameters)
        {
            List<CalendarEventTimeRange> calendarEventsTimeRange = new List<CalendarEventTimeRange>();
            foreach (CalendarEvents calendarEvents in parameters.Calendar.CalendarEvents)
            {
                calendarEventsTimeRange.AddRange(base.ComputeRepetitions(new TimePeriodParameters() { CalendarEvent = calendarEvents.ToSchedulerDto(), CalendarEvents = new List<CalendarEventDto>(), Start = parameters.Start, End = parameters.End }));
            }

            List<CalendarEventDto> events = new List<CalendarEventDto>();
            foreach (CalendarEventTimeRange calendarEventTimeRange in calendarEventsTimeRange)
            {
                events.Add(calendarEventTimeRange.ToNoRepetitionCalendarEvent());
            }

            return events;
        }

        private ITimePeriodCollection[] GetAvailabilityPeriodsSeparated(TimePeriodParameters parameters)
        {
            List<CalendarEventDto> availableEvents = parameters.CalendarEvents.FindAll(e => e.CategoryId == 1).ToList();
            List<CalendarEventDto> unavailableEvents = parameters.CalendarEvents.FindAll(e => e.CategoryId == 0).ToList();

            ITimePeriodCollection availablePeriods = new TimePeriodCollection();
            ITimePeriodCollection unavailablePeriods = new TimePeriodCollection();

            ITimePeriodCollection[] result = new ITimePeriodCollection[2];

            foreach (CalendarEventDto calendarEvent in availableEvents)
            {
                availablePeriods.AddAll(ComputeRepetitionsAsTimePeriodCollection(new TimePeriodParameters() { CalendarEvent = calendarEvent, CalendarEvents = new List<CalendarEventDto>(), Start = parameters.Start, End = parameters.End }));
            }

            foreach (CalendarEventDto calendarEvent in unavailableEvents)
            {
                unavailablePeriods.AddAll(ComputeRepetitionsAsTimePeriodCollection(new TimePeriodParameters() { CalendarEvent = calendarEvent, CalendarEvents = new List<CalendarEventDto>(), Start = parameters.Start, End = parameters.End }));
            }

            if (parameters.Combiner == null) parameters.Combiner = new TimePeriodCombiner<TimeRange>();
            if (parameters.Substractor == null) parameters.Substractor = new TimePeriodSubtractor<TimeRange>();

            result[0] = parameters.Combiner.CombinePeriods(availablePeriods);
            result[1] = parameters.Combiner.CombinePeriods(unavailablePeriods);

            result[0] = TrimPeriodCollection(new TimePeriodParameters() { Collection1 = result[0], Start = parameters.Start, End = parameters.End, Substractor = parameters.Substractor });
            result[1] = TrimPeriodCollection(new TimePeriodParameters() { Collection1 = result[1], Start = parameters.Start, End = parameters.End, Substractor = parameters.Substractor });

            return result;
        }

        private ITimePeriodCollection TrimPeriodCollection(TimePeriodParameters parameters)
        {
            ITimePeriodCollection trim = new TimePeriodCollection();
            if (parameters.Collection1.Start < parameters.Start) trim.Add(new TimeRange(parameters.Collection1.Start, parameters.Start));
            if (parameters.Collection1.End > parameters.End) trim.Add(new TimeRange(parameters.End, parameters.Collection1.End));

            if (parameters.Substractor == null)
            {
                parameters.Substractor = new TimePeriodSubtractor<TimeRange>();
            }

            if (trim.Count > 0) parameters.Collection1 = parameters.Substractor.SubtractPeriods(parameters.Collection1, trim);
            return parameters.Collection1;
        }

        private List<Calendars> CombineByPreferenceLevels(IEnumerable<PreferenceCalendarDto> preferenceCalendars)
        {
            Dictionary<int, List<Calendars>> preferenceGroup = new Dictionary<int, List<Calendars>>();
            foreach (PreferenceCalendarDto predefinedCalendar in preferenceCalendars)
            {
                preferenceGroup.TryGetValue(predefinedCalendar.Preference, out List<Calendars> calendarsInLevel);
                if (calendarsInLevel == null) calendarsInLevel = new List<Calendars>();
                calendarsInLevel.Add(predefinedCalendar.Calendar);
                preferenceGroup[predefinedCalendar.Preference] = calendarsInLevel;
            }

            IEnumerable<int> Keys = preferenceGroup.Keys.OrderByDescending(e => e);
            List<Calendars> preferenceOrderedCalendars = new List<Calendars>();
            for (int i = 0; i < Keys.Count(); i++)
            {
                List<Calendars> calendars = preferenceGroup[Keys.ElementAt(i)];
                if (calendars.Count > 1) preferenceOrderedCalendars.Add(Combine(calendars, $"{WorkOrderCalculatedSLaDateConstants.ConbinedCalendarFor} {Keys.ElementAt(i)}"));
                else preferenceOrderedCalendars.Add(calendars.First());
            }
            return preferenceOrderedCalendars;
        }

        private Calendars Combine(List<Calendars> Calendars, string name)
        {
            Calendars combinedCalendar = new Calendars
            {
                Id = 0,
                Name = name,
                Description = WorkOrderCalculatedSLaDateConstants.ConbinedCalendarFor,
            };
            HashSet<CalendarEvents> CombinedEvents = new HashSet<CalendarEvents>();
            foreach (Calendars calendar in Calendars)
            {
                CombinedEvents = new HashSet<CalendarEvents>(CombinedEvents.Union(calendar.CalendarEvents));
            }
            combinedCalendar.CalendarEvents = CombinedEvents;
            return combinedCalendar;
        }

        private TimePeriodCollection ComputeRepetitionsAsTimePeriodCollection(TimePeriodParameters parameters)
        {
            TimeRange limitTimeRange = new TimeRange(parameters.Start, parameters.End);
            TimePeriodCollection returnCollection = new TimePeriodCollection();
            List<CalendarEventTimeRange> repetitions = ComputeRepetitions(new TimePeriodParameters() { CalendarEvent = parameters.CalendarEvent, CalendarEvents = parameters.CalendarEvents, Start = parameters.Start, End = parameters.End });

            IEnumerable<TimeRange> SelectedRanges = repetitions.FindAll(r => limitTimeRange.IntersectsWith(r))
                .Select(e => new TimeRange()
                {
                    Start = e.Start,
                    End = e.End,
                    Duration = (e.End - e.Start)
                });

            returnCollection.AddAll(SelectedRanges);

            return returnCollection;
        }

        private static DateTime GetEpoch()
        {
            return new DateTime(1970, 1, 1);
        }
    }
}