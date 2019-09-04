using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.RepetitionParameter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.RepetitionParameters
{
    public interface IRepetitionParameterService
    {
        ResultDto<IList<RepetitionParameterDto>> GetAll();
        ResultDto<RepetitionParametersDetailDto> GetFirst();
        ResultDto<RepetitionParametersDetailDto> Update(RepetitionParametersDetailDto model);
    }
}