using Group.Salto.Common.Constants;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Expense;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.ExpenseTicket
{
    public class ExpenseTicketViewModel
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Date { get; set; }
        public int? WorkOrderId { get; set; }
        public Guid? ExpenseTicketStatusId { get; set; }
        public string Status { get; set; }
        public string NamePeople { get; set; }
        public string ValidationDate { get; set; }
        public string ValidationObservations { get; set; }
        public string PaymentInformation { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public IEnumerable<ExpenseDto> TicketExpenses { get; set; }       

    }
}