using Group.Salto.Common.Constants.ExpenseTicket;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Expense;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.SOM.Web.Models.Expense;
using Group.Salto.SOM.Web.Models.ExpenseTicket;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExpenseDetailsViewModelExtensions
    {
        public static ExpenseTicketExtDto ToExtDto(this ExpenseDetailsViewModel source)
        {
            ExpenseTicketExtDto result = null;
            if (source != null)
            {
                result = new ExpenseTicketExtDto()
                {
                    Id = source.Id,
                    Description = source.Description,
                    Amount = source.Amount.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    Date = source.Date,
                    Factor = source.Factor,
                    ExpenseTypeId = source.ExpenseTypeId,
                    Status = source.ExpenseStatusId,
                    StatusExpense = source.ExpenseStatus,
                    PaymentMethodId = source.PaymentMethodId,
                    PaymentInformation = source.PaymentInformation,
                    ValidationObservations = source.Observations                 
                };
            }
            return result;
        }

        public static ExpenseTicketExtDto ToValidateDto(this ExpenseDetailsViewModel source,ExpenseTicketExtDto data,int peopleId)
        {
            
            if (source != null && data != null)
            {
                data.Id = source.Id;
                data.Description = source.Description;
                data.Amount = source.Amount.StringToDecimal(CultureInfo.InvariantCulture.TwoLetterISOLanguageName);
                data.Date = source.Date;
                data.ValidationDate = source.ValidationDate;
                data.Factor = source.Factor;
                data.ExpenseTypeId = source.ExpenseTypeId;
                data.PaymentMethodId = source.PaymentMethodId;
                if (source.PaymentInformation != null && String.Compare(source.NextStatus, ExpenseTicketConstants.ExpenseTicketPaid) == 0)
                {
                    data.PaymentInformation = source.PaymentInformation;
                }
                if (source.Observations != null && String.Compare(source.NextStatus, ExpenseTicketConstants.ExpenseTicketRejected) == 0)
                {
                    data.ValidationObservations = source.Observations;
                }
                data.validatorPeopleId = peopleId;                
            }
            return data;
        }

        public static ResultViewModel<ExpenseDetailsViewModel> ToViewModel(this ResultDto<ExpenseTicketExtDto> source)
        {
            var response = source != null ? new ResultViewModel<ExpenseDetailsViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ExpenseDetailsViewModel ToViewModel(this ExpenseTicketExtDto source)
        {
            ExpenseDetailsViewModel result = null;
            if (source != null)
            {
                result = new ExpenseDetailsViewModel()
                {
                    Id = source.Id,
                    Amount = ((decimal?)source.Amount)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    Description = source.Description,
                    PaymentMethodId = source.PaymentMethodId,
                    ExpenseTypeId = source.ExpenseTypeId,
                    ExpenseStatusId = source.Status,
                    ExpenseStatus = source.StatusExpense,
                    Date = source.Date,
                    ValidationDate = source.ValidationDate,
                };
                if (source.PeopleValidator != null)
                {
                    result.PeopleValidator = source.PeopleValidator.Name + " " + source.PeopleValidator.Surname + " " + source.PeopleValidator.SecondSurname;
                }
            }
            return result;
        }

        public static IList<ExpenseDetailsViewModel> ToListViewModel(this IList<ExpenseTicketExtDto> source)
        {
            var temp = source?.MapList(x => x.ToViewModel());
            return temp;
        }
    }
}