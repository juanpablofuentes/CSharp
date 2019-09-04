using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents;
using Group.Salto.SOM.Web.Models.CalendarEvent;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Common.Enums;
using Group.Salto.Common.Constants.CalendarEvent;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CalendarEventDetailViewModelExtensions
    {
        public static CalendarEventResponse ToViewModel(this CalendarEventDto source)
        {
            CalendarEventRequestViewModel result = null;
            if (source != null)
            {
                result = new CalendarEventRequestViewModel();
                result.Id = source.Id;
                result.Color = source.Color;
                result.Text = source.Name;
                result.Description = source.Description;
                result.Event_pid = source.ParentEventId;
                result.Cost_hour = ((decimal?)source.CostHour)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName);
                result.Calendar_id = source.CalendarId;
                result.Event_category_id = source.CategoryId;

                string recType = string.Empty;
                if (source.Repetition is EventRepetition)
                {
                    EventRepetition eventRepetition = ((EventRepetition)source.Repetition);
                    result._End_date = (eventRepetition.EndDate != null && eventRepetition.EndDate != DateTime.MinValue) ? eventRepetition.EndDate.ToString(CalendarEventConstants.CalendarFormatDate) : null;

                    result.Start_date = ((EventRepetition)source.Repetition).StartDate;
                    result.Start_date = result.Start_date.AddHours(result.Start_date.Hour * -1);
                    result.Start_date = result.Start_date.AddMinutes(result.Start_date.Minute * -1);
                    result.Start_date = result.Start_date.AddSeconds(result.Start_date.Second * -1);
                    result.Start_date = result.Start_date.AddSeconds(eventRepetition.StartTime.TotalSeconds);
                    result.End_date = (eventRepetition.EndDate != null && eventRepetition.EndDate != DateTime.MinValue) ? eventRepetition.EndDate : DateTime.MaxValue;
                    result._Start_date = result.Start_date.ToString(CalendarEventConstants.CalendarFormatDate);
                    result.Event_length = (eventRepetition.Duration * 60).ToString();

                    if (source.Repetition is DailyEventRepetition)
                    {
                        DailyEventRepetition repetition = (DailyEventRepetition)source.Repetition;
                        recType = ConvertToRecType("day", repetition.RepetitionPeriod.ToString(), extra: eventRepetition.HasEnd ? (repetition.NumberOfRepetitions > 0 ? repetition.NumberOfRepetitions.ToString() : "") : "no");
                    }
                    else if (source.Repetition is WeeklyEventRepetition)
                    {
                        WeeklyEventRepetition repetition = (WeeklyEventRepetition)source.Repetition;
                        List<int> days = new List<int>();
                        for (int i = 0; i < repetition.OnWeekDays.Length; i++)
                        {
                            if (repetition.OnWeekDays[i])
                            {
                                days.Add(i);
                            }
                        }

                        recType = ConvertToRecType("week", repetition.RepetitionPeriod.ToString(), days: string.Join(",", days), extra: eventRepetition.HasEnd ? (repetition.NumberOfRepetitions > 0 ? repetition.NumberOfRepetitions.ToString() : "") : "no");
                    }
                    else if (source.Repetition is MonthlyEventRepetition)
                    {
                        MonthlyEventRepetition repetition = (MonthlyEventRepetition)source.Repetition;
                        recType = ConvertToRecType("month", repetition.RepetitionPeriod.ToString(), extra: eventRepetition.HasEnd ? (repetition.NumberOfRepetitions > 0 ? repetition.NumberOfRepetitions.ToString() : "") : "no");
                    }
                    else if (source.Repetition is YearlyEventRepetition)
                    {
                        YearlyEventRepetition repetition = (YearlyEventRepetition)source.Repetition;
                        recType = ConvertToRecType("year", repetition.RepetitionPeriod.ToString(), extra: eventRepetition.HasEnd ? (repetition.NumberOfRepetitions > 0 ? repetition.NumberOfRepetitions.ToString() : "") : "no");
                    }
                }

                if (source.Repetition is NoRepetition)
                {
                    result.Start_date = ((NoRepetition)source.Repetition).StartDate;
                    result.End_date = ((NoRepetition)source.Repetition).EndDate;

                    result.Event_length = ((int)result.End_date.Subtract(result.Start_date).TotalSeconds).ToString();
                    if (source.ReplacedEventOccurrenceTS != long.MinValue)
                    {
                        result.Event_pid = source.ParentEventId;
                        result.Event_length = source.ReplacedEventOccurrenceTS.ToString();
                    }
                    recType += string.Empty;
                }
                result.Rec_type = recType;
            }

            return new CalendarEventResponse
             {
                 Id = result.Id.ToString(),
                 Text = result.Text,
                 Start_date = result.Start_date.ToString(CalendarEventConstants.CalendarFormatDate),
                 End_date = result.End_date.ToString(CalendarEventConstants.CalendarFormatDate),
                 Calendar_id = result.Calendar_id,
                 Event_category_id = result.Event_category_id,
                 Color = result.Color,
                 Description = result.Description,
                 Cost_hour = result.Cost_hour,
                 Event_length = result.Event_length,
                 Rec_type = result.Rec_type,
                 Is_occurrence_deletion = result.Is_occurrence_deletion,
                 Event_pid = result.Event_pid
             };
        }

        public static IList<CalendarEventResponse> ToViewModel(this IList<CalendarEventDto> source)
        {
            if (source == null) return null;
            return source.Select(e => ToViewModel(e)).ToList();
        }

        public static CalendarEventDto ToDto(this CalendarEventRequestViewModel source)
        {
            CalendarEventDto result = null;
            if (source != null)
            {
                result = new CalendarEventDto();
                result.Id = source.Id;
                result.ParentEventId = source.Event_pid;
                result.Color = source.Color;
                result.Name = source.Text;
                result.Description = source.Description;
                result.CalendarId = source.Calendar_id;
                result.CategoryId = source.Event_category_id;
                result.CostHour = (double?)source.Cost_hour.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName);

                if (string.IsNullOrEmpty(source.Rec_type))
                {
                    result.Repetition = new NoRepetition
                    {
                        StartDate = source.Start_date,
                        EndDate = source.End_date
                    };

                    if (!string.IsNullOrEmpty(source.Is_occurrence_deletion) && source.Is_occurrence_deletion.ToLower() == "true")
                    {
                        result.DeletedOccurrence = true;
                    }
                }
                else
                {
                    string rec_type = source.Rec_type.Split('_')[0];
                    string[] tokenizedEvRep = source.Rec_type.Split('_');
                    bool hasEnd = source.Rec_type.Split('#').Length == 1 || source.Rec_type.Split('#')[1] != "no";
                    int aux = 0;
                    int numRepetitions = (source.Rec_type.Split('#').Length > 1 && source.Rec_type.Split('#')[1] != "no" && int.TryParse(source.Rec_type.Split('#')[1], out aux))
                        ? int.Parse(source.Rec_type.Split('#')[1])
                        : int.MinValue;

                    DateTime repetitionStartDate = source._Start_date != null ? DateTime.Parse(source._Start_date) : source.Start_date;
                    DateTime repetitionEndDate = DateTime.MinValue;

                    if (hasEnd)
                    {
                        repetitionEndDate = source._End_date != null ? DateTime.Parse(source._End_date) : DateTime.MinValue;
                    }
                    int duration = (int)source.End_date.Subtract(source.Start_date).TotalMinutes;
                    switch (rec_type)
                    {
                        case "day":
                            {
                                result.Repetition = new DailyEventRepetition();
                                break;
                            }
                        case "week":
                            {
                                string daysString = tokenizedEvRep[4].Split('#')[0];
                                bool[] days = new bool[] { false, false, false, false, false, false, false };
                                foreach (string dayNum in daysString.Split(','))
                                {
                                    days[(int.Parse(dayNum))] = true;
                                }
                                result.Repetition = new WeeklyEventRepetition
                                {
                                    OnWeekDays = days
                                };
                                break;
                            }
                        case "month":
                            {
                                result.Repetition = new MonthlyEventRepetition { OnDayNumber = repetitionStartDate.Day };
                                break;
                            }
                        case "year":
                            {
                                result.Repetition = new YearlyEventRepetition
                                {
                                    OnMonthNumber = repetitionStartDate.Month,
                                    OnDayNumber = repetitionStartDate.Day
                                };
                                break;
                            }
                    }
                    EventRepetition er = (EventRepetition)result.Repetition;
                    er.Duration = duration;
                    er.RepetitionPeriod = int.Parse(tokenizedEvRep[1]);
                    er.StartDate = repetitionStartDate;
                    er.StartTime = repetitionStartDate.TimeOfDay;
                    er.EndDate = repetitionEndDate;
                    er.HasEnd = hasEnd;
                    er.NumberOfRepetitions = numRepetitions;
                }
                
            }
            return result;
        }

        private static string ConvertToRecType(string type = "", string count = "", string day = "", string count2 = "", string days = "", string extra = "")
        {
            string[] repArray = new string[5];
            repArray[0] = type;
            repArray[1] = count;
            repArray[2] = day;
            repArray[3] = count2;
            repArray[4] = days;

            string rep = string.Join("_", repArray);
            if (extra == null) extra = string.Empty;
            {
                rep += "#" + extra;
            }

            return rep;
        }
    }
}