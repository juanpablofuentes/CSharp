using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Actions;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Actions
{
    public interface IActionService
    {
        ResultDto<IList<ActionDto>> GetAll();
        ResultDto<ActionDto> GetById(int id);
        ResultDto<ActionDto> UpdateAction(ActionDto source);
        ResultDto<IList<ActionDto>> GetAllFiltered(ActionFilterDto filter);
        IEnumerable<ActionDto> GetAllKeyValuesDto();
        Dictionary<int, string> GetAllKeyValues();
    }
}