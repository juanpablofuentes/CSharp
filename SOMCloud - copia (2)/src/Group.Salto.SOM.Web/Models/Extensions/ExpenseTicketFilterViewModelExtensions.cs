using Group.Salto.ServiceLibrary.Common.Dtos.ExpenseTicket;
using Group.Salto.SOM.Web.Models.ExpenseTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExpenseTicketFilterViewModelExtensions
    {
        public static ExpenseTicketFilterDto ToDto(this ExpenseTicketFilterViewModel source)
        {
            ExpenseTicketFilterDto result = null;
            if (source != null)
            {
                result = new ExpenseTicketFilterDto()
                {
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                    FinalDate = source.FinalDate,
                    InitialDate= source.InitialDate,
                    NamePeople = source.NamePeople.ToPeopleExpenseTicketDto(),
                    States = source.States.ToStatesExpenseTicketDto(),
                    Page = source.Page,
                    Size = source.Size
                };
            }
            return result;
        }
    }
}