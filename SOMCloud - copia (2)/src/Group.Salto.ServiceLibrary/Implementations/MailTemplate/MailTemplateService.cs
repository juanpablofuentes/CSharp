using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.MailTemplate;
using Group.Salto.ServiceLibrary.Common.Dtos.MailTemplate;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Group.Salto.ServiceLibrary.Common.Dtos;
using System.Linq;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.Extensions;
using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkForm;
using Group.Salto.Common.Constants.MailTemplate;

namespace Group.Salto.ServiceLibrary.Implementations.MailTemplate
{
    public class MailTemplateService : BaseFilterService, IMailTemplateService
    {
        private readonly IMailTemplateRepository _mailTemplateRepository;
        
        public MailTemplateService(ILoggingService logginingService,
                                   IMailTemplateRepository mailTemplateRepository,
                                   IMailTemplateQueryFactory queryFactory) : base(queryFactory, logginingService)
        {
            _mailTemplateRepository = mailTemplateRepository ?? throw new ArgumentNullException(nameof(IMailTemplateRepository));
        }
        
        public ResultDto<IList<MailTemplateDto>> GetAll()
        {
                var data = _mailTemplateRepository.GetAll().ToList();
                return ProcessResult(MailTemplateDtoExtensions.ToListDto(data));
        }

        public ResultDto<IList<MailTemplateDto>> GetAllFiltered(MailTemplateFilterDto filter)
        {
            var query = _mailTemplateRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(MailTemplateDtoExtensions.ToListDto(query.ToList())); 
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get MailTemplate Key Value");
            var data = _mailTemplateRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public ResultDto<MailTemplateDto> Create(MailTemplateDto model)
        {
            LogginingService.LogInfo($"Create MailTemplate");
            var entity = model.ToEntity();
            var resultSave = _mailTemplateRepository.CreateMailTemplate(entity);
            return ProcessResult(resultSave.Entity.ToDto(), resultSave);
        }

        public ResultDto<MailTemplateDto> GetById(int id)
        {
            var data = _mailTemplateRepository.GetById(id);
            return ProcessResult(data.ToDto());
        }

        public ResultDto<MailTemplateDto> Update(MailTemplateDto model)
        {
            LogginingService.LogInfo($"Update MailTemplateDto with id {model.Id}");

            ResultDto<MailTemplateDto> result = null;
            var localModel = _mailTemplateRepository.GetById(model.Id);
            if (localModel != null)
            {
                localModel.Name = model.Name;
                localModel.Name = model.Name;
                localModel.Subject = model.Subject;
                localModel.Content = model.Content;
                var resultSave = _mailTemplateRepository.UpdateMailTemplate(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<MailTemplateDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }
        
        public ResultDto<bool> DeleteMailTemplate(int id)
        {
            ResultDto<bool> result = null;
            var MailTemplate = _mailTemplateRepository.GetByIdIncludeReferencesToDelete(id);
            if (MailTemplate != null)
            {
                var resultSave = _mailTemplateRepository.DeleteMailTemplate(MailTemplate);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false,
            };
        }
        
        public ResultDto<ErrorDto> CanDelete(int id)
        {
            var MailTemplate = _mailTemplateRepository.GetByIdCanDelete(id);

            ErrorDto result = new ErrorDto() { ErrorMessageKey = string.Empty };

            if (MailTemplate.Tasks?.Any() == true)
            {
                result.ErrorMessageKey = MailTemplateConstant.MailTemplateDeleteErrorMessage;
            }
            
            return ProcessResult(result);
        }

        private IList<RateDto> GetAvailableSalesRates(Entities.Tenant.Items item, IQueryable<Entities.Tenant.SalesRate> rates)
        {
            IList<RateDto> result = null;
            var availableRates = rates?.Where(x => !item.ItemsSalesRate.Any(y => y.SalesRateId == x.Id));
            if (availableRates?.Count() > 0)
            {
                result = new List<RateDto>();
                foreach (var rate in availableRates)
                {
                    result.Add(new RateDto
                    {
                        Id = rate.Id,
                        Name = rate.Name
                    });
                }
            }
            return result;
        }

        private IQueryable<Entities.Tenant.MailTemplate> OrderBy(IQueryable<Entities.Tenant.MailTemplate> data, MailTemplateFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }
    }
}