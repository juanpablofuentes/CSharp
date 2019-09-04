using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ExpenseDtoExtensions
    {
        public static ExpenseDto ToDto(this Expenses dbModel)
        {
            var dto = new ExpenseDto
            {
                Id = dbModel.Id,
                ExpenseTypeId = dbModel.ExpenseTypeId,
                PaymentMethodId = dbModel.PaymentMethodId,
                Description = dbModel.Description,
                Amount = dbModel.Amount,
                Date = dbModel.Date.ToLocalTime().ToShortDateString(),
                Factor = dbModel.Factor,
                UpdateDate = dbModel.UpdateDate
            };

            return dto;
        }

        public static IEnumerable<ExpenseDto> ToDto(this IEnumerable<Expenses> dbModelList)
        {
            var dtoList = new List<ExpenseDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }

        public static Expenses ToEntity(this ExpenseDto source)
        {
            Expenses result = null;
            if (source != null)
            {
                result = new Expenses()
                {
                    Amount = source.Amount,
                    Factor = source.Factor,
                    Description = source.Description,
                    ExpenseTicketId = source.ExpenseTicketId,
                    ExpenseTypeId = source.ExpenseTypeId,
                    PaymentMethodId = source.PaymentMethodId,
                    Date = DateTime.Parse(source.Date)
                };
            }
            return result;
        }
    }
}