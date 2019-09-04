﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.WorkOrderType;

namespace Group.Salto.SOM.Web.Models.CollectionTypeWorkOrders
{
    public class CollectionTypeWorkOrdersDetailViewModel : CollectionTypeWorkOrdersViewModel, IValidatableObject
    {
        public IList<WorkOrderTypeDetailViewModel> Childs { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.Name))
            {
                errors.Add(new ValidationResult(LocalizedExtensions.GetUILocalizedText(LocalizationsConstants.RequiredMessage), new[] { nameof(Name) }));
            }

            return errors;
        }
    }
}