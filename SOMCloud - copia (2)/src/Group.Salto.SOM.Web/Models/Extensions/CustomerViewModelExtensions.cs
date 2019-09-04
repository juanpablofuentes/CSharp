using System;
using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Controls.Table.Models;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Customer;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Customer
{
    public static class CustomerViewModelExtensions
    {
        public static bool IsValid(this CustomerViewModel source)
        {
            return source != null
                   && !string.IsNullOrEmpty(source.Name);
        }

        public static CustomerDto ToDto(this CustomerViewModel source)
        {
            CustomerDto customer = null;
            if (source != null)
            {
                customer = new CustomerDto()
                {
                    InvoicingContactEmail = source.InvoicingContactEmail,
                    InvoicingContactName = source.InvoicingContactName,
                    InvoicingContactFirstSurname = source.InvoicingContactFirstSurname,
                    InvoicingContactSecondSurname = source.InvoicingContactSecondSurname,
                    TechnicalAdministratorEmail = source.TechnicalAdministratorEmail,
                    TechnicalAdministratorName = source.TechnicalAdministratorName,
                    TechnicalAdministratorFirstSurname = source.TechnicalAdministratorFirstSurname,
                    TechnicalAdministratorSecondSurname = source.TechnicalAdministratorSecondSurname,
                    NumberEmployees = source.NumberEmployees ?? 0,
                    Telephone = source.Telephone,
                    CustomerCode = source.CustomerCode,
                    Address = source.Address,
                    NumberTeamMembers = source.NumberTeamMembers ?? 0,
                    Name = source.Name,
                    NIF = source.NIF,
                    IsActive = source.IsActive,
                    NumberAppUsers = source.NumberAppUsers ?? 0,
                    NumberWebUsers = source.NumberWebUsers ?? 0,
                    Id = source.Id ?? default(Guid),
                    ModulesAssociated = source.ModulesAssigned,
                };
            }
            return customer;
        }

        public static CustomerViewModel ToViewModel(this CustomerDto source)
        {
            CustomerViewModel customer = null;
            if (source != null)
            {
                customer = new CustomerViewModel()
                {
                    Id = source.Id,
                    InvoicingContactEmail = source.InvoicingContactEmail,
                    InvoicingContactName = source.InvoicingContactName,
                    InvoicingContactFirstSurname = source.InvoicingContactFirstSurname,
                    InvoicingContactSecondSurname = source.InvoicingContactSecondSurname,
                    TechnicalAdministratorEmail = source.TechnicalAdministratorEmail,
                    TechnicalAdministratorName = source.TechnicalAdministratorName,
                    TechnicalAdministratorFirstSurname = source.TechnicalAdministratorFirstSurname,
                    TechnicalAdministratorSecondSurname = source.TechnicalAdministratorSecondSurname,
                    NumberEmployees = source.NumberEmployees,
                    Telephone = source.Telephone,
                    CustomerCode = source.CustomerCode,
                    Address = source.Address,
                    NumberTeamMembers = source.NumberTeamMembers,
                    Name = source.Name,
                    NIF = source.NIF,
                    IsActive = source.IsActive,
                    NumberAppUsers = source.NumberAppUsers,
                    NumberWebUsers = source.NumberWebUsers,
                    UpdateStatusDate = source.UpdateStatusDate,
                    ModulesAssigned = source.ModulesAssociated,
                    DateCreated = source.DateCreated,
                };
            }
            return customer;
        }

        public static ResultViewModel<CustomersViewModel> ToResultViewModel(this ResultDto<IList<CustomerDto>> source,
                                                                                    string title = null, string message = null)
        {
            return source.ToViewModel(c => new CustomersViewModel()
            {
                Customers = new MultiItemViewModel<CustomerViewModel, Guid>(c.MapList(x => x.ToViewModel()))
            }, title, message);
        }

        public static ResultViewModel<CustomerViewModel> ToResultViewModel(this ResultDto<CustomerDto> source,
                                                                            string title = null, string message = null)
        {
            return source.ToViewModel(c => c.ToViewModel(), title, message);
        }

        public static IList<CustomerViewModel> ToViewModel(this IList<CustomerDto> source)
        {
            return source?.MapList(c => c.ToViewModel());
        }
    }
}
