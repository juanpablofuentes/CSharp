using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.SOM.Web.Models.ExpenseType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExpenseTypeFilterViewModelExtensions
    {
        public static ExpenseTypeFilterDto ToDto(this ExpenseTypeFilterViewModel source)
        {
            ExpenseTypeFilterDto result = null;
            if (source != null)
            {
                result = new ExpenseTypeFilterDto()
                {
                    Type = source.Type,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }
    }
}