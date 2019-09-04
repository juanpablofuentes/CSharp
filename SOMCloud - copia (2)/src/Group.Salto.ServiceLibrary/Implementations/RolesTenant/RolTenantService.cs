using Group.Salto.Common;
using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Rol;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.RolesTenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.RolesTenant;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.RolesTenant
{
    public class RolTenantService : BaseService, IRolTenantService
    {
        private readonly ICache _cacheService;
        private readonly IRolesTenantRepository _rolesTenantRepository;

        public RolTenantService(ILoggingService logginingService,
            ICache cacheService,
            IRolesTenantRepository rolesTenantRepository) : base(logginingService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException($"{nameof(ICache)} is null");
            _rolesTenantRepository = rolesTenantRepository ?? throw new ArgumentNullException($"{nameof(IRolesTenantRepository)} is null");
        }

        public ResultDto<IList<RolTenantListDto>> GetListFiltered(RolTenantFilterDto filter)
        {
            LogginingService.LogInfo($"Get Rol Tenant filtered");
            IQueryable<Entities.Tenant.RolesTenant> query = _rolesTenantRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = OrderBy(query, filter);

            return ProcessResult(query.ToList().ToListDto());
        }

        public ResultDto<IEnumerable<RolTenantDto>> GetAll()
        {
            LogginingService.LogInfo($"Get all Roles Tenant availables");
            ResultDto<IEnumerable<RolTenantDto>> res = (ResultDto<IEnumerable<RolTenantDto>>)_cacheService.GetData(AppCache.Roles, AppCache.RolesKey);

            if (res is null)
            {
                IEnumerable<Entities.Tenant.RolesTenant> result = _rolesTenantRepository.GetAll().ToList();
                res = ProcessResult(result.MapList(rt => rt.ToDto()));
                _cacheService.SetData(AppCache.Roles, AppCache.RolesKey, res);
            }
            return res;
        }

        public ResultDto<RolTenantDto> GetById(int Id)
        {
            LogginingService.LogInfo($"Get Rol Tenant by Id {Id}");
            string rolkey = $"{AppCache.RolKey}{Id}";

            ResultDto<RolTenantDto> res = (ResultDto<RolTenantDto>)_cacheService.GetData(AppCache.RolId, rolkey);
            if (res is null)
            {
                Entities.Tenant.RolesTenant result = _rolesTenantRepository.GetById(Id.ToString());
                res = ProcessResult(result.ToDto());

                _cacheService.SetData(AppCache.RolId, rolkey, res);
            }
            return res;
        }

        public ResultDto<RolTenantDto> Create(RolTenantDto rolTenant)
        {
            LogginingService.LogInfo($"Creating new Rol Tenant");
            List<ErrorDto> errors = new List<ErrorDto>();

            rolTenant.Id = _rolesTenantRepository.GetMaxId() + 1;
            Entities.Tenant.RolesTenant newRolTenant = rolTenant.ToEntity();
            var result = _rolesTenantRepository.CreateRolTenant(newRolTenant);

            if (result.IsOk)
            {
                _cacheService.RemoveData(AppCache.Roles, AppCache.RolesKey);
            }
            else
            {
                errors.Add(new ErrorDto() { ErrorType = ErrorType.SaveChangesException, ErrorMessageKey = RolConstants.RolCreateError });
            }
            return ProcessResult(newRolTenant.ToDto(), errors);
        }

        public ResultDto<RolTenantDto> Update(RolTenantDto rolTenant)
        {
            LogginingService.LogInfo($"Update Rol Tenant");
            List<ErrorDto> errors = new List<ErrorDto>();

            Entities.Tenant.RolesTenant localRolTenant = _rolesTenantRepository.GetById(rolTenant.Id.ToString());
            if (localRolTenant != null)
            {
                ResultDto<RolTenantDto> result = null;
                localRolTenant.UpdateRolTenantEntity(rolTenant.ToEntity());
                var resultRepository = _rolesTenantRepository.UpdateRolTenant(localRolTenant);
                if (resultRepository.IsOk)
                {
                    _cacheService.RemoveData(AppCache.Roles, AppCache.RolesKey);
                    _cacheService.RemoveData(AppCache.RolId, $"{AppCache.RolKey}{rolTenant.Id}");
                }
                else
                {
                    errors.Add(new ErrorDto() { ErrorType = ErrorType.SaveChangesException, ErrorMessageKey = RolConstants.RolUpdateError });
                }
                result = ProcessResult(localRolTenant.ToDto(), errors);
            }
            else
            {
                errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = RolConstants.RolNotExist });
            }
            return ProcessResult(rolTenant, errors);
        }

        private IQueryable<Entities.Tenant.RolesTenant> OrderBy(IQueryable<Entities.Tenant.RolesTenant> query, RolTenantFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }
    }
}