using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PeopleCollection;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCollection;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.PeopleCollection
{
    public class PeopleCollectionService : BaseService, IPeopleCollectionService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IPeopleCollectionRepository _peopleCollectionRepository;

        public PeopleCollectionService(ILoggingService logginingService,
                                        IPeopleRepository peopleRepository,
                                        IPeopleCollectionRepository peopleCollectionRepository) : base(logginingService)
        {
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null");
            _peopleCollectionRepository = peopleCollectionRepository ?? throw new ArgumentNullException($"{nameof(IPeopleCollectionRepository)} is null");
        }

        public ResultDto<IList<PeopleCollectionBaseDto>> GetAllFiltered(PeopleCollectionFilterDto filter)
        {
            var query = _peopleCollectionRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Info.Contains(filter.Description));
            var data = query.MapList(x => x.ToBaseDto());
            data = OrderBy(data.AsQueryable(), filter).ToList();
            return ProcessResult(data);
        }

        public ResultDto<PeopleCollectionDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get PeopleCollection by id {id}");
            var result = _peopleCollectionRepository.GetById(id)?.ToDto();
            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<PeopleCollectionDto> Create(PeopleCollectionDto model)
        {
            LogginingService.LogInfo($"Create PeopleCollection");
            var entity = model.ToEntity();
            entity = AssingPeople(entity, model.People);
            entity = AssingPeopleAdmin(entity, model.PeopleAdmin);
            var resultSave = _peopleCollectionRepository.CreatePeopleCollection(entity);
            return ProcessResult(resultSave.Entity.ToDto(), resultSave);
        }

        public ResultDto<PeopleCollectionDto> Update(PeopleCollectionDto model)
        {
            LogginingService.LogInfo($"Update PeopleCollection with id {model.Id}");

            ResultDto<PeopleCollectionDto> result = null;
            var localModel = _peopleCollectionRepository.GetById(model.Id);
            if (localModel != null)
            {
                localModel.Name = model.Name;
                localModel.Info = model.Description;
                localModel = AssingPeople(localModel, model.People);
                localModel = AssingPeopleAdmin(localModel, model.PeopleAdmin);
                var resultSave = _peopleCollectionRepository.UpdatePeopleCollection(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<PeopleCollectionDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete PeopleCollection by id {id}");
            ResultDto<bool> result = null;
            var localPeopleCollection = _peopleCollectionRepository.GetById(id);
            if (localPeopleCollection != null)
            {
                localPeopleCollection.PeopleCollectionPermission?.Clear();
                localPeopleCollection.PeopleCollectionsPeople?.Clear();
                var resultSave = _peopleCollectionRepository.DeletePeopleCollection(localPeopleCollection);
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
            return ProcessResult(!_peopleCollectionRepository.GetById(id)?.PeopleCollectionsPeople?.Any() ?? false);
        }

        public ResultDto<List<MultiSelectItemDto>> GetPeopleCollectionMultiSelect(int userId, List<int> peopleCollections)
        {
            LogginingService.LogInfo($"GetpeopleCollectionMultiSelect");
            Entities.Tenant.People people = _peopleRepository.GetByConfigId(userId);

            IEnumerable<BaseNameIdDto<int>> allKeyValues = GetAllKeyValues(people.Id);
            return GetMultiSelect(allKeyValues, peopleCollections);
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(int peopleId)
        {
            LogginingService.LogInfo($"Get people collections Key Value");
            var data = _peopleCollectionRepository.GetAllKeyValuesByPeopleId(peopleId);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        private IQueryable<PeopleCollectionBaseDto> OrderBy(IQueryable<PeopleCollectionBaseDto> query, PeopleCollectionFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }

        private PeopleCollections AssingPeople(PeopleCollections entity, IList<PeopleSelectableDto> people)
        {
            entity.PeopleCollectionsPeople?.Clear();
            if (people != null && people.Any())
            {
                entity.PeopleCollectionsPeople = entity.PeopleCollectionsPeople ?? new List<PeopleCollectionsPeople>();
                var peopleToAssing = _peopleRepository.GetByPeopleIds(people.Select(x => x.Id).ToList());
                var tempList = new List<PeopleCollectionsPeople>();
                foreach (var personResponsable in people)
                {
                    var person = peopleToAssing.Single(x => x.Id == personResponsable.Id);
                    tempList.Add(new PeopleCollectionsPeople()
                    {
                        People = person,
                    });
                }

                entity.PeopleCollectionsPeople = tempList;
            }
            return entity;
        }

        private PeopleCollections AssingPeopleAdmin(PeopleCollections entity, IList<PeopleSelectableDto> people)
        {
            entity.PeopleCollectionsAdmins?.Clear();
            if (people != null && people.Any())
            {
                entity.PeopleCollectionsAdmins = entity.PeopleCollectionsAdmins ?? new List<PeopleCollectionsAdmins>();
                var peopleToAssing = _peopleRepository.GetByPeopleIds(people.Select(x => x.Id).ToList());
                var tempList = new List<PeopleCollectionsAdmins>();
                foreach (var personResponsable in people)
                {
                    var person = peopleToAssing.Single(x => x.Id == personResponsable.Id);
                    tempList.Add(new PeopleCollectionsAdmins()
                    {
                        People = person,
                    });
                }
                entity.PeopleCollectionsAdmins = tempList;
            }
            return entity;
        }
    }
}