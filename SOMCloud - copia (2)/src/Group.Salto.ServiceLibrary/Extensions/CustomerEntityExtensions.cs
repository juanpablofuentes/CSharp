using System;
using Group.Salto.Entities;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CustomerEntityExtensions
    {
        public static void UpdateCustomer(this Customers target, Customers source)
        {
            if (source != null && target != null)
            {
                if (target.IsActive != source.IsActive)
                {
                    target.UpdateStatusDate = DateTime.UtcNow;
                }
                target.Name = source.Name;
                target.InvoicingContactEmail = source.InvoicingContactEmail;
                target.InvoicingContactName = source.InvoicingContactName;
                target.InvoicingContactFirstSurname = source.InvoicingContactFirstSurname;
                target.InvoicingContactSecondSurname = source.InvoicingContactSecondSurname;
                target.TechnicalAdministratorEmail = source.TechnicalAdministratorEmail;
                target.TechnicalAdministratorName = source.TechnicalAdministratorName;
                target.TechnicalAdministratorFirstSurname = source.TechnicalAdministratorFirstSurname;
                target.TechnicalAdministratorSecondSurname = source.TechnicalAdministratorSecondSurname;
                target.NumberEmployees = source.NumberEmployees;
                target.Telephone = source.Telephone;
                target.CustomerCode = source.CustomerCode;
                target.Address = source.Address;
                target.NumberTeamMembers = source.NumberTeamMembers;
                target.NIF = source.NIF;
                target.NumberAppUsers = source.NumberAppUsers;
                target.NumberWebUsers = source.NumberWebUsers;
                target.IsActive = source.IsActive;
            }
        }
    }
}
