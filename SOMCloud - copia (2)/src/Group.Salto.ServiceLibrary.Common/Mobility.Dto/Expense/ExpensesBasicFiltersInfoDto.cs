using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Expense
{
    public class ExpensesBasicFiltersInfoDto
    {
        public IEnumerable<ExpenseTypeDto> ExpenseTypes { get; set; }
        public IEnumerable<PaymentMethodDto> PaymentTypes { get; set; }
        public IEnumerable<ExpenseStatusDto> ExpenseStatus { get; set; }
    }
}
