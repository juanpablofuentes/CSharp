using Group.Salto.Common;
using Group.Salto.Common.Constants.EventCategories;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.EventCategories;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.EventCategories;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.EventCategories
{
    public class EventCategoriesService : BaseService, IEventCategoriesService
    {
        private readonly IEventCategoriesRepository _eventCategoriesRepository;

        public EventCategoriesService(ILoggingService logginingService,
            IEventCategoriesRepository eventCategoriesRepository) : base(logginingService)
        {
            _eventCategoriesRepository = eventCategoriesRepository ?? throw new ArgumentNullException($"{nameof(IEventCategoriesRepository)} is null ");
        }

        public ResultDto<IList<EventCategoriesDto>> GetAll()
        {
            LogginingService.LogInfo($"Get All Event Categories");
            var data = _eventCategoriesRepository.GetAllNotDeleted().ToList();
            return ProcessResult(data.ToDto());
        }

        public ResultDto<EventCategoriesDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id Event Categories");
            var data = _eventCategoriesRepository.GetByIdNotDeleted(id);
            return ProcessResult(data.ToListDto());
        }

        public ResultDto<EventCategoriesDto> CreateEventCategories(EventCategoriesDto source)
        {
            LogginingService.LogInfo($"Creat Event Categories");

            var newCategory = source.ToEntity();
            var result = _eventCategoriesRepository.CreateEventCategories(newCategory);

            return ProcessResult(result.Entity?.ToListDto(), result);
        }

        public ResultDto<bool> DeleteEventCategories(int id)
        {
            LogginingService.LogInfo($"Delete Event Categories");

            List<ErrorDto> errors = new List<ErrorDto>();
            bool deleteResult = false;
            var categoryToDelete = _eventCategoriesRepository.GetByIdNotDeleted(id);

            if (categoryToDelete != null)
            {
                deleteResult = _eventCategoriesRepository.DeleteEventCategories(categoryToDelete);
            }
            else
            {
                errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = EventCategoriesConstants.EventCategoryNotExistMessage });
            }

            return ProcessResult(deleteResult, errors);
        }

        public ResultDto<EventCategoriesDto> UpdateEventCategories(EventCategoriesDto source)
        {
            LogginingService.LogInfo($"Update Event Categories");

            ResultDto<EventCategoriesDto> result = null;
            var findCategory = _eventCategoriesRepository.GetByIdNotDeleted(source.Id);

            if (findCategory != null)
            {
                var updatedKnowledge = findCategory.Update(source);
                var resultRepository = _eventCategoriesRepository.UpdateEventCategories(updatedKnowledge);
                result = ProcessResult(resultRepository.Entity?.ToListDto(), resultRepository);
            }

            return result ?? new ResultDto<EventCategoriesDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<IList<EventCategoriesDto>> GetAllFiltered(EventCategoriesFilterDto filter)
        {
            LogginingService.LogInfo($"Get All Event Categories Filtered");

            var query = _eventCategoriesRepository.GetAllNotDeleted();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Name.Contains(filter.Description));
            query = OrderBy(query, filter);

            return ProcessResult(query.ToList().MapList(c => c.ToListDto()));
        }

        private IQueryable<CalendarEventCategories> OrderBy(IQueryable<CalendarEventCategories> query, EventCategoriesFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Event Categories");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValuesNotDeleted()
        {
            LogginingService.LogInfo($"Get Event Categories Key Value");
            var data = _eventCategoriesRepository.GetAllKeyValuesNotDeleted();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}