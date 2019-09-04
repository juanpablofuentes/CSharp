using Group.Salto.ServiceLibrary.Common.Contracts.Templates;
using Group.Salto.ServiceLibrary.Common.Dtos.Templates;

namespace Group.Salto.ServiceLibrary.Implementations.Templates
{
    public class TemplateForgotPasswordService : ITemplateForgotPasswordService
    {
        public string GetMailBody(TemplateForgotPasswordDto resetPasswordDto)
        {
            string template = resetPasswordDto.template;
            string legalLinks = "<a href=\"#\">" + resetPasswordDto.Privacy + "</a>&nbsp;<a href=\"#\">" + resetPasswordDto.LegalInfo + "</a> ";
            string[] considerations = resetPasswordDto.LongForgetMailConsiderations.Split('|');
            string[] footerText = resetPasswordDto.LongForgetMailFooter.Split('|');

            template = template.Replace("{LogoImage}", resetPasswordDto.ImageUrl);
            template = template.Replace("{title}", resetPasswordDto.LongForgetMailTitle);
            template = template.Replace("{greet}", resetPasswordDto.LongForgetMailGreet);
            template = template.Replace("{href}", resetPasswordDto.Href);
            template = template.Replace("{linkText}", resetPasswordDto.LongForgetMailLink);
            template = template.Replace("{considerations_0}", considerations[0]);
            template = template.Replace("{considerations_1}", considerations[1]);
            template = template.Replace("{considerations_2}", considerations[2]);
            template = template.Replace("{farewell}", resetPasswordDto.LongForgetMailFarewell);
            template = template.Replace("{footerText_0}", footerText[0]);
            template = template.Replace("{footerText_1}", footerText[1]);
            template = template.Replace("{legalLinks}", legalLinks);

            return template;
        }
    }
}