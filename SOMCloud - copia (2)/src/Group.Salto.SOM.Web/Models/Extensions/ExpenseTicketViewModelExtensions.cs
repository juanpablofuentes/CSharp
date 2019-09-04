using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.SOM.Web.Models.ExpenseTicket;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExpenseTicketViewModelExtensions
    {
        public static ResultViewModel<ExpenseTicketViewModel> ToViewModel(this ResultDto<ExpenseTicketDto> source)
        {
            var response = source != null ? new ResultViewModel<ExpenseTicketViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ExpenseTicketViewModel ToViewModel(this ExpenseTicketDto source)
        {
            ExpenseTicketViewModel result = null;

            if (source != null)
            {
                result = new ExpenseTicketViewModel()
                {
                    Id = source.Id,
                    UpdateDate = source.UpdateDate,
                    Date = source.Date,
                    WorkOrderId = source.WorkOrderId,
                    Status = source.Status,
                    NamePeople = source.People?.Name + " " + source.People?.Surname + " " + source.People?.SecondSurname,
                    ValidationDate = source.ValidationDate,
                    ValidationObservations = source.ValidationObservations,
                    PaymentInformation = source.PaymentInformation,
                    TicketExpenses = source.TicketExpenses,
                    ExpenseTicketStatusId = source.ExpenseTicketStatusId,
                    Amount = Decimal.Round(source.TicketExpenses.FirstOrDefault().Amount,2)
                };
            }
            return result;
        }

        public static IList<ExpenseTicketViewModel> ToViewModel(this IList<ExpenseTicketDto> source)
        {                       
            var temp = source?.MapList(x => x.ToViewModel());           
            return temp;
        }
    }
}