using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ExtraFields;
using Group.Salto.ServiceLibrary.Common.Contracts.ExtraFieldTypes;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFields;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.ExtraFields
{
    public class ExtraFieldsService : BaseService, IExtraFieldsService
    {
        private readonly IExtraFieldsRepository _extraFieldsRepository;
        private readonly IExtraFieldTypesService _extraFieldTypesService;

        public ExtraFieldsService(ILoggingService logginingService, IExtraFieldTypesService extraFieldTypesService, IExtraFieldsRepository extraFieldsRepository) : base(logginingService)
        {
            _extraFieldsRepository = extraFieldsRepository ?? throw new ArgumentNullException($"{nameof(IExtraFieldsRepository)} is null");
            _extraFieldTypesService = extraFieldTypesService ?? throw new ArgumentNullException($"{nameof(IExtraFieldTypesService)} is null");
        }

        public IList<BaseNameIdDto<int>> GetAllByDelSystemKeyValues(int languageId, bool isSystem)
        {
            LogginingService.LogInfo($"Get all Extra Fields Filtered by DelSystem");
            var query = _extraFieldsRepository.GetAllByDelSystemWithIncludeTranslations(isSystem);
            var data = query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = !x.ExtraFieldsTranslations.Any(t => t.LanguageId == languageId) ? x.Name : x.ExtraFieldsTranslations.FirstOrDefault(t => t.LanguageId == languageId).NameText
            }).ToList();
            return data;
        }

        public ResultDto<IList<ExtraFieldsDto>> GetAllFiltered(ExtraFieldsFilterDto filter)
        {
            var query = _extraFieldsRepository.GetAllWithIncludeTranslations();
            var types = _extraFieldTypesService.GetAllKeyValues();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToListDto());
        }

        public ResultDto<ExtraFieldsDetailDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get ExtraFields by id {id}");
            var result = _extraFieldsRepository.GetByIdWithIncludeTranslations(id)?.ToDetailDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }        

        public ResultDto<ExtraFieldsDetailDto> Create(ExtraFieldsDetailDto model)
        {
            ResultDto<ExtraFieldsDetailDto> result = null;
            var entity = model.ToEntity();
            var resultRepository = _extraFieldsRepository.CreateExtraFields(entity);
            result = ProcessResult(resultRepository?.Entity?.ToDetailDto(), resultRepository);
            return result;
        }

        public ResultDto<ExtraFieldsDetailDto> Update(ExtraFieldsDetailDto model)
        {
            LogginingService.LogInfo($"Update ExtraField with id {model.Id}");

            ResultDto<ExtraFieldsDetailDto> result = null;
            var localModel = _extraFieldsRepository.GetByIdWithIncludeTranslations(model.Id);
            if (localModel != null)
            {
                localModel.Name = model.Name;
                localModel.Description = model.Description;
                localModel.AllowedStringValues = model.AllowedStringValues;
                localModel.DelSystem = model.DelSystem.HasValue ? model.DelSystem.Value : false; 
                localModel.IsMandatory = model.IsMandatory;
                localModel.MultipleChoice = model.MultipleChoice;
                localModel.ErpSystemInstanceQueryId = model.ErpSystemInstanceQueryId;
                localModel.Type = model.TypeId;
                localModel = UpdateTranslations(localModel, model.Translations);
                var resultSave = _extraFieldsRepository.UpdateExtraFields(localModel);
                result = ProcessResult(resultSave.Entity.ToDetailDto(), resultSave);
            }
            return result ?? new ResultDto<ExtraFieldsDetailDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }       

        public ResultDto<IList<ExtraFieldsDetailDto>> GetAllFilteredByLanguage(ExtraFieldsFilterDto filter)
        {
            LogginingService.LogInfo($"Get all ExtraField status with include translations");
            var query = _extraFieldsRepository.GetAllWithIncludeTranslations();
            var types = _extraFieldTypesService.GetAllKeyValues();
            query = query.WhereIfNotDefault(filter.Name,
                au => !au.ExtraFieldsTranslations.Any(t => t.LanguageId == filter.LanguageId)
                ? au.Name.Contains(filter.Name)
                : au.ExtraFieldsTranslations.FirstOrDefault(t => t.LanguageId == filter.LanguageId).NameText.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description,
                au => !au.ExtraFieldsTranslations.Any(t => t.LanguageId == filter.LanguageId)
                ? au.Description.Contains(filter.Description)
                : au.ExtraFieldsTranslations.FirstOrDefault(t => t.LanguageId == filter.LanguageId).DescriptionText.Contains(filter.Description));
            var result = OrderBy(query.MapList(wos => wos.FilterByLanguage(filter.LanguageId).ToExtraFieldsListDto(types, wos)).AsQueryable(), filter).ToList();
            return ProcessResult<IList<ExtraFieldsDetailDto>>(result);
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete ExtraField by id {id}");
            ResultDto<bool> result = null;
            var localExtraFields = _extraFieldsRepository.GetByIdWithIncludeTranslations(id);
            if (localExtraFields != null)
            {
                var resultSave = _extraFieldsRepository.DeleteExtraFields(localExtraFields);
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

        public ResultDto<bool> CanDelete(int id)
        {
            var resu = _extraFieldsRepository.GetByIdWithCollectionsServices(id);
            return ProcessResult(!resu?.ExtraFieldsValues?.Any() & !resu?.CollectionsExtraFieldExtraField?.Any() ?? false);
        }

        private IQueryable<ExtraFieldsDetailDto> OrderBy(IQueryable<ExtraFieldsDetailDto> query, ExtraFieldsFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }

        private IQueryable<Entities.Tenant.ExtraFields> OrderBy(IQueryable<Entities.Tenant.ExtraFields> data, ExtraFieldsFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }

        private Entities.Tenant.ExtraFields UpdateTranslations(Entities.Tenant.ExtraFields localModel, IList<ContentTranslationDto> translations)
        {
            localModel.ExtraFieldsTranslations?.Clear();
            localModel.ExtraFieldsTranslations = translations.ToExtraFieldsTranslationEntity();
            return localModel;
        }

        public int GetExtraFieldIdFormName(string extraFieldName)
        {
            Entities.Tenant.ExtraFields extraField = _extraFieldsRepository.GetExtraFieldFromName(extraFieldName);
            return extraField.Id;
        }
    }
}