using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.CalendarEvent
{
    public class CalendarEventRequestViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public int Calendar_id { get; set; }
        public int Event_category_id { get; set; }
        public string _Start_date { get; set; }
        public string _End_date { get; set; }
        public string Color { get; set; }
        public string Cost_hour { get; set; }
        public string Event_length { get; set; }
        public string Rec_type { get; set; }
        public string Rec_pattern { get; set; }
        public string Is_occurrence_deletion { get; set; }
        public int Event_pid { get; set; }
    }
}