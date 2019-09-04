using System.Collections.Generic;
using Group.Salto.Common;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ErrorsDtoExtensions
    {
        public static ErrorsDto ToErrorsDto(this SaveError source)
        {
            ErrorsDto result = null;
            if (source != null)
            {
                result = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>()
                    {
                        new ErrorDto()
                        {
                            ErrorType = source.ErrorType,
                            ErrorMessageKey = source.ErrorMessage,
                        }
                    },
                };
            }

            return result;
        }

    }
}
