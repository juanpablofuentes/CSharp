using Group.Salto.Common;
using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Module;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Modules;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Module
{
    public class ModuleService : BaseService, IModuleService
    {
        private readonly ICache _cache;
        private readonly IModuleRepository _moduleRepository;
        private readonly IActionGroupRepository _actionGroupRepository;

        public ModuleService(ILoggingService logginingService,
                                ICache cache,
                                IModuleRepository moduleRepository,
                                IActionGroupRepository actionGroupRepository) : base(logginingService)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(ICache));
            _moduleRepository = moduleRepository ?? throw new ArgumentNullException(nameof(IModuleRepository));
            _actionGroupRepository = actionGroupRepository ?? throw new ArgumentNullException(nameof(IActionGroupRepository));
        }

        public IList<ModuleDto> GetAll()
        {
            LogginingService.LogInfo("ModulesService GetAll Modules");
            var result = GetCacheData();
            return result;
        }

        public IList<ModuleDto> GetCacheData()
        {
            LogginingService.LogInfo("ModulesService Get Modules from Cache");
            var modules = _cache.GetData(AppCache.Modules, AppCache.Modules) as IList<ModuleDto>;
            if (modules == null)
            {
                LogginingService.LogInfo("ModulesService set Modules on Cache");
                modules = _moduleRepository.All().ToList().ToDto();
                _cache.SetData(AppCache.Modules, AppCache.Modules, modules);
            }
            return modules;
        }

        public ResultDto<IList<ModuleDto>> GetAllFiltered(ModuleFilterDto filter)
        {
            LogginingService.LogInfo($"Get all modules filtered.");
            var query = _moduleRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Key.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().MapList(c => c.ToDto()));
        }

        public ResultDto<ModuleDetailDto> GetByIdIncludeActionGroups(Guid? id)
        {
            LogginingService.LogInfo($"Get Module by Id");
            var data = _moduleRepository.GetByIdIncludeActionGroups(id.Value);
            return ProcessResult(data.ToDetailDto());
        }

        public ResultDto<ModuleDetailDto> Create(ModuleDetailDto model)
        {
            var entity = model.ToEntity();
            entity = AssignActionGroups(entity, model.ActionGroupsSelected);
            var resultRepository = _moduleRepository.CreateModule(entity);
            return ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
        }

        public ResultDto<ModuleDetailDto> Update(ModuleDetailDto model)
        {
            ResultDto<ModuleDetailDto> result = null;
            var entity = _moduleRepository.GetByIdIncludeActionGroups(model.Id);
            if (entity != null)
            {
                entity.Update(model);
                entity = AssignActionGroups(entity, model.ActionGroupsSelected);
                var resultRepository = _moduleRepository.UpdateModule(entity);
                result = ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
            }
            return result ?? ProcessResult(model, new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        private IQueryable<Entities.Module> OrderBy(IQueryable<Entities.Module> query, ModuleFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Key);
            return query;
        }

        private Entities.Module AssignActionGroups(Entities.Module entity, IList<Guid> actionGroupIds)
        {
            entity.ModuleActionGroups?.Clear();
            if (actionGroupIds != null && actionGroupIds.Any())
            {
                entity.ModuleActionGroups = entity.ModuleActionGroups ?? new List<ModuleActionGroups>();
                var elements = _actionGroupRepository.GetAllByIds(actionGroupIds);
                foreach (var element in elements)
                {
                    entity.ModuleActionGroups.Add(new ModuleActionGroups()
                    {
                        ActionGroups = element,
                    });
                }
            }
            return entity;
        }
    }
}
