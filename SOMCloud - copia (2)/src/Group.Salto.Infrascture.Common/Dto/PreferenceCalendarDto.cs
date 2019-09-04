using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Dto
{
    public class PreferenceCalendarDto
    {
        public Calendars Calendar { get; set; }
        public int Preference { get; set; }
    }
}