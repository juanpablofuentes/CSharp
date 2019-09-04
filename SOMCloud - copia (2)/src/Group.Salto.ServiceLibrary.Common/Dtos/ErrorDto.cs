using Group.Salto.Common;

namespace Group.Salto.ServiceLibrary.Common.Dtos
{
    public class ErrorDto
    {
        public ErrorType ErrorType { get; set; }
        public string ErrorMessageKey { get; set; }
    }
}