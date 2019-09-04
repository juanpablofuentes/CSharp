using Group.Salto.Common;
using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Rol;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Contracts.Rol;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Rols;
using Group.Salto.ServiceLibrary.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.Rol
{
    public class RolService : BaseService, IRolService
    {
        private readonly ICache _cacheService;
        private readonly IIdentityService _identityService;

        public RolService(ILoggingService logginingService,
                          ICache cacheService,
                          IIdentityService identityService) : base(logginingService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException($"{nameof(cacheService)} is null");
            _identityService = identityService ?? throw new ArgumentNullException($"{nameof(identityService)} is null");
        }

        public ResultDto<IEnumerable<RolDto>> GetAll()
        {
            LogginingService.LogInfo($"Get all Roles availables");

            ResultDto<IEnumerable<RolDto>> res = (ResultDto<IEnumerable<RolDto>>)_cacheService.GetData(AppCache.Roles, AppCache.RolesKey);

            if (res is null)
            {
                IEnumerable<Entities.Roles> result = _identityService.GetAllRoles().ToList();
                res = ProcessResult(result.MapList(r => r.ToDto()));

                _cacheService.SetData(AppCache.Roles, AppCache.RolesKey, res);
            }

            return res;
        }

        public async Task<ResultDto<RolDto>> GetById(int Id)
        {
            LogginingService.LogInfo($"Get Rol by Id {Id}");
            string rolkey = $"{AppCache.RolKey}{Id}";

            ResultDto<RolDto> res = (ResultDto<RolDto>)_cacheService.GetData(AppCache.RolId, rolkey);
            if (res is null)
            {
                //TODO: Carmen. RolesActionGroupsActions
                Entities.Roles result = await _identityService.GetRolById(Id.ToString());
                res = ProcessResult(result.ToDto());

                _cacheService.SetData(AppCache.RolId, rolkey, res);
            }
            return res;
        }

        public ResultDto<IList<RolListDto>> GetListFiltered(RolFilterDto filter)
        {
            LogginingService.LogInfo($"Get Rol filtered");
            IQueryable<Entities.Roles> query = _identityService.GetAllRoles();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = OrderBy(query, filter);

            return ProcessResult(query.ToList().ToListDto());
        }

        private IQueryable<Entities.Roles> OrderBy(IQueryable<Entities.Roles> query, RolFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);

            return query;
        }

        public async Task<ResultDto<RolDto>> CreateRol(RolDto rol)
        {
            LogginingService.LogInfo($"Creating new Rol");
            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidateRol(rol, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                int nextId = _identityService.GetMaxId() + 1;
                rol.Id = nextId;
                Entities.Roles newRol = rol.ToEntity();

                IdentityResult result = await _identityService.CreateNewRol(newRol);

                if (result.Succeeded)
                {
                    _cacheService.RemoveData(AppCache.Roles, AppCache.RolesKey);
                }
                else
                {
                    errors.Add(new ErrorDto() { ErrorType = ErrorType.SaveChangesException, ErrorMessageKey = RolConstants.RolCreateError });
                }

                return ProcessResult(newRol.ToDto(), errors);
            }

            return ProcessResult(rol, errors);
        }

        public async Task<ResultDto<RolDto>> UpdateRol(RolDto rol)
        {
            LogginingService.LogInfo($"Update Rol");
            List<ErrorDto> errors = new List<ErrorDto>();

            if (!ValidateRol(rol, out var validations))
            {
                errors.AddRange(validations);
            }
            else
            {
                //TODO: Carmen. RolesActionGroupsActions
                Entities.Roles localRol = await _identityService.GetRolById(rol.Id.ToString());
                if (localRol != null)
                {
                    ResultDto<RolDto> result = null;
                    localRol.ActionsRoles.Clear();
                    localRol.UpdateRolEntity(rol.ToEntity());

                    IdentityResult resultRepository = await _identityService.UpdateRol(localRol);

                    if (resultRepository.Succeeded)
                    {
                        _cacheService.RemoveData(AppCache.Roles, AppCache.RolesKey);
                        _cacheService.RemoveData(AppCache.RolId, $"{AppCache.RolKey}{rol.Id}");
                    }
                    else
                    {
                        errors.Add(new ErrorDto() { ErrorType = ErrorType.SaveChangesException, ErrorMessageKey = RolConstants.RolUpdateError });
                    }
                    result = ProcessResult(localRol.ToDto(), errors);
                }
                else
                {
                    errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = RolConstants.RolNotExist });
                }
            }

            return ProcessResult(rol, errors);
        }

        public async Task<ResultDto<bool>> DeleteRol(int Id)
        {
            LogginingService.LogInfo($"Delete Rol");
            List<ErrorDto> errors = new List<ErrorDto>();
            bool rolResult = false;

            //TODO: Carmen. RolesActionGroupsActions
            Entities.Roles rolToDelete = await _identityService.GetRolById(Id.ToString());
            if (rolToDelete != null)
            {
                bool existUseInRol = await _identityService.ExistUsersInRol(rolToDelete.Name);
                if (!existUseInRol)
                {
                    IdentityResult resultRepository = await _identityService.DeleteRol(rolToDelete);

                    if (resultRepository.Succeeded)
                    {
                        _cacheService.RemoveData(AppCache.Roles, AppCache.RolesKey);
                        _cacheService.RemoveData(AppCache.RolId, $"{AppCache.RolKey}{Id}");
                    }
                    else
                    {
                        errors.Add(new ErrorDto() { ErrorType = ErrorType.SaveChangesException, ErrorMessageKey = RolConstants.RolDeleteErrorMessage });
                    }
                }
                else
                {
                    errors.Add(new ErrorDto() { ErrorType = ErrorType.ValidationError, ErrorMessageKey = RolConstants.RolUsersInRol });
                }
            }
            else
            {
                errors.Add(new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists, ErrorMessageKey = RolConstants.RolNotExist });
            }

            return ProcessResult(rolResult, errors);
        }

        private bool ValidateRol(RolDto rol, out List<ErrorDto> result)
        {
            result = new List<ErrorDto>();
            if (!rol.IsValid())
            {
                result.Add(new ErrorDto()
                {
                    ErrorType = ErrorType.ValidationError,
                });
            }

            return !result.Any();
        }
    }
}