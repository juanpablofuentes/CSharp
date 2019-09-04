using Group.Salto.Common;
using Group.Salto.Common.Constants.Assets;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Assets;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Assets
{
    public class AssetsService : BaseFilterService, IAssetsService
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IAssetStatusesRepository _assetStatusesRepository;
        private readonly ISiteUserRepository _siteUserRepository;
        private readonly IModelsRepository _modelRepository;
        private readonly ISubFamiliesRepository _subfamilyRepository;
        private readonly IUsagesRepository _usageRepository;
        private readonly IContractsRepository _contractsRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly ISitesRepository _sitesRepository;

        public AssetsService(ILoggingService logginingService,
            IAssetsQueryFactory queryFactory,
            IAssetsRepository assetsRepository,
            ISiteUserRepository siteUserRepository,
            IModelsRepository modelRepository,
            ISubFamiliesRepository subfamilyRepository,
            IUsagesRepository usageRepository,
            IContractsRepository contractsRepository,
            IPeopleRepository peopleRepository,
            IAssetStatusesRepository assetStatusesRepository,
            ISitesRepository sitesRepository) : base(queryFactory, logginingService)
        {
            _assetsRepository = assetsRepository ?? throw new ArgumentNullException($"{nameof(IAssetsRepository)} is null");
            _assetStatusesRepository = assetStatusesRepository ?? throw new ArgumentNullException($"{nameof(IAssetStatusesRepository)} is null");
            _siteUserRepository = siteUserRepository ?? throw new ArgumentNullException($"{nameof(ISiteUserRepository)} is null");
            _modelRepository = modelRepository ?? throw new ArgumentNullException($"{nameof(IModelsRepository)} is null");
            _subfamilyRepository = subfamilyRepository ?? throw new ArgumentNullException($"{nameof(ISubFamiliesRepository)} is null");
            _usageRepository = usageRepository ?? throw new ArgumentNullException($"{nameof(IUsagesRepository)} is null");
            _contractsRepository = contractsRepository ?? throw new ArgumentNullException($"{nameof(IContractsRepository)} is null");
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null");
            _sitesRepository = sitesRepository ?? throw new ArgumentNullException($"{nameof(ISitesRepository)} is null");
        }

        public ResultDto<IList<AssetsListDto>> GetAllFiltered(AssetsFilterDto filter)
        {
            AssetsFilterRepositoryDto repositoryFilter = new AssetsFilterRepositoryDto()
            {
                SerialNumber = filter.SerialNumber,
                SitesId = filter.SitesId,
                StatusesSelected = filter.StatusesSelected?.Select(x => x.Id).ToArray(),
                ModelsSelected = filter.ModelsSelected?.Select(x => x.Id).ToArray(),
                BrandsSelected = filter.BrandsSelected?.Select(x => x.Id).ToArray(),
                FamiliesSelected = filter.FamiliesSelected?.Select(x => x.Id).ToArray(),
                SubFamiliesSelected = filter.SubFamiliesSelected?.Select(x => x.Id).ToArray(),
                SitesSelected = filter.SitesSelected?.Select(x => x.Id).ToArray(),
                FinalClientsSelected = filter.FinalClientsSelected?.Select(x => x.Id).ToArray()
            };

            List<Entities.Tenant.Assets> query = _assetsRepository.GetAllFiltered(repositoryFilter).ToList();
            var status = _assetStatusesRepository.GetAll();
            IList<AssetsListDto> result = query.ToListDto(status.ToList()).ToList();
            result = OrderBy(result, filter);
            return ProcessResult(result);
        }

        private List<AssetsListDto> OrderBy(IList<AssetsListDto> data, AssetsFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.SerialNumber);
            return query.ToList();
        }

        public ResultDto<AssetDetailsDto> GetById(int id)
        {
            var entity = _assetsRepository.GetById(id);
            return ProcessResult(entity.ToDetailsDto(), entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<AssetForWorkOrderDetailsDto> GetForWorkOrderEditById(int id)
        {
            var entity = _assetsRepository.GetForWorkOrderEditById(id);
            return ProcessResult(entity.ToForWorkOrderDetailsDto(), entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<AssetDetailsDto> GetAssetPartialDetailBySiteIdWithFinalClient(int id)
        {
            var entity = _sitesRepository.GetByIdWithFinalClient(id);
            return ProcessResult(entity.ToPartialDetailsDto(), entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<AssetDetailsDto> Create(AssetDetailsDto model)
        {
            var entity = model.ToEntity();
            entity = AssignContract(entity, model.SelectedContract, false);
            entity = AssignHiredServices(entity, model.HiredServices, false);
            
            var people = _peopleRepository.GetByConfigId(model.UserId);
            entity.AssetsAudit = new List<AssetsAudit>();
            var audit = new AssetsAudit
            {
                Asset = entity,
                RegistryDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                UserName = people.FullName
            };
            var changes = new AssetsAuditChanges
            {
                Property = AssetsConstants.AssetsCreateTitle
            };
            audit.AssetsAuditChanges = new List<AssetsAuditChanges>() { changes };
            entity.AssetsAudit.Add(audit);                
            
            var result = _assetsRepository.CreateAsset(entity);
            return ProcessResult(result.Entity?.ToDetailsDto(), result);
        }

        public ResultDto<AssetDetailsDto> Update(AssetDetailsDto model)
        {
            ResultDto<AssetDetailsDto> result = null;     
            var entity = _assetsRepository.GetById(model.Id);
            if (entity != null)
            {
                entity = AddUpdateAssetsAudit(entity, model);
                entity.Update(model);
                entity = AssignContract(entity, model.SelectedContract, true);
                entity = AssignStatus(entity, model.SelectedStatus);
                entity = AssignSubFamily(entity, model.SelectedSubFamily);
                entity = AssignModel(entity, model.SelectedModel);
                entity = AssignUser(entity, model.SelectedSiteUser);
                entity = AssignLocation(entity,
                    new KeyValuePair<int?, string>(model.SelectedSiteLocation.Key,
                    model.SelectedSiteLocation.Value));
                entity = AssignHiredServices(entity, model.HiredServices, true);
                var resultRepository = _assetsRepository.UpdateAsset(entity);
                result = ProcessResult(resultRepository.Entity.ToDetailsDto(), resultRepository);
            }
            else
            {
                result = ProcessResult(model, new ErrorDto()
                {
                    ErrorType = ErrorType.EntityNotExists
                });
            }
            return result;        
        }

        public ResultDto<bool> Transfer(AssetsTransferDto model) 
        {
            ResultDto<bool> result = null;
            if (model != null && model?.SelectedAssetsId.Length > 0 )
            {
                List<Entities.Tenant.Assets> updateAssets = new List<Entities.Tenant.Assets>();
                var people = _peopleRepository.GetByConfigId(model.UserId);
                if (model.AssetsStatusId == null && 
                    !model.SelectedSiteLocation.Key.HasValue &&
                    !model.SelectedSiteUser.Key.HasValue)
                {
                    result.Data = true;
                    return result;
                }
                foreach (var assetId in model.SelectedAssetsId)
                {
                    var asset = _assetsRepository.GetById(assetId);
                    if (asset == null)
                    {
                        result.Data = false;
                        if (result.Errors == null)
                        {
                            result.Errors = new ErrorsDto();
                        }
                        result.Errors.AddError(new ErrorDto
                        {
                            ErrorType = ErrorType.EntityNotExists
                        });
                        return result;
                    }
                    asset.AssetsAudit = asset.AssetsAudit ?? new List<AssetsAudit>();
                    asset.AssetsAudit.Add(new AssetsAudit
                    {
                        AssetId = asset.Id,
                        RegistryDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        UserName = people.FullName
                    });

                    asset = AssignStatus(asset, model.AssetsStatusId);
                    asset = AssignUser(asset, model.SelectedSiteUser);
                    asset = AssignLocation(asset, model.SelectedSiteLocation);

                    updateAssets.Add(asset);
                }
                var transfer = _assetsRepository.UpdateTransferedAsset(updateAssets);
                result = new ResultDto<bool>
                {
                    Data = true,
                    Errors = transfer.Error?.ToErrorsDto()
                };
                return result;
            }
            return result;
        }
        
        public Entities.Tenant.Assets AddUpdateAssetsAudit(Entities.Tenant.Assets entity, AssetDetailsDto model) 
        {
            var people = _peopleRepository.GetByConfigId(model.UserId);
            entity.AssetsAudit = entity.AssetsAudit ?? new List<AssetsAudit>();
            var changes = GetAssetChanges(entity, model.ToEntity());
            if (changes.Count > 0)
            {
                entity.AssetsAudit.Add(new AssetsAudit
                {
                    AssetId = entity.Id,
                    RegistryDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    UserName = people.FullName,
                    AssetsAuditChanges = new List<AssetsAuditChanges>()
                });
                var audit = entity.AssetsAudit.Last();  

                foreach (var change in changes)
                {
                    audit.AssetsAuditChanges.Add(AddNewAssetChange(change.Property, change.OldValue, change.NewValue));
                }
            }
            return entity;
        }

        public List<AssetsAuditChanges> GetAssetChanges(Entities.Tenant.Assets oldEntity, Entities.Tenant.Assets newEntity) 
        {
            var result = new List<AssetsAuditChanges>();
            if (oldEntity.SerialNumber != newEntity.SerialNumber)
            {
                result.Add(AddNewAssetChange(AssetsConstants.AssetsDetailsSerialNumber,
                                        oldEntity.SerialNumber,
                                        newEntity.SerialNumber));
            }
            if (oldEntity.StockNumber != newEntity.StockNumber)
            {
                result.Add(AddNewAssetChange(AssetsConstants.AssetsDetailsStockNumber,
                                        oldEntity.StockNumber,
                                        newEntity.StockNumber));
            }
            if (oldEntity.AssetNumber != newEntity.AssetNumber)
            {
                result.Add(AddNewAssetChange(AssetsConstants.AssetsDetailsAssetNumber,
                                        oldEntity.AssetNumber,
                                        newEntity.AssetNumber));
            }
            if (oldEntity.Observations != newEntity.Observations)
            {
                result.Add(AddNewAssetChange(AssetsConstants.AssetsDetailsObservations,
                                        oldEntity.Observations,
                                        newEntity.Observations));
            }                      
            var warrantyChanges = GetWarrantyChanges(oldEntity, newEntity);
            return result.Concat(warrantyChanges).ToList();
        }

        public ResultDto<AssetsLocationsDto> GetLocationsAndUserSiteById(int id)
        {
            var entity = _assetsRepository.GetLocationsAndUserSiteById(id);
            return ProcessResult(entity.ToAssetsLocationsDto(), entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get Assets Key Value");
            var data = _assetsRepository.GetAll();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = x.SerialNumber,
            }).ToList();
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValuesByLocationFinalClient(List<int> IdsToMatch)
        {
            LogginingService.LogInfo($"Get Assets Key Value by LocationFinalClient");
            var data = _assetsRepository.GetAllAssetsByLocationFCIds(IdsToMatch);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = x.SerialNumber ?? x.Observations,
            }).ToList(); 
        }

        private List<AssetsAuditChanges> GetWarrantyChanges(Entities.Tenant.Assets oldEntity, Entities.Tenant.Assets newEntity)
        {
            var result = new List<AssetsAuditChanges>();
            if (oldEntity.Guarantee.IdExternal != newEntity.Guarantee.IdExternal)
            {
                result.Add(AddNewAssetChange(AssetsConstants.AssetsWarrantyNumber,
                                        oldEntity.Guarantee.IdExternal.ToString(),
                                        newEntity.Guarantee.IdExternal.ToString()));
            }
            if (oldEntity.Guarantee.StdStartDate != newEntity.Guarantee.StdStartDate || 
                oldEntity.Guarantee.StdEndDate != newEntity.Guarantee.StdEndDate)
            {
                result.Add(AddNewAssetChange(AssetsConstants.AssetsWarrantyStandardPeriod,
                                            oldEntity.Guarantee.StdStartDate?.ToString()+"-"+
                                            oldEntity.Guarantee.StdEndDate?.ToString(),
                                            newEntity.Guarantee.StdStartDate?.ToString()+"-"+
                                            newEntity.Guarantee.StdEndDate?.ToString()));
            }
            if (oldEntity.Guarantee.Standard != newEntity.Guarantee.Standard)
            {
                result.Add(AddNewAssetChange(AssetsConstants.AssetsWarrantyStandardTitle,
                                        oldEntity.Guarantee.Standard,
                                        newEntity.Guarantee.Standard));
            }           
            if (oldEntity.Guarantee.BlnStartDate != newEntity.Guarantee.BlnStartDate ||
                oldEntity.Guarantee.BlnEndDate != newEntity.Guarantee.BlnEndDate)
            {
                result.Add(AddNewAssetChange(AssetsConstants.AssetsWarrantyMaintenancePeriod,
                                        oldEntity.Guarantee.BlnStartDate?.ToString()+"-"+
                                        oldEntity.Guarantee.BlnEndDate?.ToString(),
                                        newEntity.Guarantee.BlnStartDate?.ToString()+"-"+
                                        newEntity.Guarantee.BlnEndDate?.ToString()));
            }
            if (oldEntity.Guarantee.Armored != newEntity.Guarantee.Armored)
            {
                result.Add(AddNewAssetChange(AssetsConstants.AssetsWarrantyMaintenanceTitle,
                                        oldEntity.Guarantee.Armored,
                                        newEntity.Guarantee.Armored));
            }           
            if (oldEntity.Guarantee.ProStartDate != newEntity.Guarantee.ProStartDate ||
                oldEntity.Guarantee.ProEndDate != newEntity.Guarantee.ProEndDate)
            {
                result.Add(AddNewAssetChange(AssetsConstants.AssetsWarrantyManufacturerPeriod,
                                        oldEntity.Guarantee.ProStartDate?.ToString()+"-"+
                                        oldEntity.Guarantee.ProEndDate?.ToString(),
                                        newEntity.Guarantee.ProStartDate?.ToString()+"-"+
                                        newEntity.Guarantee.ProEndDate?.ToString()));
            }
            if (oldEntity.Guarantee.Provider != newEntity.Guarantee.Provider)
            {
                result.Add(AddNewAssetChange(AssetsConstants.AssetsWarrantyManufacturerTitle,
                                        oldEntity.Guarantee.Provider,
                                        newEntity.Guarantee.Provider));
            }

            return result;
        }

        private AssetsAuditChanges AddNewAssetChange(string prop, string oldValue, string newValue) 
        {
            return new AssetsAuditChanges
            {
                Property = prop,
                OldValue = oldValue,
                NewValue = newValue
            };
        }

        private Entities.Tenant.Assets AssignHiredServices(Entities.Tenant.Assets entity, List<HiredServicesDto> hiredServices, bool auditable)
        {
            if (hiredServices != null && hiredServices.Any())
            {
                entity.AssetsHiredServices = entity.AssetsHiredServices ?? new List<AssetsHiredServices>();
                foreach (var service in hiredServices)
                {
                    var toUpdate = entity.AssetsHiredServices.FirstOrDefault(x => x.AssetId == service.Id);
                    if (toUpdate != null)
                    {
                        toUpdate.HiredService = service.ToEntity();
                    }
                    else
                    {
                        entity.AssetsHiredServices.Add(new AssetsHiredServices()
                        {
                             HiredService  = service.ToEntity()
                        });
                    }
                    if (auditable)
                    {
                        entity.AssetsAudit.Last()
                            .AssetsAuditChanges.Add(
                            AddNewAssetChange(AssetsConstants.AssetsHiredServicesTitle,
                            string.Empty,
                            service.Name));
                    }
                }
            }
            return entity;
        }

        private Entities.Tenant.Assets AssignUser(Entities.Tenant.Assets entity, KeyValuePair<int?,string> siteUser)
        {
            if (siteUser.Key.HasValue && 
                siteUser.Key > 0 &&
                siteUser.Key.Value != entity.UserId)
            {
                var audit = entity.AssetsAudit.Last();
                audit.AssetsAuditChanges = audit?.AssetsAuditChanges ?? new List<AssetsAuditChanges>();
                audit.AssetsAuditChanges.Add(
                    AddNewAssetChange(AssetsConstants.AssetsLocationUser,
                    entity.User?.Name,
                    siteUser.Value));
                entity.UserId = siteUser.Key;
            }
            return entity;
        }

        private Entities.Tenant.Assets AssignModel(Entities.Tenant.Assets entity, KeyValuePair<int?,string> model)
        {
            if (model.Key.HasValue &&
                model.Key.Value > 0 &&
                model.Key.Value != entity.ModelId)
            {
                entity.AssetsAudit.Last()
                    .AssetsAuditChanges.Add(
                    AddNewAssetChange(AssetsConstants.AssetsDetailsModel,
                    entity.Model?.Name,
                    model.Value));
                entity.ModelId = model.Key.Value;
            }
            return entity;
        }

        private Entities.Tenant.Assets AssignSubFamily(Entities.Tenant.Assets entity, KeyValuePair<int?,string> subFamily)
        {
            if (subFamily.Key.HasValue && 
                subFamily.Key.Value > 0  &&
                subFamily.Key.Value != entity.SubFamilyId)
            {
                entity.AssetsAudit.Last()
                    .AssetsAuditChanges.Add(
                    AddNewAssetChange(AssetsConstants.AssetsDetailsSubFamily,
                    entity.SubFamily?.Nom,
                    subFamily.Value));
                entity.SubFamilyId = subFamily.Key.Value;
            }
            return entity;
        }

        private Entities.Tenant.Assets AssignStatus(Entities.Tenant.Assets entity, int? modelStatusId)
        {
            if (modelStatusId.HasValue &&
                modelStatusId.Value > 0 &&
                modelStatusId.Value != entity.AssetStatusId)
            {
                var newStatus = _assetStatusesRepository.GetById(modelStatusId.Value);
                var audit = entity.AssetsAudit.Last();
                audit.AssetsAuditChanges = audit.AssetsAuditChanges ?? new List<AssetsAuditChanges>();
                audit.AssetsAuditChanges.Add(
                    AddNewAssetChange(AssetsConstants.AssetsDetailsStatus,
                    entity.AssetStatus?.Name,
                    newStatus?.Name));
                entity.AssetStatusId = modelStatusId.Value;
            }
            return entity;
        }

        private Entities.Tenant.Assets AssignContract(Entities.Tenant.Assets entity, int? modelContracId, bool auditable)
        {
            entity.AssetsContracts?.Clear();
            if (modelContracId.HasValue &&
                modelContracId.Value > 0 &&
                !entity.AssetsContracts.Any(t => t.ContractsId == modelContracId)) 
            {
                if (auditable)
                {
                    var newContract = _contractsRepository.GetById(modelContracId.Value);
                    entity.AssetsAudit.Last()
                            .AssetsAuditChanges.Add(
                            AddNewAssetChange(AssetsConstants.AssetsDetailsContract,
                            string.Empty,
                            newContract.Object));
                }
                entity.AssetsContracts = entity.AssetsContracts ?? new List<AssetsContracts>();
                entity.AssetsContracts.Add(new AssetsContracts()
                {
                    ContractsId  = modelContracId.Value
                });                
            }
            return entity;
        }

        private Entities.Tenant.Assets AssignLocation(Entities.Tenant.Assets entity, KeyValuePair<int?,string> location)
        {
            if (location.Key.HasValue && 
                location.Key.Value > 0 &&
                location.Key.Value != entity.LocationClientId)
            {
                entity.AssetsAudit.Last()
                    .AssetsAuditChanges.Add(
                    AddNewAssetChange(AssetsConstants.AssetsLocationLocation,
                    entity.LocationClient?.Name,
                    location.Value));
                entity.LocationClientId = location.Key.Value;
            }
            return entity;
        }
    }
}