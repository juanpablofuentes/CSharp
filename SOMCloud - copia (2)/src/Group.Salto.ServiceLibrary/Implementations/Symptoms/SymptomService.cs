using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Symptom;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Symptoms;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.Common;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Implementations.Symptoms
{
    public class SymptomService : BaseService, ISymptomService
    {
        private readonly ISymptomRepository _symptomRepository;

        public SymptomService(ILoggingService logginingService,
                                  ISymptomRepository SymptomRepository) : base(logginingService)
        {
            _symptomRepository = SymptomRepository ?? throw new ArgumentNullException(nameof(ISymptomRepository));            
        }

        public ResultDto<IList<SymptomBaseDto>> GetAllFiltered(SymptomFilterDto filter)
        {
            var query = _symptomRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToDto());            
        }

        private IQueryable<Symptom> OrderBy(IQueryable<Symptom> query, SymptomFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }

        public ResultDto<SymptomBaseDto> GetById(int id)
        {
            var localSymptom = _symptomRepository.GetById(id);
            return ProcessResult(localSymptom.ToBaseDto(), localSymptom != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<SymptomBaseDto> Create(SymptomBaseDto model)
        {
            var entity = model.ToEntity();
            var result = _symptomRepository.CreateSymptom(entity);
            return ProcessResult(result.Entity?.ToBaseDto(), result);
        }

        public ResultDto<SymptomBaseDto> Update(SymptomBaseDto model)
        {
            ResultDto<SymptomBaseDto> result = null;
            var findSymptom = _symptomRepository.GetById(model.Id);
            if (findSymptom != null)
            {
                var updatedSymptom = findSymptom.Update(model);
                var resultRepository = _symptomRepository.UpdateSymptom(updatedSymptom);
                result = ProcessResult(resultRepository.Entity?.ToBaseDto(), resultRepository);
            }
            return result ?? new ResultDto<SymptomBaseDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model
            };
        }

        //TODO: Verify if it has a child Symptom
        public ResultDto<bool> Delete(int id)
        {
            ResultDto<bool> result = null;
            var findSymptom = _symptomRepository.GetById(id);
            if (findSymptom == null)
            {
                return result ?? new ResultDto<bool>()
                {
                    Errors = new ErrorsDto()
                    {
                        Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                    },
                    Data = false,
                };
            }
            if (findSymptom.SymptomCollectionSymptoms?.Count == 0)
            {
                var resultSave = _symptomRepository.DeleteSymptom(findSymptom);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.ValidationError } }
                },
                Data = false,
            };
        }
        
        public IList<BaseNameIdDto<int>> GetOrphansKeyValue()
        {
            var data = _symptomRepository.GetOrphansKeyValue();
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value
            }).ToList();
        }
    }
}