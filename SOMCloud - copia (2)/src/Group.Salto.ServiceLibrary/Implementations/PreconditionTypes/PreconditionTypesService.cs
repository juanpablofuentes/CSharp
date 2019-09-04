using Group.Salto.Common.Constants.LiteralPrecondition;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Trigger;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.PreconditionsTypes;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.PreconditionTypes
{
    public class PreconditionTypesService : BaseService, IPreconditionTypesService
    {
        private readonly IPreconditionTypesRepository _preconditionTypesRepository;
        private readonly IPreconditionsRepository _preconditionsRepository;

        public PreconditionTypesService(ILoggingService logginingService,
                             IPreconditionsRepository preconditionsRepository,
                             IPreconditionTypesRepository preconditionTypesRepository) : base(logginingService)
        {
            _preconditionTypesRepository = preconditionTypesRepository ?? throw new ArgumentNullException($"{nameof(IPreconditionTypesRepository)} is null ");
            _preconditionsRepository = preconditionsRepository ?? throw new ArgumentNullException($"{nameof(IPreconditionsRepository)} is null ");
        }

        public IList<BaseNameIdDto<Guid>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get PreconditionTypes Key Value");
            var data = _preconditionTypesRepository.GetAll().OrderBy(x => x.Name);

            return data.Select(x => new BaseNameIdDto<Guid>()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public List<PreconditionsTypesDto> GetAll()
        {
            LogginingService.LogInfo($"Get PreconditionTypes Key Value");
            var data = _preconditionTypesRepository.GetAll();

            return data.ToListDto();
        }

        public PreconditionsTypesDto GetById(Guid id)
        {
            LogginingService.LogInfo($"Get PreconditionTypes By Id");
            var data = _preconditionTypesRepository.GetById(id);
            return data.ToDto();
        }

        public PreconditionsTypesDto GetByName(string name)
        {
            LogginingService.LogInfo($"Get PreconditionTypes By Name");
            var data = _preconditionTypesRepository.GetByName(name);
            return data.ToDto();
        }

        public IList<BaseNameIdDto<Guid>> GetAllKeyValuesByPrecondition(int preconditionId)
        {
            LogginingService.LogInfo($"Get PreconditionTypes Key Value");

            var AllTypes = _preconditionTypesRepository.GetAll().OrderBy(x=>x.Name).Select(x => new BaseNameIdDto<Guid>()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            var types = DeleteRelatedTypes(AllTypes);

            if (preconditionId != 0)
            {
                var precondition = _preconditionsRepository.GetById(preconditionId);

                var literals = precondition?.LiteralsPreconditions?.Select(x => new BaseNameIdDto<Guid>()
                {
                    Id = x.PreconditionsTypeId,
                    Name = x.NomCampModel,
                });

                if (literals != null)
                {
                    List<BaseNameIdDto<Guid>> resTypes = new List<BaseNameIdDto<Guid>>();
                    foreach (var type in types)
                    {
                        if (!literals.Any(x => x.Id == type.Id))
                        {
                            resTypes.Add(type);
                        }
                    }
                    resTypes = AddRelatedTypes(resTypes, literals);
                    return resTypes;
                }
                return types;
            }
            else
            {
                return types;
            }
        }

        private List<BaseNameIdDto<Guid>> DeleteRelatedTypes(List<BaseNameIdDto<Guid>> types)
        {
            types.Remove(types.Where(x => x.Name == LiteralPreconditionConstants.WoTypeN2).FirstOrDefault());
            types.Remove(types.Where(x => x.Name == LiteralPreconditionConstants.WoTypeN3).FirstOrDefault());
            types.Remove(types.Where(x => x.Name == LiteralPreconditionConstants.WoTypeN4).FirstOrDefault());
            types.Remove(types.Where(x => x.Name == LiteralPreconditionConstants.WoTypeN5).FirstOrDefault());
            types.Remove(types.Where(x => x.Name == LiteralPreconditionConstants.FinalClientLocation).FirstOrDefault());
            types.Remove(types.Where(x => x.Name == LiteralPreconditionConstants.Asset).FirstOrDefault());

            return types;
        }

        private List<BaseNameIdDto<Guid>> AddRelatedTypes(List<BaseNameIdDto<Guid>> types, IEnumerable<BaseNameIdDto<Guid>> literals)
        {
            var allTypes = _preconditionTypesRepository.GetAll().OrderBy(x => x.Name).Select(x => new BaseNameIdDto<Guid>()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            var finalClientTypeId = allTypes.Where(y => y.Name == LiteralPreconditionConstants.FinalClient).Select(y => y.Id).FirstOrDefault();
            var finalClientLocationTypeId = allTypes.Where(y => y.Name == LiteralPreconditionConstants.FinalClientLocation).Select(y => y.Id).FirstOrDefault();

            var woType1Id = allTypes.Where(y => y.Name == LiteralPreconditionConstants.WoTypeN1).Select(y => y.Id).FirstOrDefault();
            var woType2Id = allTypes.Where(y => y.Name == LiteralPreconditionConstants.WoTypeN2).Select(y => y.Id).FirstOrDefault();
            var woType3Id = allTypes.Where(y => y.Name == LiteralPreconditionConstants.WoTypeN3).Select(y => y.Id).FirstOrDefault();
            var woType4Id = allTypes.Where(y => y.Name == LiteralPreconditionConstants.WoTypeN4).Select(y => y.Id).FirstOrDefault();
            var woType5Id = allTypes.Where(y => y.Name == LiteralPreconditionConstants.WoTypeN5).Select(y => y.Id).FirstOrDefault();

            if (literals.Any(x => x.Id == finalClientLocationTypeId))
            {
                types.Add(allTypes.Where(x => x.Name == LiteralPreconditionConstants.Asset).FirstOrDefault());
            }
            else if (literals.Any(x => x.Id == finalClientTypeId))
            {
                types.Add(allTypes.Where(x => x.Name == LiteralPreconditionConstants.FinalClientLocation).FirstOrDefault());
            }

            if (literals.Any(x => x.Id == woType4Id))
            {
                types.Add(allTypes.Where(x => x.Name == LiteralPreconditionConstants.WoTypeN5).FirstOrDefault());
            }
            else if(literals.Any(x => x.Id == woType3Id))
            {
                types.Add(allTypes.Where(x => x.Name == LiteralPreconditionConstants.WoTypeN4).FirstOrDefault());
            }
            else if (literals.Any(x => x.Id == woType2Id))
            {
                types.Add(allTypes.Where(x => x.Name == LiteralPreconditionConstants.WoTypeN3).FirstOrDefault());
            }
            else if (literals.Any(x => x.Id == woType1Id))
            {
                types.Add(allTypes.Where(x => x.Name == LiteralPreconditionConstants.WoTypeN2).FirstOrDefault());
            }

            return types;
        }
    }
}