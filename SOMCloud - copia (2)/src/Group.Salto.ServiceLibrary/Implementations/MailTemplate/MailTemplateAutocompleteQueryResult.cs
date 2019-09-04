using Group.Salto.Common.Enums;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.MailTemplate;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.MailTemplate
{
    public class MailTemplateAutocompleteQueryResult : IMailTemplateAutocompleteQueryResult
    {
        private IMailTemplateRepository _mailTemplateRepository;

        public MailTemplateAutocompleteQueryResult(IMailTemplateRepository mailTemplateRepository)
        {
            _mailTemplateRepository = mailTemplateRepository ?? throw new ArgumentNullException($"{nameof(IMailTemplateRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            FilterQueryDto filterQuery = new FilterQueryDto() { Name = queryTypeParameters.Text, Active = ActiveEnum.Active };
            return _mailTemplateRepository.GetAllByFilters(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }
    }
}