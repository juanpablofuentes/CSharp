using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.SOM.Web.Models.ExpenseType;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExpenseTypeViewModelExtensions
    {
        public static ResultViewModel<ExpenseTypeViewModel> ToViewModel(this ResultDto<ExpenseTypeDto> source)
        {
            var response = source != null ? new ResultViewModel<ExpenseTypeViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ExpenseTypeViewModel ToViewModel(this ExpenseTypeDto source)
        {
            ExpenseTypeViewModel result = null;
            if (source != null)
            {
                result = new ExpenseTypeViewModel()
                {
                    Id = source.Id,
                    Type = source.Type,
                    Unit = source.Unit
                };
            }
            return result;
        }

        public static IList<ExpenseTypeViewModel> ToViewModel(this IList<ExpenseTypeDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static ExpenseTypeDto ToDto(this ExpenseTypeViewModel source)
        {
            ExpenseTypeDto result = null;
            if (source != null)
            {
                result = new ExpenseTypeDto()
                {
                    Id = source.Id,
                    Type = source.Type,
                    Unit = source.Unit
                };
            }
            return result;
        }
    }
}