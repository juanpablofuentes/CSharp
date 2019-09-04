using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.SymptomCollection;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.SymptomCollection;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.SymptomCollection
{
    public class SymptomCollectionService : BaseService, ISymptomCollectionService
    {
        private readonly ISymptomCollectionRepository _symptomCollectionRepository;
        private readonly ISymptomRepository _symptomRepository;

        public SymptomCollectionService(ILoggingService logginingService,
                                  ISymptomRepository SymptomRepository,
                                  ISymptomCollectionRepository SymptomCollectionRepository) : base(logginingService)
        {
            _symptomCollectionRepository = SymptomCollectionRepository ?? throw new ArgumentNullException(nameof(ISymptomCollectionRepository));
            _symptomRepository = SymptomRepository ?? throw new ArgumentNullException(nameof(ISymptomRepository));
        }

        public ResultDto<IList<SymptomCollectionBaseDto>> GetAllFiltered(SymptomCollectionFilterDto filter)
        {
            var query = _symptomCollectionRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Element, au => au.Description.Contains(filter.Element));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToBaseDto());            
        }

        private IQueryable<SymptomCollections> OrderBy(IQueryable<SymptomCollections> query, SymptomCollectionFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }

        public ResultDto<SymptomCollectionDto> GetById(int id) 
        {
            var localSymptomCollection = _symptomCollectionRepository.GetById(id);
            return ProcessResult(localSymptomCollection.ToDto(), localSymptomCollection != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });        
        }

        public ResultDto<SymptomCollectionDto> Create(SymptomCollectionDto model)
        {
            ResultDto<SymptomCollectionDto> result = null;
            var entity = model.ToEntity();
            entity = AssignSymptoms(entity, model.SymptomSelected);
            var resultRepository = _symptomCollectionRepository.CreateSymptomCollection(entity);
            result = ProcessResult(resultRepository?.Entity?.ToDto(), resultRepository);          
            return result;       
        }

        public ResultDto<SymptomCollectionDto> Update(SymptomCollectionDto model)
        {
            ResultDto<SymptomCollectionDto> result = null;
            var findSymptomCollection = _symptomCollectionRepository.GetById(model.Id);
            if (findSymptomCollection != null)
            {
                var entity = findSymptomCollection.Update(model);
                entity = AssignSymptoms(entity, model.SymptomSelected);
                var resultRepository = _symptomCollectionRepository.UpdateSymptomCollection(entity);
                result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
            }
            return result ?? new ResultDto<SymptomCollectionDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model
            };          
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues() 
        {
            var data = _symptomCollectionRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();            
        }
        
        public ResultDto<bool> Delete(int id)
        {
            ResultDto<bool> result = null;
            var findsymptomcollection = _symptomCollectionRepository.GetById(id);
            if (findsymptomcollection != null && findsymptomcollection.SymptomCollectionSymptoms?.Count == 0)
            {
                var resultSave = _symptomCollectionRepository.DeleteSymptomCollection(findsymptomcollection);
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
        
        private SymptomCollections AssignSymptoms(SymptomCollections entity, IList<int> symptoms)
        {
            entity.SymptomCollectionSymptoms?.Clear();
            if (symptoms != null && symptoms.Any())
            {
                entity.SymptomCollectionSymptoms = entity.SymptomCollectionSymptoms ?? new List<SymptomCollectionSymptoms>();
                var localsymptoms = _symptomRepository.GetAllById(symptoms);
                foreach (var localsymptom in localsymptoms)
                {
                    entity.SymptomCollectionSymptoms.Add(new SymptomCollectionSymptoms()
                    {
                        Symptom  = localsymptom                       
                    });
                }
            }
            return entity;
        }
    }
}