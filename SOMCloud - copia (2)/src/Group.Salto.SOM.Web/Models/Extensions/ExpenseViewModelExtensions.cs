using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Expense;
using Group.Salto.SOM.Web.Models.Expense;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExpenseViewModelExtensions
    {
        public static ResultViewModel<ExpenseViewModel> ToViewModel(this ResultDto<ExpenseDto> source)
        {
            var response = source != null ? new ResultViewModel<ExpenseViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ExpenseViewModel ToViewModel(this ExpenseDto source)
        {
            ExpenseViewModel result = null;
            if (source != null)
            {
                result = new ExpenseViewModel()
                {
                    Id = source.Id,
                    Description = source.Description,
                    Amount = ((decimal?)source.Amount)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    Date = DateTime.Parse(source.Date),
                    Factor = source.Factor,
                    ExpenseTypeId = source.ExpenseTypeId,
                    ExpenseType = source.ExpenseType,
                    PaymentMethodId = source.PaymentMethodId,
                    PaymentMethod = source.PaymentMethod,
                    ExpenseTicketId = source.ExpenseTicketId
                };
            }
            return result;
        }

        public static IList<ExpenseViewModel> ToViewModel(this IList<ExpenseDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static ExpenseDto ToDto(this ExpenseViewModel source)
        {
            ExpenseDto result = null;
            if (source != null)
            {
                result = new ExpenseDto()
                {
                    Id = source.Id,
                    Description = source.Description,
                    Amount = source.Amount.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    Date = source.Date.ToLocalTime().ToShortDateString(),
                    Factor = source.Factor,
                    ExpenseTypeId = source.ExpenseTypeId,
                    ExpenseType = source.ExpenseType,
                    PaymentMethodId = source.PaymentMethodId,
                    PaymentMethod = source.PaymentMethod,
                    ExpenseTicketId = source.ExpenseTicketId
                };
            }
            return result;
        }       
    }
}