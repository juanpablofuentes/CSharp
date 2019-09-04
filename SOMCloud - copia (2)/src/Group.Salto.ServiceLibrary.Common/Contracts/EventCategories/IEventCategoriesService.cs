using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.EventCategories;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.EventCategories
{
    public interface IEventCategoriesService
    {
        ResultDto<IList<EventCategoriesDto>> GetAll();
        ResultDto<EventCategoriesDto> GetById(int id);
        ResultDto<IList<EventCategoriesDto>> GetAllFiltered(EventCategoriesFilterDto filter);
        ResultDto<EventCategoriesDto> CreateEventCategories(EventCategoriesDto source);
        ResultDto<EventCategoriesDto> UpdateEventCategories(EventCategoriesDto source);
        ResultDto<bool> DeleteEventCategories(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValuesNotDeleted();
    }
}