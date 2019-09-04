using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.MailTemplate;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkForm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.MailTemplate
{
    public interface IMailTemplateService
    {
        ResultDto<IList<MailTemplateDto>> GetAll();
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        ResultDto<IList<MailTemplateDto>> GetAllFiltered(MailTemplateFilterDto filter);
        ResultDto<MailTemplateDto> Create(MailTemplateDto model);
        ResultDto<MailTemplateDto> GetById(int id);
        ResultDto<MailTemplateDto> Update(MailTemplateDto model);
        ResultDto<bool> DeleteMailTemplate(int id);
        ResultDto<ErrorDto> CanDelete(int id);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
    }
}