using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Client
{
    public class ClientGeneralDetailViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string CorporateName { get; set; }
        public string ComercialName { get; set; }
        public string Alias { get; set; }
        public string InternCode { get; set; }
        public string ContableCode { get; set; }
        public string Address { get; set; }
        public IList<KeyValuePair<int, string>> Countries { get; set; }
        public int? CountrySelected { get; set; }
        public int? StateSelected { get; set; }
        public int? RegionSelected { get; set; }
        public int? MunicipalitySelected { get; set; }
        public int? PostalCode { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Mail { get; set; }
        public string Web { get; set; }
        public string Observations { get; set; }
        public bool UnListed { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.CorporateName))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(CorporateName) }));
            }

            if (!string.IsNullOrEmpty(this.Mail))
            {
                if (!ValidationsHelper.IsEmailValid(this.Mail))
                {
                    errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.EmailFormatInvalidMessage), new[] { nameof(Mail) }));
                }
            }

            return errors;
        }
    }
}