using Group.Salto.Common;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.SiteUser;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteUser;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.SiteUser
{
    public class SiteUserService : BaseService, ISiteUserService
    {
        private readonly ISiteUserRepository _siteUserRepository;
        private readonly IAssetsRepository _assetsRepository;
        public SiteUserService(ILoggingService logginingService, ISiteUserRepository siteUserRepository, IAssetsRepository assetsRepository) : base(logginingService)
        {
            _siteUserRepository = siteUserRepository ?? throw new ArgumentNullException($"{nameof(ISiteUserRepository)} is null");
            _assetsRepository = assetsRepository ?? throw new ArgumentNullException($"{nameof(IAssetsRepository)} is null");
        }

        public ResultDto<IList<SiteUserListDto>> GetAllFiltered(SiteUserFilterDto filter)
        {
            var query = _siteUserRepository.GetAll(filter.SitesId);
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToListDto());
        }

        public ResultDto<SiteUserDetailDto> GetById(int id)
        {
            var entity = _siteUserRepository.GetById(id);
            return ProcessResult(entity.ToDetailDto(), entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<SiteUserDetailDto> Update(SiteUserDetailDto model)
        {
            ResultDto<SiteUserDetailDto> result = null;
            var entity = _siteUserRepository.GetById(model.Id);
            if (entity != null)
            {
                entity.UpdateSiteUser(model);
                var resultRepository = _siteUserRepository.UpdateSiteUser(entity);
                result = ProcessResult(resultRepository?.Entity?.ToDetailDto(), resultRepository);
            }
            return result ?? new ResultDto<SiteUserDetailDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }

        public ResultDto<SiteUserDetailDto> Create(SiteUserDetailDto model)
        {
            var entity = model.ToEntity();
            var result = _siteUserRepository.CreateSiteUser(entity);
            return ProcessResult(result.Entity?.ToDetailDto(), result);
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete SiteUser by id {id}");
            ResultDto<bool> result = null;
            var localSiteUser = _siteUserRepository.GetByIdIncludeReferencesToDelete(id);
            if (localSiteUser != null)
            {
                if(localSiteUser.Assets?.Any() == true)
                {
                    localSiteUser.Assets.Clear();
                }

                var resultSave = _siteUserRepository.DeleteSiteUser(localSiteUser);
                result = ProcessResult(resultSave);              

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

        public ResultDto<ErrorDto> CanDelete(int id)
        {
            ErrorDto result = new ErrorDto() { ErrorMessageKey = string.Empty };
            var siteUser = _siteUserRepository.GetSiteUserRelationshipsById(id);
            if (siteUser != null)
            {
                if(siteUser.WorkOrders?.Any() == true)
                {
                    result.ErrorMessageKey = "SiteUserCanDeleteHaveWorkOrder";
                }
                else if (siteUser.WorkOrdersDeritative?.Any() == true)
                {
                    result.ErrorMessageKey = "SiteUserCanDeleteHaveWorkOrderDeritative";
                }
            }         
            return ProcessResult(result);
        }

        private IQueryable<Entities.Tenant.SiteUser> OrderBy(IQueryable<Entities.Tenant.SiteUser> data, SiteUserFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }

        public IList<BaseNameIdDto<int>> FilterByClientSite(QueryCascadeDto queryCascadeDto) 
        {
            var query = _siteUserRepository.FilterByClientSite(queryCascadeDto.Text, queryCascadeDto.Selected);
            var result = query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = x.FullName
            }).ToList();
            return result;
        }
    }
}