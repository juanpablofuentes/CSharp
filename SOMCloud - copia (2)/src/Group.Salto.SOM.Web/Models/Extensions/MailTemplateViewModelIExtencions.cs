using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.MailTemplate;
using Group.Salto.SOM.Web.Models.MailTemplate;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class MailTemplateViewModelIExtencions
    {
        public static MailTemplateDto ToDto(this MailTemplateViewModel source)
        {
            MailTemplateDto result = null;
            if (source != null)
            {
                result = new MailTemplateDto()
                {
                    Name = source.Name,
                    Content = source.Content,
                    Subject = source.Subject,
                    Id = source.Id
                };
            }
            return result;
        }
        public static IList<MailTemplateDto> ToListDto(this IList<MailTemplateViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }
        public static MailTemplateViewModel ToViewModel(this MailTemplateDto source)
        {
            MailTemplateViewModel result = null;
            if (source != null)
            {
                result = new MailTemplateViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Content = source.Content,
                    Subject = source.Subject,
                };
            }
            return result;
        }
        public static IList<MailTemplateViewModel> ToListViewModel(this IList<MailTemplateDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static ResultViewModel<MailTemplateViewModel> ToViewModel(this ResultDto<MailTemplateDto> source)
        {
            var response = source != null ? new ResultViewModel<MailTemplateViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static MailTemplateListViewModel ToListMViewModel(this MailTemplateViewModel source)
        {
            MailTemplateListViewModel result = null;
            if (source != null)
            {
                result = new MailTemplateListViewModel()
                {
                    MailTemplate = source
                };
            }
            return result;
        }
    }
}