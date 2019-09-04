using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Tools;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Tools;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.ToolsImplementation
{
    public class ToolsService : BaseService, IToolsService
    {
        private readonly IToolsRepository _toolsRepository;
        private readonly IToolsTypeRepository _toolstypeRepository;

        public ToolsService(ILoggingService logginingService, IToolsRepository toolsRepository,IToolsTypeRepository toolstypeRepository) : base(logginingService)
        {
            _toolsRepository = toolsRepository ?? throw new ArgumentNullException($"{nameof(IToolsRepository)} is null ");
            _toolstypeRepository = toolstypeRepository ?? throw new ArgumentNullException($"{nameof(IToolsTypeRepository)} is null ");

        }

        public ResultDto<IList<ToolsDto>> GetAll()
        {
            LogginingService.LogInfo($"Get All Tools");
            var data = _toolsRepository.GetAll().ToList();
            return ProcessResult(data.ToListDto());
        }

        public ResultDto<IList<ToolsDto>> GetAllFiltered(ToolsFilterDto filter)
        {
            LogginingService.LogInfo($"Get All Tools Filtered");
            var query = _toolsRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().MapList(c => c.ToListDto()));
        }

        private IQueryable<Entities.Tenant.Tools> OrderBy(IQueryable<Entities.Tenant.Tools> query, ToolsFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Tools");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Observations);
            return query;
        }

        public ResultDto<ToolsDetailsDto> CreateTool(ToolsDetailsDto source)
        {
            LogginingService.LogInfo($"Create Tool");
            var newTool = source.ToEntity();
            newTool = AssignToolsType(newTool, source.Types);
            var result = _toolsRepository.CreateTools(newTool);
            return ProcessResult(result.Entity?.ToDetailDto(), result);
        }

        public ResultDto<ToolsDetailsDto> UpdateTools(ToolsDetailsDto source)
        {
            LogginingService.LogInfo($"Update Tools");
            ResultDto<ToolsDetailsDto> result = null;
            var findTool = _toolsRepository.GetById(source.Id);
            if (findTool != null)
            {
                var updatedTool = findTool.Update(source);
                updatedTool = AssignToolsType(updatedTool, source.Types);
                var resultRepository = _toolsRepository.UpdateTools(updatedTool);
                result = ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
            }

            return result ?? new ResultDto<ToolsDetailsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeleteTools(int id)
        {
            LogginingService.LogInfo($"Delete Tools by id {id}");
            ResultDto<bool> result = null;
            var localTools = _toolsRepository.GetById(id);
            if (localTools != null)
            {
                localTools = RemoveAssingationForToolType(localTools);
                localTools.Vehicle = null;
                var resultSave = _toolsRepository.DeleteTools(localTools);
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

        public ResultDto<ToolsDetailsDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id Tools");
            var data = _toolsRepository.GetById(id);
            return ProcessResult(data.ToDetailDto());
        }

        private Tools AssignToolsType(Tools entity, IList<BaseNameIdDto<int>> tooltype)
        {
            entity.ToolsToolTypes?.Clear();
            if (tooltype != null && tooltype.Any())
            {
                entity.ToolsToolTypes = entity.ToolsToolTypes ?? new List<ToolsToolTypes>();
                var localToolsType = _toolstypeRepository.FilterById(tooltype.Select(x => x.Id));
                foreach (var tool in localToolsType)
                {
                    entity.ToolsToolTypes.Add(new ToolsToolTypes()
                    {
                       ToolType = tool
                    });
                }
            }
            return entity;
        }

        private Tools RemoveAssingationForToolType(Tools localTools)
        {
            localTools.ToolsToolTypes?.Clear();
            return localTools;
        }
    }
}