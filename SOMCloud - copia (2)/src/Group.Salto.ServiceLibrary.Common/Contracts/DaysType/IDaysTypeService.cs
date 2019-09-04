﻿using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.DaysType
{
    public interface IDaysTypeService
    {
        IList<BaseNameIdDto<Guid>> GetAllKeyValues();
    }
}