using Group.Salto.DataAccess.Tenant.Repositories;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Postcondition;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.PostconditionsTypes;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.PostconditionTypes
{
    public class PostconditionTypesService : BaseService, IPostconditionTypesService
    {
        private readonly IPostconditionTypesRepository _postconditionTypesRepository;
        private readonly IPostconditionsCollectionRepository _postconditionsRepository;

        public PostconditionTypesService(ILoggingService logginingService,
                            IPreconditionTypesRepository preconditionTypesRepository,
                            IPostconditionsCollectionRepository postconditionsRepository,
                            IPostconditionTypesRepository postconditionTypesRepository) : base(logginingService)
        {
            _postconditionTypesRepository = postconditionTypesRepository ?? throw new ArgumentNullException($"{nameof(IPostconditionTypesRepository)} is null");
            _postconditionsRepository = postconditionsRepository ?? throw new ArgumentNullException($"{nameof(IPostconditionsCollectionRepository)} is null");
        }

        public IList<BaseNameIdDto<Guid>> GetAllKeyValuesByPostcondition(int id)
        {
            LogginingService.LogInfo($"Get PreconditionTypes Key Value");
            var allTypes = _postconditionTypesRepository.GetAll().ToListDto();

            allTypes = DeleteRelatedTypes(allTypes);

            if (id != 0)
            {
                var postconditionCollection = _postconditionsRepository.GetById(id);
                var postconditionTypes = postconditionCollection?.Postconditions?.Select(x => new BaseNameIdDto<Guid>()
                {
                    Id = x.PostconditionsTypeId,
                    Name = x.NameFieldModel,
                });

                if (postconditionTypes != null)
                {
                    List<PostconditionsTypesDto> resTypes = new List<PostconditionsTypesDto>();
                    foreach (var type in allTypes)
                    {
                        if (!postconditionTypes.Any(x => x.Id == type.Id))
                        {
                            resTypes.Add(type);
                        }
                    }
                    resTypes = AddRelatedTypes(resTypes, postconditionTypes);
                    return resTypes.Select(x => new BaseNameIdDto<Guid>()
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToList();
                }
            }
            
            return allTypes.Select(x => new BaseNameIdDto<Guid>()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList(); 
        }

        public IList<BaseNameIdDto<Guid>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get PreconditionTypes Key Value");
            var allTypes = _postconditionTypesRepository.GetAll().Select(x => new BaseNameIdDto<Guid>()
            {
                Id = x.Id,
                Name = x.Name,
            }).OrderBy(x => x.Name).ToList();

            return allTypes;
        }

        public List<PostconditionsTypesDto> GetAll()
        {
            LogginingService.LogInfo($"Get PostconditionTypes Get All");
            var data = _postconditionTypesRepository.GetAll();

            return data.ToListDto();
        }

        public PostconditionsTypesDto GetPostconditionsTypeByName(string name)
        {
            LogginingService.LogInfo($"Get PostconditionTypes by Name");
            var postconditionsTypes = _postconditionTypesRepository.GetByName(name);
            return postconditionsTypes.ToDto();
        }

        private List<PostconditionsTypesDto> DeleteRelatedTypes(List<PostconditionsTypesDto> types)
        {
            types.Remove(types.Where(x => x.Description == nameof(PostconditionActionTypeEnum.TipusOTN2)).FirstOrDefault());
            types.Remove(types.Where(x => x.Description == nameof(PostconditionActionTypeEnum.TipusOTN3)).FirstOrDefault());
            types.Remove(types.Where(x => x.Description == nameof(PostconditionActionTypeEnum.TipusOTN4)).FirstOrDefault());
            types.Remove(types.Where(x => x.Description == nameof(PostconditionActionTypeEnum.TipusOTN5)).FirstOrDefault());

            return types;
        }

        private List<PostconditionsTypesDto> AddRelatedTypes(List<PostconditionsTypesDto> types, IEnumerable<BaseNameIdDto<Guid>> postconditionTypes)
        {
            var allTypes = _postconditionTypesRepository.GetAll();
            
            var woType1Id = allTypes.Where(y => y.Description == nameof(PostconditionActionTypeEnum.TipusOTN1)).Select(y => y.Id).FirstOrDefault();
            var woType2Id = allTypes.Where(y => y.Description == nameof(PostconditionActionTypeEnum.TipusOTN2)).Select(y => y.Id).FirstOrDefault();
            var woType3Id = allTypes.Where(y => y.Description == nameof(PostconditionActionTypeEnum.TipusOTN3)).Select(y => y.Id).FirstOrDefault();
            var woType4Id = allTypes.Where(y => y.Description == nameof(PostconditionActionTypeEnum.TipusOTN4)).Select(y => y.Id).FirstOrDefault();
            var woType5Id = allTypes.Where(y => y.Description == nameof(PostconditionActionTypeEnum.TipusOTN5)).Select(y => y.Id).FirstOrDefault();

            if (postconditionTypes.Any(x => x.Id == woType4Id))
            {
                types.Add(allTypes.Where(x => x.Id == woType5Id).FirstOrDefault().ToDto());
            }
            else if (postconditionTypes.Any(x => x.Id == woType3Id))
            {
                types.Add(allTypes.Where(x => x.Id == woType4Id).FirstOrDefault().ToDto());
            }
            else if (postconditionTypes.Any(x => x.Id == woType2Id))
            {
                types.Add(allTypes.Where(x => x.Id == woType3Id).FirstOrDefault().ToDto());
            }
            else if (postconditionTypes.Any(x => x.Id == woType1Id))
            {
                types.Add(allTypes.Where(x => x.Id == woType2Id).FirstOrDefault().ToDto());
            }

            return types;
        }
    }
}