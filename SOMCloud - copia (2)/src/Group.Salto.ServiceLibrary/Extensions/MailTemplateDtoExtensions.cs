using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.MailTemplate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class MailTemplateDtoExtensions
    {
        public static MailTemplateDto ToDto(this MailTemplate source)
        {
            MailTemplateDto result = null;
            if (source != null)
            {
                result = new MailTemplateDto()
                {
                    Name = HttpUtility.HtmlDecode(source.Name),
                    Content = source.Content,
                    Subject = source.Subject,
                    Id = source.Id

                };
            }
            return result;
        }
        public static IList<MailTemplateDto> ToListDto(this IList<MailTemplate> source)
        {
            return source?.MapList(x => x.ToDto());
        }

        public static MailTemplate ToEntity(this MailTemplateDto source)
        {
            MailTemplate result = null;
            if (source != null)
            {
                result = new MailTemplate()
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