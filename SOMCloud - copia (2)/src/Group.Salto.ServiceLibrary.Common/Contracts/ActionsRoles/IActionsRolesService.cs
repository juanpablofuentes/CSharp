using Group.Salto.ServiceLibrary.Common.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ActionsRoles
{
    public interface IActionsRolesService
    {
        Task<ResultDto<List<MultiSelectItemDto>>> GetActionsRoles(int? rolId);
    }
}