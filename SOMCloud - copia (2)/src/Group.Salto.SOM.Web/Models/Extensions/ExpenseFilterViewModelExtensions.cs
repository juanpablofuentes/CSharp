using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.SOM.Web.Models.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExpenseFilterViewModelExtensions
    {
        public static ExpenseFilterDto ToDto(this ExpenseFilterViewModel source)
        {
            ExpenseFilterDto result = null;
            if (source != null)
            {
                result = new ExpenseFilterDto()
                {
                    Description = source.Description,
                    Amount = source.Amount,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }
    }
}