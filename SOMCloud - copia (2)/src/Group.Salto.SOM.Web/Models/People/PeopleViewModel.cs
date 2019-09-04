using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.People
{
    public class PeopleViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string FisrtSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
        public bool IsClientWorker { get; set; }
        public string UserName { get; set; }
        public int? UserConfigurationId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            
            return errors;
        }
    }
}
