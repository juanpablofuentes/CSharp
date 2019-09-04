using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Modules;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Module
{
    public interface IModuleService
    {
        IList<ModuleDto> GetAll();
        ResultDto<IList<ModuleDto>> GetAllFiltered(ModuleFilterDto filter);
        ResultDto<ModuleDetailDto> Create(ModuleDetailDto model);
        ResultDto<ModuleDetailDto> GetByIdIncludeActionGroups(Guid? id);
        ResultDto<ModuleDetailDto> Update(ModuleDetailDto model);
    }
}