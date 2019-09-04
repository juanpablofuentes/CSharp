using Group.Salto.ServiceLibrary.Common.Dtos.Templates;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Templates
{
    public interface ITemplateForgotPasswordService
    {
        string GetMailBody(TemplateForgotPasswordDto resetPasswordDto);
    }
}
