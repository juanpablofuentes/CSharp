using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IMailTemplateRepository
    {
        IQueryable<MailTemplate> GetAll();
        Dictionary<int, string> GetAllKeyValues();
        MailTemplate GetByIdIncludeReferencesToDelete(int Id);
        SaveResult<MailTemplate> CreateMailTemplate(MailTemplate entity);
        MailTemplate GetById(int Id);
        SaveResult<MailTemplate> UpdateMailTemplate(MailTemplate entity);
        SaveResult<MailTemplate> DeleteMailTemplate(MailTemplate entity);
        MailTemplate GetByIdCanDelete(int Id);
        IQueryable<MailTemplate> GetAllByFilters(FilterQueryDto filterQuery);
    }
}