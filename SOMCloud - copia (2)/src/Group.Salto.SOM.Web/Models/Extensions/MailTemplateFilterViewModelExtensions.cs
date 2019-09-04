using Group.Salto.SOM.Web.Models.MailTemplate;
using Group.Salto.ServiceLibrary.Common.Dtos.MailTemplate;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class MailTemplateFilterViewModelExtensions
    {
        public static MailTemplateFilterDto ToDto(this MailTemplateFilterViewModel source)
        {
            MailTemplateFilterDto result = null;
            if (source != null)
            {
                result = new MailTemplateFilterDto()
                {
                    Name = source.Name,
                    Content = source.Content,
                    Subject = source.Subject,
                    Id = source.Id
                };
            }
            return result;
        }
    }
}