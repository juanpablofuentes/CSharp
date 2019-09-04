using Group.Salto.Common.Enums;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CalendarEventDtoExtensions
    {
        public static CalendarEventDto ToSchedulerDto(this CalendarEvents source)
        {
            CalendarEventDto result = null;
            if (source != null)
            {
                result = new CalendarEventDto()
                {
                    Id = source.Id,
                    ParentEventId = source.ParentEventId.HasValue ? source.ParentEventId.Value : 0,
                    CalendarId = source.CalendarId.HasValue ? source.CalendarId.Value : 0,
                    CategoryId = source.Category.HasValue ? source.Category.Value: 0,
                    Color = source.Color,
                    Description = source.Description,
                    Name = source.Name,
                    DeletedOccurrence = source.DeletedOccurrence.HasValue ? source.DeletedOccurrence.Value : false,
                    ReplacedEventOccurrenceTS = source.ReplacedEventOccurrenceTs.HasValue ? source.ReplacedEventOccurrenceTs.Value : 0,
                    CostHour = source.CostHour
                };

                if (source.RepetitionType == (int)EventRepetitionTypeEnum.Single)
                {
                    NoRepetition rep = new NoRepetition
                    {
                        StartDate = source.StartDate.Value,
                        EndDate = source.EndDate.HasValue ? source.EndDate.Value: DateTime.MinValue
                    };
                    result.Repetition = rep;
                }
                else
                {
                    EventRepetition repetition = null;
                    switch (source.RepetitionType)
                    {
                        case (int)EventRepetitionTypeEnum.Dailiy:
                        {
                            repetition = new DailyEventRepetition();
                            break;
                        }

                        case (int)EventRepetitionTypeEnum.Weekly:
                        {
                            repetition = new WeeklyEventRepetition
                            {
                                OnWeekDays = new bool[]
                                {
                                    source.RepeatOnMonday.HasValue ? source.RepeatOnMonday.Value : false,
                                    source.RepeatOnTuesday.HasValue ? source.RepeatOnTuesday.Value : false,
                                    source.RepeatOnWednesday.HasValue ? source.RepeatOnWednesday.Value : false,
                                    source.RepeatOnThursday.HasValue ? source.RepeatOnThursday.Value : false,
                                    source.RepeatOnFriday.HasValue ? source.RepeatOnFriday.Value : false,
                                    source.RepeatOnSaturday.HasValue ? source.RepeatOnSaturday.Value : false,
                                    source.RepeatOnSunday.HasValue ? source.RepeatOnSunday.Value : false,
                                }
                            };

                            break;
                        }
                        case (int)EventRepetitionTypeEnum.Monthly:
                        {
                            repetition = new MonthlyEventRepetition { OnDayNumber = source.RepeatOnDayNumber.HasValue ? source.RepeatOnDayNumber.Value : 0 };
                            break;
                        }
                        case (int)EventRepetitionTypeEnum.Yearly:
                        {
                            repetition = new YearlyEventRepetition
                            {
                                OnMonthNumber = source.RepeatOnMonthNumber.HasValue ? source.RepeatOnMonthNumber.Value : 0,
                                OnDayNumber = source.RepeatOnDayNumber.HasValue ? source.RepeatOnDayNumber.Value : 0,
                            };

                            break;
                        }
                    }

                    repetition.StartDate = source.StartDate.HasValue ? source.StartDate.Value : DateTime.MinValue;
                    repetition.EndDate = source.EndDate.HasValue ? source.EndDate.Value : DateTime.MinValue;
                    repetition.HasEnd = source.HasEnd.HasValue ? source.HasEnd.Value : false;
                    
                    repetition.Duration = source.Duration.HasValue ? source.Duration.Value : 0;
                    int numberRepetitions = source.NumberOfRepetitions.HasValue ? source.NumberOfRepetitions.Value : 0;
                    repetition.NumberOfRepetitions = numberRepetitions > 0 ? numberRepetitions : 0;
                    repetition.StartTime = source.StartTime.HasValue ? source.StartTime.Value : DateTime.Now.TimeOfDay;
                    repetition.RepetitionPeriod = source.RepetitionPeriod.HasValue ? source.RepetitionPeriod.Value : 0;
                    result.Repetition = repetition;
                };
            }

            return result;
        }

        public static IList<CalendarEventDto> ToSchedulerDto(this IList<CalendarEvents> source)
        {
            return source.MapList(x => x.ToSchedulerDto());
        }

        public static CalendarEventDto ToNoRepetitionCalendarEvent(this CalendarEventTimeRange calendarEventTimeRange)
        {
            return new CalendarEventDto
            {
                Id = calendarEventTimeRange.Event.Id,
                DeletedOccurrence = false,
                ParentEventId = calendarEventTimeRange.Event.ParentEventId,
                CalendarId = calendarEventTimeRange.Event.CalendarId,
                Color = calendarEventTimeRange.Event.Color,
                Description = calendarEventTimeRange.Event.Description,
                Name = calendarEventTimeRange.Event.Name,
                ReplacedEventOccurrenceTS = calendarEventTimeRange.Event.ReplacedEventOccurrenceTS,
                CostHour = calendarEventTimeRange.Event.CostHour,
                CategoryId = calendarEventTimeRange.Event.CategoryId,
                Repetition = new NoRepetition
                {
                    StartDate = calendarEventTimeRange.Start,
                    EndDate = calendarEventTimeRange.End
                }
            };
        }

        public static CalendarEvents ToEntity(this CalendarEventDto source)
        {
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            int? repetitionType = null;
            TimeSpan? startTime = null;
            int? duration = null;
            bool hasEnd = false;
            int? numberRepetitions = null;
            int? repetitionPeriod = null;
            bool repeatOnMonday = false;
            bool repeatOnTuesday = false;
            bool repeatOnWednesday = false;
            bool repeatOnThursday = false;
            bool repeatOnFriday = false;
            bool repeatOnSaturday = false;
            bool repeatOnSunday = false;
            int? repeatOnDayNumber = null;
            int? repeatOnMonthNumber = null;

            if (source.Repetition is NoRepetition)
            {
                NoRepetition repetition = (NoRepetition)source.Repetition;
                startDate = repetition.StartDate;
                endDate = repetition.EndDate;
                repetitionType = (int)EventRepetitionTypeEnum.Single;
            }
            else
            {
                EventRepetition repetition = (EventRepetition)source.Repetition;
                startDate = repetition.StartDate.Date;
                endDate = repetition.EndDate;
                startTime = repetition.StartTime;
                duration = repetition.Duration;
                hasEnd = repetition.HasEnd;
                numberRepetitions = repetition.NumberOfRepetitions;
                repetitionPeriod = repetition.RepetitionPeriod;

                if (repetition is DailyEventRepetition)
                {
                    repetitionType = (int)EventRepetitionTypeEnum.Dailiy;
                }

                if (repetition is WeeklyEventRepetition)
                {
                    repetitionType = (int)EventRepetitionTypeEnum.Weekly;
                    WeeklyEventRepetition weeklyEventRepetition = (WeeklyEventRepetition)repetition;
                    if (weeklyEventRepetition.OnWeekDays != null)
                    {
                        repeatOnMonday = weeklyEventRepetition.OnWeekDays[0];
                        repeatOnTuesday = weeklyEventRepetition.OnWeekDays[1];
                        repeatOnWednesday = weeklyEventRepetition.OnWeekDays[2];
                        repeatOnThursday = weeklyEventRepetition.OnWeekDays[3];
                        repeatOnFriday = weeklyEventRepetition.OnWeekDays[4];
                        repeatOnSaturday = weeklyEventRepetition.OnWeekDays[5];
                        repeatOnSunday = weeklyEventRepetition.OnWeekDays[6];
                    }
                }

                if (repetition is MonthlyEventRepetition)
                {
                    repetitionType = (int)EventRepetitionTypeEnum.Monthly;
                    MonthlyEventRepetition monthlyEventRepetition = (MonthlyEventRepetition)repetition;
                    repeatOnDayNumber = monthlyEventRepetition.OnDayNumber;
                }

                if (repetition is YearlyEventRepetition)
                {
                    repetitionType = (int)EventRepetitionTypeEnum.Yearly;
                    YearlyEventRepetition yearlyEventRepetition = (YearlyEventRepetition)repetition;
                    repeatOnDayNumber = yearlyEventRepetition.OnDayNumber;
                    repeatOnMonthNumber = yearlyEventRepetition.OnMonthNumber;
                }
            }

            CalendarEvents result = null;
            if (source != null)
            {
                result = new CalendarEvents()
                {
                    CalendarId = source.CalendarId,
                    Name = source.Name,
                    StartDate = startDate,
                    EndDate = endDate == DateTime.MinValue ? (DateTime?) null: endDate,
                    Description = source.Description,
                    Category = source.CategoryId,
                    Color = source.Color,
                    CostHour = source.CostHour,
                    RepetitionType = repetitionType,
                    StartTime = startTime,
                    Duration = duration,
                    HasEnd = hasEnd,
                    NumberOfRepetitions = numberRepetitions,
                    RepetitionPeriod = repetitionPeriod,
                    RepeatOnMonday = repeatOnMonday,
                    RepeatOnTuesday = repeatOnTuesday,
                    RepeatOnWednesday = repeatOnWednesday,
                    RepeatOnThursday = repeatOnThursday,
                    RepeatOnFriday = repeatOnFriday,
                    RepeatOnSaturday = repeatOnSaturday,
                    RepeatOnSunday = repeatOnSunday,
                    RepeatOnDayNumber = repeatOnDayNumber,
                    RepeatOnMonthNumber = repeatOnMonthNumber,
                    ParentEventId = source.ParentEventId > 0 ? source.ParentEventId : (int?) null,
                    DeletedOccurrence = source.DeletedOccurrence,
                    ReplacedEventOccurrenceTs = source.ReplacedEventOccurrenceTS,
                    ModificationDate = DateTime.Now
                };
            }
            return result;
        }

        public static void UpdateCalendarEvent(this CalendarEvents target, CalendarEvents source)
        {
            if (source != null && target != null)
            {
                target.CalendarId = source.CalendarId;
                target.Name = source.Name;
                target.StartDate = source.StartDate;
                target.EndDate = source.EndDate;
                target.Description = source.Description;
                target.Category = source.Category;
                target.Color = source.Color;
                target.CostHour = source.CostHour;
                target.RepetitionType = source.RepetitionType;
                target.StartTime = source.StartTime;
                target.Duration = source.Duration;
                target.HasEnd = source.HasEnd;
                target.NumberOfRepetitions = source.NumberOfRepetitions;
                target.RepetitionPeriod = source.RepetitionPeriod;
                target.RepeatOnMonday = source.RepeatOnMonday;
                target.RepeatOnTuesday = source.RepeatOnTuesday;
                target.RepeatOnWednesday = source.RepeatOnWednesday;
                target.RepeatOnThursday = source.RepeatOnThursday;
                target.RepeatOnFriday = source.RepeatOnFriday;
                target.RepeatOnSaturday = source.RepeatOnSaturday;
                target.RepeatOnSunday = source.RepeatOnSunday;
                target.RepeatOnDayNumber = source.RepeatOnDayNumber;
                target.RepeatOnMonthNumber = source.RepeatOnMonthNumber;
                target.ParentEventId = source.ParentEventId;
                target.DeletedOccurrence = source.DeletedOccurrence;
                target.ReplacedEventOccurrenceTs = source.ReplacedEventOccurrenceTs;
                target.ModificationDate = DateTime.Now;
            };
        }

        public static bool IsValid(this CalendarEventDto source)
        {
            bool result = false;
            result = source != null;
            return result;
        }
    }
}