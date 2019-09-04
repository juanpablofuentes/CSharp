using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Expense;
using Group.Salto.ServiceLibrary.Common.Contracts.ExpenseTicket;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Expense
{
    public class ExpenseQueryResult : IExpenseQueryResult
    {
        private IExpenseTicketRepository _expenseticketRepository;

        public ExpenseQueryResult(IExpenseTicketRepository expenseticketRepository)
        {
            _expenseticketRepository = expenseticketRepository ?? throw new ArgumentNullException($"{nameof(IExpenseTicketRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            Int32.TryParse(queryTypeParameters.Value, out int companyId);
            var data = _expenseticketRepository.GetAllPeopleKeyValues();
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}