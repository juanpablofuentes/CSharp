using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.ToolsType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ToolsType
{
    public interface IToolsTypeService
    {
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        ResultDto<IList<ToolsTypeDto>> GetAllFiltered(ToolsTypeFilterDto filter);
        ResultDto<ToolsTypeDetailsDto> GetById(int id);
        ResultDto<ToolsTypeDetailsDto> CreateToolsType(ToolsTypeDetailsDto source);
        ResultDto<ToolsTypeDetailsDto> UpdateToolsType(ToolsTypeDetailsDto source);
        ResultDto<bool> DeleteToolsType(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}