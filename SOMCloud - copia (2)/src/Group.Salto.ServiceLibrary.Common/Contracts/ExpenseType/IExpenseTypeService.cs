using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ExpenseType
{
    public interface IExpenseTypeService
    {
        ResultDto<ExpenseTypeDto> GetById(int id);
        ResultDto<IList<ExpenseTypeDto>> GetAllFiltered(ExpenseTypeFilterDto filter);
        ResultDto<ExpenseTypeDto> UpdateExpenseType(ExpenseTypeDto source);
        ResultDto<bool> DeleteExpenseType(int id);
        ResultDto<ExpenseTypeDto> CreateExpenseType(ExpenseTypeDto source);
    }
}