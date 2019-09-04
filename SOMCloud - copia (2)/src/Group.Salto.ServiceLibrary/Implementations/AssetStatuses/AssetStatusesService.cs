using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.AssetStatuses;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.AssetStatuses;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.AssetStatuses
{
    public class AssetStatusesService : BaseFilterService, IAssetStatusesService
    {
        private readonly IAssetStatusesRepository _assetStatusesRepository;
        private readonly IAssetsRepository _assetsRepository;

        public AssetStatusesService(
            ILoggingService logginingService, 
            IAssetStatusesRepository assetStatusesRepository,
            IAssetsRepository assetsRepository,
            IAssetStatusesQueryFactory queryFactory) : base(queryFactory, logginingService)
        {
            _assetStatusesRepository = assetStatusesRepository ?? throw new ArgumentNullException($"{nameof(IAssetStatusesRepository)} is null ");
            _assetsRepository = assetsRepository ?? throw new ArgumentNullException($"{nameof(IAssetsRepository)} is null ");
        }

        public ResultDto<AssetStatusesDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id AssetStatuses");
            var data = _assetStatusesRepository.GetById(id);
            return ProcessResult(data.ToDetailDto());
        }

        public ResultDto<IList<AssetStatusesDto>> GetAllFiltered(AssetStatusesFilterDto filter)
        {
            LogginingService.LogInfo($"Get All AssetStatuses Filtered");
            var query = _assetStatusesRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.IsRetiredState, au => au.IsRetiredState == filter.IsRetiredState);
            var data = query.ToList().MapList(c => c.ToDto()).AsQueryable();
            data = OrderBy(data, filter);
            return ProcessResult<IList<AssetStatusesDto>>(data.ToList());
        }

        public ResultDto<AssetStatusesDto> CreateAssetStatuses(AssetStatusesDto source)
        {
            LogginingService.LogInfo($"Create Assetstatuses");
            var newAssetStatuses = source.ToEntity();
            if (source.IsDefault )
            {
                newAssetStatuses = _assetStatusesRepository.SetIsDefault(newAssetStatuses);
            }
            var result = _assetStatusesRepository.CreateAssetStatuses(newAssetStatuses);
            return ProcessResult(result.Entity?.ToDetailDto(), result);
        }

        public ResultDto<AssetStatusesDto> UpdateAssetStatuses(AssetStatusesDto source)
        {
            LogginingService.LogInfo($"Update AssetStatuses");
            ResultDto<AssetStatusesDto> result = null;
            var findAssetStatuses = _assetStatusesRepository.GetById(source.Id);
            if (findAssetStatuses != null)
            {
                var updatedAssetStatuses = findAssetStatuses.Update(source);
                if (source.IsDefault)
                {
                    updatedAssetStatuses = _assetStatusesRepository.SetIsDefault(updatedAssetStatuses);
                }
                var resultRepository = _assetStatusesRepository.UpdateAssetStatuses(updatedAssetStatuses);
                result = ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
            }

            return result ?? new ResultDto<AssetStatusesDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeleteAssetStatuses(int id)
        {
            LogginingService.LogInfo($"Delete assetStatuses by id {id}");
            ResultDto<bool> result = null;
            var assetStatuses = _assetStatusesRepository.GetById(id);
            var teamsAsigned = _assetsRepository.GetAssetsByAssetStatusId(assetStatuses.Id);
            if (assetStatuses == null )
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
           
            if (teamsAsigned.Count() == 0 && assetStatuses.IsDefault != true)
            {
                var resultSave = _assetStatusesRepository.DeleteAssetStatuses(assetStatuses);
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

        public IList<BaseNameIdDto<int>> GetAllKeyValues() 
        {
            var data = _assetStatusesRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();        
        }

        private IQueryable<AssetStatusesDto> OrderBy(IQueryable<AssetStatusesDto> query, AssetStatusesFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Asset Statuses");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.IsRetiredState);
            return query;
        }
    }
}