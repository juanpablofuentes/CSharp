using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.ExpenseTicket;
using Group.Salto.Common.Enums;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group.Salto.SOM.Web.Models.Expense
{
    public class ExpenseDetailsViewModel : ExpenseViewModel

    {
        public IEnumerable<SelectListItem> ExpenseTypesList { get; set; }
        public IEnumerable<SelectListItem> PaymentMethodsList { get; set; }
        public IFormFile UploadTicket { get; set; }
        public RequestFileDto FileLoaded { get; set; }
        public bool FileExists { get; set; }
        public ModeActionTypeEnum ModeActionType { get; set; }
        public string ExpenseStatus { get; set; }
        public Guid? ExpenseStatusId { get; set; }
        public string NextStatus { get; set; }
        public string PeopleValidator { get; set; }
        public IList<BaseNameIdDto<Guid>> StatusGuids { get; set; }
        public DateTime? ValidationDate { get; set; }
        public bool ValidateExpenseTicketPending()
        {
            return ExpenseStatusId == StatusGuids.FirstOrDefault(x => x.Name == ExpenseTicketConstants.ExpenseTicketPending).Id;
        }
        public bool ValidateExpenseTicketAccepted()
        {
            return ExpenseStatusId == StatusGuids.FirstOrDefault(x => x.Name == ExpenseTicketConstants.ExpenseTicketAccepted).Id;
        }
        public bool ValidateExpenseTicketFinished()
        {
            return ExpenseStatusId == StatusGuids.FirstOrDefault(x => x.Name == ExpenseTicketConstants.ExpenseTicketFinished).Id;
        }
        public bool ValidateExpenseTicketPaid()
        {
            return ExpenseStatusId == StatusGuids.FirstOrDefault(x => x.Name == ExpenseTicketConstants.ExpenseTicketPaid).Id;
        }
        public bool ValidateExpenseTicketEscaled()
        {
            return ExpenseStatusId == StatusGuids.FirstOrDefault(x => x.Name == ExpenseTicketConstants.ExpenseTicketEscaled).Id;
        }
        public bool ValidateExpenseTicketRejected()
        {
            return ExpenseStatusId == StatusGuids.FirstOrDefault(x => x.Name == ExpenseTicketConstants.ExpenseTicketRejected).Id;
        }          
    }
}