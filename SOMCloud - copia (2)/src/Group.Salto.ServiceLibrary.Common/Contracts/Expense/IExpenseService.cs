using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.ServiceLibrary.Common.Dtos.ExpenseTicket;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Expense;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Expense
{
    public interface IExpenseService
    {
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        IList<BaseNameIdDto<Guid>> GetExpenseTicketStatus();
        ExpensesBasicFiltersInfoDto GetBasicFiltersInfo();
        ResultDto<IList<ExpenseTicketDto>> GetAllFiltered(ExpenseTicketFilterDto filter);
        ResultDto<IList<ExpenseTicketExtDto>> GetAllExpenseFiltered(ExpenseTicketFilterDto filter);
        IEnumerable<ExpenseTicketExtDto> GetExpensesFromAppUser(int peopleId);
        ResultDto<int> AddExpense(ExpenseTicketExtDto expenseTicketExtDto, int peopleIdInt);
        ResultDto<ExpenseTicketExtDto> CreateExpense(ExpenseTicketExtDto expenseTicketExtDto, int peopleIdInt);
        ResultDto<ExpenseTicketExtDto> UpdateExpense(ExpenseTicketExtDto source);
        ResultDto<bool> DeleteExpense(int id);
        int CountId(ExpenseTicketFilterDto filter);
        ResultDto<ExpenseTicketExtDto> ValidateExpense(ExpenseTicketExtDto expenseData, Guid? nextStatus);
        FileContentDto GetFileFromExpense(int id);
        ResultDto<bool> AddFileToExpense(RequestFileDto requestFileDto);
        IList<ExpenseTicketDto> CalculateAmount(IList<ExpenseTicketDto> source);
        Dictionary<Guid, string> GetDefaultStates();
        ResultDto<ExpenseTicketExtDto> GetByIdWithExpenseAndFile(int Id);
        IList<BaseNameIdDto<int>> GetPaymentMethodKeyValues();
        IList<BaseNameIdDto<int>> GetExpenseTypeKeyValues();
    }
}