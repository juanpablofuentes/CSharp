using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Flows;
using Group.Salto.SOM.Web.Models.Flows;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Task;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class FlowsFilterViewModelExtensions
    {
        public static FlowsFilterDto ToFilterDto(this FlowsFilterViewModel source)
        {
            FlowsFilterDto result = null;
            if (source != null)
            {
                result = new FlowsFilterDto()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }

        public static FlowsViewModel ToViewModel(this FlowsListDto source)
        {
            FlowsViewModel result = null;
            if (source != null)
            {
                result = new FlowsViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this FlowsListDto source, FlowsViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
                target.Published = source.Published;
            }
        }

        public static IList<FlowsViewModel> ToListViewModel(this IList<FlowsListDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static ResultViewModel<FlowsViewModel> ToViewModel(this ResultDto<FlowsDto> source)
        {
            var response = source != null ? new ResultViewModel<FlowsViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static FlowsViewModel ToViewModel(this FlowsDto source)
        {
            FlowsViewModel result = null;
            if (source != null)
            {
                result = new FlowsViewModel()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Published = source.Published
                };
            }

            return result;
        }

        public static FlowsDto ToDto(this FlowsViewModel source)
        {
            FlowsDto result = null;
            if (source != null)
            {
                result = new FlowsDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description, 
                    Published = source.Published
                };
            }

            return result;
        }

        public static ResultViewModel<FlowsDetailViewModel> ToDetailViewModel(this ResultDto<FlowsViewModel> source)
        {
            var response = source != null ? new ResultViewModel<FlowsDetailViewModel>()
            {
                Data = source.Data.ToDetailViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static FlowsDetailViewModel ToDetailViewModel(this FlowsViewModel source)
        {
            FlowsDetailViewModel result = null;
            if (source != null)
            {
                result = new FlowsDetailViewModel()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Published = source.Published
                };
            }

            return result;
        }

        public static FlowsDetailViewModel ToViewModel(this FlowsWithTasksDictionaryDto source)
        {
            FlowsDetailViewModel result = null;
            if (source != null)
            {
                result = new FlowsDetailViewModel()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Published = source.Published,
                    TasksList = new TasksListViewModel()
                    {
                        TasksListDictionary = source.FlowTasksDictionary.ToList()
                    }
                };

            }
            return result;
        }
        public static ResultViewModel<FlowsDetailViewModel> ToDetailViewModel(this ResultDto<FlowsWithTasksDictionaryDto> source)
        {
            var response = source != null ? new ResultViewModel<FlowsDetailViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }
    }
}