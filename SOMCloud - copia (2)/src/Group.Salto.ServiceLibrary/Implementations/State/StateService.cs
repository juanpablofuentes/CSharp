using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.States;
using System;
using System.Linq;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Implementations.State
{
    public class StateService : BaseFilterService, IStateService
    {
        private readonly IStatesRepository _stateRepository;
        private readonly ICache _cacheService;

        public StateService(
            ILoggingService logginingService,
            IStateQueryFactory queryFactory,
            IStatesRepository stateRepository,
            ICache cacheService) : base(queryFactory, logginingService)
        {
            _stateRepository = stateRepository ?? throw new ArgumentNullException($"{nameof(IStatesRepository)} is null ");
            _cacheService = cacheService ?? throw new ArgumentNullException($"{nameof(cacheService)} is null");
        }

        public string GetStatesForInsert()
        {
            LogginingService.LogInfo($"GetStatesForInsert");
            string res = (string)_cacheService.GetData(AppCache.StatesForInsert, AppCache.StatesForInsertKey);

            if (string.IsNullOrEmpty(res))
            {
                res = _stateRepository.GetAll().ToList().ToInsertDto();
                _cacheService.SetData(AppCache.StatesForInsert, AppCache.StatesForInsertKey, res);
            }
            
            return res;
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get States Key Value");
            var data = Filter(new Common.Dtos.Query.QueryRequestDto() { QueryType = Common.Dtos.Query.QueryTypeEnum.Autocomplete, QueryTypeParameters = new Common.Dtos.Query.QueryTypeParametersDto() { } });

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public ResultDto<List<MultiSelectItemDto>> GetStatesMultiSelect(List<int> selectItems)
        {
            LogginingService.LogInfo($"GetStatesMultiSelect");
            IEnumerable<BaseNameIdDto<int>> states = GetAllKeyValues();
            return GetMultiSelect(states, selectItems);
        }

        public string GetNamesComaSeparated(List<int> ids)
        {
            List<string> names = _stateRepository.GetByIds(ids).Select(x => x.Name).ToList();
            return names.Aggregate((i, j) => $"{i}, {j}");
        }
    }
}