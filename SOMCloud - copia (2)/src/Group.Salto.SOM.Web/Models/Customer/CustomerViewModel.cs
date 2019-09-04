using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Customer;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;

namespace Group.Salto.SOM.Web.Models.Customer
{
    public class CustomerViewModel : IValidatableObject
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int? NumberWebUsers { get; set; }
        public int? NumberAppUsers { get; set; }
        public int? NumberTeamMembers { get; set; }
        public int? NumberEmployees { get; set; }
        public string InvoicingContactName { get; set; }
        public string InvoicingContactFirstSurname { get; set; }
        public string InvoicingContactSecondSurname { get; set; }
        public string InvoicingContactEmail { get; set; }
        public string TechnicalAdministratorName { get; set; }
        public string TechnicalAdministratorFirstSurname { get; set; }
        public string TechnicalAdministratorSecondSurname { get; set; }
        public string TechnicalAdministratorEmail { get; set; }
        public string Telephone { get; set; }
        public string CustomerCode { get; set; }
        public string NIF { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }

        private DateTime _updateStatusDateTime;
        public DateTime UpdateStatusDate
        {
            get => _updateStatusDateTime.ToLocalTime();
            set => _updateStatusDateTime = value;
        }

        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get => _dateCreated.ToLocalTime();
            set => _dateCreated = value;
        }
        public IList<Guid> ModulesAssigned { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                            new[] { nameof(Name) }));
            }
            if (string.IsNullOrEmpty(this.InvoicingContactName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(InvoicingContactName) }));
            }
            if (string.IsNullOrEmpty(this.InvoicingContactFirstSurname))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(InvoicingContactFirstSurname) }));
            }
            if (string.IsNullOrEmpty(this.InvoicingContactEmail))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(InvoicingContactEmail) }));
            }
            else if (!string.IsNullOrEmpty(this.InvoicingContactEmail) && !ValidationsHelper.IsEmailValid(this.InvoicingContactEmail))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.EmailFormatInvalidMessage),
                    new[] { nameof(InvoicingContactEmail) }));
            }
            if (string.IsNullOrEmpty(this.TechnicalAdministratorName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(TechnicalAdministratorName) }));
            }
            if (string.IsNullOrEmpty(this.TechnicalAdministratorFirstSurname))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(TechnicalAdministratorFirstSurname) }));
            }
            if (string.IsNullOrEmpty(this.TechnicalAdministratorEmail))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(TechnicalAdministratorEmail) }));
            }
            else if (!string.IsNullOrEmpty(this.TechnicalAdministratorEmail) && !ValidationsHelper.IsEmailValid(this.TechnicalAdministratorEmail))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.EmailFormatInvalidMessage),
                    new[] { nameof(TechnicalAdministratorEmail) }));
            }

            if (string.IsNullOrEmpty(this.NIF))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(NIF) }));
            }
            else if (!ValidationsHelper.ValidateNIF(this.NIF.Trim()) && !ValidationsHelper.ValidateCif(this.NIF.Trim()))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.NIFInavalidFormat),
                    new[] { nameof(NIF) }));
            }
            if (!this.NumberWebUsers.HasValue)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(NumberWebUsers) }));
            }
            else if (NumberWebUsers.Value < 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.MustBePossitveNumber),
                    new[] { nameof(NumberWebUsers) }));
            }
            if (!this.NumberAppUsers.HasValue)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(NumberAppUsers) }));
            }
            else if (NumberAppUsers.Value < 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.MustBePossitveNumber),
                    new[] { nameof(NumberAppUsers) }));
            }
            if (!this.NumberTeamMembers.HasValue)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(NumberTeamMembers) }));
            }
            else if (NumberTeamMembers.Value < 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.MustBePossitveNumber),
                    new[] { nameof(NumberTeamMembers) }));
            }
            if (string.IsNullOrEmpty(this.Address?.Trim()))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Address) }));
            }
            if (string.IsNullOrEmpty(this.Telephone?.Trim()))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(Telephone) }));
            }
            if (!string.IsNullOrEmpty(this.CustomerCode) && this.CustomerCode.Length > CustomerConstants.CustomerCodeMaxLenght)
            {
                var message = LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.LenghtMaxInvalid)
                              +" "+CustomerConstants.CustomerCodeMaxLenght;
                    
                errors.Add(new ValidationResult(message,
                    new[] { nameof(CustomerCode) }));
            }
            if (!this.NumberEmployees.HasValue)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage),
                    new[] { nameof(NumberEmployees) }));
            }
            else if (NumberEmployees.Value < 0)
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.MustBePossitveNumber),
                    new[] { nameof(NumberEmployees) }));
            }
            return errors;
        }
    }
}
