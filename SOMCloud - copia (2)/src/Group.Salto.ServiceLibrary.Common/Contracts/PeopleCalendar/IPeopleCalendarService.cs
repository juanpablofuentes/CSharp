using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCalendar;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.PeopleCalendar
{
    public interface IPeopleCalendarService
    {
        ResultDto<IList<CalendarDto>> GetByPeopleIdNotDeleted(int peopleId);
        ResultDto<PeopleCalendarDto> Create(PeopleCalendarDto model);
        ResultDto<PeopleCalendarDto> Update(PeopleCalendarDto model);
        ResultDto<bool> Delete(PeopleCalendarDto model);
    }
}