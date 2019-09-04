using Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents;
using System;

namespace Group.Salto.ServiceLibrary.Extensions
{
    static class CalendarEventsExtensions
    {
        public static bool OnWeekdays(this WeeklyEventRepetition wrep, DayOfWeek dayOfWeek)
        {
            int _dayOfWeek = ((int)dayOfWeek) % 7;
            return wrep.OnWeekDays[_dayOfWeek];
        }

        public static DateTime JumptoNextActiveDay(this WeeklyEventRepetition wrep, DateTime thisDay)
        {
            int currDay = (int)thisDay.DayOfWeek;
            int nextDay = currDay + 1;
            bool jumpWeek = false;
            if (nextDay > 6)
            {
                nextDay = 0;
                jumpWeek = true;
            }
            while (!wrep.OnWeekDays[nextDay])
            {
                nextDay++;
                if (nextDay > 6)
                {
                    jumpWeek = true;
                    nextDay = 0;
                }
            }

            DateTime nextDate;
            if (jumpWeek)
            {
                nextDate = wrep.JumpToNextActiveWeek(thisDay);
                nextDate.AddDays(nextDay - 1); //Adjust start of week on mondays
            }
            else
            {
                nextDate = thisDay.AddDays(nextDay - currDay);
            }
            return nextDate;
        }

        public static bool IsActiveWeek(this WeeklyEventRepetition wrep, DateTime thisDate)
        {
            int WeeksBetween = GetWeeksBetween(wrep.StartDate, thisDate);
            return (WeeksBetween % wrep.RepetitionPeriod == 0);
        }

        private static DateTime JumpToNextActiveWeek(this WeeklyEventRepetition wrep, DateTime thisDate)
        {
            int WeeksBetween = GetWeeksBetween(wrep.StartDate, thisDate);
            DateTime week = thisDate.AddDays(-((int)thisDate.DayOfWeek) + 1); //Monday based
            int weeksToAdd = wrep.RepetitionPeriod - (WeeksBetween % wrep.RepetitionPeriod);
            return week.AddDays(7 * weeksToAdd);
        }

        public static bool IsActiveDay(this WeeklyEventRepetition wrep, DateTime thisDay)
        {
            return (wrep.IsActiveWeek(thisDay) && wrep.OnWeekdays(thisDay.DayOfWeek));
        }

        private static int GetWeeksBetween(DateTime periodStart, DateTime periodEnd)
        {
            const DayOfWeek firstDayOfWeek = DayOfWeek.Monday;
            const DayOfWeek lastDayOfWeek = DayOfWeek.Sunday;
            const int daysInWeek = 7;

            int daysBetweenStartDateAndPreviousFirstDayOfWeek = (int)periodStart.DayOfWeek - (int)firstDayOfWeek;
            DateTime firstDayOfWeekBeforeStartDate = daysBetweenStartDateAndPreviousFirstDayOfWeek >= 0 ? periodStart.AddDays(-daysBetweenStartDateAndPreviousFirstDayOfWeek) : periodStart.AddDays(-(daysBetweenStartDateAndPreviousFirstDayOfWeek + daysInWeek));

            int daysBetweenEndDateAndFollowingLastDayOfWeek = (int)lastDayOfWeek - (int)periodEnd.DayOfWeek;
            DateTime lastDayOfWeekAfterEndDate = daysBetweenEndDateAndFollowingLastDayOfWeek >= 0 ? periodEnd.AddDays(daysBetweenEndDateAndFollowingLastDayOfWeek) : periodEnd.AddDays(daysBetweenEndDateAndFollowingLastDayOfWeek + daysInWeek);

            return (int)((lastDayOfWeekAfterEndDate - firstDayOfWeekBeforeStartDate).TotalDays / daysInWeek);
        }
    }
}