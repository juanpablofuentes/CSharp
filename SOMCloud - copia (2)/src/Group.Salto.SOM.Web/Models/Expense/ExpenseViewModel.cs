using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Expense
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }
        public int ExpenseTypeId { get; set; }
        public string ExpenseType { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public int ExpenseTicketId { get; set; }
        public double Factor { get; set; }
        public string PaymentInformation { get; set; }
        public string Observations { get; set; }
    }
}