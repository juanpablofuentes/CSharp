using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Brand;
using Group.Salto.ServiceLibrary.Common.Contracts.Brands;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Brand;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Brand
{
    public class BrandsService : BaseFilterService, IBrandsService
    {
        private readonly IBrandsRepository _brandsRepository;
        private readonly IModelsRepository _modelsRepository;
        public BrandsService(ILoggingService logginingService, 
            IBrandsRepository brandsRepository, 
            IModelsRepository modelsRepository, 
            IBrandsQueryFactory queryFactory) : base(queryFactory, logginingService)
        {
            _brandsRepository = brandsRepository ?? throw new ArgumentNullException($"{nameof(IBrandsRepository)} is null ");
            _modelsRepository = modelsRepository ?? throw new ArgumentNullException($"{nameof(IModelsRepository)} is null ");
        }

        public ResultDto<BrandsDetailsDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id Brands");
            var data = _brandsRepository.GetBrandsWithModels(id);
            return ProcessResult(data.ToDetailDto());
        }

        public ResultDto<IList<BrandsDto>> GetAllFiltered(BrandsFilterDto filter)
        {
            LogginingService.LogInfo($"Get All  Brands Filtered");
            var query = _brandsRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            var data = query.ToList().MapList(c => c.ToDto()).AsQueryable();
            data = OrderBy(data, filter);
            return ProcessResult<IList<BrandsDto>>(data.ToList());
        }

        public ResultDto<BrandsDetailsDto> CreateBrands(BrandsDetailsDto source)
        {
            LogginingService.LogInfo($"Create Brands");
            var newBrands = source.ToEntity();
            var result = _brandsRepository.CreateBrands(newBrands);
            return ProcessResult(result.Entity?.ToDetailDto(), result);
        }

        public BrandsDetailsDto GetBrandsWithModels(int id)
        {
            LogginingService.LogInfo($"Get Brands models key value");
            var data = _brandsRepository.GetBrandsWithModels(id);
            return data.ToDetailDto();
        }

        public ResultDto<BrandsDetailsDto> UpdateBrands(BrandsDetailsDto source)
        {
            LogginingService.LogInfo($"Update Brands");
            ResultDto<BrandsDetailsDto> result = null;
            var findBrands = _brandsRepository.GetBrandsWithModels(source.Id);
            if (findBrands != null)
            {
                var updatedBrands = findBrands.Update(source);
                updatedBrands = AssignModels(updatedBrands, source.Models);
                var resultRepository = _brandsRepository.UpdateBrands(updatedBrands);
                result = ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
            }

            return result ?? new ResultDto<BrandsDetailsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeleteBrands(int id)
        {
            LogginingService.LogInfo($"Delete brands by id {id}");
            ResultDto<bool> result = null;
            var brands = _brandsRepository.GetBrandsWithModels(id);
            if (brands != null)
            {
                brands = DeleteModelsFromBrand(brands);
                var resultSave = _brandsRepository.DeleteBrands(brands);
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

        private IQueryable<BrandsDto> OrderBy(IQueryable<BrandsDto> query, BrandsFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Brands");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Url);
            return query;
        }

        private Entities.Tenant.Brands AssignModels(Entities.Tenant.Brands entity, IList<ModelDto> models)
        {
            if (models != null && models.Any())
            {
                entity.Models = entity.Models ?? new List<Entities.Tenant.Models>();
                foreach (var mod in models)
                {
                    var temp = entity.Models.SingleOrDefault(x => x.Id == mod.Id);
                    if (temp != null)
                    {
                        temp.Name = mod.Name;
                        temp.Description = mod.Description;
                        temp.Url = mod.Url;
                    }
                    else
                    {
                        entity.Models.Add(new Entities.Tenant.Models()
                        {
                            Name = mod.Name,
                            Description = mod.Description,
                            Url = mod.Url,
                            BrandId = mod.BrandId
                        });
                    }
                }
            }
            return entity;
        }

        private Entities.Tenant.Brands DeleteModelsFromBrand(Entities.Tenant.Brands localBrands)
        {
            if (localBrands.Models != null) {
                foreach (var mod in localBrands.Models.ToList()) {
                    _modelsRepository.DeleteOnContextModels(mod);
                }
            }
            return localBrands;
        }
    }
}