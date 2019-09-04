using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.EventCategories;
using Group.Salto.SOM.Web.Models.EventCategories;
using Group.Salto.SOM.Web.Models.EventCategoriesViewModel;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class EventCategoriesDetailViewModelExtension
    {
        public static ResultViewModel<EventCategoriesDetailViewModel> ToDetailViewModel(this ResultDto<EventCategoriesDto> source)
        {
            var response = source != null ? new ResultViewModel<EventCategoriesDetailViewModel>()
            {
                Data = source.Data.ToEditViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static EventCategoriesDetailViewModel ToEditViewModel(this EventCategoriesDto source)
        {
            EventCategoriesDetailViewModel eventCategories = null;
            if (source != null)
            {
                eventCategories = new EventCategoriesDetailViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    CategoriId = source.AvailabilityCategoriesId
                };
            }
            return eventCategories;
        }

        public static EventCategoriesDto ToDto(this EventCategoriesDetailViewModel source)
        {
            EventCategoriesDto result = null;
            if (source != null)
            {
                result = new EventCategoriesDto()
                {
                    Id = source.Id ?? 0,
                    Name = source.Name,
                    Description = source.Description,
                    AvailabilityCategoriesId = source.CategoriId
                };
            }
            return result;
        }
    }
}