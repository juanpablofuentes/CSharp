using Group.Salto.Common;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Actions;
using Group.Salto.ServiceLibrary.Common.Contracts.ActionsRoles;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.ActionsRoles
{
    public class ActionsRolesService : BaseService, IActionsRolesService
    {
        private readonly IIdentityService _identityService;
        private readonly IActionService _actionService;

        public ActionsRolesService(ILoggingService logginingService,
            IIdentityService identityService,
            IActionService actionService) : base(logginingService)
        {
            _identityService = identityService ?? throw new ArgumentNullException($"{nameof(identityService)} is null ");
            _actionService = actionService ?? throw new ArgumentNullException($"{nameof(actionService)} is null ");
        }

        public async Task<ResultDto<List<MultiSelectItemDto>>> GetActionsRoles(int? rolId)
        {
            LogginingService.LogInfo($"GetActionsRoles for rol {rolId}");
            IEnumerable<ActionDto> actions = _actionService.GetAllKeyValuesDto();
            IList<Entities.ActionsRoles> actionsRoles = new List<Entities.ActionsRoles>();

            if (rolId.HasValue)
            {
                //TODO: Carmen. RolesActionGroupsActions
                var data = await _identityService.GetRolById(rolId.Value.ToString());
                actionsRoles = data.ActionsRoles.ToList();
            }

            List<MultiSelectItemDto> multiSelectItemDto = new List<MultiSelectItemDto>();
            foreach (ActionDto action in actions)
            {
                bool isCheck = actionsRoles.Any(x => x.ActionId == action.Id);

                multiSelectItemDto.Add(new MultiSelectItemDto()
                {
                    LabelName = action.Name,
                    Value = action.Id.ToString(),
                    IsChecked = isCheck
                });
            }

            return ProcessResult(multiSelectItemDto, multiSelectItemDto != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }
    }
}