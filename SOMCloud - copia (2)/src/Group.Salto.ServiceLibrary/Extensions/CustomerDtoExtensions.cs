using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Customer;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CustomerDtoExtensions
    {
        public static CustomerDto ToDto(this Customers source)
        {
            CustomerDto result = null;
            if (source != null)
            {
                result = new CustomerDto()
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
                    MunicipalitySelected = source.Municipality.ToIdsDto(),
                    NumberTeamMembers = source.NumberTeamMembers,
                    Name = source.Name,
                    IsActive = source.IsActive,
                    ConnString = source.ConnString,
                    NIF = source.NIF,
                    NumberAppUsers = source.NumberAppUsers,
                    NumberWebUsers = source.NumberWebUsers,
                    UpdateStatusDate = source.UpdateStatusDate,
                    DatabaseName = source.DatabaseName,
                    ModulesAssociated = source.ModulesAssigned?.Select(x=>x.ModuleId).ToList(),
                    DateCreated = source.DateCreated,
                };
            }

            return result;
        }

        public static Customers ToEntity(this CustomerDto source)
        {
            Customers result = null;
            if (source != null)
            {
                result = new Customers()
                {
                    Id = source.Id,
                    ConnString = source.ConnString,
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
                    Name = source.Name.Trim(),
                    NumberAppUsers = source.NumberAppUsers,
                    NIF = source.NIF,
                    NumberWebUsers = source.NumberWebUsers,
                    IsActive = source.IsActive,
                    UpdateStatusDate = source.UpdateStatusDate,
                    DatabaseName = source.DatabaseName,
                };
            }

            return result;
        }

        public static bool IsValid(this CustomerDto source)
        {
            return source != null
                   && !string.IsNullOrEmpty(source.Name)
                   && !string.IsNullOrEmpty(source.InvoicingContactEmail)
                   && ValidationsHelper.IsEmailValid(source.InvoicingContactEmail)
                   && !string.IsNullOrEmpty(source.InvoicingContactEmail)
                   && !string.IsNullOrEmpty(source.NIF)
                   && (ValidationsHelper.ValidateNIF(source.NIF) || ValidationsHelper.ValidateCif(source.NIF))
                   && !string.IsNullOrEmpty(source.TechnicalAdministratorEmail)
                   && ValidationsHelper.IsEmailValid(source.TechnicalAdministratorEmail)
                   && !string.IsNullOrEmpty(source.TechnicalAdministratorName)
                   && source.NumberAppUsers >= 0
                   && source.NumberTeamMembers >= 0
                   && source.NumberWebUsers >= 0;
        }
    }
}
