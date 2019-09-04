using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Actions;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.Actions
{
    public class ActionService : BaseService, IActionService
    {
        private readonly IActionRepository _actionRepository;

        public ActionService(ILoggingService logginingService, IActionRepository actionRepository) : base(logginingService)
        {
            _actionRepository = actionRepository ?? throw new ArgumentNullException($"{nameof(actionRepository)} is null ");
        }

        public ResultDto<IList<ActionDto>> GetAll()
        {
            LogginingService.LogInfo($"Get all Actions avaible");
            var data = _actionRepository.GetAll().ToList();
            return ProcessResult(data.ToDto());
        }

        public ResultDto<ActionDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get Actions with id = {id} avaible");
            var data = _actionRepository.FindById(id);
            return ProcessResult(data.ToDto());
        }

        public ResultDto<ActionDto> UpdateAction(ActionDto source)
        {
            LogginingService.LogInfo($"Update action with id = {source.Id}");
            ResultDto<ActionDto> result = null;
            var localAction = _actionRepository.FindById(source.Id);
            if (localAction != null)
            {
                var updatedAction = localAction.Update(source);
                var resultRepository = _actionRepository.UpdateAction(updatedAction);
                result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
            }
            return result ?? new ResultDto<ActionDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<IList<ActionDto>> GetAllFiltered(ActionFilterDto filter)
        {
            var query = _actionRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Name.Contains(filter.Description));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().MapList(c => c.ToDto()));
        }

        public IEnumerable<ActionDto> GetAllKeyValuesDto()
        {
            LogginingService.LogInfo($"ActionService -> GetAllKeyValues");
            IEnumerable<ActionDto> result = _actionRepository.GetAllKeyValues().FromDictionaryToActionDto();
            return result;
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            LogginingService.LogInfo($"ActionService -> GetAllKeyValues");
            Dictionary<int, string> result = _actionRepository.GetAllKeyValues();
            return result;
        }

        private IQueryable<Entities.Actions> OrderBy(IQueryable<Entities.Actions> query, ActionFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.UpdateDate);
            return query;
        }
    }
}