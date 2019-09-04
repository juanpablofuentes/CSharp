using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Tools
{
    public interface IToolsService
    {
        ResultDto<IList<ToolsDto>> GetAll();
        ResultDto<IList<ToolsDto>> GetAllFiltered(ToolsFilterDto filter);
        ResultDto<ToolsDetailsDto> GetById(int id);
        ResultDto<ToolsDetailsDto> CreateTool(ToolsDetailsDto source);
        ResultDto<ToolsDetailsDto> UpdateTools(ToolsDetailsDto source);
        ResultDto<bool> DeleteTools(int id);
    }
}
