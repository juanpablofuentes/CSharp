using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ClosureCode;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ClosingCode;
using Group.Salto.ServiceLibrary.Common.Dtos.ClosureCode;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.ClosureCode
{
    public class ClosureCodeService : BaseService, IClosureCodeService
    {
        private readonly ICollectionsClosureCodesRepository _collectionsClosureCodesRepository;

        public ClosureCodeService(ILoggingService logginingService, ICollectionsClosureCodesRepository collectionsClosureCodesRepository) : base(logginingService)
        {
            _collectionsClosureCodesRepository = collectionsClosureCodesRepository ?? throw new ArgumentNullException($"{nameof(ICollectionsClosureCodesRepository)}");
        }

        public ResultDto<IList<ClosureCodeBaseDto>> GetAllFiltered(ClosureCodeFilterDto filter)
        {
            var query = _collectionsClosureCodesRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToBaseDto());
        }

        public ResultDto<ClosureCodeDto> GetById(int id)
        {
            var localClosureCode = _collectionsClosureCodesRepository.GetById(id);
            if (localClosureCode != null)
            {
                localClosureCode.ClosingCodes = localClosureCode.ClosingCodes.Where(x => !x.IsDeleted).ToList();
            }
            return ProcessResult(localClosureCode.ToDto(), localClosureCode != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<ClosureCodeDto> Create(ClosureCodeDto model)
        {
            ResultDto<ClosureCodeDto> result = null;
            var entity = model.ToEntity();
            var resultRepository = _collectionsClosureCodesRepository.CreateClosureCode(entity);
            result = ProcessResult(resultRepository?.Entity?.ToDto(), resultRepository);
            return result;
        }

        public ResultDto<ClosureCodeDto> Update(ClosureCodeDto model)
        {
            ResultDto<ClosureCodeDto> result = null;

            var entity = _collectionsClosureCodesRepository.GetById(model.Id);
            if (entity != null)
            {
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.ClosingCodes = UpdateClosingCodes(entity.ClosingCodes, model.ClosingCodes, entity);
                var resultRepository = _collectionsClosureCodesRepository.UpdateClosureCode(entity);
                result = ProcessResult(resultRepository?.Entity?.ToDto(), resultRepository);
            }
            return result ?? new ResultDto<ClosureCodeDto>()
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
            LogginingService.LogInfo($"Delete ClosureCode by id {id}");
            ResultDto<bool> result = null;
            var entity = _collectionsClosureCodesRepository.GetById(id);
            if (entity != null)
            {
                entity.ClosingCodes?.Clear();
                entity.Projects?.Clear();
                var resultSave = _collectionsClosureCodesRepository.DeleteClosureCode(entity);
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
            var closureCode = _collectionsClosureCodesRepository.GetById(id);
            var result = (!closureCode?.Projects?.Any() ?? true)
                            && (!closureCode?.ClosingCodes?.Any() ?? true);
            return ProcessResult(result);
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get ClosureCode Key Value");
            var data = _collectionsClosureCodesRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        private ICollection<ClosingCodes> UpdateClosingCodes(ICollection<ClosingCodes> localClosingCodes,
                                                             IList<ClosingCodeDto> modesClosingCodes, CollectionsClosureCodes entity)
        {
            var planeNodesTarget = ConvertTreeToList(modesClosingCodes, null);
            var nodesToUpdate = planeNodesTarget?.Where(x => x.Id != 0);
            var nodesToDelete = localClosingCodes?.Where(x => planeNodesTarget == null || !planeNodesTarget.Any(y => y.Id == x.Id));
            var nodesToAdd = FindNewNodes(modesClosingCodes);
            UpdateNodes(nodesToUpdate, localClosingCodes);
            DeleteNodes(nodesToDelete, localClosingCodes);
            AddNodes(nodesToAdd, localClosingCodes, entity);
            return localClosingCodes;
        }

        private void AddNodes(IList<ClosingCodeExtendedDto> nodesToAdd,
                                ICollection<ClosingCodes> localClosingCodes, CollectionsClosureCodes entity)
        {
            foreach (var node in nodesToAdd)
            {
                var newNode = node.ToEntity(entity);
                newNode.UpdateDate = DateTime.UtcNow;

                if (node.Parent != null)
                {
                    var parent = localClosingCodes.SingleOrDefault(x => x.Id == node.Parent.Id);
                    if (parent != null)
                    {
                        parent.InverseClosingCodesFather = parent.InverseClosingCodesFather ?? new List<ClosingCodes>();
                        parent.InverseClosingCodesFather.Add(newNode);
                    }
                }
                else
                {
                    localClosingCodes.Add(newNode);
                }
            }
        }

        private IList<ClosingCodeExtendedDto> FindNewNodes(IList<ClosingCodeDto> nodesClosingCodes, ClosingCodeDto parent = null)
        {
            var list = new List<ClosingCodeExtendedDto>();
            if (nodesClosingCodes != null && nodesClosingCodes.Any())
            {
                foreach (var node in nodesClosingCodes)
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
                        list.Add(new ClosingCodeExtendedDto()
                        {
                            Name = node.Name,
                            Parent = parent,
                            Description = node.Description,
                            Childs = node.Childs
                        });
                    }
                }
            }

            return list;
        }

        private void DeleteNodes(IEnumerable<ClosingCodes> nodesToDelete, ICollection<ClosingCodes> collection)
        {
            if (nodesToDelete != null && nodesToDelete.Any())
            {
                var nodesToDeleteId = nodesToDelete.Select(x => x.Id).ToList();
                foreach (var nodeToDeleteId in nodesToDeleteId)
                {
                    var nodeToRemove = collection.SingleOrDefault(x => x.Id == nodeToDeleteId);
                    if (nodeToRemove != null)
                    {
                        collection.Remove(nodeToRemove);
                    }
                }
            }
        }

        private void UpdateNodes(IEnumerable<ClosingCodeDto> nodesSource, ICollection<ClosingCodes> nodesTarget)
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
                    }
                }
            }
        }

        private IQueryable<CollectionsClosureCodes> OrderBy(IQueryable<CollectionsClosureCodes> query, ClosureCodeFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }

        private List<ClosingCodeExtendedDto> ConvertTreeToList(IList<ClosingCodeDto> elements,
                                                                ClosingCodeExtendedDto parent)
        {
            List<ClosingCodeExtendedDto> result = null;
            if (elements != null && elements.Any())
            {
                result = new List<ClosingCodeExtendedDto>();
                var childs = new List<ClosingCodeExtendedDto>();
                var elementsToPlane = elements.Select(x => new ClosingCodeExtendedDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
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
    }
}