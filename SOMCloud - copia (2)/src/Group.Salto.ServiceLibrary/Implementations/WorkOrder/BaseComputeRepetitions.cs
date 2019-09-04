using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.ServiceLibrary.Implementations.CalendarEvent;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrder
{
    public class BaseComputeRepetitions : BaseService
    {
        public BaseComputeRepetitions(ILoggingService logginingService): base (logginingService) { }

        protected List<CalendarEventTimeRange> ComputeRepetitions(TimePeriodParameters parameters)//CalendarEventDto CalendarEvent, List<CalendarEventDto> CalendarEvents, DateTime Start, DateTime End
        {
            TimeRange limitTimeRange = new TimeRange(parameters.Start, parameters.End);
            List<CalendarEventTimeRange> repetitions = new List<CalendarEventTimeRange>();
            if (parameters.CalendarEvent.Repetition is NoRepetition)
            {
                NoRepetition rep = (NoRepetition)parameters.CalendarEvent.Repetition;
                TimeRange eventRange = new TimeRange(rep.StartDate, rep.EndDate);

                if (limitTimeRange.IntersectsWith(eventRange)) repetitions.Add(new CalendarEventTimeRange(rep.StartDate, rep.EndDate, parameters.CalendarEvent));

                return repetitions;
            }
            else
            {
                //Compute the repetitions of the event
                #region compute_repetitions

                EventRepetition rep = ((EventRepetition)parameters.CalendarEvent.Repetition);
                if (rep.StartDate.Add(rep.StartTime) > parameters.End || (rep.HasEnd && (parameters.Start > rep.EndDate.Add(rep.StartTime) && rep.NumberOfRepetitions == int.MinValue))) return repetitions;

                DateTime currentDate = rep.StartDate.Add(rep.StartTime);
                //if the repetition begins before the start, set the current date to be the start date plus the start time
                if (currentDate < parameters.Start) currentDate = parameters.Start.Date.Add(rep.StartTime);

                if (parameters.CalendarEvent.Repetition is DailyEventRepetition drep)
                {
                    while (true)
                    {
                        repetitions.Add(new CalendarEventTimeRange(currentDate, currentDate.AddMinutes(rep.Duration), parameters.CalendarEvent));
                        currentDate = currentDate.AddDays(drep.RepetitionPeriod);
                        if (currentDate > parameters.End) break;
                    }
                }

                if (parameters.CalendarEvent.Repetition is WeeklyEventRepetition wrep)
                {
                    if (wrep.HasEnd && parameters.End > wrep.EndDate) parameters.End = wrep.EndDate;
                    if (currentDate < wrep.StartDate.Add(wrep.StartTime)) currentDate = wrep.StartDate;
                    if (currentDate <= parameters.End) //the Period asked ends before the event repetition starts
                    {
                        while (currentDate < parameters.End)
                        {
                            if (wrep.IsActiveDay(currentDate))
                            {
                                DateTime repetitionStart = currentDate.Date.Add(wrep.StartTime);
                                DateTime repetitionEnd = repetitionStart.AddMinutes(wrep.Duration);
                                repetitions.Add(new CalendarEventTimeRange(repetitionStart, repetitionEnd, parameters.CalendarEvent));
                            }
                            currentDate = wrep.JumptoNextActiveDay(currentDate);
                        }
                    }
                }

                if (parameters.CalendarEvent.Repetition is MonthlyEventRepetition mrep)
                {
                    repetitions.Add(new CalendarEventTimeRange(currentDate, currentDate.AddMinutes(rep.Duration), parameters.CalendarEvent));
                    while (true)
                    {
                        //increase the month
                        currentDate = currentDate.AddMonths(rep.RepetitionPeriod);
                        //set the apropriate day
                        currentDate = currentDate.AddDays(mrep.OnDayNumber - currentDate.Day);
                        if (currentDate > parameters.End)
                        {
                            break;
                        }
                        repetitions.Add(new CalendarEventTimeRange(currentDate, currentDate.AddMinutes(rep.Duration), parameters.CalendarEvent));
                    }
                }
                if (parameters.CalendarEvent.Repetition is YearlyEventRepetition yrep)
                {
                    repetitions.Add(new CalendarEventTimeRange(currentDate, currentDate.AddMinutes(rep.Duration), parameters.CalendarEvent));
                    DateTime monthDayDate = currentDate;
                    while (true)
                    {
                        //set  the month
                        currentDate = currentDate.AddMonths(yrep.OnMonthNumber - currentDate.Month);
                        //set the day
                        currentDate = currentDate.AddDays(yrep.OnDayNumber - currentDate.Day);
                        //increase the year
                        currentDate = currentDate.AddYears(1);
                        if (currentDate > parameters.End)
                        {
                            break;
                        }
                        repetitions.Add(new CalendarEventTimeRange(currentDate, currentDate.AddMinutes(rep.Duration), parameters.CalendarEvent));
                    }
                }

                #endregion

                //Remove all repetitions that ARE included before the end of the event
                if (rep.HasEnd && rep.EndDate != DateTime.MinValue) repetitions.RemoveAll(e => e.Start > rep.EndDate);

                //Remove repetition excess
                if (rep.HasEnd && rep.NumberOfRepetitions > 0)
                {
                    if (parameters.CalendarEvent.Repetition is WeeklyEventRepetition)
                    {
                        while (repetitions.Count > (rep.NumberOfRepetitions + 1))
                        {
                            repetitions.RemoveAt(repetitions.FindIndex(e => e.Start == repetitions.Max(r => r.Start)));
                        }
                    }
                    else
                    {
                        while (repetitions.Count > rep.NumberOfRepetitions)
                        {
                            repetitions.RemoveAt(repetitions.FindIndex(e => e.Start == repetitions.Max(r => r.Start)));
                        }
                    }
                }

                //Apply the modifications
                foreach (CalendarEventDto mod in parameters.CalendarEvents)
                {
                    if (mod.Repetition is MonthlyEventRepetition monthrep)
                    {
                        //repetitions.Add(new CalendarEventTimeRange(currentDate, currentDate.AddMinutes(mrep.Duration), baseEvent));
                        DateTime repStart = monthrep.StartDate.Date.AddSeconds(monthrep.StartTime.TotalSeconds);
                        repStart = repStart.AddDays(monthrep.OnDayNumber - repStart.Day);
                        DateTime originalEventDate = CalendarAggregationUtils.FromUnixTime(mod.ReplacedEventOccurrenceTS); //todo:puede ser nulo
                        int idx = repetitions.FindIndex(e => e.Start == originalEventDate);

                        if (idx >= 0)
                        {
                            repetitions.RemoveAt(idx);
                            //its a modification of an occurrence and it's inside the time range
                            if (!mod.DeletedOccurrence && repStart < parameters.End && repStart.AddMinutes(monthrep.Duration) > parameters.Start)
                            {
                                //add the moddified occurrence
                                repetitions.Insert(idx, new CalendarEventTimeRange(repStart, repStart.AddMinutes(monthrep.Duration), parameters.CalendarEvent));
                            }

                            while (true)
                            {
                                //increase the month
                                repStart = repStart.AddMonths(monthrep.RepetitionPeriod);
                                //set the apropriate day
                                repStart = repStart.AddDays(monthrep.OnDayNumber - repStart.Day);
                                if (repStart > parameters.End)
                                {
                                    break;
                                }
                                if (!mod.DeletedOccurrence && repStart < parameters.End && repStart.AddMinutes(monthrep.Duration) > parameters.Start)
                                {
                                    //add the moddified occurrence
                                    repetitions.Insert(idx, new CalendarEventTimeRange(repStart, repStart.AddMinutes(monthrep.Duration), parameters.CalendarEvent));
                                }
                            }
                        }
                        else
                        {
                            //its a modification of an occurrence and it's inside the time range
                            if (!mod.DeletedOccurrence && repStart < parameters.End && repStart.AddMinutes(monthrep.Duration) > parameters.Start)
                            {
                                //add the moddified occurrence
                                repetitions.Add(new CalendarEventTimeRange(repStart, repStart.AddMinutes(monthrep.Duration), parameters.CalendarEvent));
                            }

                            while (true)
                            {
                                //increase the month
                                repStart = repStart.AddMonths(monthrep.RepetitionPeriod);
                                //set the apropriate day
                                repStart = repStart.AddDays(monthrep.OnDayNumber - repStart.Day);
                                if (repStart > parameters.End)
                                {
                                    break;
                                }
                                if (!mod.DeletedOccurrence && repStart < parameters.End && repStart.AddMinutes(monthrep.Duration) > parameters.Start)
                                {
                                    //add the moddified occurrence
                                    repetitions.Add(new CalendarEventTimeRange(repStart, repStart.AddMinutes(monthrep.Duration), parameters.CalendarEvent));
                                }
                            }
                        }
                    }
                    else if (mod.Repetition is NoRepetition)
                    {
                        NoRepetition r = (NoRepetition)mod.Repetition;
                        //Get the date time of the original event
                        DateTime originalEventDate = CalendarAggregationUtils.FromUnixTime(mod.ReplacedEventOccurrenceTS);
                        int idx = repetitions.FindIndex(e => e.Start == originalEventDate);
                        if (idx >= 0)
                        {
                            repetitions.RemoveAt(idx);
                            //its a modification of an occurrence and it's inside the time range
                            if (!mod.DeletedOccurrence && r.StartDate < parameters.End && r.EndDate > parameters.Start)
                            {
                                //add the moddified occurrence
                                repetitions.Insert(idx, new CalendarEventTimeRange(r.StartDate, r.EndDate, parameters.CalendarEvent));
                            }
                        }
                    }
                }
            }
            return repetitions;
        }
    }
}
