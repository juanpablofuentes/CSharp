using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ExpenseTicketStatusExtensions
    {
        public static ExpenseStatusDto ToDto(this ExpenseTicketStatus dbModel)
        {
            var dto = new ExpenseStatusDto
            {
                Id = dbModel.Id,
                Status = dbModel.Description
            };

            return dto;
        }

        public static IEnumerable<ExpenseStatusDto> ToDto(this IEnumerable<ExpenseTicketStatus> dbModelList)
        {
            var dtoList = new List<ExpenseStatusDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }
    }
}
