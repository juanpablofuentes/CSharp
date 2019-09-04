using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Expense;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ExpenseTicketDtoExtensions
    {
        public static ExpenseTicketDto ToDto(this ExpensesTickets dbModel)
        {
            var dto = new ExpenseTicketDto
            {
                Id = dbModel.Id,
                UpdateDate = dbModel.UpdateDate,
                Date = dbModel.Date.ToLocalTime().ToShortDateString(),
                WorkOrderId = dbModel.WorkOrderId,
                Status = dbModel.Status,
                People = dbModel.People.ToDto(),
                PeopleValidator = dbModel.PeopleValidator.ToDto(),
                ValidationDate = dbModel.ValidationDate?.ToLocalTime().ToShortDateString(),
                ValidationObservations = dbModel.ValidationObservations,
                PaymentInformation = dbModel.PaymentInformation,
                TicketExpenses = dbModel.Expenses.ToDto(),
                ExpenseTicketStatusId = dbModel.ExpenseTicketStatusId
            };

            return dto;
        }

        public static IEnumerable<ExpenseTicketDto> ToDto(this IEnumerable<ExpensesTickets> dbModelList)
        {
            var dtoList = new List<ExpenseTicketDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }

        public static ExpenseTicketExtDto ToExtDto(this ExpensesTickets dbModel)
        {
            var dto = new ExpenseTicketExtDto
            {
                Id = dbModel.Id,
                UpdateDate = dbModel.UpdateDate,
                Date = dbModel.Date,
                WorkOrderId = dbModel.WorkOrderId,
                Status = dbModel.ExpenseTicketStatusId,
                StatusExpense = dbModel.Status,
                PeopleValidator = dbModel.PeopleValidator.ToDto(),
                ValidationDate = dbModel.ValidationDate,
                ValidationObservations = dbModel.ValidationObservations,
                PaymentInformation = dbModel.PaymentInformation,
            };

            var expense = dbModel.Expenses?.FirstOrDefault();
            if (expense != null)
            {
                dto.ExpenseId = expense.Id;
                dto.ExpenseTypeId = expense.ExpenseTypeId;
                dto.PaymentMethodId = expense.PaymentMethodId;
                dto.Description = expense.Description;
                dto.Amount = expense.Amount;
                dto.ExpenseDate = expense.Date;
                dto.Factor = expense.Factor;
                dto.ExpenseUpdateDate = expense.UpdateDate;
            }

            return dto;
        }

        public static IEnumerable<ExpenseTicketExtDto> ToExtDto(this IEnumerable<ExpensesTickets> dbModelList)
        {
            var dtoList = new List<ExpenseTicketExtDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToExtDto());
            }

            return dtoList;
        }

        public static ExpensesTickets Update(this ExpensesTickets target, ExpenseTicketExtDto source)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.UpdateDate = source.UpdateDate;
                target.Date = source.Date;
                target.PaymentInformation = source.PaymentInformation;
                target.WorkOrderId = source.WorkOrderId;               
                target.ValidationDate = source.ValidationDate;                
                target.ValidationObservations = source.ValidationObservations;
                target.ExpenseTicketStatusId = source.Status;
                target.Status = source.StatusExpense;
                if (source.PeopleValidator != null)
                {
                    target.PeopleValidatorId = source.PeopleValidator.Id;
                }
                var expenseUpdate = target.Expenses.FirstOrDefault();
                if (expenseUpdate != null)
                {
                    expenseUpdate.PaymentMethodId = source.PaymentMethodId;
                    expenseUpdate.ExpenseTypeId = source.ExpenseTypeId;
                    expenseUpdate.Amount = source.Amount;
                    expenseUpdate.Description = source.Description;
                    expenseUpdate.Date = source.Date;
                }
            }
            return target;
        }
    }
}