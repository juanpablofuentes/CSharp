using Group.Salto.Common;
using Group.Salto.Common.Constants.LiteralPrecondition;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.Project;
using Group.Salto.ServiceLibrary.Common.Contracts.Trigger;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.PreconditionLiteralValues;
using Group.Salto.ServiceLibrary.Common.Dtos.Preconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.PreconditionsTypes;
using Group.Salto.ServiceLibrary.Common.Dtos.SitesFinalClients;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.LiteralPreconditions
{
    public class LiteralPreconditionsService : BaseService, ILiteralPreconditionsService
    {
        private readonly ILiteralPreconditionsRepository _literalPreconditionsRepository;
        private readonly IProjectsService _projectsService;
        private readonly IPreconditionTypesService _preconditionTypesService;
        private readonly ILiteralQueryFactory _literalQueryFactory;
        private readonly IPreconditionsLiteralValuesRepository _literalValuesRepository;
        private readonly IPreconditionsRepository _preconditionsRepository;
        
        public LiteralPreconditionsService(ILoggingService logginingService,
                                ILiteralQueryFactory literalQueryFactory,
                                ILiteralPreconditionsRepository literalPreconditionsRepository,
                                IPreconditionTypesService preconditionTypesService,
                                IPreconditionsLiteralValuesRepository literalValuesRepository,
                                IPreconditionsRepository preconditionsRepository,
                                IProjectsService projectsService) : base(logginingService)
        {
            _literalPreconditionsRepository = literalPreconditionsRepository ?? throw new ArgumentNullException(nameof(ILiteralPreconditionsRepository));
            _projectsService = projectsService ?? throw new ArgumentNullException(nameof(IProjectsService));
            _preconditionTypesService = preconditionTypesService ?? throw new ArgumentNullException(nameof(IPreconditionTypesService));
            _literalQueryFactory = literalQueryFactory ?? throw new ArgumentNullException(nameof(ILiteralQueryFactory));
            _literalValuesRepository = literalValuesRepository ?? throw new ArgumentNullException(nameof(IPreconditionsLiteralValuesRepository));
            _preconditionsRepository = preconditionsRepository ?? throw new ArgumentNullException(nameof(IPreconditionsRepository));
        }

        public LiteralPreconditionsDto GetLiteralPrecondition(int id)
        {
            LogginingService.LogInfo($"Get LiteraPreconditions by id");
            var preconditionTypes = _preconditionTypesService.GetAll();
            var result = _literalPreconditionsRepository.GetLiteralPreconditions(id).ToDto(preconditionTypes);

            return result;
        }

        public List<MultiSelectItemDto> GetLiteralValuesList(FilterQueryParametersBase filterQueryParameters)
        {
            LogginingService.LogInfo($"Get LiteralValues for LiteralPrecondition {filterQueryParameters.LiteralPreconditionId}");

            IList<BaseNameIdDto<int>> literalValuesBaseNameId;
            var query = _literalQueryFactory.GetQuery(filterQueryParameters.LiteralPreconditionTypeName);

            if (filterQueryParameters.LiteralPreconditionTypeName == LiteralPreconditionConstants.WoCategory) /*WoCategory*/
            {
                PreconditionsTypesDto type = _preconditionTypesService.GetByName(LiteralPreconditionConstants.Project);
                IList<PreconditionLiteralValuesDto> myPreconditionLv = _literalPreconditionsRepository.GetLiteralValuesOfPrecondition(filterQueryParameters.PreconditionId, type.Id).ToDto(type.Description);

                WoCategoryQueryParameters woCategoryQueryParameters = new WoCategoryQueryParameters()
                {
                    LanguageId = filterQueryParameters.LanguageId,
                    LiteralPreconditionId = filterQueryParameters.LiteralPreconditionId,
                    PreconditionId = filterQueryParameters.PreconditionId,
                    ProjectsIdsToMatch = new List<int>(),
                    UserId = filterQueryParameters.UserId,
                };
                woCategoryQueryParameters.ProjectsIdsToMatch.AddRange(myPreconditionLv.Select(x => x.TypeId).ToList());
                literalValuesBaseNameId = query.GetAllKeyValues(woCategoryQueryParameters);
            }
            else if (filterQueryParameters.LiteralPreconditionTypeName == LiteralPreconditionConstants.FinalClientLocation) /*FinalClientLocation*/
            {
                PreconditionsTypesDto type = _preconditionTypesService.GetByName(LiteralPreconditionConstants.FinalClient);
                IList<PreconditionLiteralValuesDto> myPreconditionLv = _literalPreconditionsRepository.GetLiteralValuesOfPrecondition(filterQueryParameters.PreconditionId, type.Id).ToDto(type.Description);
                SitesFinalClientsQueryParameters sitesFinalClientsQueryParameters = new SitesFinalClientsQueryParameters()
                {
                    LanguageId = filterQueryParameters.LanguageId,
                    LiteralPreconditionId = filterQueryParameters.LiteralPreconditionId,
                    PreconditionId = filterQueryParameters.PreconditionId,
                    FinalClientIdsToMatch = new List<int>(),
                };
                sitesFinalClientsQueryParameters.FinalClientIdsToMatch.AddRange(myPreconditionLv.Select(x => x.TypeId).ToList());
                literalValuesBaseNameId = query.GetAllKeyValues(sitesFinalClientsQueryParameters);
            }
            else if (filterQueryParameters.LiteralPreconditionTypeName == LiteralPreconditionConstants.Asset) /*Assets*/
            {
                PreconditionsTypesDto type = _preconditionTypesService.GetByName(LiteralPreconditionConstants.FinalClientLocation);
                IList<PreconditionLiteralValuesDto> myPreconditionLv = _literalPreconditionsRepository.GetLiteralValuesOfPrecondition(filterQueryParameters.PreconditionId, type.Id).ToDto(type.Description);
                AssetQueryParameters assetQueryParameters = new AssetQueryParameters()
                {
                    LanguageId = filterQueryParameters.LanguageId,
                    LiteralPreconditionId = filterQueryParameters.LiteralPreconditionId,
                    PreconditionId = filterQueryParameters.PreconditionId,
                    LocationFinalClientIdsToMatch = new List<int>()
                };
                assetQueryParameters.LocationFinalClientIdsToMatch.AddRange(myPreconditionLv.Select(x => x.TypeId).ToList());
                literalValuesBaseNameId = query.GetAllKeyValues(assetQueryParameters);
            }
            else if (filterQueryParameters.LiteralPreconditionTypeName == LiteralPreconditionConstants.WoTypeN1) /*WoTypeN1*/
            {
                WOTypeQueryParameters wOTypeQueryParameters = new WOTypeQueryParameters()
                {
                    LanguageId = filterQueryParameters.LanguageId,
                    LiteralPreconditionId = filterQueryParameters.LiteralPreconditionId,
                    PreconditionId = filterQueryParameters.PreconditionId,
                    WOTypeIdsToMatch = new List<int?>(),
                };
                wOTypeQueryParameters.WOTypeIdsToMatch.Add(null);
                literalValuesBaseNameId = query.GetAllKeyValues(wOTypeQueryParameters);
            }
            else if(filterQueryParameters.LiteralPreconditionTypeName == LiteralPreconditionConstants.WoTypeN2) /*WoTypeN2*/
            {
                PreconditionsTypesDto type = _preconditionTypesService.GetByName(LiteralPreconditionConstants.WoTypeN1);
                IList<PreconditionLiteralValuesDto> myPreconditionLv = _literalPreconditionsRepository.GetLiteralValuesOfPrecondition(filterQueryParameters.PreconditionId, type.Id).ToDto(type.Description);
                WOTypeQueryParameters wOTypeQueryParameters = new WOTypeQueryParameters()
                {
                    LanguageId = filterQueryParameters.LanguageId,
                    LiteralPreconditionId = filterQueryParameters.LiteralPreconditionId,
                    PreconditionId = filterQueryParameters.PreconditionId,
                    WOTypeIdsToMatch = new List<int?>(),
                };
                wOTypeQueryParameters.WOTypeIdsToMatch.AddRange(myPreconditionLv.Select(x => (int?)x.TypeId).ToList());
                literalValuesBaseNameId = query.GetAllKeyValues(wOTypeQueryParameters);
            }
            else if(filterQueryParameters.LiteralPreconditionTypeName == LiteralPreconditionConstants.WoTypeN3) /*WoTypeN3*/
            {
                PreconditionsTypesDto type = _preconditionTypesService.GetByName(LiteralPreconditionConstants.WoTypeN2);
                IList<PreconditionLiteralValuesDto> myPreconditionLv = _literalPreconditionsRepository.GetLiteralValuesOfPrecondition(filterQueryParameters.PreconditionId, type.Id).ToDto(type.Description);
                WOTypeQueryParameters wOTypeQueryParameters = new WOTypeQueryParameters()
                {
                    LanguageId = filterQueryParameters.LanguageId,
                    LiteralPreconditionId = filterQueryParameters.LiteralPreconditionId,
                    PreconditionId = filterQueryParameters.PreconditionId,
                    WOTypeIdsToMatch = new List<int?>(),
                };
                wOTypeQueryParameters.WOTypeIdsToMatch.AddRange(myPreconditionLv.Select(x => (int?)x.TypeId).ToList());
                literalValuesBaseNameId = query.GetAllKeyValues(wOTypeQueryParameters);
            }
            else if (filterQueryParameters.LiteralPreconditionTypeName == LiteralPreconditionConstants.WoTypeN4) /*WoTypeN4*/
            {
                PreconditionsTypesDto type = _preconditionTypesService.GetByName(LiteralPreconditionConstants.WoTypeN3);
                IList<PreconditionLiteralValuesDto> myPreconditionLv = _literalPreconditionsRepository.GetLiteralValuesOfPrecondition(filterQueryParameters.PreconditionId, type.Id).ToDto(type.Description);
                WOTypeQueryParameters wOTypeQueryParameters = new WOTypeQueryParameters()
                {
                    LanguageId = filterQueryParameters.LanguageId,
                    LiteralPreconditionId = filterQueryParameters.LiteralPreconditionId,
                    PreconditionId = filterQueryParameters.PreconditionId,
                    WOTypeIdsToMatch = new List<int?>(),
                };
                wOTypeQueryParameters.WOTypeIdsToMatch.AddRange(myPreconditionLv.Select(x => (int?)x.TypeId).ToList());
                literalValuesBaseNameId = query.GetAllKeyValues(wOTypeQueryParameters);
            }
            else if (filterQueryParameters.LiteralPreconditionTypeName == LiteralPreconditionConstants.WoTypeN5) /*WoTypeN5*/
            {
                PreconditionsTypesDto type = _preconditionTypesService.GetByName(LiteralPreconditionConstants.WoTypeN4);
                IList<PreconditionLiteralValuesDto> myPreconditionLv = _literalPreconditionsRepository.GetLiteralValuesOfPrecondition(filterQueryParameters.PreconditionId, type.Id).ToDto(type.Description);
                WOTypeQueryParameters wOTypeQueryParameters = new WOTypeQueryParameters()
                {
                    LanguageId = filterQueryParameters.LanguageId,
                    LiteralPreconditionId = filterQueryParameters.LiteralPreconditionId,
                    PreconditionId = filterQueryParameters.PreconditionId,
                    WOTypeIdsToMatch = new List<int?>(),
                    UserId = filterQueryParameters.UserId,
                };
                wOTypeQueryParameters.WOTypeIdsToMatch.AddRange(myPreconditionLv.Select(x => (int?)x.TypeId).ToList());
                literalValuesBaseNameId = query.GetAllKeyValues(wOTypeQueryParameters);

            }else /* BASE FILTERS*/
            {
                literalValuesBaseNameId = query.GetAllKeyValues(filterQueryParameters);
            }

            LiteralPreconditionsDto literalPrecondition = null;            
            literalPrecondition = GetLiteralPrecondition(filterQueryParameters.LiteralPreconditionId);

            List<MultiSelectItemDto> multiSelectItemDto = new List<MultiSelectItemDto>();
            foreach (BaseNameIdDto<int> literalValue in literalValuesBaseNameId)
            {
                bool isCheck = false;
                if (literalPrecondition != null) {
                    isCheck = literalPrecondition.PreconditionsLiteralValues.Any(x => x.TypeId == literalValue.Id);
                }

                multiSelectItemDto.Add(new MultiSelectItemDto()
                {
                    LabelName = literalValue.Name,
                    Value = literalValue.Id.ToString(),
                    IsChecked = isCheck
                });
            }
            
            return multiSelectItemDto;
        }

        public IList<PreconditionLiteralValuesDto> GetLiteralValues(FilterQueryParametersBase filterQueryParameters)
        {
            LiteralPreconditionsDto literalPrecondition = GetLiteralPrecondition(filterQueryParameters.LiteralPreconditionId);

            return literalPrecondition.PreconditionsLiteralValues;
        }

        public LiteralPreconditionsDto Update(LiteralPreconditionsDto model)
        {
            var resultSave = new LiteralPreconditionsDto();
            var preconditionTypes = _preconditionTypesService.GetAll();
            var nomCampModel = preconditionTypes.Where(x => x.Id == model.PreconditionsTypeId).Select(x=> x.Description).First();
            var localModel = _literalPreconditionsRepository.GetLiteralPreconditions(model.Id);
            if (localModel != null)
            {
                localModel.NomCampModel = nomCampModel;
                localModel.ComparisonOperator = model.ComparisonOperator;

                DeleteLiteralValues(localModel.PreconditionsLiteralValues);
                localModel = localModel.AssignLiteralValues(model.PreconditionsLiteralValues);
                
                if (model.PreconditionsLiteralValues != null) {
                    var res = _literalPreconditionsRepository.UpdateLiteralPreconditions(localModel);
                    resultSave = res.Entity.ToDto(preconditionTypes);
                }
                else
                {
                    var res = _literalPreconditionsRepository.DeleteLiteralPreconditions(localModel);
                    resultSave = res.Entity.ToDto(preconditionTypes);
                }
                CheckPrecondition(resultSave.PreconditionId);
            }
            return resultSave;
        }

        public LiteralPreconditionsDto Create(LiteralPreconditionsDto model)
        {
            var resultSave = new LiteralPreconditionsDto();
            var type = _preconditionTypesService.GetById(model.PreconditionsTypeId);
            var entity = model.ToEntity(type);
            DeleteLiteralValues(entity.PreconditionsLiteralValues);
            var modelWithLiteralValues = entity.AssignLiteralValues(model.PreconditionsLiteralValues);
            if (model.PreconditionsLiteralValues != null)
            {
                var res = _literalPreconditionsRepository.CreateLiteralPreconditions(modelWithLiteralValues);
                return resultSave = res.Entity.ToDto(type);
            }
            return resultSave;
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete LiteralPrecondition by id {id}");
            ResultDto<bool> result = null;
            var localLiteralPrecondition = _literalPreconditionsRepository.GetLiteralPreconditions(id);
            if (localLiteralPrecondition != null)
            {
                DeleteLiteralValues(localLiteralPrecondition.PreconditionsLiteralValues);
                var resultSave = _literalPreconditionsRepository.DeleteLiteralPreconditions(localLiteralPrecondition);
                result = ProcessResult(resultSave.IsOk, resultSave);
                CheckPrecondition(localLiteralPrecondition.PreconditionId);
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

        public LiteralsPreconditions DeleteOnContext(int id)
        {
            LogginingService.LogInfo($"Delete LiteralPrecondition by id {id}");
            LiteralsPreconditions result = null;
            var localLiteralPrecondition = _literalPreconditionsRepository.GetLiteralPreconditions(id);
            if (localLiteralPrecondition != null)
            {
                DeleteLiteralValues(localLiteralPrecondition.PreconditionsLiteralValues);
                result = _literalPreconditionsRepository.DeleteLiteralPreconditionsOnContext(localLiteralPrecondition);
            }
            return result;
        }

        private bool DeleteLiteralValues (ICollection<PreconditionsLiteralValues> literalsValuesCollection) {
            if (literalsValuesCollection != null &&literalsValuesCollection.Any() == true)
            {
                foreach (var literalValues in literalsValuesCollection)
                {
                    _literalValuesRepository.DeleteOnContextPreconditionsLiteralValues(literalValues);
                }
            }
            return true;
        }

        public PreconditionsDto CheckPrecondition(int id) /*TODO REFACTOR DAVID*/
        {
            LogginingService.LogInfo($"Check Precondition by {id}");
            var changes = false;
            var preconditionTypes = _preconditionTypesService.GetAll();
            PreconditionsDto precondition = _preconditionsRepository.GetById(id).ToDto(preconditionTypes);

            LiteralPreconditionsDto literalWoCategory = null;
            LiteralPreconditionsDto literalProject = null;
            LiteralPreconditionsDto literalAsset = null;
            LiteralPreconditionsDto literalLocationFinalClient = null;
            LiteralPreconditionsDto literalFinalClient = null;
            LiteralPreconditionsDto literalWoTypeN1 = null;
            LiteralPreconditionsDto literalWoTypeN2 = null;
            LiteralPreconditionsDto literalWoTypeN3 = null;
            LiteralPreconditionsDto literalWoTypeN4 = null;
            LiteralPreconditionsDto literalWoTypeN5 = null;


            if (precondition.LiteralsPreconditions != null && precondition.LiteralsPreconditions?.Any() == true)
            {
                foreach (var literalPre in precondition.LiteralsPreconditions)
                {
                    switch (literalPre.PreconditionsTypeName)
                    {
                        case (LiteralPreconditionConstants.WoCategory):
                            literalWoCategory = literalPre;
                            break;

                        case (LiteralPreconditionConstants.Project):
                            literalProject = literalPre;
                            break;

                        case (LiteralPreconditionConstants.Asset):
                            literalAsset = literalPre;
                            break;

                        case (LiteralPreconditionConstants.FinalClientLocation):
                            literalLocationFinalClient = literalPre;
                            break;

                        case (LiteralPreconditionConstants.FinalClient):
                            literalFinalClient = literalPre;
                            break;

                        case (LiteralPreconditionConstants.WoTypeN1):
                            literalWoTypeN1 = literalPre;
                            break;

                        case (LiteralPreconditionConstants.WoTypeN2):
                            literalWoTypeN2 = literalPre;
                            break;

                        case (LiteralPreconditionConstants.WoTypeN3):
                            literalWoTypeN3 = literalPre;
                            break;

                        case (LiteralPreconditionConstants.WoTypeN4):
                            literalWoTypeN4 = literalPre;
                            break;

                        case (LiteralPreconditionConstants.WoTypeN5):
                            literalWoTypeN5 = literalPre;
                            break;
                    }
                }

                if (literalWoCategory != null && literalProject != null && !changes)
                {
                    WoCategoryQueryParameters woCategoryQueryParameters = new WoCategoryQueryParameters()
                    {
                        ProjectsIdsToMatch = literalProject.PreconditionsLiteralValues.Select(x => x.TypeId).ToList(),
                    };

                    changes = CheckLiteralValues(literalWoCategory, woCategoryQueryParameters);
                }

                if (literalWoCategory != null && literalProject == null && !changes)
                {
                    Delete(literalWoCategory.Id);
                    changes = true;
                }

                if (literalLocationFinalClient != null && literalFinalClient != null && !changes)
                {
                    SitesFinalClientsQueryParameters sitesFinalClientQueryParameters = new SitesFinalClientsQueryParameters()
                    {
                        FinalClientIdsToMatch = literalFinalClient.PreconditionsLiteralValues.Select(x => x.TypeId).ToList(),
                    };

                    changes = CheckLiteralValues(literalLocationFinalClient, sitesFinalClientQueryParameters);
                }

                if (literalAsset != null && literalLocationFinalClient != null && !changes)
                {
                    AssetQueryParameters assetQueryParameters = new AssetQueryParameters()
                    {
                        LocationFinalClientIdsToMatch = literalLocationFinalClient.PreconditionsLiteralValues.Select(x => x.TypeId).ToList(),
                    };

                    changes = CheckLiteralValues(literalAsset, assetQueryParameters);
                }

                if (literalAsset != null && (literalLocationFinalClient == null || literalFinalClient == null) && !changes)
                {
                    if (literalLocationFinalClient != null && literalFinalClient == null)
                    {
                        Delete(literalLocationFinalClient.Id);
                    }
                    Delete(literalAsset.Id);
                    changes = true;
                }

                if (literalWoTypeN2 != null && literalWoTypeN1 != null && !changes)
                {
                    WOTypeQueryParameters wOTypeQueryParameters = new WOTypeQueryParameters()
                    {
                        WOTypeIdsToMatch = literalWoTypeN1.PreconditionsLiteralValues.Select(x => (int?) x.TypeId).ToList(),
                    };

                    changes = CheckLiteralValues(literalWoTypeN2, wOTypeQueryParameters);
                }
                
                if (literalWoTypeN4 != null && literalWoTypeN3 != null && !changes)
                {
                    WOTypeQueryParameters wOTypeQueryParameters = new WOTypeQueryParameters()
                    {
                        WOTypeIdsToMatch = literalWoTypeN3.PreconditionsLiteralValues.Select(x => (int?)x.TypeId).ToList(),
                    };

                    changes = CheckLiteralValues(literalWoTypeN4, wOTypeQueryParameters);
                }

                if (literalWoTypeN5 != null && literalWoTypeN4 != null && !changes)
                {
                    WOTypeQueryParameters wOTypeQueryParameters = new WOTypeQueryParameters()
                    {
                        WOTypeIdsToMatch = literalWoTypeN4.PreconditionsLiteralValues.Select(x => (int?)x.TypeId).ToList(),
                    };

                    changes = CheckLiteralValues(literalWoTypeN5, wOTypeQueryParameters);
                }

                if (literalWoTypeN2 != null && literalWoTypeN1 == null)
                {
                    Delete(literalWoTypeN2.Id);
                    changes = true;
                }
                if (literalWoTypeN3 != null && literalWoTypeN2 == null)
                {
                    Delete(literalWoTypeN3.Id);
                    changes = true;
                }
                if (literalWoTypeN4 != null && literalWoTypeN3 == null)
                {
                    Delete(literalWoTypeN4.Id);
                    changes = true;
                }
                if (literalWoTypeN5 != null && literalWoTypeN4 == null)
                {
                    Delete(literalWoTypeN5.Id);
                    changes = true;
                }
            }
            else
            {
                _preconditionsRepository.DeleteOnContextPreconditions(precondition.ToEntity());
            }
            if (changes)
            {
                CheckPrecondition(id);
            }
            return precondition;
        }

        private bool CheckLiteralValues(LiteralPreconditionsDto literalPrecondition, IFilterQueryParameters queryParameters)
        {
            var changes = false;
            var query = _literalQueryFactory.GetQuery(literalPrecondition.PreconditionsTypeName);
            IList<BaseNameIdDto<int>> possibleLiteralValues = query.GetAllKeyValues(queryParameters);
            foreach (var lv in literalPrecondition.PreconditionsLiteralValues)
            {
                if (!possibleLiteralValues.Any(x => x.Id == lv.TypeId))
                {
                    var entityLiteralValues = _literalValuesRepository.GetById(lv.Id);
                    _literalValuesRepository.DeletePreconditionsLiteralValues(entityLiteralValues);
                    changes = true;
                }
            }
            return changes;
        }
    }
}