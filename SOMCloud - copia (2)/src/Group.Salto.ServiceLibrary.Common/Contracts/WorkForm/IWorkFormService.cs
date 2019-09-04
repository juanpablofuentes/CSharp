using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkForm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkForm
{
    public interface IWorkFormService
    {
        ResultDto<IList<WorkFormDto>> GetAllWorkForm();
    }
}