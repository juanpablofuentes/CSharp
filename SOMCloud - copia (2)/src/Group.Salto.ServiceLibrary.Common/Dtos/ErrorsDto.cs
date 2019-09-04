using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos
{
    public class ErrorsDto
    {
        public List<ErrorDto> Errors { get; set; }

        public void AddError(ErrorDto error)
        {
            if (Errors == null)
            {
                Errors = new List<ErrorDto>();
            }
            Errors.Add(error);
        }

        public void AddRangeError(List<ErrorDto> errors)
        {
            if (Errors == null)
            {
                Errors = new List<ErrorDto>();
            }
            Errors.AddRange(errors);
        }
    }
}