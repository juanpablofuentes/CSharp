using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CollectionTypeWorkOrders;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionTypeWorkOrders;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.CollectionTypeWorkOrders
{
    public class CollectionTypeWorkOrdersService : BaseService, ICollectionTypeWorkOrdersService
    {
        private readonly ICollectionTypeWorkOrdersRepository _collectionTypeWorkOrdersRepository;
        private readonly IWorkOrdersRepository _workOrdersRepository;
        private readonly IWorkOrderTypesRepository _workOrderTypesRepository;

        public CollectionTypeWorkOrdersService(ILoggingService logginingService,
            ICollectionTypeWorkOrdersRepository typeWorkOrdersRepository,
            IWorkOrdersRepository workOrdersRepository,
            IWorkOrderTypesRepository workOrderTypesRepository) : base(logginingService)
        {
            _collectionTypeWorkOrdersRepository = typeWorkOrdersRepository ?? throw new ArgumentNullException($"{nameof(ICollectionTypeWorkOrdersRepository)} is null");
            _workOrdersRepository = workOrdersRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrdersRepository)} is null");
            _workOrderTypesRepository = workOrderTypesRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderTypesRepository)} is null");
        }

        public ResultDto<IList<CollectionTypeWorkOrdersDto>> GetAllFiltered(CollectionTypeWorkOrdersFilterDto filter)
        {
            var query = _collectionTypeWorkOrdersRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToListDto());
        }

        public ResultDto<CollectionTypeWorkOrdersDetailDto> GetById(int id)
        {
            var localCollectionTypes = _collectionTypeWorkOrdersRepository.GetWorkOrderTypeById(id);
            return ProcessResult(localCollectionTypes.ToDto(), localCollectionTypes != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<CollectionTypeWorkOrdersDetailDto> Create(CollectionTypeWorkOrdersDetailDto model)
        {
            ResultDto<CollectionTypeWorkOrdersDetailDto> result = null;
            var entity = model.ToEntity();
            var resultRepository = _collectionTypeWorkOrdersRepository.CreateCollectionsTypesWorkOrders(entity);
            result = ProcessResult(resultRepository?.Entity?.ToDto(), resultRepository);
            return result;
        }

        public ResultDto<CollectionTypeWorkOrdersDetailDto> Update(CollectionTypeWorkOrdersDetailDto model)
        {
            ResultDto<CollectionTypeWorkOrdersDetailDto> result = null;

            var entity = _collectionTypeWorkOrdersRepository.GetWorkOrderTypeById(model.Id);
            if (entity != null)
            {
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.WorkOrderTypes = UpdateWorkOrderTypes(entity.WorkOrderTypes, model.WorkOrderTypes, entity);
                var resultRepository = _collectionTypeWorkOrdersRepository.UpdateCollectionsTypesWorkOrders(entity);
                result = ProcessResult(resultRepository?.Entity?.ToDto(), resultRepository);
            }
            return result ?? new ResultDto<CollectionTypeWorkOrdersDetailDto>()
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
            LogginingService.LogInfo($"Delete WO Type Collection by id {id}");
            ResultDto<bool> result = null;
            var entity = _collectionTypeWorkOrdersRepository.GetWorkOrderTypeById(id);
            if (entity != null)
            {
                if (entity.WorkOrderTypes.Any())
                {
                    foreach (var types in entity.WorkOrderTypes.ToList())
                    {
                        _workOrderTypesRepository.DeleteOnContext(types);
                    }
                }
                var resultSave = _collectionTypeWorkOrdersRepository.DeleteCollectionsTypesWorkOrders(entity);
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
            var collection = _collectionTypeWorkOrdersRepository.GetWorkOrderTypeAndProyectById(id);
            var workOrderTypes = CanDeleteTreeLevel(id);
            var result = collection != null && !collection.Projects.Any()
                ? workOrderTypes.Data
                : false;
            return ProcessResult(result);
        }

        public ResultDto<bool> CanDeleteTreeLevel(int id)
        {
            var result = false;
            var workOrderTypeIntoWorkOrder = _workOrdersRepository.ExistWorkOrderTypes(id);
            if (!workOrderTypeIntoWorkOrder)
            {
                var workOrderTypeChild = _workOrderTypesRepository.GetAllByWorkOrderTypesFatherId(id);
                result = workOrderTypeChild != null && workOrderTypeChild.Any() ? ChildAssignedToWorkOrderTypes(workOrderTypeChild) : true;
            }

            return ProcessResult(result);
        }

        public List<string> GetWorkOrderTypes(int initvalue, IList<WorkOrderTypeFatherDto> workOrderTypes)
        {
            List<string> ids = new List<string>();
            WorkOrderTypeFatherDto data = null;
            int? value = initvalue;
            do
            {
                data = FindElementOnTree(workOrderTypes, value.Value);
                if (data != null)
                {
                    value = data.FatherId;
                    ids.Add(data.Name);
                }
            }
            while (data !=null && data.FatherId.HasValue);
            return ids;
        }

        public IList<BaseNameIdDto<int>> GetAllWOTypesKeyValues(List<int?> IdsToMatch)
        {
            LogginingService.LogInfo($"Get GetAllWOTypesKeyValues");
            var data = _workOrderTypesRepository.GetAllByWorkOrderTypesByFathersIds(IdsToMatch);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetAllWorkOrderTypesKeyValues()
        {
            LogginingService.LogInfo($"Get Project Key Value");
            var data = _workOrderTypesRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get CollectionTypeWorkOrders Key Value");
            var data = _collectionTypeWorkOrdersRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public ResultDto<List<MultiSelectItemDto>> GetWorkOrderTypesMultiSelect(List<int> selectItems)
        {
            LogginingService.LogInfo($"GetWorkOrderTypesMultiSelect");
            IEnumerable<BaseNameIdDto<int>> allTypes = GetAllWorkOrderTypesKeyValues();
            return GetMultiSelect(allTypes, selectItems);
        }

        public string GetNamesComaSeparated(List<int> ids)
        {
            List<string> names = _workOrderTypesRepository.GetByIds(ids).Select(x => x.Name).ToList();
            return names.Aggregate((i, j) => $"{i}, {j}");
        }

        private WorkOrderTypeFatherDto FindElementOnTree(IList<WorkOrderTypeFatherDto> valuesOnFind, int id)
        {
            for (var i = 0; i < valuesOnFind.Count; i++)
            {
                if (valuesOnFind[i].Id == id)
                {
                    return valuesOnFind[i];
                }
                else if (valuesOnFind[i].Childs != null && valuesOnFind[i].Childs.Count > 0)
                {
                    var item = FindElementOnTree(valuesOnFind[i].Childs, id);
                    if (item != null)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        private bool ChildAssignedToWorkOrderTypes(IQueryable<WorkOrderTypes> workOrderTypes)
        {
            var result = false;
            foreach (var level in workOrderTypes)
            {
                result = _workOrdersRepository.ExistWorkOrderTypes(level.Id);
                if (!result)
                {
                    var child = _workOrderTypesRepository.GetAllByWorkOrderTypesFatherId(level.Id);
                    if (child != null && child.Any())
                    {
                        result = ChildAssignedToWorkOrderTypes(child);
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private ICollection<WorkOrderTypes> UpdateWorkOrderTypes(ICollection<WorkOrderTypes> localWorkOrderTypes,
                                                                IList<WorkOrderTypeDto> nodesWorkOrderTypes,
                                                                CollectionsTypesWorkOrders entity)
        {
            var planeNodesTarget = ConvertTreeToList(nodesWorkOrderTypes, null);
            var nodesToUpdate = planeNodesTarget?.Where(x => x.Id != 0);
            var nodesToDelete = localWorkOrderTypes?.Where(x => planeNodesTarget == null || !planeNodesTarget.Any(y => y.Id == x.Id));
            var nodesToAdd = FindNewNodes(nodesWorkOrderTypes);
            UpdateNodes(nodesToUpdate, localWorkOrderTypes);
            DeleteNodes(nodesToDelete, localWorkOrderTypes);
            AddNodes(nodesToAdd, localWorkOrderTypes, entity);
            return localWorkOrderTypes;
        }

        private void AddNodes(IList<WorkOrderTypeExtendedDto> nodesToAdd,
                              ICollection<WorkOrderTypes> localWorkOrderTypes,
                              CollectionsTypesWorkOrders entity)
        {
            foreach (var node in nodesToAdd)
            {
                var newNode = node.ToEntity(entity);
                newNode.UpdateDate = DateTime.UtcNow;

                if (node.Parent != null)
                {
                    var parent = localWorkOrderTypes.SingleOrDefault(x => x.Id == node.Parent.Id);
                    if (parent != null)
                    {
                        parent.InverseWorkOrderTypesFather = parent.InverseWorkOrderTypesFather ?? new List<WorkOrderTypes>();
                        parent.InverseWorkOrderTypesFather.Add(newNode);
                    }
                }
                else
                {
                    localWorkOrderTypes.Add(newNode);
                }
            }
        }

        private IList<WorkOrderTypeExtendedDto> FindNewNodes(IList<WorkOrderTypeDto> nodesWorkOrderTypes, WorkOrderTypeDto parent = null)
        {
            var list = new List<WorkOrderTypeExtendedDto>();
            if (nodesWorkOrderTypes != null && nodesWorkOrderTypes.Any())
            {
                foreach (var node in nodesWorkOrderTypes)
                {
                    if (node.Id != 0)
                    {
                        var newChildNodes = FindNewNodes(node.Childs?.ToList(), node);
                        if (newChildNodes != null && newChildNodes.Any())
                        {
                            list.AddRange(newChildNodes);
                        }
                    }
                    else
                    {
                        list.Add(new WorkOrderTypeExtendedDto()
                        {
                            Name = node.Name,
                            Parent = parent,
                            Description = node.Description,
                            Childs = node.Childs,
                            Serie = node.Serie
                        });
                    }
                }
            }

            return list;
        }

        private void DeleteNodes(IEnumerable<WorkOrderTypes> nodesToDelete, ICollection<WorkOrderTypes> collection)
        {
            if (nodesToDelete != null && nodesToDelete.Any())
            {
                ToSoftDelete(nodesToDelete);
            }
        }

        private void ToSoftDelete(IEnumerable<WorkOrderTypes> itemToSoftDelete)
        {
            foreach (var nodeToDelete in itemToSoftDelete)
            {
                var nodeToSoftDelete = _workOrderTypesRepository.GetById(nodeToDelete.Id);
                if (nodeToSoftDelete != null)
                {
                    nodeToSoftDelete.IsDeleted = true;
                }
            }
        }

        private void UpdateNodes(IEnumerable<WorkOrderTypeDto> nodesSource, ICollection<WorkOrderTypes> nodesTarget)
        {
            if (nodesSource != null && nodesSource.Any())
            {
                foreach (var node in nodesSource)
                {
                    var nodeToUpdate = nodesTarget.SingleOrDefault(x => x.Id == node.Id);
                    if (nodeToUpdate != null)
                    {
                        nodeToUpdate.Name = node.Name;
                        nodeToUpdate.Description = node.Description;
                        nodeToUpdate.UpdateDate = DateTime.UtcNow;
                        nodeToUpdate.Serie = node.Serie;
                    }
                }
            }
        }

        private List<WorkOrderTypeExtendedDto> ConvertTreeToList(IList<WorkOrderTypeDto> elements, WorkOrderTypeExtendedDto parent)
        {
            List<WorkOrderTypeExtendedDto> result = null;
            if (elements != null && elements.Any())
            {
                result = new List<WorkOrderTypeExtendedDto>();
                var childs = new List<WorkOrderTypeExtendedDto>();
                var elementsToPlane = elements.Select(x => new WorkOrderTypeExtendedDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Serie = x.Serie,
                    Parent = parent,
                    Childs = x.Childs,
                });
                foreach (var element in elementsToPlane.Where(x => x.Childs != null && x.Childs.Any()))
                {
                    childs.AddRange(ConvertTreeToList(element.Childs, element));
                    element.Childs = null;
                }
                result.AddRange(elementsToPlane);
                result.AddRange(childs);
            }
            return result;
        }

        private IQueryable<CollectionsTypesWorkOrders> OrderBy(IQueryable<CollectionsTypesWorkOrders> data, CollectionTypeWorkOrdersFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }
    }
}