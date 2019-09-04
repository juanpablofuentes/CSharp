using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.MailTemplate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class MailTemplateListDtoExtencion
    {
        public static MailTemplateListDto ToListDto2(this MailTemplate source)
        {
            MailTemplateListDto result = null;
            if (source != null)
            {
                result = new MailTemplateListDto()
                {
                    Name = source.Name,
                    Content = source.Content,
                    Subject = source.Subject,
                    Id = source.Id

                };
            }
            return result;
        }
        public static IList<MailTemplateDto> ToListDto2(this IList<MailTemplate> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
    
}

