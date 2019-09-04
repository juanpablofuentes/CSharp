using Group.Salto.Common.Enums;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.Log;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrder
{
    public class OrderedCalendars : BaseComputeRepetitions, IOrderedCalendars
    {
        public OrderedCalendars(ILoggingService logginingService) : base(logginingService) { }

        public OrderedCalendarsDto NewOrderedCalendars(OrderedCalendarsDto parameter)
        {
            OrderedCalendarsDto orderedCalendarsDto = new OrderedCalendarsDto
            {
                Calendars = parameter.Calendars,
                Preference = parameter.Preference,
                Type = parameter.Type
            };
            GetLimits(orderedCalendarsDto);

            return orderedCalendarsDto;
        }

        private void GetLimits(OrderedCalendarsDto orderedCalendarsDto)
        {
            List<CalendarEvents> calendarEvents = new List<CalendarEvents>();
            foreach (Calendars calendar in orderedCalendarsDto.Calendars)
            {
                calendarEvents.AddRange(calendar.CalendarEvents.Where(x => x.StartDate != x.EndDate));
            }

            orderedCalendarsDto.ActiveStart = DateTime.MaxValue; //Min Event Start Value
            orderedCalendarsDto.ActiveEnd = DateTime.MinValue; //Max Event End Value

            foreach (CalendarEvents calendarEvent in calendarEvents)
            {
                //Get ActiveStart value
                if (calendarEvent.RepetitionType.Equals((int)EventRepetitionTypeEnum.Single))//No repetition events
                {
                    //ActiveStartCheck
                    if (orderedCalendarsDto.ActiveStart != DateTime.MinValue && calendarEvent.StartDate.HasValue && calendarEvent.StartDate < orderedCalendarsDto.ActiveStart) orderedCalendarsDto.ActiveStart = calendarEvent.StartDate.Value;
                    //ActiveEndCheck
                    if (orderedCalendarsDto.ActiveEnd != DateTime.MaxValue && calendarEvent.EndDate.HasValue && calendarEvent.EndDate > orderedCalendarsDto.ActiveEnd) orderedCalendarsDto.ActiveEnd = calendarEvent.EndDate.Value;
                }
                else
                {
                    //ActiveStartCheck
                    if (orderedCalendarsDto.ActiveStart != DateTime.MinValue && calendarEvent.StartDate.HasValue)
                    {
                        DateTime realCeStartDate = calendarEvent.StartDate.Value.Add(calendarEvent.StartTime ?? TimeSpan.Zero);
                        if (realCeStartDate < orderedCalendarsDto.ActiveStart) orderedCalendarsDto.ActiveStart = realCeStartDate;
                    }

                    //ActiveEndCheck
                    if (orderedCalendarsDto.ActiveEnd != DateTime.MaxValue)
                    {
                        if (!(calendarEvent.HasEnd ?? false))
                        {
                            orderedCalendarsDto.ActiveEnd = DateTime.MaxValue;
                        }
                        else if (calendarEvent.EndDate > calendarEvent.StartDate) //skip weird events!
                        {
                            orderedCalendarsDto.ActiveEnd = GetLastRepetitionEnd(calendarEvent);
                        }
                    }
                }
            }
        }

        private DateTime GetLastRepetitionEnd(CalendarEvents calendarEvent)
        {
            if (!(calendarEvent.HasEnd ?? false)) return DateTime.MaxValue;

            if (calendarEvent.EndDate.HasValue)
            {
                TimeSpan Period = TimeSpan.Zero;
                switch (calendarEvent.RepetitionType)
                {
                    case (int)EventRepetitionTypeEnum.Single:
                        return calendarEvent.EndDate.Value;

                    case (int)EventRepetitionTypeEnum.Dailiy:
                        if ((calendarEvent.RepetitionPeriod ?? 0) > 0) Period = TimeSpan.FromDays(((calendarEvent.RepetitionPeriod ?? 1) * 2) + 1);
                        break;

                    case (int)EventRepetitionTypeEnum.Weekly:
                        if ((calendarEvent.RepetitionPeriod ?? 0) > 0) Period = TimeSpan.FromDays(((calendarEvent.RepetitionPeriod ?? 1) * 7) + 1);
                        break;

                    case (int)EventRepetitionTypeEnum.Monthly:
                        if ((calendarEvent.RepetitionPeriod ?? 0) > 0) Period = TimeSpan.FromDays(((calendarEvent.RepetitionPeriod ?? 1) * 31) + 1);
                        break;

                    case (int)EventRepetitionTypeEnum.Yearly:
                        if ((calendarEvent.RepetitionPeriod ?? 0) > 0) Period = TimeSpan.FromDays(((calendarEvent.RepetitionPeriod ?? 1) * 366) + 1);
                        break;
                }

                List<CalendarEventTimeRange> repetitions = base.ComputeRepetitions(new TimePeriodParameters() { CalendarEvent = calendarEvent.ToSchedulerDto(), CalendarEvents = new List<CalendarEventDto>(), Start = calendarEvent.EndDate.Value.Subtract(Period), End = calendarEvent.EndDate.Value.Add(Period) });
                if (repetitions.Count == 0) throw new ArgumentOutOfRangeException("Can't Find Last repetition end");
                return repetitions.Last().Start.AddMinutes(calendarEvent.Duration ?? 0);
            }
            else throw new ArgumentOutOfRangeException("Can't Read Event EndDate");
        }
    }
}